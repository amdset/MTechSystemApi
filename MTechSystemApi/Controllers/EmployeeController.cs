using Microsoft.AspNetCore.Mvc;
using MTechSystemApi.DataAccess;
using MTechSystemApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MTechSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IDataAccess _dataAccess;
        private readonly IConfiguration _configuration;

        public EmployeeController(IDataAccess dataAccess, IConfiguration configuration)
        {
            _dataAccess=dataAccess;
            _configuration=configuration;
        }


        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task< ActionResult< List<Employee>>> Get()
        {
            string sql = "SELECT * FROM Employee";
            string conn = _configuration.GetConnectionString("mysql_conn_db");
            var lstEmployees = await _dataAccess.LoadData<Employee>(sql, null, conn);
            return lstEmployees;
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void Post([FromBody] Employee employee)
        {

        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
