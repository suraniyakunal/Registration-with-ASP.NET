using RegistrationForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegistrationForm.Controllers
{
    public class LoginController : Controller
    {
        Database1Entities db = new Database1Entities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(user u)
        {
            var user = db.users.Where(model => model.username == u.username && model.password == u.password).FirstOrDefault();

            if(user != null)
            {
                Session["UserId"] = u.Id.ToString();
                Session["Username"] = u.username.ToString();
                TempData["LoginSuccessMessage"] = "<script>alert('Login Successful...')</script>";
                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewBag.ErrorMessage = "<script>alert('Username or Password is Incorrect...')</script>";
                return View();
            }
            
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(user u)
        {
            if (ModelState.IsValid == true)
            {
                db.users.Add(u);
                int a = db.SaveChanges();

                if (a > 0)
                {
                    ViewBag.InserMessage = "<script>alert('Your are Registered...')</script>";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.InserMessage = "<script>alert('Your are not Registered !! TRY AGAIN')</script>";

                }
            }
            return View();
        }
    }
}