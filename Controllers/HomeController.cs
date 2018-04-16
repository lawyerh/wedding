using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using raw_wedding.Models;

namespace raw_wedding.Controllers
{

    public class HomeController : Controller
    {
        private readonly DbConnector _dbConnector;

        public HomeController(DbConnector connect)
        {
            _dbConnector = connect;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("newUser")]
        public IActionResult newUser(Register user)
        {
            if(ModelState.IsValid)
            {
                List<Dictionary<string,object>> check = _dbConnector.Query($"SELECT * FROM users where(email = '{user.Email}');");
                if(check.Count > 0)
                {
                    ViewBag.error = "That email is already registered";
                    return View("Index", user);
                }

                _dbConnector.Execute($"insert into users(FirstName, LastName, Email, pass, created_at, updated_at) values ('{user.Fname}', '{user.Lname}', '{user.Email}', '{user.pass}', now(), now());");
                return RedirectToAction("Dashboard");
            }
            return View("Index", user);
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("loginUser")]
        public IActionResult loginUser(Login user)
        {
            if(ModelState.IsValid)
            {
                List<Dictionary<string,object>> check = _dbConnector.Query($"select * from users where(email = '{user.Email}');");
                if(check.Count > 0)
                {
                    if((string)check[0]["pass"] == user.Password)
                    {
                        HttpContext.Session.SetInt32("user", (int)check[0]["id"]);
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        ViewBag.error = "Password is incorrect.";
                        return View("Login");
                    }
                }
                ViewBag.noUser = "That email is not registered. Please create an account.";
                return View("Login");
            }
            return View("Login");
        }

        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            ViewBag.weddings = _dbConnector.Query("select * from wedding");
            List<Dictionary<string, object>> counts = _dbConnector.Query("select COUNT(*) as count from guests group by wedding_id;");
            for(int i = 0; i < ViewBag.weddings.Count; i++)
            {
                ViewBag.weddings[i]["count"] = counts[i]["count"];
            }
            return View();
        }

    }
}
