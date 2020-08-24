using HotGuys.Models;
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
        
        private HotGuysModels db = new HotGuysModels();
        

        public ActionResult Index()
        {
            return View();
            
        }
        // GET: Home/Details/2
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotChoiceViewModels hotChoiceViewModels = db.HotChoiceViewModels.Find(id);
            if(Session["count"] == null)
            {
                Session["count"] = "0";
            }
            int count = Session["count"].ToString().Length;
            Session["temp"] =count+"Name:"+hotChoiceViewModels.Name+ "   Price:" + hotChoiceViewModels.Price.ToString();
            Session["front"] = "Details";
            if (hotChoiceViewModels == null)
            {
                return HttpNotFound();
            }
            return View(hotChoiceViewModels);
        }
        public ActionResult Store()
        {
            Session["front"] = "Store";
            return View(db.HotChoiceViewModels.Include(h=> h.User).ToList());
        }
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
            if(Session["front"].ToString() == "Details")
            {
                Session["cart"] += Session["temp"].ToString();
                Session["count"] += "0";
            }
            ViewBag.Message = "Your shopping cart list:" + Session["cart"].ToString();

            return View();
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
        public ActionResult Send_Email()
        {
            return View(new SendEmailViewModel());
        }
        [HttpPost]
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