using HN_Backend.Interface;
using HN_Backend.Models;
using HN_Backend.Repository;
using HN_Project.Interface;
using HN_Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HN_Backend.Service
{
    public class PointOfSalesService
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly ICurrentStock _currentStockRepo;
        private readonly ICurrentStockDetail _currentStockDetailRepo;
        private readonly IProductRepository _productRepo;
        private readonly ISalesOrder _salesOrder;
        private readonly ISalesOrderDetail _salesOrderDetail;
        private readonly ISalesOrderSerial _salesOrderSerial;
        private readonly ICollectionIn _collection;
        private readonly ICollectionDetail _collectionDetail;
        private readonly IUnitOfWork _unitOfWork;

        public PointOfSalesService(ICustomerRepository customerRepo, ICurrentStock currentStockRepo, ICurrentStockDetail currentStockDetailRepo, IProductRepository productRepo, ISalesOrder salesOrder, ISalesOrderDetail salesOrderDetail, ISalesOrderSerial salesOrderSerial, ICollectionIn collection, ICollectionDetail collectionDetail, IUnitOfWork unitOfWork)
        {
            _customerRepo = customerRepo;
            _currentStockRepo = currentStockRepo;
            _currentStockDetailRepo = currentStockDetailRepo;
            _productRepo = productRepo;
            _salesOrder = salesOrder;
            _salesOrderDetail = salesOrderDetail;
            _salesOrderSerial = salesOrderSerial;
            _collection = collection;
            _collectionDetail = collectionDetail;
            _unitOfWork = unitOfWork;
        }


        public async Task<List<SaveInvoiceResponseDto>> CreatePointOfSalesAsync(InvoicePOSDto payload)
        {
            // 1. Start an explicit transaction block to safely encompass stock logic
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // 2. Instantiate Main Sales Order
                var newOrder = new SalesOrder
                {
                    //OrderDate = DateTime.UtcNow,
                    CustomerId = payload.Customer.CustomerId,
                    SalesOrderNo = "SO-001",
                    InvoiceNo="INV-001",
                    EmployeeId = payload.SalesPerson.EmployeeId,
                    PaymentMethodId = payload.PaymentOption.PaymentModeId,
                    TotalAmount = payload.Summary.GrandTotal,
                    DiscountAmount = payload.Summary.Discount,
                    //VatAmount = payload.Summary.vat,
                    GrandTotal = payload.Summary.GrandTotal,
                    DueAmount = 0,
                    PaidAmount = payload.Summary.GrandTotal,
                    LocationId = 1,
                    EntryBy = 1,
                    SalesOrderDetails = new List<SalesOrderDetail>() // Navigation property collection
                };

                // 3. Create Collection (Payment)
                var newCollection = new Collection
                {
                    //Amount = payload.PaidAmount,
                    SalesOrder = newOrder, 
                    CollectedBy = newOrder.EmployeeId,
                    CustomerId = newOrder.CustomerId,
                    LocationId = newOrder.LocationId,
                    CollectionAgainst = "POS",
                    CollectionNo="CO-001",
                    CollectionDetails = new List<CollectionDetail>()
                };

                var collectionDetail = new CollectionDetail
                {
                    BankId = 1, 
                    Collection = newCollection,
                    CollectionReference = payload.PaymentOption.ChequeNo ?? payload.PaymentOption.CardNo,
                    Amount = payload.PaymentOption.ReceiveAmount 
                };
                newCollection.CollectionDetails.Add(collectionDetail);

                // 4. Process Loop for Items (Details, Serials, and Stock adjustment)
                foreach (var item in payload.ProductRowsAllInfo)
                {
                    var orderDetail = new SalesOrderDetail
                    {
                        SalesOrder = newOrder, // Linked directly
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        SalesOrderSerials = new List<SalesOrderSerial>()
                    };
                    // FIX 2: Correct string validation safety check
                    if (!string.IsNullOrEmpty(item.SerialNo))
                    {
                        orderDetail.SalesOrderSerials.Add(new SalesOrderSerial
                        {
                            SalesOrder = newOrder,
                            SalesOrderDetail = orderDetail,
                            SerialNo = item.SerialNo
                        });
                    }
                    newOrder.SalesOrderDetails.Add(orderDetail);

                    //if (item.SerialNo != null || item.SerialNo !="")
                    //{ 
                    //    orderDetail.SalesOrderSerials.Add(new SalesOrderSerial
                    //    {
                    //        SalesOrderDetail = orderDetail,  
                    //        SerialNo = item.SerialNo
                    //    }); 
                    //} 
                    //newOrder.SalesOrderDetails.Add(orderDetail);

                    // 5. Update/Delete Stock Data in memory
                    var currentStock = await _currentStockRepo.GetCurrentStockByProductIdAsync(item.ProductId); //apatot productid diye rakhlam then next e update korte hobe
                    if (currentStock.Count>0)
                    {
                        foreach (var stock in currentStock)
                        {
                            stock.Quantity -= item.Quantity;
                            if (stock.Quantity <= 0)
                            {
                                await _currentStockRepo.DeleteCurrentStockAsync(stock.ProductId);
                            }
                            else
                            {
                                await _currentStockRepo.UpdateCurrentStockAsync(stock);
                            }
                        }

                        //currentStock.Quantity -= item.Quantity;  
                        //if (currentStock.Quantity <= 0)
                        //{ 
                        //    await _currentStockRepo.DeleteCurrentStockAsync(currentStock.ProductId);
                        //}
                        //else
                        //{ 
                        //    await _currentStockRepo.UpdateCurrentStockAsync(currentStock);
                        //}
                    }
                }

                // 6. Tell repositories to add the base tracked records
                await _salesOrder.CreateSalesOrderAsync(newOrder);
                await _collection.CreateCollectionAsync(newCollection);
                // Note: Because details, serials, collections are nested in navigation lists, 
                // EF Core implicitly tracks them too!

                // 7. Fire ONE single Save Changes and commit transaction to database
                await _unitOfWork.CommitTransactionAsync();

                // 8. Map to return response DTO
                return new List<SaveInvoiceResponseDto> {
                    new SaveInvoiceResponseDto { SalesOrderNo = newOrder.SalesOrderNo }
                };
            }
            catch (Exception)
            {
                // Something failed! Rollback everything immediately safely
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        } 
    }
}
 
