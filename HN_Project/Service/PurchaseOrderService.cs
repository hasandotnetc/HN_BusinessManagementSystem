using HN_Backend.Data;
using HN_Backend.DTOs;
using HN_Backend.DTOs.Customer;
using HN_Backend.DTOs.PurchaseOrder;
using HN_Backend.Enums;
using HN_Backend.Helpers;
using HN_Backend.Interface;
using HN_Backend.Models;
using HN_Backend.Repository;
using Microsoft.Data.SqlClient;

namespace HN_Backend.Service
{
    public class PurchaseOrderService
    {
        private readonly IPurchaseOrder _PurchaseOrder;
        private readonly CurrentSessionData _currentSessionData;
        private readonly IEventNoOrCodeGeneration _eventNoGeneration;
        private readonly IUnitOfWork _unitOfWork;
        public PurchaseOrderService(IPurchaseOrder purchaseOrder, IEventNoOrCodeGeneration eventNoGeneration, IUnitOfWork unitOfWork,CurrentSessionData currentSessionData)
        {
            _PurchaseOrder = purchaseOrder;
            _eventNoGeneration = eventNoGeneration;
            _currentSessionData = currentSessionData;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<string>> CreatePurchaseOrder(PurchaseOrderCreateDto createDto)
        {
            long companyId = _currentSessionData.CompanyId;
            long userId = _currentSessionData.UserId;
            long locationId = _currentSessionData.LocationId;

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var purchaseOrderNo = await _eventNoGeneration.EventNoGeneration(EventTypeEnum.PurchaseOrder.ToString(), "POR", companyId, locationId);

                var purchaseOrder = new PurchaseOrder
                {
                    PurchaseOrderNo = purchaseOrderNo,
                    OrderRefNo = createDto.OrderRefNo,
                    Podate = createDto.Podate,
                    PurchaseOrderType = createDto.PurchaseOrderType,
                    OrderType = createDto.OrderType,
                    SupplierId = createDto.SupplierId,
                    SupplierContactPerson = createDto.SupplierContactPerson,
                    SupplierContactNo = createDto.SupplierContactNo,
                    SupplierAddress = createDto.SupplierAddress,
                    EmployeeId = createDto.EmployeeId,
                    DeliveryTo = createDto.DeliveryTo,
                    ShipToAddress = createDto.ShipToAddress,
                    ShipmentMode = createDto.ShipmentMode,
                    PartialShipment = createDto.PartialShipment,
                    DeliveryDate = createDto.DeliveryDate,
                    PaymentMethodId = createDto.PaymentMethodId,
                    ExpectedPaymentReleaseDate = createDto.ExpectedPaymentReleaseDate,
                    TermsAndCondition = createDto.TermsAndCondition,
                    PaymentTerms = createDto.PaymentTerms,
                    Remarks = createDto.Remarks,
                    CompanyId = companyId,
                    LocationId = locationId,
                    EntryBy = userId,
                    ApprovalStatus = "N",
                    CreateOn = DateTime.Now
                };
                 
                foreach (var detailDto in createDto.Details)
                {
                    var detail = new PurchaseOrderDetail
                    {
                        ProductId = detailDto.ProductId,
                        Quantity = detailDto.Quantity,
                        UnitTypeId = detailDto.UnitTypeId,
                        Cost = detailDto.Cost,
                        CreateOn = DateTime.Now
                    };

                    foreach (var taxDto in detailDto.Taxes)
                    {
                        if (taxDto.TaxAmount > 0)
                        {
                            var tax = new PurchaseOrderDetailTax
                            {
                                TaxId = taxDto.TaxId ?? 0,
                                TaxAmount = taxDto.TaxAmount,
                                TaxOn = taxDto.TaxOn,
                                IsIncluded = taxDto.IsIncluded,
                                CreateOn = DateTime.Now
                            };

                            await _PurchaseOrder.CreatePurchaseOrderDetailTax(tax);
                            detail.PurchaseOrderDetailTaxes.Add(tax);
                        }
                    }
                     
                    await _PurchaseOrder.CreatePurchaseOrderDetail(detail);
                    purchaseOrder.PurchaseOrderDetails.Add(detail);
                }
                 
                purchaseOrder.TotalAmount = purchaseOrder.PurchaseOrderDetails.Sum(d =>(d.Quantity * d.Cost) + d.PurchaseOrderDetailTaxes.Sum(t => t.TaxAmount ?? 0));
                 
                await _PurchaseOrder.CreatePurchaseOrder(purchaseOrder);
                 
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return new ServiceResult<string>
                {
                    Success = true,
                    Message = "Purchase Order created successfully.",
                    Data = purchaseOrderNo
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
