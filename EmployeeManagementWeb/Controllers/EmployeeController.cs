using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeManagementProject.DTOs.Employee;
using EmployeeManagementProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManagementProject.Controllers
{

    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IDepartmentService departmentService;
        private readonly INotyfService notyf;

        public EmployeeController(
            IEmployeeService employeeService,
            IDepartmentService departmentService,
            INotyfService notyf)
        {
            this.employeeService = employeeService;
            this.departmentService = departmentService;
            this.notyf = notyf;
        }

        [HttpGet("employees")]
        public async Task<IActionResult> Employees()
        {
            var response = await employeeService.GetAllEmployeesAsync(CancellationToken.None);

            if (!response.Status || response.Data == null)
            {
                return View(new List<EmployeeDto>());
            }

            return View(response.Data); 
        }

        [HttpGet("create-employee")]
        public async Task<IActionResult> CreateEmployee(CancellationToken cancellationToken = default)
        {
            await PopulateDepartmentsDropDown(cancellationToken);
            ViewBag.Titles = GetTitles();
            var model = new CreateEmployeeDto(); 
            return View(model);
        }

        [HttpPost("create-employee")]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDto request, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDepartmentsDropDown(cancellationToken, request.Id);
                ViewBag.Titles = GetTitles();
                return View(request);
            }

            var result = await employeeService.AddEmployeeAsync(request, cancellationToken);

            if (!result.Status)
            {
                notyf.Error(result.Message);
                await PopulateDepartmentsDropDown(cancellationToken, request.Id);
                ViewBag.Titles = GetTitles();
                return View(request);
            }

            notyf.Success(result.Message);
            return RedirectToAction("Employees", "Employee");
        }


        [HttpGet("get-employee-by-id/{id:guid}")]
        public async Task<IActionResult> EmployeeDetails(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await employeeService.GetEmployeeByIdAsync(id, cancellationToken);

            if (!result.Status)
            {
                notyf.Error(result.Message);
                return View(new EmployeeDto());
            }

            return View(result.Data);
        }

        [HttpGet("update-employee/{id:guid}")]
        public async Task<IActionResult> EditEmployee(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await employeeService.GetEmployeeByIdAsync(id, cancellationToken);

            if (!result.Status)
            {
                notyf.Error(result.Message);
                return View(new CreateEmployeeDto());
            }

            var dto = new CreateEmployeeDto
            {
                Id = result.Data.Id,
                FirstName = result.Data.FirstName,
                LastName = result.Data.LastName,
                OtherName = result.Data.OtherName,
                Gender = result.Data.Gender,
                PhoneNumber = result.Data.PhoneNumber,
                Email = result.Data.Email,
                Address = result.Data.Address,
                Title = result.Data.Title,
                DateOfBirth = result.Data.DateOfBirth
            };

            await PopulateDepartmentsDropDown(cancellationToken, dto.Id);
            ViewBag.Titles = GetTitles();
            return View(dto);
        }

        [HttpPost("update-employee/{id:guid}")]
        public async Task<IActionResult> EditEmployee(Guid id, CreateEmployeeDto request, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDepartmentsDropDown(cancellationToken, request.Id);
                ViewBag.Titles = GetTitles();
                return View(request);
            }

            var result = await employeeService.UpdateEmployeeAsync(id, request, cancellationToken);

            if (!result.Status)
            {
                notyf.Error(result.Message);
                await PopulateDepartmentsDropDown(cancellationToken, request.Id);
                ViewBag.Titles = GetTitles();
                return View(request);
            }

            notyf.Success(result.Message);
            return RedirectToAction("Departments", "Department"); 
        }

        [HttpPost("delete-employee/{id:guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await employeeService.DeleteEmployeeAsync(id, cancellationToken);

            if (!result.Status)
            {
                notyf.Error(result.Message);
            }
            else
            {
                notyf.Success(result.Message);
            }

            return RedirectToAction("Employees", "Employee"); 
        }

        private async Task PopulateDepartmentsDropDown(CancellationToken cancellationToken = default, Guid? selectedId = null)
        {
            var result = await departmentService.GetAllDepartmentsAsync(cancellationToken);

            var list = new List<SelectListItem>();

            if (result?.Status == true && result.Data != null)
            {
                list = result.Data.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),  
                    Text = d.Name,
                    Selected = (selectedId != null && d.Id == selectedId)
                }).ToList();
            }

            ViewBag.Departments = list;
        }
        private List<SelectListItem> GetTitles()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Mrs", Value = "Mrs" },
                new SelectListItem { Text = "Mr", Value = "Mr" },
                new SelectListItem { Text = "Miss", Value = "Miss" },
                new SelectListItem { Text = "Master", Value = "Master" },
                new SelectListItem { Text = "Prof", Value = "Prof" },
                new SelectListItem { Text = "Dr", Value = "Dr" },
            };
        }
    }
}
