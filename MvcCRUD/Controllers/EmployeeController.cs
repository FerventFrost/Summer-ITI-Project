using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MvcCRUD.Models;

namespace MvcCRUD.Controllers
{
    ///Parent For All Action Results
    /// => ActionResult
    public class EmployeeController : Controller
    {
        //EF Runtime
        CompanyContext db = new CompanyContext();
        //

        [HttpGet]
        public ActionResult Index()
        {
            //Connect to Db and retrieve employees
            var emps = db.Employees.ToList();
            //return view and pass employees
            return View(emps);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            //Add Emp To DB
            db.Employees.Add(emp);
            //Save Changes
            db.SaveChanges();
            //Redirect to Action Index
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var emp = db.Employees.FirstOrDefault(ww => ww.Id == id);
            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(Employee newEmp)
        {
            //get employee from DB with same PK
            var oldEmp = db.Employees.FirstOrDefault(ww => ww.Id == newEmp.Id);
            oldEmp.Name = newEmp.Name;
            oldEmp.Age = newEmp.Age;
            oldEmp.Salary = newEmp.Salary;
            oldEmp.DeptId = newEmp.DeptId;

            //savechanges
            db.SaveChanges();

            //Redirect
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Details(int id) 
        {
            var emp = db.Employees.Find(id);
            ViewData["e1"] = emp;
            return View();
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var emp = db.Employees.Find(id);
            db.Employees.Remove(emp);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}