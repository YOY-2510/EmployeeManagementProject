using EmployeeManagementProject.DTOs.Department;
using EmployeeManagementProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementProject.Controllers
{

    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("Create-Department")]
        public async Task<IActionResult> CreateDepartment()
        {
            return View();
        }

        [HttpPost("Create-Department")]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDto request, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _departmentService.AddDepartmentAsync(request, cancellationToken);

            if (!result.Status)
                return View();

            result.Message = "Department created successfully.";
            if (result.Status)
            {
                return RedirectToAction("Departments");
            }

            return View();
        }

        [HttpGet("get-all-departments")]
        public async Task<IActionResult> Departments(CancellationToken cancellationToken)
        {
            var result = await _departmentService.GetAllDepartmentsAsync(cancellationToken);

            if (!result.Status)
                return View(Enumerable.Empty<DepartmentDto>());

            return View(result.Data);
        }

        [HttpGet("get-department-by-id/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _departmentService.GetDepartmentByIdAsync(id, cancellationToken);

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

        [HttpDelete("❌Delete-Department/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _departmentService.DeleteDepartmentAsync(id, cancellationToken);

            if (!result.Status)
                return NotFound(result);

            return Ok(result);
        }
    }
}
