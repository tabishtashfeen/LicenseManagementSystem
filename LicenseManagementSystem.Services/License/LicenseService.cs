using LicenseManagementSystem.Helper.Functions;
using LicenseManagementSystem.Repositories.License;
using LicenseManagementSystem.Repositories.UnitofWork;
using LicenseManagementSystem.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Services.License
{
    public class LicenseService : ILicenseService
    {
        private readonly ILicenseRepository _licenseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IUsersRepository _usersRepository;
        public LicenseService(ILicenseRepository licenseRepository, IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService,
            IUsersRepository usersRepository)
        {
            _licenseRepository = licenseRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _usersRepository = usersRepository;
        }
        public async Task<string> CreateLicenseKey(CreateLicenseRequestModel createLicense)
        {
            var newLicense = _mapper.Map<LicenseManagementSystem.Models.License.License>(createLicense);
            newLicense.IsActivated = false;
            newLicense.Key = LicenseKeyGenerator.GenerateLicenseKey();
            newLicense.CreatedDate = DateTime.Now;
            await _licenseRepository.CreateLicense(newLicense);
            if (_unitOfWork.SaveWithCount() > 0)
            {
                var user = await _usersRepository.GetUserById(newLicense.UserId);
                var userName = user.FirstName + " " + user.LastName;
                try
                {
                    await _emailService.SendEmailAsync(userName, user.Email, "License Key", newLicense.Key, user.UserName);
                    return "License Successfully Created!";
                }
                catch (Exception)
                {
                    return "License Successfully Created! But Email Failed to send.";
                }
            }
            return "Failed to Create the Product!";
        }
        public async Task<List<LicenseResponseModel>> GetAllLicensesService() => await _licenseRepository.GetAllLicenses();
        public async Task<List<LicenseResponseModel>> GetUserLicensesByIdService(long id) => await _licenseRepository.GetUserLicensesById(id);
        public async Task<string> ActiveteLicenseService(long userId, string key)
        {
            try
            {
                var license = await _licenseRepository.ActiveteLicense(userId, key);
                if (license != null)
                {
                    if (_unitOfWork.SaveWithCount() > 0)
                    {
                        return "License Activated!";
                    }
                    else
                    {
                        return "License Activation failed!";
                    }
                }
                throw new KeyNotFoundException();
            }
            catch (KeyNotFoundException)
            {
                return "License key is not valid!";
            }
            catch (Exception)
            {
                return "Failed to activate license";
            }

        }
    }
}
