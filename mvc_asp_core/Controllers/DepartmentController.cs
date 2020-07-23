using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mvc_asp_core.Models;
using mvc_asp_core.Models.UserIdentityModel;

namespace mvc_asp_core.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly HospitalContext _db;
        private UserManager<IdentityModel> userManager;
        public DepartmentController(HospitalContext _context, UserManager<IdentityModel> user)
        {
            _db = _context;
            userManager = user;
        }
        public IActionResult Index()
        {
           List<Department> departmentList = _db.Department.ToList();
            return View(departmentList);
        }
        //[Authorize(Roles ="Admin")]
        //Create
        [HttpGet]
        public IActionResult Create()
        {           
            return View();
        }
        
        [HttpPost]
        //[Authorize(Roles ="Admin")]
        public IActionResult Create(Department d)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Department.Add(d);
                    if (_db.SaveChanges() > 0)
                    {
                        TempData["Result"] = "Record Saved Successfully. & Id is "+d.Id ;
                    }
                    else
                    {
                        TempData["Result"] = "Record Creation Failed";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Result"] = ex.Message.ToString();
                }              
            }
            else if (!ModelState.IsValid)
            {                                
                return View();
            }
            return RedirectToAction("Index");
        }
        //[Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Department department = _db.Department.Find(id);
            return View(department);
        }
        //[Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult Edit(Department d)
        {
            var department = _db.Department.Find(d.Id);
            if (department != null)
            {
                department.Id = d.Id;
                department.Name = d.Name;
                department.Details = d.Details;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Entry(department).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    if (_db.SaveChanges() > 0)
                    {
                        TempData["Result"] = "Record Update Successfully.";
                    }
                    else
                    {
                        TempData["Result"] = "Record Update Failed";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Result"] = ex.Message.ToString();
                }
            }
            else if (!ModelState.IsValid)
            {
                return View();
            }
            return RedirectToAction("Index");
        }
        //[Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Department department = _db.Department.Find(id);
            return View(department);
        }
        //[Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult Delete(Department d)
        {
            var department = _db.Department.Find(d.Id);
            _db.Department.Remove(department);
            if (_db.SaveChanges()>0)
            {
                TempData["Result"] = "Record Deleted Successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}