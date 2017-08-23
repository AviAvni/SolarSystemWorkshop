using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolarSystemWorkshop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(string data)
        {
            var model = SolarSystem.Planets;
            switch (data)
            {
                case "Moons":
                    model = SolarSystem.Moons;
                    break;
                case "Planets":
                    model = SolarSystem.Planets;
                    break;
                case "Regions":
                    model = SolarSystem.Regions;
                    break;
                case "SmallBodies":
                    model = SolarSystem.SmallBodies;
                    break;
                case "Stars":
                    model = SolarSystem.Stars;
                    break;
                default:
                    break;
            }
            return View(model);
        }

        public ActionResult Gallery(string data)
        {
            var model = SolarSystem.Galleries;

            if (!string.IsNullOrEmpty(data))
            {
                model = SolarSystem.getImages(data);
            }

            return View(model);
        }
    }
}