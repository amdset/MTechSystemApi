using AutoMapper;
using MTechSystemApi.DataAccess;
using MTechSystemApi.Models;

namespace MTechSystemApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IConfiguration _configuration;
        private readonly IDataAccess _dataAccess;
        private readonly IMapper _mapper;
        private readonly string _connMysql;

        public EmployeeService(IConfiguration configuration, IDataAccess dataAccess, IMapper mapper)
        {
            _configuration=configuration;
            _dataAccess=dataAccess;
            _mapper=mapper;
            _connMysql = _configuration.GetConnectionString("mysql_conn_db");
        }

        public async Task<List<EmployeeEntity>> GetAll()
        {
            string sql = "SELECT * FROM Employee";
            var lstEmployees = await _dataAccess.LoadData<EmployeeEntity>(sql, null, _connMysql);
            return lstEmployees.OrderBy(e => e.BornDate).ToList();
        }

        public async Task<EmployeeEntity> GetById(int id)
        {
            string sql = "SELECT * FROM Employee WHERE ID=@id";
            var parameters = new { id = id };
            var employee = await _dataAccess.LoadSigleRaw<EmployeeEntity>(sql, parameters, _connMysql);
            return employee;
        }

        public async Task<EmployeeEntity> GetByRfc(string rfc)
        {
            string sql = "SELECT * FROM Employee WHERE RFC=@rfc";
            var parameters = new { rfc = rfc };
            var employee = await _dataAccess.LoadSigleRaw<EmployeeEntity>(sql, parameters, _connMysql);
            return employee;
        }

        public async Task<bool> DeleteById(int id)
        {
            string sql = "DELETE FROM Employee WHERE ID=@id";
            var parameters = new { id = id };

            var result = await _dataAccess.SaveData<EmployeeEntity>(sql, parameters, _connMysql);
            return result>0;
        }

        public async Task<bool> DeleteByRfc(string rfc)
        {
            string sql = "DELETE FROM Employee WHERE RFC=@rfc";
            var parameters = new { rfc = rfc };

            var result = await _dataAccess.SaveData<EmployeeEntity>(sql, parameters, _connMysql);
            return result>0;
        }

        public async Task<EmployeeEntity> Save(EmployeeRequest employee)
        {
            string sql = @"INSERT INTO Employee(Name, LastName, RFC, BornDate, Status)
VALUES(@Name, @LastName, @RFC, @BornDate, @Status)";

            var result = await _dataAccess.SaveData<EmployeeEntity>(sql, employee, _connMysql);
            if (result<=0)
            {
                return null;
            }

            var newEmployee = await GetByRfc(employee.RFC);

            return newEmployee;
        }

        public async Task<bool> Update(int id, EmployeeRequest employee)
        {
            string sql = "UPDATE Employee SET Name=@Name, LastName=@LastName, BornDate=BornDate, Status=@Status WHERE ID=@ID ";
            var employeeToUpdate = await GetById(id);
            if (employeeToUpdate==null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(employee.Name))
            {
                employeeToUpdate.Name = employee.Name;
            }
            if (!string.IsNullOrWhiteSpace(employee.LastName))
            {
                employeeToUpdate.LastName = employee.LastName;
            }
            if (employee.BornDate != null)
            {
                employeeToUpdate.BornDate = employee.BornDate;
            }

            employeeToUpdate.Status = employee.Status;

            var result = await _dataAccess.SaveData<EmployeeEntity>(sql, employeeToUpdate, _connMysql);

            return result >0;
        }
    }
}
