using HotGuys.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HotGuys.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }
        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotChoiceViewModels hotChoiceViewModels = db.HotChoiceViewModels.Include(h => h.User).FirstOrDefault(h => h.Id == id);
            if (hotChoiceViewModels == null)
            {
                return HttpNotFound();
            }
            var comments = db.Comments.Where(c => c.HotChoiceId == hotChoiceViewModels.Id).Include(c => c.User).ToList();
            foreach (var comment in comments)
            {
                comment.User = db.ApplicationUsers.Where(u => u.Id == comment.UserId).First();
            }
            hotChoiceViewModels.Comments = comments;

            // ViewData["Comments"] =  db.Comments.Where(c => c.HotChoiceId == hotChoiceViewModels.Id).ToList();
            return View(hotChoiceViewModels);
        }
        public ActionResult Store()
        {
            var hotchoices = db.HotChoiceViewModels.Include(h => h.User).ToList();
            
            foreach(var hotchoice in hotchoices)
            {
                hotchoice.Comments = db.Comments.Where(c => c.HotChoiceId == hotchoice.Id).Include(c=>c.User).ToList();
                foreach(var comment in hotchoice.Comments)
                {
                    comment.User = db.ApplicationUsers.Where(u => u.Id == comment.UserId).First();
                }
 
            }
            return View(hotchoices);
            
        }

        // GET: Home/AddComment/5
        [HttpGet]
        [Authorize]
        public ActionResult AddComment(int? id)
        {
            ViewBag.HotchoiceId = id;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddComment([Bind(Include = "Id,Rating,Comment,HotChoiceId,UserId")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                comments.UserId = User.Identity.GetUserId();
                db.Comments.Add(comments);
                db.SaveChanges();
                return RedirectToAction("Details/"+ comments.HotChoiceId);
            }

            return View(comments);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Hot guys, find your favorite hot pot.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Please contact us at support@hotguys.com";

            return View();
        }
    }
}