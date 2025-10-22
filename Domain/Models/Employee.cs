using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test_ci_cd_for_cqrs.Domain.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long EmployeeID { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
    }
}
