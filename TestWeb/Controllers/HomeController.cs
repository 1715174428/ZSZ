using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestIService;
using TestService;

namespace TestWeb.Controllers
{
    public class HomeController : Controller
    {
         public IUserService UserService { get; set; }
        public INewsService NewsService { get; set; }
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Login()
        {
            
            return Content(UserService.CheckLogin());
        }
        public ActionResult AddNews()
        {

            return Content(NewsService.AddNews("特朗普", "死了"));
        }
        public ActionResult NewTonJson()
        {

            return  new JsonNetResult() { Data = new { Name = "zhangsan", Age = 12, Time = DateTime.Now } };
        }
        public ActionResult Equals()
        {
            UserService a = new UserService();
            var b = new UserService();
            var c = a;

            return Content($"a==b{a.Equals(b)}a==c{a.Equals(c)}b==c{b.Equals(c)},a===b{string.ReferenceEquals(a, b)},a===c{string.ReferenceEquals(a, c)},b===c{string.ReferenceEquals(b, c)}");
          
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}