using HN_Backend.Data;
using HN_Backend.DTOs;
using HN_Backend.DTOs.PurchaseIn.CreateDto;
using HN_Backend.DTOs.PurchaseOrder;
using HN_Backend.Enums;
using HN_Backend.Helpers;
using HN_Backend.Interface;
using HN_Backend.Repository;

namespace HN_Backend.Service
{
    public class PurchaseService
    {
        private readonly IPurchase _purchase; 
        private readonly CurrentSessionData _currentSessionData;
        private readonly IEventNoOrCodeGeneration _eventNoGeneration;
        private readonly IUnitOfWork _unitOfWork;
        public PurchaseService(IPurchase purchase, IEventNoOrCodeGeneration eventNoOrCodeGeneration, IUnitOfWork unitOfWork, CurrentSessionData currentSessionData)
        {
            _purchase = purchase;
            _currentSessionData = currentSessionData;
            _unitOfWork = unitOfWork;
            _eventNoGeneration = eventNoOrCodeGeneration;
        }

        //public async Task<>

        public async Task<ServiceResult<string>> CreatePurchase(PurchaseInCreateDto createDto)
        {
            long companyId = _currentSessionData.CompanyId;
            long userId = _currentSessionData.UserId;
            long locationId = _currentSessionData.LocationId;

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var purchaseInNo = await _eventNoGeneration.EventNoGeneration(
                    EventTypeEnum.Purchase.ToString(), "PUR", companyId, locationId);

                // 1. Create Parent Entity
                var purchaseOrder = new PurchaseIn
                {
                    PurchaseInId = Guid.NewGuid(), // Set PK directly
                    PurchaseInNo = purchaseInNo,
                    ReferenceNo = createDto.ReferenceNo,
                    SupplierId = createDto.SupplierId,
                    Date = createDto.Date,
                    PaymentMethodId = createDto.PaymentMethodId,
                    Remarks = createDto.Remarks,
                    AdditionalCost = createDto.AdditionalCost,
                    Amount = createDto.Amount,
                    Discount = createDto.Discount,
                    DiscountType = createDto.DiscountType,
                    TotalAmount = createDto.TotalAmount,
                    PreviousDeu = createDto.PreviousDeu,
                    Company = companyId,
                    LocationId = locationId,
                    EntryBy = userId,
                    Approved = "N",
                    CreateOn = DateTime.Now
                };

                foreach (var detailDto in createDto.PurchaseInDetails)
                {
                    // 2. Create Detail Entity
                    var detailId = Guid.NewGuid();
                    var detail = new PurchaseInDetail
                    {
                        PurchaseInDetailId = detailId, // Set PK directly
                        PurchaseInId = purchaseOrder.PurchaseInId, // Set FK directly
                        ProductId = detailDto.ProductId,
                        Quantity = detailDto.Quantity,
                        UnitType = detailDto.UnitType,
                        Cost = detailDto.Cost,
                        AdditionalCost = detailDto.AdditionalCost,
                        PurchaseInAdditionalCost = detailDto.PurchaseInAdditionalCost,
                        DiscountAmount = detailDto.DiscountAmount,
                        DiscountType = detailDto.DiscountType,
                        PurchaseInDiscount = detailDto.PurchaseInDiscount,
                        PurchaseInDiscountType = detailDto.PurchaseInDiscountType,
                        LocationId = detailDto.LocationId ?? locationId,
                        BatchNo = detailDto.BatchNo,
                        LotNo = detailDto.LotNo,
                        ExpiryDate = detailDto.ExpiryDate,
                        ManufactureDate = detailDto.ManufactureDate
                    };

                    // 3. Process Child Taxes
                    foreach (var taxDto in detailDto.Taxes)
                    {
                        if (taxDto.TaxAmount > 0)
                        {
                            var tax = new PurchaseInDetailTax
                            {
                                PurchaseInDetailTaxId = Guid.NewGuid(), // Set PK
                                PurchaseInDetailId = detailId,         // Set FK
                                TaxId = taxDto.TaxId,
                                TaxAmount = taxDto.TaxAmount,
                                TaxOn = taxDto.TaxOn,
                                CreateOn = DateTime.Now
                            };

                            detail.PurchaseInDetailTaxes.Add(tax);
                        }
                    }

                    // 4. Process Child Serials
                    foreach (var serial in detailDto.PurchaseInDetailSerials)
                    {
                        var item = new PurchaseInDetailSerial
                        {
                            PurchaseInDetailSerialId = Guid.NewGuid(), // Set PK
                            PurchaseInDetailId = detailId,             // Set FK
                            SerialNo = serial.SerialNo
                        };

                        detail.PurchaseInDetailSerials.Add(item);
                    }

                    purchaseOrder.PurchaseInDetails.Add(detail);
                }
                 
                await _purchase.CreatePurchaseIn(purchaseOrder);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return new ServiceResult<string>
                {
                    Success = true,
                    Message = "Purchase Order created successfully.",
                    Data = purchaseInNo
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ServiceResult<string>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
