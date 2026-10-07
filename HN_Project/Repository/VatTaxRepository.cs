using HN_Backend.Data;
using HN_Backend.DTOs;
using HN_Backend.Interface;
using Microsoft.EntityFrameworkCore;

namespace HN_Backend.Repository
{
    public class VatTaxRepository:IVatTax
    {
        private readonly ApplicationDbContext _db;
        public VatTaxRepository(ApplicationDbContext db) 
        { 
            this._db = db;
        }

        public async Task<List<TaxesDropdownDto>> GetTaxes()
        {
            return await _db.Taxes.Select(x => new TaxesDropdownDto
            {
                TaxId = x.TaxId,
                TaxName = x.TaxName
            }).ToListAsync();
        }
    }
}
