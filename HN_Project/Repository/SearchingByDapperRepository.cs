using Dapper;
using HN_Backend.DTOs;
using HN_Project.DTOs;
using HN_Project.Interface;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HN_Project.Repository
{
    public class SearchingByDapperRepository : ISearchingByDapperRepository
    {
        //private readonly ApplicationDbContext _db;
        //public SearchingByDapperRepository(ApplicationDbContext db)
        //{ 
        //    _db = db;
        //}

        //private readonly string _connectionString;

        //public SearchingByDapperRepository(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        private readonly string _connectionString;

        public SearchingByDapperRepository(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<CurrentStockProductVM>> GetProductByNameCodeSerialModelNoWithCurrentStock(string objParam)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @" 
                    SELECT TOP 30
                    P.ProductId,
                    P.Code,
                    P.Name,
                    P.Model,
                    P.Price,
                    P.Discount,
                    P.Warranty,
                    P.Picture,
                    SUM(ISNULL(CS.Quantity,0)) AS Quantity,
                    ROUND(ISNULL(SUM(NULLIF(CS.Cost,0)) / SUM(NULLIF(CS.Quantity,0)),0),2) AS Cost,
                    CSD.SerialNo
                    FROM Product P 
                    LEFT JOIN CurrentStock CS ON P.ProductId = CS.ProductId
                    LEFT JOIN CurrentStockDetail CSD ON CS.CurrentStockId=CSD.CurrentStockId
                       WHERE 
                        CS.Quantity > 0
                        AND (P.Name LIKE @Search
                        OR P.Code LIKE @Search
                        OR P.Model LIKE @Search
                        OR (CSD.SerialNo IS NOT NULL AND CSD.SerialNo LIKE @Search))
                    GROUP BY P.ProductId,
                    P.Code,
                    P.Name,
                    P.Model,
                    P.Price,
                    P.Discount,
                    P.Warranty,
                    P.Picture,
                    CSD.SerialNo";

            var result = await connection.QueryAsync<CurrentStockProductVM>(
                sql,
                new
                {
                    Search = string.IsNullOrWhiteSpace(objParam) ? "" : "%" + objParam + "%"

                    //Search = "%" + objParam + "%"
                });

            return result.ToList();
             
        }


        public async Task<PaginationResponse<ProductPaginationVM>> GetProductByPaginationRequest(PaginationRequest request)
        {
            using var connection = new SqlConnection(_connectionString);

            var parameter = new DynamicParameters();

            parameter.Add("@Page", request.Page);
            parameter.Add("@PageSize", request.PageSize);
            parameter.Add("@Search", request.Search);

            string sql = @"
    SELECT
        P.ProductID,
        P.Name AS ProductName,
        P.Model,
        G.Name AS GroupName,
        C.Name AS CategoryName,
        B.Name AS BrandName,
        P.Price,
        '' AS StockType,
        P.Picture AS ImageUrl
    FROM Product P
    LEFT JOIN ProductGroup G ON P.GroupID = G.ProductGroupId
    LEFT JOIN Category C ON P.CategoryID = C.CategoryID
    LEFT JOIN Brand B ON P.BrandID = B.BrandID
    WHERE
    (
        @Search IS NULL
        OR @Search = ''
        OR P.Name LIKE '%' + @Search + '%'
        OR G.Name LIKE '%' + @Search + '%'
        OR C.Name LIKE '%' + @Search + '%'
        OR B.Name LIKE '%' + @Search + '%'
    )
    ORDER BY P.ProductID DESC
    OFFSET (@Page - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    SELECT COUNT(*)
    FROM Product P
    LEFT JOIN ProductGroup G ON P.GroupID = G.ProductGroupId
    LEFT JOIN Category C ON P.CategoryID = C.CategoryID
    LEFT JOIN Brand B ON P.BrandID = B.BrandID
    WHERE
    (
        @Search IS NULL
        OR @Search = ''
        OR P.Name LIKE '%' + @Search + '%'
        OR G.Name LIKE '%' + @Search + '%'
        OR C.Name LIKE '%' + @Search + '%'
        OR B.Name LIKE '%' + @Search + '%'
    );";

            using var multi = await connection.QueryMultipleAsync(sql, parameter);

            var products = (await multi.ReadAsync<ProductPaginationVM>()).ToList();

            int totalRecords = await multi.ReadFirstAsync<int>();

            return new PaginationResponse<ProductPaginationVM>
            {
                Data = products,
                TotalRecords = totalRecords,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / request.PageSize)
            };
        }
    }
}
