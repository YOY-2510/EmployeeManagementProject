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
        public async Task<IActionResult> Department(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _departmentService.GetDepartmentByIdAsync(id, cancellationToken);

            if (!result.Status)
                return View(new DepartmentDto());

            return View(result.Data);

        }

        [HttpGet("update-department/{id:guid}")]
        public async Task<IActionResult> EditDepartment(Guid id, CancellationToken cancellationToken)
        {
            var result = await _departmentService.GetDepartmentByIdAsync(id, cancellationToken);

            if (!result.Status)
                return View(new DepartmentDto());

            return View(result.Data);
        }

        [HttpPost("update-department/{id:guid}")]
        public async Task<IActionResult> EditDepartment(Guid id, UpdateDepartmentDto request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _departmentService.UpdateDepartmentAsync(id, request, cancellationToken);

            if (!result.Status)
            {
                return RedirectToAction("EditDepartment", new { id });
            }


            return RedirectToAction("Departments");
        }

        [HttpGet("delete-department/{id:guid}")]
        public async Task<IActionResult> DeleteDepartment(Guid id, CancellationToken cancellationToken)
        {
            var result = await _departmentService.DeleteDepartmentAsync(id, cancellationToken);

            if (!result.Status)
            {
                return RedirectToAction("Departments");
            }

            return RedirectToAction("Departments");
        }
    }
}
