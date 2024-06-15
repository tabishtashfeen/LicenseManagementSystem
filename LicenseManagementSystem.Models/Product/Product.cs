using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicenseManagementSystem.Models.Product
{
    [Table("Products")]
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string? Description { get; set; }
    }
}
