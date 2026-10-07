using HN_Backend.DTOs;
using HN_Backend.Interface;

namespace HN_Backend.Service
{
    public class VatTaxService
    {
        private readonly IVatTax _vatTax;
        public VatTaxService(IVatTax vatTax)
        {
            this._vatTax = vatTax;
        }

        public async Task<List<TaxesDropdownDto>> GetTaxes()
        {
            return await _vatTax.GetTaxes();
        }

    }
}
