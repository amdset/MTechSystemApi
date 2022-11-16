using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MTechSystemApi.DataAccess;
using MTechSystemApi.Models;
using MTechSystemApi.Services;
using System.Text.RegularExpressions;

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
        [HttpGet("{name?}")]
        public async Task<ActionResult<List<EmployeeEntity>>> Get(string name = "")
        {
            return await _employeeService.GetAll(name);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id:int}", Name = nameof(Get))]
        public async Task<ActionResult<EmployeeEntity>> GetById(int id)
        {
            var employee = await _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            employee.Href = Url.Link(nameof(Get), new { id = id });

            return employee;
        }


        [HttpGet("rfc/{rfc}", Name = nameof(GetByRfc))]
        public async Task<ActionResult<EmployeeEntity>> GetByRfc( string rfc)
        {
            var employee = await _employeeService.GetByRfc(rfc);
            if (employee == null)
            {
                return NotFound();
            }
            employee.Href = Url.Link(nameof(GetByRfc), new { rfc = rfc });

            return employee;
        }

        // POST api/<EmployeeController>
        [HttpPost(Name = nameof(Post))]
        [ProducesResponseType(201)]
        public async Task<ActionResult<EmployeeEntity>> Post([FromBody] EmployeeRequest employee)
        {
            if ((int)employee.Status <= 0 || (int)employee.Status > 3)
            {
                employee.Status = EmployeeStatus.NotSet;
            }
            

            if(RfcValido(employee.RFC) == false)
            {
                return BadRequest("The fiel RFC has not a valid value");
            }

            employee.RFC = employee.RFC.ToUpper();

            var newEmployee = await _employeeService.Save(employee);
            if (newEmployee == null)
            {
                return new StatusCodeResult(500);
            }

            newEmployee.Href = Url.Link(nameof(Post), null)+$"/{newEmployee.ID}";
            return newEmployee;
        }

        private bool RfcValido(string rfc)
        {
            // patron del RFC, persona moral
            var rfc_pattern_pm = "^(([A-ZÑ&]{3})([0-9]{2})([0][13578]|[1][02])(([0][1-9]|[12][\\d])|[3][01])([A-Z0-9]{3}))|" +
                  "(([A-ZÑ&]{3})([0-9]{2})([0][13456789]|[1][012])(([0][1-9]|[12][\\d])|[3][0])([A-Z0-9]{3}))|" +
                  "(([A-ZÑ&]{3})([02468][048]|[13579][26])[0][2]([0][1-9]|[12][\\d])([A-Z0-9]{3}))|" +
                  "(([A-ZÑ&]{3})([0-9]{2})[0][2]([0][1-9]|[1][0-9]|[2][0-8])([A-Z0-9]{3}))$";

            // patron del RFC, persona fisica
           var _rfc_pattern_pf = "^(([A-ZÑ&]{4})([0-9]{2})([0][13578]|[1][02])(([0][1-9]|[12][\\d])|[3][01])([A-Z0-9]{3}))|" +
                                  "(([A-ZÑ&]{4})([0-9]{2})([0][13456789]|[1][012])(([0][1-9]|[12][\\d])|[3][0])([A-Z0-9]{3}))|" +
                                  "(([A-ZÑ&]{4})([02468][048]|[13579][26])[0][2]([0][1-9]|[12][\\d])([A-Z0-9]{3}))|" +
                                  "(([A-ZÑ&]{4})([0-9]{2})[0][2]([0][1-9]|[1][0-9]|[2][0-8])([A-Z0-9]{3}))$";
            return Regex.IsMatch(rfc, rfc_pattern_pm, RegexOptions.IgnoreCase) || Regex.IsMatch(rfc, _rfc_pattern_pf);
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EmployeeRequest employee)
        {
            var result = await _employeeService.Update(id, employee);
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
