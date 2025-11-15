using System.ComponentModel.DataAnnotations;

namespace EmployeesCh12.Models
{
    public class Location
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string Type { get; set; } = string.Empty;

        [StringLength(100)]
        public string Address { get; set; } = string.Empty;

        [StringLength(10)]
        public string? Zipcode { get; set; }

        public ICollection<DepartmentLocation> DepartmentLocations { get; set; } = new List<DepartmentLocation>();
    }
}