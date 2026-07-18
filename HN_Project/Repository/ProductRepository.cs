 
using HN_Project.DTOs;
using HN_Project.Interface;
using HN_Backend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HN_Project.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            {
                _db = db;
            }
        }

    }
}
