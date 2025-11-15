using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesCh12.Models
{
    public enum Category
    {
        none, single, family
    }

    public class Benefits
    {
        public int ID { get; set; }

        [DisplayFormat(NullDisplayText = "single")]
        public Category? Category { get; set; }

        public bool Dental { get; set; } = false;
        public bool Vision { get; set; } = false;
        public bool Health { get; set; } = false;

        [Column(TypeName = "decimal(18,4)")]
        public decimal LifeIns { get; set; } = 25000;

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}