using HN_Backend.DTOs.PaymentMethod;
using HN_Backend.Helpers;
using HN_Backend.Interface;

namespace HN_Backend.Service
{
    public class PaymentMethodService
    {
        private readonly IPaymentMethod _paymentMethod;
        private readonly CurrentSessionData _currentSessionData;
        public PaymentMethodService(IPaymentMethod paymentMethod, CurrentSessionData currentSessionData)
        {
            _paymentMethod = paymentMethod;
            _currentSessionData = currentSessionData;
        }
        public async Task<List<PaymentMethodDropdownDto>> GetPaymentMethodDropdownList()
        {
            long CompanyId = _currentSessionData.CompanyId;
            return await _paymentMethod.GetPaymentMethodDropdownList(CompanyId);
        }
    }
}
