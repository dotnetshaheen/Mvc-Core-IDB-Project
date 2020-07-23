using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc_asp_core.Models;

namespace mvc_asp_core.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly HospitalContext _context;
        private readonly IHostingEnvironment _Host;

        public EmployeesController(HospitalContext context, IHostingEnvironment Host)
        {
            _context = context;
            _Host = Host;
        }

        
        public async Task<IActionResult> Index(int? id)
        {
            var vm = new PatientServiceRelatedEntity();

            vm.employees = await _context.Employee.Include(i => i.PEServices)
                                                    .ThenInclude(i => i.Patient).ToListAsync();

            if (id != null)
            {
                ViewData["EmployeeId"] = id.Value;
                Employee employee = vm.employees.Where(i => i.Id == id.Value).Single();
                //vm.services = employee.PEServices.Select(s => s.Service);
                vm.patients = employee.PEServices.Select(s => s.Patient);
            }

            //var hospitalContext = _context.Employee.Include(e => e.EmployeeCategory).Include(e => e.department).Include(e => e.designation);
            return View(vm);
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.EmployeeCategory)
                .Include(e => e.department)
                .Include(e => e.designation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        
        public IActionResult Create()
        {
            ViewData["EmCategoryID"] = new SelectList(_context.EmployeeCategorie, "Id", "Name");
            ViewData["DeptID"] = new SelectList(_context.Department, "Id", "Details");
            ViewData["DesignationId"] = new SelectList(_context.Designation, "Id", "Id");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.imgFile != null)
                {
                    string ext = Path.GetExtension(employee.imgFile.FileName).ToLower();
                    if (ext == ".jpg" || ext == ".png")
                    {
                        string fileName = Path.Combine(_Host.WebRootPath, "images\\employee", employee.imgFile.FileName);
                        using (var fileStream = new FileStream(fileName, FileMode.Create))
                        {
                            employee.imgFile.CopyTo(fileStream);
                            employee.Photo = "/images/employee/" + employee.imgFile.FileName;
                        }
                    }
                }


                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmCategoryID"] = new SelectList(_context.EmployeeCategorie, "Id", "Name", employee.EmCategoryID);
            ViewData["DeptID"] = new SelectList(_context.Department, "Id", "Details", employee.DeptID);
            ViewData["DesignationId"] = new SelectList(_context.Designation, "Id", "Id", employee.DesignationId);
            return View(employee);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["EmCategoryID"] = new SelectList(_context.EmployeeCategorie, "Id", "Name", employee.EmCategoryID);
            ViewData["DeptID"] = new SelectList(_context.Department, "Id", "Details", employee.DeptID);
            ViewData["DesignationId"] = new SelectList(_context.Designation, "Id", "Id", employee.DesignationId);
            return View(employee);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (employee.imgFile != null)
                    {
                        string ext = Path.GetExtension(employee.imgFile.FileName).ToLower();
                        if (ext == ".jpg" || ext == ".png")
                        {
                            string fileName = Path.Combine(_Host.WebRootPath, "images\\employee", employee.imgFile.FileName);
                            using (var fileStream = new FileStream(fileName, FileMode.Create))
                            {
                                employee.imgFile.CopyTo(fileStream);
                                employee.Photo = "/images/employee/" + employee.imgFile.FileName;
                            }
                        }
                    }

                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmCategoryID"] = new SelectList(_context.EmployeeCategorie, "Id", "Name", employee.EmCategoryID);
            ViewData["DeptID"] = new SelectList(_context.Department, "Id", "Details", employee.DeptID);
            ViewData["DesignationId"] = new SelectList(_context.Designation, "Id", "Id", employee.DesignationId);
            return View(employee);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.EmployeeCategory)
                .Include(e => e.department)
                .Include(e => e.designation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
