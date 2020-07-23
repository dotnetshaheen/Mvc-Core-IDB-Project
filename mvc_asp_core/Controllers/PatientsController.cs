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

using mvc_asp_core.Models.Pagelist;

namespace mvc_asp_core.Controllers
{
    public class PatientsController : Controller
    {
        private readonly HospitalContext _context;
        private readonly IHostingEnvironment _Host;

        public PatientsController(HospitalContext context, IHostingEnvironment Host)
        {
            _context = context;
            _Host = Host;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public IList<Patient> Patients { get; set; }


        
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {



            ViewData["CurrentSort"] = sortOrder;

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            var patient = from s in _context.Patients
                           select s;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                patient = patient.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString)
                                       );
            }

            switch (sortOrder)
            {
                case "name_desc":
                    patient = patient.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    patient = patient.OrderBy(s => s.Dob);
                    break;
                case "date_desc":
                    patient = patient.OrderByDescending(s => s.Dob);
                    break;
                default:
                    patient = patient.OrderBy(s => s.LastName);
                    break;
            }


            int pageSize = 3;
            return View(await PaginatedList<Patient>.CreateAsync(patient.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        
        public IActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                if (patient.imgFile != null)
                {
                    string ext = Path.GetExtension(patient.imgFile.FileName).ToLower();
                    if (ext == ".jpg" || ext == ".png")
                    {
                        string fileName = Path.Combine(_Host.WebRootPath, "images\\patient", patient.imgFile.FileName);
                        using (var fileStream = new FileStream(fileName, FileMode.Create))
                        {
                            patient.imgFile.CopyTo(fileStream);
                            patient.Photo = "/images/patient/" + patient.imgFile.FileName;
                        }
                    }
                }

                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (patient.imgFile != null)
                    {
                        string ext = Path.GetExtension(patient.imgFile.FileName).ToLower();
                        if (ext == ".jpg" || ext == ".png")
                        {
                            string fileName = Path.Combine(_Host.WebRootPath, "images\\patient", patient.imgFile.FileName);
                            using (var fileStream = new FileStream(fileName, FileMode.Create))
                            {
                                patient.imgFile.CopyTo(fileStream);
                                patient.Photo = "/images/patient/" + patient.imgFile.FileName;
                            }
                        }
                    }
                    

                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
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
            return View(patient);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
    }
}
