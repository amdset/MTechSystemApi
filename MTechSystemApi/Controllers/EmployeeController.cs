using Microsoft.AspNetCore.Mvc;
using MTechSystemApi.DataAccess;
using MTechSystemApi.Models;
using MTechSystemApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MTechSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<ActionResult<List<EmployeeEntity>>> Get()
        {
            return await _employeeService.GetAll();
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeEntity>> Get(int id)
        {
            var employee = await _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // POST api/<EmployeeController>
        [HttpPost(Name = nameof(Post))]
        public async Task<ActionResult<EmployeeEntity>> Post([FromBody] EmployeeEntity employee)
        {
            if ((int)employee.Status <= 0 || (int)employee.Status >= 3)
            {
                employee.Status = EmployeeStatus.NotSet;
            }
            var newEmployee = await _employeeService.Save(employee);
            if (newEmployee == null)
            {
                return new StatusCodeResult(500);
            }

            newEmployee.Href = Url.Link(nameof(Post), null);
            return newEmployee;
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EmployeeEntity employee)
        {
            var result =  await _employeeService.Update(id, employee);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _employeeService.DeleteById(id);
            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
