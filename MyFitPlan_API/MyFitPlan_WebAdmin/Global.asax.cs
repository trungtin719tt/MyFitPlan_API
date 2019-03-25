using AutoMapper;
using Data;
using MyFitPlan_WebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyFitPlan_WebAdmin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            System.Data.Entity.Database.SetInitializer<MyFitPlanDBContext>(null);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Initialization();
        }
        protected void Initialization()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Food, FoodEditViewModel>();
                cfg.CreateMap<FoodEditViewModel, Food>();
            });
        }
    }
}
