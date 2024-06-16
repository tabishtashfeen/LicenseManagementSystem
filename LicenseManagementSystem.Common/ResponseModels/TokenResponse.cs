using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Common.ResponseModels
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public UserResponseModel UserData { get; set; }
    }
}
