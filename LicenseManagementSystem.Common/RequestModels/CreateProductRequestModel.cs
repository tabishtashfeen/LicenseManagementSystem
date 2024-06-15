using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Common.RequestModels
{
    public class CreateProductRequestModel
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string? Description { get; set; }
    }
}
