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
    public class ContentsController : Controller
    {
        private ARMPROJECT1Entities db = new ARMPROJECT1Entities();

        // GET: Contents
        public ActionResult Index()
        {
            return View(db.Contents.ToList());
        }

        ////////////////////////////////
        public ActionResult IndexUser()
        {
            return View(db.Contents.ToList());
        }
        ////////////////////////////////

        // GET: Contents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.Contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        // GET: Contents/Create
        public ActionResult Create()
        {


            ViewBag.Content_CategoryID = new SelectList(db.Categories, "Category_Id", "Category_Data");
            return View();
        }

        // POST: Contents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Content_Id,Content_CategoryID,Content_Name,Content_Image,Content_Data")] Content content, HttpPostedFileBase UpFile)
        {

            if (UpFile != null)
            {
                byte[] Temp = new byte[UpFile.ContentLength];
                UpFile.InputStream.Read(Temp, 0, UpFile.ContentLength);
                content.Content_Image = Temp; // เนื้อภาพ
            }
            if (ModelState.IsValid)
            {
                db.Contents.Add(content);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Content_CategoryID = new SelectList(db.Categories, "Category_Id", "Category_Data", content.Content_CategoryID);
            return View(content);
        }

        // GET: Contents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.Contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            ViewBag.Content_CategoryID = new SelectList(db.Categories, "Category_Id", "Category_Data", content.Content_CategoryID);
            return View(content);
        }

        // POST: Contents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Content_Id,Content_CategoryID,Content_Name,Content_Image,Content_Data")] Content content, HttpPostedFileBase UpFile)
        {

            if (UpFile != null)
            {
                byte[] Temp = new byte[UpFile.ContentLength];
                UpFile.InputStream.Read(Temp, 0, UpFile.ContentLength);
                content.Content_Image = Temp; // เนื้อภาพ
            }
            if (ModelState.IsValid)
            {
                db.Entry(content).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Content_CategoryID = new SelectList(db.Categories, "Category_Id", "Category_Data", content.Content_CategoryID);
            return View(content);
        }




        public ActionResult Delete(int? id)
        {
            var result = db.Contents.Find(id);
            db.Contents.Remove(result);
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
