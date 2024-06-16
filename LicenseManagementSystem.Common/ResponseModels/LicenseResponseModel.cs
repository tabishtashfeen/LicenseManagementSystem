using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Common.ResponseModels
{
    public class LicenseResponseModel
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public string UserName { get;set; }
        public string ProductName { get; set; }
        public long ProductId { get; set; }
        public long UserId { get; set; }
        public bool IsActivated { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
