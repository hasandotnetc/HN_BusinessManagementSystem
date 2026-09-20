using HN_Backend.Data;
using HN_Backend.DTOs.PaymentMethod;
using HN_Backend.Interface;
using Microsoft.EntityFrameworkCore;

namespace HN_Backend.Repository
{
    public class PaymentMethodRepository:IPaymentMethod
    {
        private readonly ApplicationDbContext _db;
        public PaymentMethodRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<PaymentMethodDropdownDto>> GetPaymentMethodDropdownList(long CompanyId)
        {
            return await _db.PaymentMethods
                .Where(x => x.CompanyId == CompanyId && x.ActiveStatus == "Y" )
                .Select(x => new PaymentMethodDropdownDto
                {
                    PaymentMethodId = x.PaymentMethodId,
                    Name = x.Name
                }).ToListAsync();
        }
    }
}
