using MTechSystemApi.Models;

namespace MTechSystemApi.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeEntity>> GetAll();

        Task<EmployeeEntity> GetById(int id);
        Task<EmployeeEntity> GetByRfc(string rfc);

        Task<EmployeeEntity> Save(EmployeeEntity employee);

        Task<bool> Update(int id,EmployeeEntity employee);

        Task<bool> DeleteById(int id);
        Task<bool> DeleteByRfc(string rfc);
    }
}
