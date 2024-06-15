using LicenseManagementSystem.DataAccess.Context;
using LicenseManagementSystem.Models.Product;
using LicenseManagementSystem.Models.License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}