using System;
using System.Web.Mvc;
using System.Web.Routing;
using jqGrid06.CustomModelBinders;

namespace jqGrid06
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(DateTime), new PersianDateModelBinder());
            ModelBinders.Binders.Add(typeof(decimal), new DecimalBinder());
        }
    }
}