using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SnippetsHub.Models;
using System.Data;

namespace SnippetsHub.Controllers
{
    public class AdvertisingController : Controller
    {
        
        static DataSet ds;
        static DataTable dt;

        // GET: Advertising
        public ActionResult Index()
        {
            if (System.IO.File.Exists(Server.MapPath(@"~/advertising.xml")))
            {

                ds = new DataSet();
                ds.ReadXml(Server.MapPath(@"~/advertising.xml"));
                dt = ds.Tables[0];

            }
            else
            {
                ds = new DataSet("AdvertisingPitches");
                dt = new DataTable("AvertisingPitch");
                dt.Columns.Add("CompanyName");
                dt.Columns.Add("CompanyContactPerson");
                dt.Columns.Add("ContactPersonEmail");
                dt.Columns.Add("AdvertisingIdea");
                ds.Tables.Add(dt);

            }

            return View();
        }

        [HttpPost]
        public ActionResult Advertising(AdvertisingModel advertising)
        {

            if (ModelState.IsValid)
            {
                DataRow row = dt.NewRow();
                row["CompanyName"] = advertising.CompanyName;
                row["CompanyContactPerson"] = advertising.CompanyContactPerson;
                row["ContactPersonEmail"] = advertising.ContactPersonEmail;
                row["AdvertisingIdea"] = advertising.AdvertisingIdea;
                dt.Rows.Add(row);

                ds.WriteXml(Server.MapPath(@"~/advertising.xml"));

                ViewData["message"] = "Thanks for your advertising Pitch. We will review it and contact you shortly";
                return View("ThankYou");
            }
            else return View("Index", advertising);
        }

       
    }
}