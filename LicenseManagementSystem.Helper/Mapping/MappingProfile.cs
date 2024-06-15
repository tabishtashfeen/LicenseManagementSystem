using AutoMapper;
using LicenseManagementSystem.Common.RequestModels;
using LicenseManagementSystem.Common.ResponseModels;
using LicenseManagementSystem.Models.Product;

namespace LicenseManagementSystem.Helper.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponseModel>().ReverseMap();
            CreateMap<Product, CreateProductRequestModel>().ReverseMap();
        }
    }
}
