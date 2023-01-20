using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Project_ARM_MVC.Models;

namespace Project_ARM_MVC.Controllers
{
    public class StudiesController : Controller
    {
        private ARMPROJECT1Entities db = new ARMPROJECT1Entities();

        // GET: Studies
        public ActionResult Index(string searching)
        {
            return View(db.Studies.Where(x => x.User.User_Name.Contains(searching) || searching == null).ToList());
        }

        public ActionResult IndexStudy()
        {
            var study = db.Studies.Include(s => s.User).Include(s => s.WContent);
            return View(study.ToList());
        }

        public ActionResult Index123()
        {
            var study = db.Studies.Include(s => s.User).Include(s => s.WContent);
            return View(study.ToList());
        }
        // GET: Studies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Study study = db.Studies.Find(id);
            if (study == null)
            {
                return HttpNotFound();
            }
            return View(study);
        }

        // GET: Studies/Create
        public ActionResult Create()
        {
            ViewBag.Study_UserId = new SelectList(db.Users, "User_Id", "User_Name");
            ViewBag.Study_WcontentID = new SelectList(db.WContents, "WContent_Id", "WContent_Name");
            return View();
        }

        // POST: Studies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        //[Bind(Include = "Study_Id,Study_Name,Study_UserId,Study_WcontentID")]
        [HttpPost]
        public ActionResult Create( Study study)
        {
            if (ModelState.IsValid)
            {
                db.Studies.Add(study);
                db.SaveChanges();
                return RedirectToAction("Index123", "Studies");
            }

            ViewBag.Study_UserId = new SelectList(db.Users, "User_Id", "User_Name", study.Study_UserId);
            ViewBag.Study_WcontentID = new SelectList(db.WContents, "WContent_Id", "WContent_Name", study.Study_WcontentID);
            return View(study);
        }

        // GET: Studies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Study study = db.Studies.Find(id);
            if (study == null)
            {
                return HttpNotFound();
            }
            ViewBag.Study_UserId = new SelectList(db.Users, "User_Id", "User_Name", study.Study_UserId);
            ViewBag.Study_WcontentID = new SelectList(db.WContents, "WContent_Id", "WContent_Name", study.Study_WcontentID);
            return View(study);
        }

        // POST: Studies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Study_Id,Study_Name,Study_UserId,Study_WcontentID")] Study study)
        {
            if (ModelState.IsValid)
            {
                db.Entry(study).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Study_UserId = new SelectList(db.Users, "User_Id", "User_Name", study.Study_UserId);
            ViewBag.Study_WcontentID = new SelectList(db.WContents, "WContent_Id", "WContent_Name", study.Study_WcontentID);
            return View(study);
        }

        // GET: Studies/Delete/5
        public ActionResult Delete(int? id)
        {
            var result = db.Studies.Find(id);
            db.Studies.Remove(result);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //// POST: Studies/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Study study = db.Study.Find(id);
        //    db.Study.Remove(study);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        public ActionResult ReportsStudy(string ReportType)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/Report1.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            reportDataSource.Value = db.Users.ToList();
            localReport.DataSources.Add(reportDataSource);
            string reportType = ReportType;
            string mimeType;
            string encoding = "Encoding.UTF8";
            string fileNameExtension;
            if (reportType == "Excel")
            {
                fileNameExtension = "xlsx";
            }
            else if (reportType == "Word")
            {
                fileNameExtension = "docx";
            }
            else if (reportType == "PDF")
            {
                fileNameExtension = "pdf";
            }
            else
            {
                fileNameExtension = "jpg";
            }

            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment;filename= arm_reportUser." + fileNameExtension);

            return File(renderedByte, fileNameExtension);

        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
