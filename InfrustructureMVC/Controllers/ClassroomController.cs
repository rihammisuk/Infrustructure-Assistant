using InfrustructureMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InfrustructureMVC.Controllers
{
    public class ClassroomController : Controller
    {
        // GET: Classroom
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAll()
        {
            return View(GetAllClassroom());
        }

        IEnumerable<Classroom> GetAllClassroom()
        {
            using (InfraDbEntities db = new InfraDbEntities())
            {
                return db.Classrooms.ToList<Classroom>();
            }

        }

        public ActionResult AddOrEdit(int id = 0)
        {
            Classroom emp = new Classroom();
            if (id != 0)
            {
                using (InfraDbEntities db = new InfraDbEntities())
                {
                    emp = db.Classrooms.Where(x => x.CId == id).FirstOrDefault<Classroom>();
                }
            }
            return View(emp);
        }

        [HttpPost]
        public ActionResult AddOrEdit(Classroom croom)
        {
            try
            {
                
                using (InfraDbEntities db = new InfraDbEntities())
                {
                    if (croom.CId == 0)
                    {
                        db.Classrooms.Add(croom);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(croom).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllClassroom()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (InfraDbEntities db = new InfraDbEntities())
                {
                    Classroom emp = db.Classrooms.Where(x => x.CId == id).FirstOrDefault<Classroom>();
                    db.Classrooms.Remove(emp);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllClassroom()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}