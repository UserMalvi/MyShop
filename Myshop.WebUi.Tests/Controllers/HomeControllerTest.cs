using Microsoft.VisualStudio.TestTools.UnitTesting;
using Myshop.WebUi;
using Myshop.WebUi.Controllers;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Myshop.WebUi.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexPagesDoesreturnProducts()
        {
            //// Arrange
            IRepository<Product> productContext = new Mocks.MockContext<Product>();
            IRepository<ProductCategory> productCategoryContext = new Mocks.MockContext<ProductCategory>();
            HomeController controller = new HomeController(productContext, productCategoryContext);

            productContext.insert(new Product());

            var result = controller.Index() as ViewResult;
            var ViewModel = (ProductListViewModel)result.ViewData.Model;

            Assert.AreEqual(1, ViewModel.Products.Count());

            //// Act
           // ViewResult result = controller.Index() as ViewResult;

            //// Assert
            //Assert.IsNotNull(result);
            ///Assert.IsTrue(1 == 1);
        }

      
    }
}