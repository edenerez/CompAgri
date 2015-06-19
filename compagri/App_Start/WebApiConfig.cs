using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Routing;

namespace CompAgri
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Enable CORS
            // This way it handle the options request too
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApiWithAction",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new 
                {
                    action = "^[A-Za-z].*"
                }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApiGet",
                routeTemplate: "api/{controller}/{id}",
                defaults: new
                {
                    action = "Get",
                    id = RouteParameter.Optional
                },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute(
                name: "DefaultApiPost",
                routeTemplate: "api/{controller}/{id}",
                defaults: new
                {
                    action = "Post",
                    id = RouteParameter.Optional
                },
                constraints: new { httpMethod = new HttpMethodConstraint(new HttpMethod[] { HttpMethod.Post, HttpMethod.Options }) });

            config.Routes.MapHttpRoute(
                name: "DefaultApiDelete",
                routeTemplate: "api/{controller}/{id}",
                defaults: new
                {
                    action = "Delete",
                    id = RouteParameter.Optional
                },
                constraints: new { httpMethod = new HttpMethodConstraint(new HttpMethod[] { HttpMethod.Delete, HttpMethod.Options }) });

            config.Routes.MapHttpRoute(
                name: "DefaultApiPut",
                routeTemplate: "api/{controller}/{id}",
                defaults: new
                {
                    action = "Put",
                    id = RouteParameter.Optional
                },
                constraints: new { httpMethod = new HttpMethodConstraint(new HttpMethod[] { HttpMethod.Put, HttpMethod.Options }) });

        }
    }
}
