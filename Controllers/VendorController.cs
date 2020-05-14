using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyService.Models;

namespace EasyService.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        public ActionResult DashBoard()
        {
            return View();
        }
        public ActionResult SelectCategory()
        {
            EasyServiceEntities easyServiceEntities = new EasyServiceEntities();
            ViewBag.categoryList = easyServiceEntities.Categories.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Id.ToString(), Text = rr.Name }).ToList();
            return View();
        }

        public ActionResult AddListing(SelectCategory model)
        {
            EasyServiceEntities easyServiceEntities = new EasyServiceEntities();

            ViewBag.categoryList = easyServiceEntities.Categories.Where(r => r.Id == model.CategoryId).ToList().Select(rr => new SelectListItem { Value = rr.Id.ToString(), Text = rr.Name }).ToList();

            ViewBag.subCategoryList = easyServiceEntities.SubCategories.Where(r => r.CategoryId == model.CategoryId).ToList().Select(rr => new SelectListItem { Value = rr.Id.ToString(), Text = rr.Name }).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult AddListing(VendorListing model)
        {
            EasyServiceEntities easyServiceEntities = new EasyServiceEntities();

            var userId = easyServiceEntities.AspNetUsers.Where(a => a.UserName == User.Identity.Name).Select(x => x.Id).FirstOrDefault();

            model.UserId = userId;
            easyServiceEntities.VendorListings.Add(model);
            int c = easyServiceEntities.SaveChanges();

            return RedirectToAction("ViewListing");
        }


        public ActionResult ViewListing()
        {
            EasyServiceEntities easyServiceEntities = new EasyServiceEntities();
            List<VendorListing> vendorListings = easyServiceEntities.VendorListings.ToList();
            return View(vendorListings);
        }

        public ActionResult EditListing(int Id)
        {
            EasyServiceEntities easyServiceEntities = new EasyServiceEntities();
            VendorListing vendorListing = easyServiceEntities.VendorListings.Where(a => a.Id == Id).FirstOrDefault();           
            return View(vendorListing);
        }
        [HttpPost]
        public ActionResult EditListing(VendorListing model)
        {
            EasyServiceEntities easyServiceEntities = new EasyServiceEntities();
            easyServiceEntities.Entry(model).State = EntityState.Modified;
            easyServiceEntities.SaveChanges();
            return RedirectToAction("ViewListing");
        }
        public ActionResult DeleteListing(int Id)
        {
            EasyServiceEntities easyServiceEntities = new EasyServiceEntities();
            VendorListing vendorListing = easyServiceEntities.VendorListings.Where(a => a.Id == Id).FirstOrDefault();
            easyServiceEntities.VendorListings.Remove(vendorListing);
            easyServiceEntities.SaveChanges();
            return RedirectToAction("ViewListing");
        }
    }
}