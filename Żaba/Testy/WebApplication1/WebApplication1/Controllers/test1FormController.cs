using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class test1FormController : Controller
    {
        // GET: test1Form
        public ActionResult Index()
        {
            
            return View();
        }

        

        // GET: test1Form/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: test1Form/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: test1Form/Submit ?
        [HttpPost]
        public ActionResult Submit(WebApplication1.Models.Case objCase)
        {
            try
            {
                // TODO: Add insert logic here
                if (objCase.Name != null && objCase.Year != null && objCase.Age!= null 
                    && objCase.Type != null && objCase.Country!= null && objCase.Area!=null
                    && objCase.Activity != null && objCase.Fatal != null && objCase.Sex != null)
                {
                    HomeController.CaseList.Add
                        (new Models.Case
                        {
                            Year = objCase.Year,
                            Type = objCase.Type,
                            Country = objCase.Country,
                            Area = objCase.Area,
                            Name = objCase.Name,
                            Sex = objCase.Sex,
                            Age = objCase.Age,
                            Fatal = objCase.Fatal,
                            Activity = objCase.Activity

                        }
                        );
                }




            ViewBag.Message = "Dodano event";
                return RedirectToAction("Form", "HomeController");
                //return RedirectToAction("Index");

            }
           catch
            {
                ViewBag.Message = "Error, źle podane dane";
                return RedirectToAction("Form", "HomeController");
                //return RedirectToAction("Error");

            }
        }

        // POST: test1Form/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: test1Form/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: test1Form/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: test1Form/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: test1Form/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
