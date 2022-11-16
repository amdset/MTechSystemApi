using System.ComponentModel.DataAnnotations;

namespace MTechSystemApi.Models
{
    public class EmployeeEntity : Resource
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        [MaxLength(13)]
        public string RFC { get; set; }

        public DateTime? BornDate { get; set; }

        public EmployeeStatus Status { get; set; } = EmployeeStatus.NotSet;

    }

    public enum EmployeeStatus
    {
        NotSet=1,
        Active=2,
        Inactive=3,
    }

}
