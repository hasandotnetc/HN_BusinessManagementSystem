using HN_Backend.Data;
using HN_Backend.DTOs;

namespace HN_Backend.Interface
{
    public interface IVatTax
    {
        Task<List<TaxesDropdownDto>> GetTaxes();
    }
}
