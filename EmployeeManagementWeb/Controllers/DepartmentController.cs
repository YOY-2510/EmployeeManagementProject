using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeManagementProject.DTOs.Department;
using EmployeeManagementProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementProject.Controllers
{

    public class DepartmentController(IDepartmentService departmentService, INotyfService notyf) : Controller
    {


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

            var result = await departmentService.AddDepartmentAsync(request, cancellationToken);

            if (!result.Status)
            {
                notyf.Error(result.Message);
                return View();
            }

            notyf.Success(result.Message);

            if (result.Status)
            {
                return RedirectToAction("Departments");
            }

            return View();
        }

        [HttpGet("get-all-departments")]
        public async Task<IActionResult> Departments(CancellationToken cancellationToken)
        {
            var result = await departmentService.GetAllDepartmentsAsync(cancellationToken);

            if (!result.Status)
            {
                notyf.Error(result.Message);

                return View(Enumerable.Empty<DepartmentDto>());
            }

            return View(result.Data);
        }

        [HttpGet("get-department-by-id/{id:guid}")]
        public async Task<IActionResult> Department(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await departmentService.GetDepartmentByIdAsync(id, cancellationToken);

            if (!result.Status)
            {
                notyf.Error(result.Message);
                return View(new DepartmentDto());

            }
            return View(result.Data);

        }

        [HttpGet("update-department/{id:guid}")]
        public async Task<IActionResult> EditDepartment(Guid id, CancellationToken cancellationToken)
        {
            var result = await departmentService.GetDepartmentByIdAsync(id, cancellationToken);

            if (!result.Status)
            {
                notyf.Error(result.Message);
                return View(new DepartmentDto());

            }
            return View(result.Data);
        }

        [HttpPost("update-department/{id:guid}")]
        public async Task<IActionResult> EditDepartment(Guid id, UpdateDepartmentDto request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await departmentService.UpdateDepartmentAsync(id, request, cancellationToken);

            if (!result.Status)
            {
                notyf.Error(result.Message);
                return RedirectToAction("EditDepartment", new { id });
            }

            notyf.Success(result.Message);
            return RedirectToAction("Departments");
        }

        [HttpPost("delete-department/{id:guid}")]
        public async Task<IActionResult> DeleteDepartment(Guid id, CancellationToken cancellationToken)
        {
            var result = await departmentService.DeleteDepartmentAsync(id, cancellationToken);

            if (!result.Status)
            {
                notyf.Error(result.Message);
                return RedirectToAction("Departments");
            }
            notyf.Success(result.Message);
            return RedirectToAction("Departments");
        }
    }
}
