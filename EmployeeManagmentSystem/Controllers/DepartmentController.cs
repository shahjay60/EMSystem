using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;

namespace EmployeeManagmentSystem.Controllers
{
    public class DepartmentController : Controller
    {
        //
        // GET: /Department/
        public EMS db = new EMS();


        public ActionResult Index()
        {
            var efData = db.tblDepartments.Where(x => x.IsActive == true).ToList();

            return View(efData);
        }

        public ActionResult Save(string deptName, int deptId)
        {
            var efData = db.tblDepartments.Where(x => x.Id == deptId).FirstOrDefault();
            var data = db.tblDepartments.Where(x => x.Name.ToLower() == deptName.ToLower() && x.IsActive==true).FirstOrDefault();

            if (data == null)
            {
                if (efData == null)
                {
                    efData = new tblDepartment();
                    db.tblDepartments.Add(efData);
                }
                efData.Id = deptId;
                efData.Name = deptName;
                efData.IsActive = true;
                db.SaveChanges();
                return Json("Ok");
            }
            else
            {

                return Json("Error");
            }

        }

        public ActionResult Delete(int deptId)
        {
            var efData = db.tblDepartments.Where(x => x.Id == deptId).FirstOrDefault();
            var efEmpData = db.tblEmployees.Where(x => x.DeptId == deptId &&x.IsActive==true).FirstOrDefault();

            if (efEmpData == null)
            {
                efData.IsActive = false;
                db.SaveChanges();
                return Json("Ok");

            }
            else
            {
                return Json("Error");
            }
        }

        public ActionResult GetDataById(int deptId)
        {
            tblDepartment mDepartment = new tblDepartment();
            var efData = db.tblDepartments.Where(x => x.Id == deptId).FirstOrDefault();
            if (efData != null)
            {
                mDepartment.Name = efData.Name;
                mDepartment.Id = efData.Id;
            }
            return Json(mDepartment);
        }


    }
}
