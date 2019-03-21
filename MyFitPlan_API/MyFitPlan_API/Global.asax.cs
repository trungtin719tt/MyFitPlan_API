using AutoMapper;
using Data;
using MyFitPlan_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyFitPlan_API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            System.Data.Entity.Database.SetInitializer<MyFitPlanDBContext>(null);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Initialization();
        }
        protected void Initialization()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Food, FoodApiViewModel>();
                cfg.CreateMap<FoodViewModel, Food>();
                cfg.CreateMap<FoodApiViewModel, Food>();
            });
        }
    }
}
