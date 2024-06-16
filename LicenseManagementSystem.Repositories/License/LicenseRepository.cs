using LicenseManagementSystem.DataAccess.Context;
using LicenseManagementSystem.Models.Product;
using LicenseManagementSystem.Models.License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicenseManagementSystem.Common.ResponseModels;

namespace LicenseManagementSystem.Repositories.License
{
    public class LicenseRepository : ILicenseRepository
    {
        private readonly DatabaseContext _databaseContext;
        public LicenseRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task CreateLicense(LicenseManagementSystem.Models.License.License license) => await _databaseContext.Licenses.AddAsync(license);
        public async Task<List<LicenseResponseModel>> GetAllLicenses()
        {
            var query = from license in _databaseContext.Licenses
                        join users in _databaseContext.Users on license.UserId equals users.Id
                        join products in _databaseContext.Products on license.ProductId equals products.Id
                        select new LicenseResponseModel
                        {
                            Id = license.Id,
                            UserName = users.UserName ?? "",
                            ProductName = products.Name ?? "",
                            ProductId = products.Id,
                            UserId = users.Id,
                            CreatedDate = license.CreatedDate,
                            IsActivated = license.IsActivated,
                            ModifiedDate = license.ModifiedDate,
                            Key = license.Key
                        };

            return await query.ToListAsync();
        }
        public async Task<List<LicenseResponseModel>> GetUserLicensesById(long id)
        {
            var query = from license in _databaseContext.Licenses
                        join users in _databaseContext.Users on license.UserId equals users.Id
                        join products in _databaseContext.Products on license.ProductId equals products.Id
                        where users.Id == id && license.IsActivated
                        select new LicenseResponseModel
                        {
                            Id = license.Id,
                            UserName = users.UserName ?? "",
                            ProductName = products.Name ?? "",
                            ProductId = products.Id,
                            UserId = users.Id,
                            CreatedDate = license.CreatedDate,
                            IsActivated = license.IsActivated,
                            ModifiedDate = license.ModifiedDate
                        };

            return await query.ToListAsync();
        }
        public async Task<LicenseManagementSystem.Models.License.License> ActiveteLicense(long userId, string key)
        {
            var license = await _databaseContext.Licenses.FirstOrDefaultAsync(x => x.UserId == userId && key == key);
            if (license != null)
            {
                license.IsActivated = true;
            }
            return license;
        }
    }
}