using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee.Api.Model
{
    public class EmployeeData
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string contactNo { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public Nullable<DateTime> createdDate { get; set; }
        public Nullable<int> isDeleted { get; set; }
    }
}
