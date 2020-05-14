using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyService.Models;

namespace EasyService.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult DashBoard()
        {
            return View();
        }

        public ActionResult ListCategories()
        {
            EasyServiceEntities easyServiceEntities = new EasyServiceEntities();

            List<Category> category = easyServiceEntities.Categories.OrderBy(a => a.Name).ToList();
            return View(category);
            
        }
        public ActionResult ListSubCategories(int Id)
        {
            EasyServiceEntities easyServiceEntities = new EasyServiceEntities();

            List<SubCategory> subCategory = easyServiceEntities.SubCategories.Where(a =>a.CategoryId == Id).OrderBy(a => a.Name).ToList();
            return View(subCategory);
        }

        public ActionResult VendorListings(int Id)
        {
            EasyServiceEntities easyServiceEntities = new EasyServiceEntities();
            List<VendorListing> vendorListings = easyServiceEntities.VendorListings.Where(a => a.SubCategoryId == Id).ToList();
            return View(vendorListings);
        }


    }
}