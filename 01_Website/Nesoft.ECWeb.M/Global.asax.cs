﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Nesoft.ECWeb.WebFramework;

namespace Nesoft.ECWeb.M
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex != null && ex is HttpException)
            {
                int statusCode = (ex as HttpException).GetHttpCode();
                switch (statusCode)
                {
                    case 404:
                        string c = ex.Message;

                        //Response.Redirect("/Error/404");
                        break;
                }
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var collection = HttpContext.Current.Request.Headers;
            if (collection.AllKeys.Contains(MobileCookie.COOKIE_NAME))
            {
                string v = collection[MobileCookie.COOKIE_NAME];
                HttpContext.Current.Response.AppendHeader(MobileCookie.COOKIE_NAME, v);
            }

            string cultureCode = LanguageHelper.GetLanguageCode();
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);

            var engine = ViewEngines.Engines.OfType<RazorViewEngine>().Single();

            //engine.AreaMasterLocationFormats = new string[]{
            //     "~/Configuration/LanguageResources/"+cultureCode+"/Areas/{2}/Views/{1}/{0}.cshtml",
            //    "~/Configuration/LanguageResources/"+cultureCode+"/Areas/{2}/Views/{1}/{0}.vbhtml",
            //    "~/Configuration/LanguageResources/"+cultureCode+"/Areas/{2}/Views/Shared/{0}.cshtml",
            //    "~/Configuration/LanguageResources/"+cultureCode+"/Areas/{2}/Views/Shared/{0}.vbhtml",
            //    "~/Areas/{2}/Views/{1}/{0}.cshtml",
            //    "~/Areas/{2}/Views/{1}/{0}.vbhtml",
            //    "~/Areas/{2}/Views/Shared/{0}.cshtml",
            //    "~/Areas/{2}/Views/Shared/{0}.vbhtml"
            //};
            //engine.AreaPartialViewLocationFormats = new string[]{
            //    "~/Configuration/LanguageResources/"+cultureCode+"/Areas/{2}/Views/{1}/{0}.cshtml",
            //    "~/Configuration/LanguageResources/"+cultureCode+"/Areas/{2}/Views/{1}/{0}.vbhtml",
            //    "~/Configuration/LanguageResources/"+cultureCode+"/Areas/{2}/Views/Shared/{0}.cshtml",
            //    "~/Configuration/LanguageResources/"+cultureCode+"/Areas/{2}/Views/Shared/{0}.vbhtml",

            //    "~/Areas/{2}/Views/{1}/{0}.cshtml",
            //    "~/Areas/{2}/Views/{1}/{0}.vbhtml",
            //    "~/Areas/{2}/Views/Shared/{0}.cshtml",
            //    "~/Areas/{2}/Views/Shared/{0}.vbhtml"
            //};


            //engine.AreaViewLocationFormats = new string[]{
            //    "~/Configuration/LanguageResources/"+cultureCode+"/Areas/{2}/Views/{1}/{0}.cshtml",
            //    "~/Configuration/LanguageResources/"+cultureCode+"/Areas/{2}/Views/{1}/{0}.vbhtml",
            //    "~/Configuration/LanguageResources/"+cultureCode+"/Areas/{2}/Views/Shared/{0}.cshtml",
            //    "~/Configuration/LanguageResources/"+cultureCode+"/Areas/{2}/Views/Shared/{0}.vbhtml",

            //    "~/Areas/{2}/Views/{1}/{0}.cshtml",
            //    "~/Areas/{2}/Views/{1}/{0}.vbhtml",
            //    "~/Areas/{2}/Views/Shared/{0}.cshtml",
            //    "~/Areas/{2}/Views/Shared/{0}.vbhtml"
            //};

            engine.MasterLocationFormats = new string[]{
                "~/Configuration/LanguageResources/"+cultureCode+"/Views/{1}/{0}.cshtml",
                "~/Configuration/LanguageResources/"+cultureCode+"/Views/{1}/{0}.vbhtml",
                "~/Configuration/LanguageResources/"+cultureCode+"/Views/Shared/{0}.cshtml",
                "~/Configuration/LanguageResources/"+cultureCode+"/Views/Shared/{0}.vbhtml",

                "~/Views/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.vbhtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.vbhtml"
            };


            engine.PartialViewLocationFormats = new string[]{ 
                "~/Configuration/LanguageResources/"+cultureCode+"/Views/{1}/{0}.cshtml",
                "~/Configuration/LanguageResources/"+cultureCode+"/Views/{1}/{0}.vbhtml",
                "~/Configuration/LanguageResources/"+cultureCode+"/Views/Shared/{0}.cshtml",
                "~/Configuration/LanguageResources/"+cultureCode+"/Views/Shared/{0}.vbhtml",

                "~/Views/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.vbhtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.vbhtml"
            };

            engine.ViewLocationFormats = new string[] 
            {
                "~/Configuration/LanguageResources/"+cultureCode+"/Views/{1}/{0}.cshtml", 
                "~/Configuration/LanguageResources/"+cultureCode+"/Views/{1}/{0}.vbhtml",
                "~/Configuration/LanguageResources/"+cultureCode+"/Views/Shared/{0}.cshtml", 
                "~/Configuration/LanguageResources/"+cultureCode+"/Views/Shared/{0}.vbhtml",

                "~/Views/{1}/{0}.cshtml", 
                "~/Views/{1}/{0}.vbhtml",
                "~/Views/Shared/{0}.cshtml", 
                "~/Views/Shared/{0}.vbhtml"
            };
        }
    }
}