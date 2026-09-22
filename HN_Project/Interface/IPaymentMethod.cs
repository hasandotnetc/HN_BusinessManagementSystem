using HN_Backend.DTOs.PaymentMethod;

namespace HN_Backend.Interface
{
    public interface IPaymentMethod
    {
        public Task<List<PaymentMethodDropdownDto>> GetPaymentMethodDropdownList(long CompanyId);
    }
}
