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
    public class UsersController : Controller
    {
        private ARMPROJECT1Entities db = new ARMPROJECT1Entities();

        // GET: Users
        public ActionResult Index(string searching)
        {
            return View(db.Users.Where(x=>x.User_Name.Contains(searching) || searching == null).ToList());
        }


        public ActionResult VDO(string searching)
        {
            return View(db.Users.Where(x => x.User_Name.Contains(searching) || searching == null).ToList());
        }

        public ActionResult Index123()
        {

            return View();
        }
        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            var userde = db.Users.Find(id);
            return View(userde);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create( User user, HttpPostedFileBase UpFile)
        {
          
            if (UpFile != null)
            {
                byte[] Temp = new byte[UpFile.ContentLength];
                UpFile.InputStream.Read(Temp, 0, UpFile.ContentLength);
                user.User_Image= Temp; // เนื้อภาพ
            }
            
            if (ModelState.IsValid)
                {

                
                var Usere1 = db.Users.Where(a => a.User_Name == user.User_Name).FirstOrDefault();
             
                if (Usere1 != null)
                {
                    TempData["User1Error"] = "Error";
                }
                else
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("LoginUser", "Home");

                }

            }

          

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User member = db.Users.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
           
            return View(member);

            
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_Id,User_Name,User_Pass,User_Image")] User user, HttpPostedFileBase UpFile)
        {

            if (UpFile != null)
            {
                byte[] Temp = new byte[UpFile.ContentLength];
                UpFile.InputStream.Read(Temp, 0, UpFile.ContentLength);
                user.User_Image = Temp; // เนื้อภาพ
            }
            //
           
            if (ModelState.IsValid)
            {
                var Usere = db.Users.Where(a => a.User_Name == user.User_Name).FirstOrDefault();
                if (Usere != null)
                {
                    TempData["UserErrorr"] = "Error";
                }
                else
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index123", "Users");
                }
              
            }
         
            return View(user);
           
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            
            
                var result = db.Users.Find(id);
                db.Users.Remove(result);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            
        }

        // POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    User user = db.Users.Find(id);
        //    db.Users.Remove(user);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ReportsUser(string ReportType)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/Report2.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "UserDataSet";
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
    }
}
