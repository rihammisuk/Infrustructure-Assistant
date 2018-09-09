using InfrustructureMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InfrustructureMVC.Controllers
{
    public class ComplainController : Controller
    {
        InfraDbEntities db = new InfraDbEntities();
        // GET: Complain
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAll()
        {
            return View(GetAllComplain());
        }



       
      




        IEnumerable<Complain> GetAllComplain()
        {
            using (InfraDbEntities db = new InfraDbEntities())
            {
                return db.Complains.Include(c => c.Classroom).Include(c => c.Infrustructure).ToList<Complain>();
            }

        }

        public ActionResult AddOrEdit(int id = 0)
        {
            Complain emp = new Complain();

            Infrustructure infra = new Infrustructure();
            Classroom croom = new Classroom();
            ViewBag.infraId = infra.IId;
            ViewBag.infraType = infra.ITypes;

            if (id != 0)
            {
                using (InfraDbEntities db = new InfraDbEntities())
                {
                    emp = db.Complains.Where(x => x.Id == id).FirstOrDefault<Complain>();
                   
                }
            }


            ViewBag.RoomId = new SelectList(db.Classrooms, "CId", "CRoomNo", emp.RoomId);
            ViewBag.InfrustructureId = new SelectList(db.Infrustructures, "IId", "ITypes", emp.InfrustructureId);

            return View(emp);
        }

        [HttpPost]
        public ActionResult AddOrEdit(Complain emp)
        {

            
            try
            {
                
                using (InfraDbEntities db = new InfraDbEntities())
                {
                    if (emp.Id == 0)
                    {
                        db.Complains.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();

                    }

                    ViewBag.RoomId = new SelectList(db.Classrooms, "CId", "CRoomNo", emp.RoomId);
                    ViewBag.InfrustructureId = new SelectList(db.Infrustructures, "IId", "ITypes", emp.InfrustructureId);

                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllComplain()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
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
                    Complain emp = db.Complains.Where(x => x.Id == id).FirstOrDefault<Complain>();
                    db.Complains.Remove(emp);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllComplain()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        
        }




        //For Student
        // GET: Com/Create
        public ActionResult Create()
        {
            ViewBag.RoomId = new SelectList(db.Classrooms, "CId", "CRoomNo");
            ViewBag.InfrustructureId = new SelectList(db.Infrustructures, "IId", "ITypes");
            return View();
        }

        // POST: Com/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,InfrustructureId,Description,StudentId,RoomId,ComplainDate,ComplainStatus")] Complain complain)
        {
            if (ModelState.IsValid)
            {
                db.Complains.Add(complain);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.RoomId = new SelectList(db.Classrooms, "CId", "CRoomNo", complain.RoomId);
            ViewBag.InfrustructureId = new SelectList(db.Infrustructures, "IId", "ITypes", complain.InfrustructureId);
            return View(complain);
        }
    }
}