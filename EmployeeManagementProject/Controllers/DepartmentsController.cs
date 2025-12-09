using EmployeeManagementProject.DTOs.Department;
using EmployeeManagementProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost("create-Departments")]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentDto request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _departmentService.AddDepartmentAsync(request, cancellationToken);

            if (!result.Status)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("Get-All-Departments")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _departmentService.GetAllDepartmentsAsync(cancellationToken);

            if (!result.Status)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("Get-Dept-By-Id/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _departmentService.GetDepartmentByIdAsync(id, cancellationToken);

            if (!result.Status)
                return NotFound(result);

            return Ok(result);

        }

        [HttpPut("Update-Dept/{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateDepartmentDto request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _departmentService.UpdateDepartmentAsync(id, request, cancellationToken);

            if (!result.Status)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _departmentService.DeleteDepartmentAsync(id, cancellationToken);

            if (!result.Status)
                return NotFound(result);

            return Ok(result);
        }
    }
}
