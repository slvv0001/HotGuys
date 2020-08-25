using HotGuys.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotGuys.Utils;

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

        // @author Lu Chen & Shuang Lv
        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotChoiceViewModels hotChoiceViewModels = db.HotChoiceViewModels.Include(h => h.User).FirstOrDefault(h => h.Id == id);
            if (Session["count"] == null)
            {
                Session["count"] = "0";
            }
            int count = Session["count"].ToString().Length;
            Session["temp"] = "No."+count + "Name:" + hotChoiceViewModels.Name + "   Price:" + hotChoiceViewModels.Price.ToString();
            Session["front"] = "Details";
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
        //@author Shuang Lv
        // GET: Home/Cart
        public ActionResult Cart()
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = "";
            }
            if (Session["temp"] == null)
            {
                Session["temp"] = "";
            }
            if (Session["count"] == null)
            {
                Session["count"] = "0";
            }
            if (Session["front"].ToString() == "Details")
            {
                Session["cart"] += Session["temp"].ToString();
                Session["count"] += "0";
            }
            ViewBag.Message = "Your shopping cart list:" +Session["cart"].ToString();

            return View();
        }

        // @author Lu Chen 
        // GET: /Home/Store
        public ActionResult Store()
        {
            var hotchoices = db.HotChoiceViewModels.Include(h => h.User).ToList();
            // retrieve comments for each hotchoice
            foreach(var hotchoice in hotchoices)
            {
                hotchoice.Comments = db.Comments.Where(c => c.HotChoiceId == hotchoice.Id).Include(c=>c.User).ToList();
                foreach(var comment in hotchoice.Comments)
                {
                    // retrieve user for a specific comment
                    comment.User = db.ApplicationUsers.Where(u => u.Id == comment.UserId).First();
                }
 
            }
            Session["front"] = "Store";
            return View(hotchoices);
            
        }
        // @author Lu Chen
        // GET: Home/AddComment/5
        [HttpGet]
        [Authorize]
        public ActionResult AddComment(int? id)
        {
            // HotChoiceId should be passed to the function
            ViewBag.HotchoiceId = id;
            return View();
        }

        // @author Lu Chen
        [HttpPost]
        [Authorize]
        public ActionResult AddComment([Bind(Include = "Id,Rating,Comment,HotChoiceId,UserId")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                // get comment userid
                comments.UserId = User.Identity.GetUserId();
                // save comment
                db.Comments.Add(comments);
                db.SaveChanges();
                // back to detail page
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
        //@author Shuang Lv
        // GET: Home/Send_Email
        public ActionResult Send_Email()
        {
            return View(new SendEmailViewModel());
        }
        [HttpPost]
        //@author Shuang Lv
        public ActionResult Send_Email(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    EmailSender es = new EmailSender();
                    es.Send(toEmail, subject, contents);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }
    }
}