//using MyShop.DataAccess.InMemory;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;

namespace Myshop.WebUi.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory> context;
        //ProductCategory productCategories;
        public ProductCategoryManagerController(IRepository<ProductCategory> context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                context.insert(productCategory);
                context.Commit();

                return RedirectToAction("Index");
            }

        }
        public ActionResult Edit(string Id)
        {
            ProductCategory productCategory = context.Find(Id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory product, string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);
            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                productCategoryToEdit.Category = product.Category;
                
                context.Commit();
                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToDelete);
            }
        }
        [HttpPost]
        [ActionName("delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

    }
}