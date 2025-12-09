using EmployeeManagementProject.DTOs;
using EmployeeManagementProject.DTOs.Employee;
using EmployeeManagementProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementProject.Controllers
{
    
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("Create-Employee")]
        public async Task<IActionResult> Create([FromForm] CreateEmployeeDto request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(BaseResponse<string>.FailResponse("Invalid input"));

            var result = await _employeeService.AddEmployeeAsync(request, cancellationToken);

            if (!result.Status)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpGet("Get-All-Employees")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _employeeService.GetAllEmployeesAsync(CancellationToken.None);
            return Ok(response);
        }

        [HttpGet("Get-Emp-By-Id/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _employeeService.GetEmployeeByIdAsync(id, CancellationToken.None);

            if (!result.Status)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPut("Update-Employee/{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] CreateEmployeeDto request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(BaseResponse<string>.FailResponse("Invalid input"));

            var result = await _employeeService.UpdateEmployeeAsync(id, request, cancellationToken);

            if (!result.Status)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("❌Delete-Employee{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id, cancellationToken);

            if (!result.Status)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
