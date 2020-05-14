using EasyService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyService.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult DashBoard()
        {
            return View();
        }

        public ActionResult ListCategory()
        {
            EasyServiceEntities easyServiceEntities = new EasyServiceEntities();
            List<Category> category = easyServiceEntities.Categories.ToList();
            return View(category);
        }

        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category model)
        {
            EasyServiceEntities easyServiceEntities = new EasyServiceEntities();
            easyServiceEntities.Categories.Add(model);
            easyServiceEntities.SaveChanges();
            return RedirectToAction("ListCategory");
        }

        public ActionResult EditCategory(int Id)
        {
            EasyServiceEntities easyServiceEntities = new EasyServiceEntities();
            Category category = easyServiceEntities.Categories.Where(x => x.Id == Id).FirstOrDefault();
            return View(category);
        }
        [HttpPost]
        public ActionResult EditCategory(Category model)
        {
            EasyServiceEntities easyServiceEntities = new EasyServiceEntities();
            easyServiceEntities.Entry(model).State = EntityState.Modified;
            easyServiceEntities.SaveChanges();
            return RedirectToAction("ListCategory");
        }
    
        public ActionResult DeleteCategory(int id)
        {
            EasyServiceEntities easyServiceEntities = new EasyServiceEntities();

            bool subCategory = easyServiceEntities.SubCategories.Where(a => a.CategoryId == id).Any();
            if(!subCategory)
            {
                Category category = easyServiceEntities.Categories.Where(a => a.Id == id).FirstOrDefault();
                easyServiceEntities.Categories.Remove(category);
                easyServiceEntities.SaveChanges();

                return RedirectToAction("ListCategory");
            }
            else
            {
                TempData["errorMessage"] = "<script>alert('There are Subcategory associated with this category, please delete those subcategory first');</script>";              
                return RedirectToAction("ListCategory");
            }


            
        }


        public ActionResult ListSubCategory(int? categoryId)
        {
            EasyServiceEntities easyServiceEntities = new EasyServiceEntities();
            if (categoryId == null)
            {
                List<SubCategory> subCategories = easyServiceEntities.SubCategories.ToList();
                return View(subCategories);
            }
            else
            {
                List<SubCategory> subCategories = easyServiceEntities.SubCategories.Where(a => a.CategoryId == categoryId).ToList();
                return View(subCategories);
            }

        }

        public ActionResult AddSubCategory(int? categoryId)
        {
            EasyServiceEntities easyServiceEntities = new EasyServiceEntities();
            if (categoryId == null)
            {                
                ViewBag.categoryList = easyServiceEntities.Categories.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Id.ToString(), Text = rr.Name }).ToList();
            }
            else
            {
                ViewBag.categoryList = easyServiceEntities.Categories.Where(r => r.Id == categoryId).ToList().Select(rr => new SelectListItem { Value = rr.Id.ToString(), Text = rr.Name }).ToList();
            }
            

            return View();
        }
        

        [HttpPost]
        public ActionResult AddSubCategory(SubCategory model)
        {
            
                EasyServiceEntities easyServiceEntities = new EasyServiceEntities();               
                easyServiceEntities.SubCategories.Add(model);
                easyServiceEntities.SaveChanges();
                return RedirectToAction("ListSubCategory");
        }

        public ActionResult EditSubCategory(int Id)
        {
            EasyServiceEntities easyServiceEntities = new EasyServiceEntities();
            SubCategory subCategory = easyServiceEntities.SubCategories.Where(a => a.Id == Id).FirstOrDefault();
            ViewBag.categoryList = easyServiceEntities.Categories.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Id.ToString(), Text = rr.Name }).ToList();

            return View(subCategory);
        }
        [HttpPost]
        public ActionResult EditSubCategory(SubCategory model)
        {
            EasyServiceEntities easyServiceEntities = new EasyServiceEntities();
            easyServiceEntities.Entry(model).State = EntityState.Modified;
            easyServiceEntities.SaveChanges();
            return RedirectToAction("ListSubCategory");
        }
        public ActionResult DeleteSubCategory(int Id)
        {
            EasyServiceEntities easyServiceEntities = new EasyServiceEntities();
            SubCategory subCategory = easyServiceEntities.SubCategories.Where(a => a.Id == Id).FirstOrDefault();
            easyServiceEntities.SubCategories.Remove(subCategory);
            easyServiceEntities.SaveChanges();
            return RedirectToAction("ListSubCategory");
        }
      
            
    }
}