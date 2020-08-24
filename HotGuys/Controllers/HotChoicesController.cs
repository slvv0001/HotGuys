using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotGuys.Models;
using Microsoft.AspNet.Identity;

namespace HotGuys.Controllers
{
    [Authorize(Roles ="merchant")]
    public class HotChoicesController : Controller
    {
        private HotGuysModels db = new HotGuysModels();
        // GET: HotChoices
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            return View(db.HotChoiceViewModels.Where(h => h.UserId ==
            userId).Include(h => h.User).ToList());
        }

        // GET: HotChoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotChoiceViewModels hotChoiceViewModels = db.HotChoiceViewModels.Find(id);
            if (hotChoiceViewModels == null)
            {
                return HttpNotFound();
            }
            return View(hotChoiceViewModels);
        }

        // GET: HotChoices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HotChoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,UserId,Image, ImageFile")] HotChoiceViewModels hotChoiceViewModels)
        {
            if (ModelState.IsValid)
            {
                hotChoiceViewModels.UserId = User.Identity.GetUserId();
                string image = Path.GetFileNameWithoutExtension(hotChoiceViewModels.ImageFile.FileName);
                string extension = Path.GetExtension(hotChoiceViewModels.ImageFile.FileName);
                image = image + DateTime.Now.ToString("yy-mm-ss-fff") + extension;
                hotChoiceViewModels.Image = "~/Resources/" + image;
                image = Path.Combine(Server.MapPath("~/Resources/"), image);
                hotChoiceViewModels.ImageFile.SaveAs(image);

                db.HotChoiceViewModels.Add(hotChoiceViewModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hotChoiceViewModels);
        }

        // GET: HotChoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotChoiceViewModels hotChoiceViewModels = db.HotChoiceViewModels.Find(id);
            if (hotChoiceViewModels == null)
            {
                return HttpNotFound();
            }
            return View(hotChoiceViewModels);
        }

        // POST: HotChoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,UserId,Image")] HotChoiceViewModels hotChoiceViewModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotChoiceViewModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotChoiceViewModels);
        }

        // GET: HotChoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotChoiceViewModels hotChoiceViewModels = db.HotChoiceViewModels.Find(id);
            if (hotChoiceViewModels == null)
            {
                return HttpNotFound();
            }
            return View(hotChoiceViewModels);
        }

        // POST: HotChoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HotChoiceViewModels hotChoiceViewModels = db.HotChoiceViewModels.Find(id);
            db.HotChoiceViewModels.Remove(hotChoiceViewModels);
            db.SaveChanges();
            return RedirectToAction("Index");
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
