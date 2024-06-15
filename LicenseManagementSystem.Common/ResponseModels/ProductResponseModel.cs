using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Common.ResponseModels
{
    public class ProductResponseModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
