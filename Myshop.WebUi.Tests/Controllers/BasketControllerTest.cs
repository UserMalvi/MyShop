using Microsoft.VisualStudio.TestTools.UnitTesting;
using Myshop.WebUi.Controllers;
using Myshop.WebUi.Tests.Mocks;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Services;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Myshop.WebUi.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTest
    {
        [TestMethod]
        public void CanAddBasketItem()
        {
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();
            IRepository<Order> orders = new MockContext<Order>();

            var httpContext = new MockHttpContext();

            IBasketService basketService = new BasketService(products, baskets);
            IOrderService orderService = new OrderService(orders);
           var controller = new BasketController(basketService);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            //basketService.AddToBasket(httpContext, "1"); 

            controller.AddToBasket("1");

            Basket basket = baskets.Collection().FirstOrDefault();

            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count);
            Assert.AreEqual("1", basket.BasketItems.ToList().FirstOrDefault().ProductId);


        }
        [TestMethod]
        public void CanGetSummaryViewModel()
        {
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();
            IRepository<Order> orders = new MockContext<Order>();


            products.insert(new Product() { Id = "1", Price = 10.00m });
            products.insert(new Product() { Id = "2", Price = 5.00m });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem() { ProductId = "2", Quantity = 1 });
            baskets.insert(basket);

            IBasketService basketService = new BasketService(products, baskets);
            IOrderService orderService = new OrderService(orders);
            var controller = new BasketController(basketService);
        }


        [TestMethod]
        public void CanCheckOutCreateOrder()
        {
            IRepository<Product> products = new MockContext<Product>();
            products.insert(new Product() { Id = "1", Price = 10.00m });
            products.insert(new Product() { Id = "2", Price = 5.00m });

            IRepository<Basket> baskets = new MockContext<Basket>();
            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quantity = 2, BasketId = basket.Id });
            basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quantity = 1, BasketId = basket.Id });


            baskets.insert(basket);

            IBasketService basketService = new BasketService(products, baskets);
                
            IRepository<Order> orders = new MockContext<Order>();
            IOrderService orderService = new OrderService(orders);

          }

        








        //IBasketService

        //var IBasketService basketService = null;
        //Controller = new BasketController(basketService);
        //var httpContext = new MockHttpContext();
        //httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerce") { Value = basket.Id });
        //Controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

        //var result = Controller.BasketSummary() as PartialViewResult;
        //Assert.AreEqual(3, basketSummary.BasketCount)
    }
    }