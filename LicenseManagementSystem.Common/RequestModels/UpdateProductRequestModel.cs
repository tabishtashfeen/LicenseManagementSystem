using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Common.RequestModels
{
    public class UpdateProductRequestModel : CreateProductRequestModel
    {
        public long Id { get; set; }
    }
}
