using System.ComponentModel.DataAnnotations;

namespace MTechSystemApi.Models
{
    public class EmployeeRequest
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        [MaxLength(13)]
        public string RFC { get; set; }

        public DateTime? BornDate { get; set; }

        public EmployeeStatus Status { get; set; } = EmployeeStatus.NotSet;
    }
}
