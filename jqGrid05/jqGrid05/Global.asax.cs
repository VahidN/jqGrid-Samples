using System;
using System.Web.Mvc;
using System.Web.Routing;
using jqGrid05.CustomModelBinders;

namespace jqGrid05
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