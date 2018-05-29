using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using Model;

namespace EmployeeManagmentSystem.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/

        public EMS db = new EMS();

        public ActionResult Index()
        {
            var efData = (from dept in db.tblDepartments
                          join emp in db.tblEmployees
                              on dept.Id equals emp.DeptId
                          where emp.IsActive == true
                          select new Employee
                          {
                              Id = emp.Id,
                              Name = emp.Name,
                              Surname = emp.Surname,
                              DepartmentName = dept.Name,
                              Address = emp.Address,
                              Qualification =emp.Qualification,
                              ContactNumber=emp.ContactNumber,
                          }).ToList();

            return View(efData);
        }

        public ActionResult Add(int id=0)
        {
            ViewBag.id = id;
            return View();

        }
        public ActionResult Save(Employee mEmployee)
        {
            var efData = db.tblEmployees.Where(x => x.Id == mEmployee.Id).FirstOrDefault();
            if (efData == null)
            {
                efData = new tblEmployee();
                db.tblEmployees.Add(efData);
            }
            efData.Id = mEmployee.Id;
            efData.Name = mEmployee.Name;
            efData.Surname = mEmployee.Surname;
            efData.DeptId = mEmployee.DeptId;
            efData.Address = mEmployee.Address;
            efData.Qualification = mEmployee.Qualification;
            efData.ContactNumber = mEmployee.ContactNumber;

            efData.IsActive = true;
            db.SaveChanges();
            return Json("Ok");
        }

        public ActionResult GetAllDepartment()
        {
            var efData = db.tblDepartments.Where(x => x.IsActive == true).ToList();
            return Json(efData);
        }

        public ActionResult Delete(int empId)
        {
            var efData = db.tblEmployees.Where(x => x.Id == empId).FirstOrDefault();
            if (efData != null)
            {
                efData.IsActive = false;
                db.SaveChanges();
            }
            return Json("Ok");
        }

        public ActionResult GetDataById(int empId)
        {
            tblEmployee mEmployee = new tblEmployee();
            var efData = db.tblEmployees.Where(x => x.Id == empId).FirstOrDefault();
            if (efData != null)
            {
                mEmployee.Name = efData.Name;
                mEmployee.Id = efData.Id;
                mEmployee.Surname = efData.Surname;
                mEmployee.DeptId = efData.DeptId;
                mEmployee.Address = efData.Address;
                mEmployee.Qualification = efData.Qualification;
                mEmployee.ContactNumber = efData.ContactNumber;

            }
            return Json(mEmployee);
        }

    }
}
