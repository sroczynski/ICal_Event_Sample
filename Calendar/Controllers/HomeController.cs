using Calendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Calendar.Data;

namespace Calendar.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Calendar(CalendarModel model)
        {
            Response.Clear();

            var response = Utils.Calendar.GenerateICalendar(model);
            string contentType = "";
            
            if (model.FileType.Equals(FileTypeEnum.ics))
            {
                Response.AddHeader("Content-Disposition", "attachment; filename=calendar.ics");
                Response.ContentType = "text/ics";

                contentType = "application/ics";
            }
            else
            {

                Response.AddHeader("Content-Disposition", "attachment; filename=calendar.csv");
                Response.ContentType = "text/csv";

                contentType = "application/csv";
            }


            return new FileStreamResult((System.IO.Stream)response, contentType);
        }

    }
}