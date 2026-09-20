using HN_Backend.Data;
using HN_Backend.DTOs;
using HN_Backend.DTOs.Supplier;
using HN_Backend.Helpers;
using HN_Backend.Interface;
using HN_Project.DTOs;

//using HN_Backend.Models;
using HN_Project.Interface;

namespace HN_Backend.Service
{
    public class SupplierService
    {
        private readonly ISupplier _supplier;
        private readonly ImageService _imageService;
        private readonly CurrentSessionData _currentSessionData;
        public SupplierService(ISupplier supplier, ImageService imageService,CurrentSessionData currentSessionData)
        {
            _supplier = supplier;
            _imageService = imageService;
            _currentSessionData = currentSessionData;
        }
        public async Task<List<SupplierAutocompleteDto>> GetSupplierByCodeNamePhone(string objParam)
        {
            long companyId = _currentSessionData.CompanyId;
            return await _supplier.GetSupplierByCodeNamePhone(objParam, companyId);
        }

        public async Task<SupplierSelectedInformationDto> GetSupplierInformationById(long SupplierId)
        {
            long companyId = _currentSessionData.CompanyId;
            return await _supplier.GetSupplierInformationById(SupplierId, companyId);
        }


        //public async Task<string> SaveSupplier(SupplierVM vm)
        //{
        //    string? imagePath = null;

        //    try
        //    {
        //        if (vm.SupplierImage != null)
        //        {
        //            imagePath = await _imageService.SaveImageAsync(vm.SupplierImage, "Supplier");
        //        }

        //        var supplier = new Supplier
        //        {
        //            Name = vm.Name,
        //            Code = vm.Code ?? "SUP-0005",
        //            Phone = vm.Phone,
        //            Email = vm.Email,
        //            Address = vm.Address,
        //            Picture = imagePath,
        //            EntryBy = 1, 
        //        };

        //        await _supplier.SaveSupplier(supplier);
        //        return supplier.Code;
        //    }
        //    catch
        //    {
        //        if (!string.IsNullOrEmpty(imagePath))
        //        {
        //            await _imageService.DeleteImageAsync(imagePath);
        //        }

        //        throw;
        //    }
        //}

    }
}