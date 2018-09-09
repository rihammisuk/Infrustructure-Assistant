using InfrustructureMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InfrustructureMVC.Controllers
{
    public class InfrustructureController : Controller
    {
        // GET: Infrustructure
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAll()
        {
            return View(GetAllInfrustructure());
        }

        IEnumerable<Infrustructure> GetAllInfrustructure()
        {
            using (InfraDbEntities db = new InfraDbEntities())
            {
                return db.Infrustructures.ToList<Infrustructure>();
            }

        }

        public ActionResult AddOrEdit(int id = 0)
        {
            Infrustructure emp = new Infrustructure();
            if (id != 0)
            {
                using (InfraDbEntities db = new InfraDbEntities())
                {
                    emp = db.Infrustructures.Where(x => x.IId == id).FirstOrDefault<Infrustructure>();
                }
            }
            return View(emp);
        }

        [HttpPost]
        public ActionResult AddOrEdit(Infrustructure inf)
        {
            try
            {
                
                using (InfraDbEntities db = new InfraDbEntities())
                {
                    if (inf.IId == 0)
                    {
                        db.Infrustructures.Add(inf);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(inf).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllInfrustructure()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
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
                    Infrustructure inf = db.Infrustructures.Where(x => x.IId == id).FirstOrDefault<Infrustructure>();
                    db.Infrustructures.Remove(inf);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllInfrustructure()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}