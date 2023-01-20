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
    public class CategoriesController : Controller
    {
        private ARMPROJECT1ConnectionString db = new ARMPROJECT1ConnectionString();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Category.ToList());
        }

        public ActionResult IndexUser()
        {
            return View(db.Category.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Create( Category category , HttpPostedFileBase UpFile)
        {
            if (UpFile != null)
            {
                byte[] Temp = new byte[UpFile.ContentLength];
                UpFile.InputStream.Read(Temp, 0, UpFile.ContentLength);
                category.Category_Image = Temp; // เนื้อภาพ
            }
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Category_Id,Category_Name,Category_Image,Category_Data")] Category category, HttpPostedFileBase UpFile)
        {

            if (UpFile != null)
            {
                byte[] Temp = new byte[UpFile.ContentLength];
                UpFile.InputStream.Read(Temp, 0, UpFile.ContentLength);
                category.Category_Image = Temp; // เนื้อภาพ
            }
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int id)
        {

            var result = db.Categories.Find(id);
            db.Categories.Remove(result);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // POST: Categories/Delete/5
       

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
