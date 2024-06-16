using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Common.RequestModels
{
    public class CreateLicenseRequestModel
    {
        public long? Id { get; set; }
        public string? Key { get; set; }
        public long ProductId { get; set; }
        public long UserId { get; set; }
        public bool IsActivated { get; set; } = false;
    }
}
