using Dapper;
using HN_Backend.DTOs;
using HN_Backend.DTOs.PaginationDto;
using HN_Backend.DTOs.Products;
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


        public async Task<List<CurrentStockProductVM>> GetProductByNameCodeModelNoWithCurrentStock(string objParam)
        {
            using var connection = new SqlConnection(_connectionString);
            string sql = @"SELECT TOP 30
                    P.ProductId,
                    P.Code,
                    P.Name,
                    P.Model,
                    P.Price,
                    P.Discount,
                    P.Warranty,
                    P.Picture,
                    SUM(ISNULL(CS.Quantity,0)) AS Quantity,
                    ROUND(ISNULL(SUM(NULLIF(CS.Cost,0)) / SUM(NULLIF(CS.Quantity,0)),0),2) AS Cost
                    FROM Product P 
                    LEFT JOIN CurrentStock CS ON P.ProductId = CS.ProductId
                    LEFT JOIN CurrentStockDetail CSD ON CS.CurrentStockId=CSD.CurrentStockId
                    WHERE  
                    (P.Name LIKE @Search
                    OR P.Code LIKE @Search
                    OR P.Model LIKE @Search) 

                    GROUP BY P.ProductId,
                    P.Code,
                    P.Name,
                    P.Model,
                    P.Price,
                    P.Discount,
                    P.Warranty,
                    P.Picture"; 
            var result = await connection.QueryAsync<CurrentStockProductVM>(
                sql,
                new
                {
                    Search = string.IsNullOrWhiteSpace(objParam) ? "" : "%" + objParam + "%" 
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
        P.Code,
        UT.UnitTypeId,
        UT.Name AS UnitTypeName,
        P.ProductNote,
        G.ProductGroupId,
        G.Name AS GroupName,
        C.CategoryId,
        C.Name AS CategoryName,
        B.BrandId,
        B.Name AS BrandName, 
        P.ProductType,
        P.Vat AS VAT,
        p.IsVatPercentageOrAmount AS IsVatPercentageOrAmount,
        P.Warranty,
        P.ActiveStatus,
        P.SerialAvailable, 
        P.Picture AS ImageUrl
    FROM Product P
    LEFT JOIN ProductGroup G ON P.GroupID = G.ProductGroupId
    LEFT JOIN Category C ON P.CategoryID = C.CategoryID
    LEFT JOIN Brand B ON P.BrandID = B.BrandID
    LEFT JOIN UnitType UT ON P.UnitTypeId = UT.UnitTypeId
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


        public async Task<PaginationResponse<CustomerPaginationDto>> GetCustomerByPaginationRequest(PaginationRequest request,long CompanyId)
        {
            using var connection = new SqlConnection(_connectionString);

            var parameter = new DynamicParameters();

            parameter.Add("@Page", request.Page);
            parameter.Add("@PageSize", request.PageSize);
            parameter.Add("@Search", request.Search);
            parameter.Add("@CompanyId", CompanyId);

            string sql = @"
   SELECT
CS.CustomerId,
CS.Code CustomerCode,
CS.Name AS CustomerName,
CS.Phone AS Phone,
CS.Email AS Email,
CS.Address,
CS.Picture,
CS.ActiveStatus,
CS.NID,
CS.OpeningBalance,
CASE WHEN CS.SupplierId IS NULL THEN 'N' ELSE 'Y' END AS SupplierAvailable,
CS.IsOwnCompanyCustomer,
CSG.CustomerGroupId,
CSG.Name As CustomerGroupName 
From Customer CS JOIN CustomerGroup CSG ON CS.CustomerGroupId = CSG.CustomerGroupId
WHERE
(
    CS.CompanyId = @CompanyId
    AND ( @Search IS NULL
    OR @Search = ''
    OR CS.Name LIKE '%' + @Search + '%'
    OR CSG.Name LIKE '%' + @Search + '%'
    OR CS.Code LIKE '%' + @Search + '%'
    OR CS.Phone LIKE '%' + @Search + '%')
) 
 ORDER BY CS.CustomerId DESC
OFFSET (@Page - 1) * @PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY;
    SELECT COUNT(*)
    From Customer CS JOIN CustomerGroup CSG ON CS.CustomerGroupId = CSG.CustomerGroupId
WHERE
(
CS.CompanyId = @CompanyId
AND(
    @Search IS NULL
    OR @Search = ''
    OR CS.Name LIKE '%' + @Search + '%'
    OR CSG.Name LIKE '%' + @Search + '%'
    OR CS.Code LIKE '%' + @Search + '%'
    OR CS.Phone LIKE '%' + @Search + '%')
) ;";

            using var multi = await connection.QueryMultipleAsync(sql, parameter);

            var products = (await multi.ReadAsync<CustomerPaginationDto>()).ToList();

            int totalRecords = await multi.ReadFirstAsync<int>();

            return new PaginationResponse<CustomerPaginationDto>
            {
                Data = products,
                TotalRecords = totalRecords,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / request.PageSize)
            };
        }
         
        public async Task<PaginationResponse<ProductUnitTypeConversionLoadGridDto>> GetProductUnitTypeConversionRatio(ProductUnitTypeConversionPaginationRequest request, long CompanyId)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameter = new DynamicParameters();
            parameter.Add("@Page", request.Page);
            parameter.Add("@PageSize", request.PageSize);
            parameter.Add("@Search", request.Search);
            parameter.Add("@CompanyId", CompanyId);

            parameter.Add("@GroupId", request.GroupId = request.GroupId == 0 ? null : request.GroupId);
            parameter.Add("@CategoryId", request.CategoryId = request.CategoryId == 0 ? null : request.CategoryId);
            parameter.Add("@BrandId", request.BrandId = request.BrandId == 0 ? null : request.BrandId);
            parameter.Add("@ProductId", request.ProductId = request.ProductId == 0 ? null : request.ProductId);
            parameter.Add("@UnitTypeId", request.UnitTypeId = request.UnitTypeId == 0 ? null : request.UnitTypeId);

            string sql = @"
                        SELECT
                            P.ProductId,
                            P.Name AS ProductName,
                            P.UnitTypeId AS BaseUnitTypeId,
                            BUT.Name AS BaseUnitTypeName,
                            PUT.UnitTypeId AS UnitTypeId,
                            CUT.Name AS UnitTypeName,
                            PUT.ConversionToUnitType,
                            PUT.IsPurchaseAllowed,
                            PUT.IsSaleAllowed,
                            PUT.IsDefaultPurchase,
                            PUT.IsDefaultSale,
                            PUT.IsActive
                        FROM Product P
                        INNER JOIN ProductUnitTypeConversion PUT ON P.ProductId = PUT.ProductId
                        LEFT JOIN UnitType BUT ON P.UnitTypeId = BUT.UnitTypeId
                        LEFT JOIN UnitType CUT ON PUT.UnitTypeId = CUT.UnitTypeId
                        WHERE
                            P.CompanyId = @CompanyId
                            AND
                            (
                                @Search IS NULL
                                OR LTRIM(RTRIM(@Search)) = ''
                                OR P.Name LIKE '%' + LTRIM(RTRIM(@Search)) + '%'
                                OR BUT.Name LIKE '%' + LTRIM(RTRIM(@Search)) + '%'
                                OR CUT.Name LIKE '%' + LTRIM(RTRIM(@Search)) + '%'
                            )
                            AND
                            (
                                @GroupId IS NULL
                                OR P.GroupId = @GroupId
                            )
                            AND
                            (
                                @CategoryId IS NULL
                                OR P.CategoryId = @CategoryId
                            )
                            AND
                            (
                                @BrandId IS NULL
                                OR P.BrandId = @BrandId
                            )
                            AND
                            (
                                @ProductId IS NULL
                                OR P.ProductId = @ProductId
                            )
                            AND
                            (
                                @UnitTypeId IS NULL
                                OR PUT.UnitTypeId = @UnitTypeId
                                OR P.UnitTypeId = @UnitTypeId
                            )

                        ORDER BY
                            P.Name ASC,
                            PUT.UnitTypeId

                        OFFSET (@Page - 1) * @PageSize ROWS
                        FETCH NEXT @PageSize ROWS ONLY;

                        SELECT COUNT(*)
                        FROM Product P
                        INNER JOIN ProductUnitTypeConversion PUT  ON P.ProductId = PUT.ProductId 
                        LEFT JOIN UnitType BUT  ON P.UnitTypeId = BUT.UnitTypeId 
                        LEFT JOIN UnitType CUT  ON PUT.UnitTypeId = CUT.UnitTypeId 
                        WHERE
                            P.CompanyId = @CompanyId 
                            AND
                            (
                                @Search IS NULL
                                OR LTRIM(RTRIM(@Search)) = ''
                                OR P.Name LIKE '%' + LTRIM(RTRIM(@Search)) + '%'
                                OR BUT.Name LIKE '%' + LTRIM(RTRIM(@Search)) + '%'
                                OR CUT.Name LIKE '%' + LTRIM(RTRIM(@Search)) + '%'
                            ) 
                            AND
                            (
                                @GroupId IS NULL
                                OR P.GroupId = @GroupId
                            ) 
                            AND
                            (
                                @CategoryId IS NULL
                                OR P.CategoryId = @CategoryId
                            ) 
                            AND
                            (
                                @BrandId IS NULL
                                OR P.BrandId = @ProductId
                            ) 
                            AND
                            (
                                @ProductId IS NULL
                                OR P.ProductId = @ProductId
                            ) 
                            AND
                            (
                                @UnitTypeId IS NULL
                                OR PUT.UnitTypeId = @UnitTypeId
                                OR P.UnitTypeId = @UnitTypeId
                            );

                        ";

            using var multi = await connection.QueryMultipleAsync(sql, parameter);
            var products =(await multi.ReadAsync<ProductUnitTypeConversionLoadGridDto>()).ToList();
            int totalRecords = await multi.ReadFirstAsync<int>();
            return new PaginationResponse<ProductUnitTypeConversionLoadGridDto>
            {
                Data = products,
                TotalRecords = totalRecords,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / request.PageSize)
            };
        }


        public async Task<List<ProductSearchAutocompleteDetailsDto>> GetProductDetailWithStock(string objParam,long companyId)
        {
            using var connection = new SqlConnection(_connectionString);
            string sql = @"	SELECT 
	                TOP 15
	                P.ProductId, P.Name, P.Code, P.Model
	                ,P.SerialAvailable
	                ,P.Picture
	                ,SUM(ISNULL(CS.Quantity,0))  AS StockUnit
	                FROM Product P
	                LEFT JOIN CurrentStock CS ON P.ProductId = CS.ProductId 
                    WHERE  
                    (P.Name LIKE @Search
                    OR P.Code LIKE @Search
                    OR P.Model LIKE @Search) 

                   	GROUP BY  P.ProductId, P.Name, P.Code, P.Model,P.SerialAvailable,P.Picture
	                ORDER BY P.Name";
            var result = await connection.QueryAsync<ProductSearchAutocompleteDetailsDto>(
                sql,
                new
                {
                    Search = string.IsNullOrWhiteSpace(objParam) ? "" : "%" + objParam + "%"
                });
            return result.ToList();
        }

    }
}
