using CocoApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace CocoApi.Controllers
{
    [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
    public class HomeController : Controller
    {

        private CocoDBEntities db = new CocoDBEntities();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

       
        public string checkuser(string user, string pass)
        {
           
             bool u = db.Drivers.Any(o => o.user == user);
            bool p = db.Drivers.Any(o => o.pass == pass);
           
            if (u == true && p == true)
            {
                var dr = db.Drivers.Where(o => o.fname == user).ToList();
                List<Driver> adr = new List<Driver>(dr);
                var cd = adr[0].code;
                var dp = db.Depots.Where(x => x.code == cd).ToList();
                List<Depot> adp = new List<Depot>(dp);

                return JsonConvert.SerializeObject(adp);
            }
            else
            {
                return "no user";
            }

            return "ok";
        }
    }
}
