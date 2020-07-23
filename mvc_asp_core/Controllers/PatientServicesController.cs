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
    public class PatientServicesController : Controller
    {
        private readonly HospitalContext _db;
        public PatientServicesController(HospitalContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> ReadRelatedDate(int? id, int? serviceId)
        {
            var relatedDate = new PatientServiceRelatedEntity();
            relatedDate.patientServices = await _db.PatientService.Include(s => s.Services)
                                             .Include(s => s.Patient)
                                             .Include(s => s.Employee)
                                             .ThenInclude(s => s.department).ToListAsync();

            if (id != null)
            {
                ViewData["PatientServiceId"] = id.Value;
                PatientServices ps = relatedDate.patientServices.Where(i => i.Id == id.Value).Single();
                

            }


            return View(relatedDate);
        }

        public IActionResult Index()
        {
            ViewBag.patient = new SelectList((from p in _db.Patients.ToList()
                                              select new
                                              {
                                                  Id = p.Id,
                                                  Name = p.Id + "-" + p.Title + " " + p.FirstName + " " + p.LastName
                                              }), "Id", "Name");

            ViewBag.Doctor = new SelectList((from emp in _db.Employee.Where(em=>em.DesignationId==1).ToList()
                                              select new
                                              {
                                                  Id = emp.Id,
                                                  Name = emp.Id + "-" + emp.Name
                                              }), "Id", "Name");
            ViewBag.service = new SelectList((from s in _db.Services.ToList()
                                             select new
                                             {
                                                 Id = s.Id,
                                                 Name = s.Id + "-" + s.Name
                                             }), "Id", "Name");
            return View();
        }

        public IActionResult Create(List<PatientServices> patientServices)
        {
            ViewBag.patient = new SelectList((from p in _db.Patients.ToList() select new {
                                                        Id=p.Id,
                                                        Name = p.Id +"-"+p.Title+" "+p.FirstName+" "+p.LastName
                                                           }), "Id","Name");
            ViewBag.Doctor = new SelectList((from emp in _db.Employee.Where(em => em.DesignationId == 1).ToList()
                                               select new
                                               {
                                                   Id = emp.Id,
                                                   Name = emp.Id + "-" + emp.Name
                                               }), "Id", "Name");
            ViewBag.service = new SelectList((from s in _db.Services.ToList()
                                              select new
                                              {
                                                  Id = s.Id,
                                                  Name = s.Id + "-" + s.Name
                                              }), "Id", "Name");
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid Model");
                var message = string.Join(" | ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
                return View("Index", message);
            }
            if (patientServices.Count > 0)
            {
                _db.AddRange(patientServices);
                if (_db.SaveChanges() > 0)
                {
                    return RedirectToAction("Index");
                }
            }
                return View();

        }
    }
}