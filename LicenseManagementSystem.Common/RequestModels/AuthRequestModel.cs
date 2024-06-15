using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Common.RequestModels
{
    public class AuthRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
