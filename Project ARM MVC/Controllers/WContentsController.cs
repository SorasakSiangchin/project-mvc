using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_ARM_MVC.Models;

namespace Project_ARM_MVC.Controllers
{
    public class WContentsController : Controller
    {
        private ARMPROJECT1Entities db = new ARMPROJECT1Entities();

        // GET: WContents
        public ActionResult Index()
        {
            var wContents = db.WContents.Include(w => w.Content);
            return View(wContents.ToList());
        }



        public ActionResult IndexUser()
        {
            
            return View(db.WContents.ToList());
        }
        // GET: WContents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WContent wContent = db.WContents.Find(id);
            if (wContent == null)
            {
                return HttpNotFound();
            }
            return View(wContent);
        }

        // GET: WContents/Create
        public ActionResult Create()
        {
            ViewBag.WContent_ContentID = new SelectList(db.WContents, "Content_Id", "Content_Name");
            return View();
        }

        // POST: WContents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WContent_Id,WContent_ContentID,WContent_Name,WContent_Image,WContent_Data")] WContent wContent, HttpPostedFileBase UpFile)
        {
            if (UpFile != null)
            {
                byte[] Temp = new byte[UpFile.ContentLength];
                UpFile.InputStream.Read(Temp, 0, UpFile.ContentLength);
                wContent.WContent_Image = Temp; // เนื้อภาพ
            }
            if (ModelState.IsValid)
            {
                db.WContents.Add(wContent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WContent_ContentID = new SelectList(db.Contents, "Content_Id", "Content_Name", wContent.WContent_ContentID);
            return View(wContent);
        }

        // GET: WContents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WContent wContent = db.WContents.Find(id);
            if (wContent == null)
            {
                return HttpNotFound();
            }
            ViewBag.WContent_ContentID = new SelectList(db.Contents, "Content_Id", "Content_Name", wContent.WContent_ContentID);
            return View(wContent);
        }

        // POST: WContents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WContent_Id,WContent_ContentID,WContent_Name,WContent_Image,WContent_Data")] WContent wContent, HttpPostedFileBase UpFile)
        {
            if (UpFile != null)
            {
                byte[] Temp = new byte[UpFile.ContentLength];
                UpFile.InputStream.Read(Temp, 0, UpFile.ContentLength);
                wContent.WContent_Image = Temp; // เนื้อภาพ
            }
            if (ModelState.IsValid)
            {
                db.Entry(wContent).State = EntityState.Modified; 
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WContent_ContentID = new SelectList(db.Contents, "Content_Id", "Content_Name", wContent.WContent_ContentID);
            return View(wContent);
        }

        // GET: WContents/Delete/5
        public ActionResult Delete(int id)
        {
            var result = db.WContents.Find(id);
            db.WContents.Remove(result);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
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
