using Bank_Branches_Individual_Mini_Project.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bank_Branches_Individual_Mini_Project.Controllers
{
    public class BankController : Controller
    {
        private readonly BankContext _context;

        public BankController(BankContext context)
        {
            _context = context;
        }

        public IActionResult ChangeLanguage(string language)
        {
            Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(language)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            
            
        }

        private static List<BankBranch> bankBranches = [
            new BankBranch {Id = 0, BranchManager = "Mohammed Ahmed", BranchName = "Salwa Branch", EmployeeCount = 45, LocationName = "Salwa", LocationURL = "https://g.co/kgs/drUiEeS" },
            new BankBranch {Id = 1, BranchManager = "Mohammed Bader", BranchName = "Mishref Branch", EmployeeCount = 51, LocationName = "Mishref", LocationURL = "https://g.co/kgs/qWHb9Lr" },
            ];
        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new BankDashboardViewModel();
            viewModel.BranchList = _context.BankBranches.Include(r => r.Employees).ToList();
            viewModel.TotalBranches = _context.BankBranches.Count();
            viewModel.TotalEmployees = _context.Employees.Count();
            viewModel.BranchWithMostEmployees = _context
                .BankBranches
                .OrderByDescending(b => b.Employees.Count)
                .FirstOrDefault();

            return View(viewModel);


        }
        [HttpGet]
        public IActionResult Create() 
        { 
            return View();
        }
        public class EditFormModel()
        {
            [Required]
            public int Id { get; set; }
            [Required]
            public string LocationName { get; set; }
            [Url]
            public string LocationURL { get; set; }
            [Required]
            public string BranchManager { get; set; }
            [Required]
            public string BranchName { get; set; }
            [Required]
            public int EmployeeCount { get; set; }

        }

        [HttpPost]
        public IActionResult Add(NewBranchForm model)
        {
            using (var context = _context) {
                if (ModelState.IsValid)
                {
                   
                    var branchManager = model.BranchManager;
                    var branchName = model.BranchName;
                    var employeeCount = model.EmployeeCount;
                    var locationName = model.LocationName;
                    var locationURL = model.LocationURL;

                    var bank = new BankBranch {BranchManager = branchManager, BranchName = branchName, EmployeeCount = employeeCount, LocationName = locationName, LocationURL = locationURL };
                    context.BankBranches.Add(bank);
                    context.SaveChanges();
                   // bankBranches.Add(new BankBranch { Id = id, BranchManager = branchManager, BranchName = branchName, EmployeeCount = employeeCount, LocationName = locationName, LocationURL = locationURL });
                    return RedirectToAction("Index");
                }
                return View(model);
            }
               
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            using (var context = _context)
            {
                var branch = context.BankBranches.FirstOrDefault(a => a.Id == id);
                if (branch == null)
                {
                    return NotFound();
                }
                return View(branch);
            }
        }
        [HttpGet]
        public IActionResult Edit (int id) 
        {
            var db = _context;
            var bank = db.BankBranches.Find(id);
            if (bank == null)
            {
                return RedirectToAction("Index");
            }
            var myForm = new EditFormModel();

            myForm.Id = id;
            myForm.BranchManager = bank.BranchManager;
            myForm.BranchName = bank.BranchName;
            myForm.LocationName = bank.LocationName;
            myForm.LocationURL = bank.LocationURL;
            myForm.EmployeeCount = bank.EmployeeCount;
            return View(myForm);
        }
        [HttpPost]
        public IActionResult Edit(int id, EditFormModel form) 
        {
            var context = _context;

            var locationName = form.LocationName;
            var locationURL = form.LocationURL;
            var branchManager = form.BranchManager;
            var employeeCount = form.EmployeeCount;
            if (ModelState.IsValid)
            {

                context.BankBranches.Add(new BankBranch
                {
                    LocationName = locationName,
                    LocationURL = locationURL,
                    BranchManager = branchManager,
                    EmployeeCount = employeeCount
                });
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            {
                return View(form);
            }


          
            return RedirectToAction("Index");
        }
        

        [HttpGet]
        public IActionResult AddEmployee(int Id)
        {
            var db = _context;
            var bank = db.BankBranches.Find(Id);
            
            return View(bank);
        }
        [HttpPost]
        public IActionResult AddEmployee(int Id, AddEmployeeForm form)
        {
            if (ModelState.IsValid)
            {
                var db = _context;
                var bank = db.BankBranches.Find(Id);
                var newEmployee = new Employee
                {
                    Id = Id,
                    Name = form.Name,
                    CivilId = form.CivilId,
                    Position = form.Position
                };

                bank.Employees.Add(newEmployee);
                db.SaveChanges();
            }
            return View(form);
        }
    }
}
