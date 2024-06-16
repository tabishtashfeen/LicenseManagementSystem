using AutoMapper;
using LicenseManagementSystem.Common.RequestModels;
using LicenseManagementSystem.Common.ResponseModels;
using LicenseManagementSystem.Models.License;
using LicenseManagementSystem.Models.Product;
using LicenseManagementSystem.Models.User;

namespace LicenseManagementSystem.Helper.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponseModel>().ReverseMap();
            CreateMap<Product, CreateProductRequestModel>().ReverseMap();
            CreateMap<License, LicenseResponseModel>().ReverseMap();
            CreateMap<License, CreateLicenseRequestModel>().ReverseMap();
            CreateMap<User, UserResponseModel>().ReverseMap();
        }
    }
}
