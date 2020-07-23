using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc_asp_core.Models;

namespace mvc_asp_core.Controllers
{
    public class EmployeeCategoriesController : Controller
    {
        private readonly HospitalContext _context;

        public EmployeeCategoriesController(HospitalContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmployeeCategorie.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeCategory = await _context.EmployeeCategorie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeCategory == null)
            {
                return NotFound();
            }

            return View(employeeCategory);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] EmployeeCategory employeeCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeCategory);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeCategory = await _context.EmployeeCategorie.FindAsync(id);
            if (employeeCategory == null)
            {
                return NotFound();
            }
            return View(employeeCategory);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] EmployeeCategory employeeCategory)
        {
            if (id != employeeCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeCategoryExists(employeeCategory.Id))
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
            return View(employeeCategory);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeCategory = await _context.EmployeeCategorie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeCategory == null)
            {
                return NotFound();
            }

            return View(employeeCategory);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeCategory = await _context.EmployeeCategorie.FindAsync(id);
            _context.EmployeeCategorie.Remove(employeeCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeCategoryExists(int id)
        {
            return _context.EmployeeCategorie.Any(e => e.Id == id);
        }
    }
}
