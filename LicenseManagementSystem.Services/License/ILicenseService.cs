using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Services.License
{
    public interface ILicenseService
    {
        Task<string> CreateLicenseKey(CreateLicenseRequestModel createLicense);
        Task<List<LicenseResponseModel>> GetAllLicensesService();
        Task<List<LicenseResponseModel>> GetUserLicensesByIdService(long id);
        Task<string> ActiveteLicenseService(long userId, string key);
    }
}
