using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RareCrewTask.Data;
using RareCrewTask.Models;
using RareCrewTask.Services;

namespace RareCrewTask.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly string _apiUrl = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";

        public EmployeeController()
        {
            _employeeService = new EmployeeService();
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                List<Employee> employees = await _employeeService.GetEmployeesFromApi(_apiUrl);

                return View(employees);
            }
            catch (ApplicationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An unexpected error occurred.";
                return View("Error");
            }
        }
    }
}