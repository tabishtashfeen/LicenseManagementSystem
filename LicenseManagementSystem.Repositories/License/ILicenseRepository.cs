﻿using LicenseManagementSystem.Common.ResponseModels;
using LicenseManagementSystem.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Repositories.License
{
    public interface ILicenseRepository
    {
        Task CreateLicense(LicenseManagementSystem.Models.License.License license);
        Task<List<LicenseResponseModel>> GetAllLicenses();
        Task<List<LicenseResponseModel>> GetUserLicensesById(long id);
        Task<LicenseManagementSystem.Models.License.License> ActiveteLicense(long userId, string key);
    }
}
