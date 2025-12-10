using EmployeeManagementProject.DTOs;
using EmployeeManagementProject.DTOs.Department;
using EmployeeManagementProject.DTOs.Employee;
using EmployeeManagementProject.Services.Interface;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManagementProject.Controllers
{

    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id, System.Threading.CancellationToken.None);

            if (employee == null || !employee.Status)
                return NotFound();

            return View(employee.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync(System.Threading.CancellationToken.None);
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateDepartmentsDropDown();
            return View();
        }

        [HttpPost("create-employee")]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDto request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDepartmentsDropDown(request.DepartmentId, cancellationToken);
                return View(request);
            }

            await _employeeService.AddEmployeeAsync(request, cancellationToken);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var employeeResult = await _employeeService.GetEmployeeByIdAsync(id, CancellationToken.None);
            if (employeeResult == null || !employeeResult.Status)
                return NotFound();

            var dto = new CreateEmployeeDto
            {
                FullName = employeeResult.Data.FullName,
                Email = employeeResult.Data.Email,
                DepartmentId = employeeResult.Data.DepartmentId
            };

            await PopulateDepartmentsDropDown(dto.DepartmentId);
            return View(dto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CreateEmployeeDto request)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDepartmentsDropDown(request.DepartmentId);
                return View(request);
            }

            var result = await _employeeService.UpdateEmployeeAsync(id, request, System.Threading.CancellationToken.None);

            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message ?? "Error updating employee");
                await PopulateDepartmentsDropDown(request.DepartmentId);
                return View(request);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id, System.Threading.CancellationToken.None);

            if (!result.Status)
            {
                TempData["Error"] = result.Message;
            }

            return RedirectToAction("Index");
        }

        private async Task PopulateDepartmentsDropDown(Guid? selectedId = null, CancellationToken cancellationToken = default)
        {
            var response = await _departmentService.GetAllDepartmentsAsync(cancellationToken);

            if (response.Status && response.Data != null)
            {
                ViewBag.Departments = new SelectList(response.Data, "Id", "Name", selectedId);
            }
            else
            {
                ViewBag.Departments = new SelectList(Enumerable.Empty<DepartmentDto>(), "Id", "Name");
            }
        }
    }
}
