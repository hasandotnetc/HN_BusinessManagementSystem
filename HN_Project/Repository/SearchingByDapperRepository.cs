using Dapper;
 
using HN_Project.DTOs;
using HN_Project.Interface;
using Microsoft.Data.SqlClient;

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
    
    }
}
