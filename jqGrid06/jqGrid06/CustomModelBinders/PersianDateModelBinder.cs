using System;
using System.Globalization;
using System.Web.Mvc;

namespace jqGrid06.CustomModelBinders
{
    /// <summary>
    /// How to register it in the Application_Start method of Global.asax.cs
    /// ModelBinders.Binders.Add(typeof(DateTime), new PersianDateModelBinder());
    /// </summary>
    public class PersianDateModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var modelState = new ModelState { Value = valueResult };
            object actualValue = null;
            try
            {
                var parts = valueResult.AttemptedValue.Split('/'); //ex. 1391/1/19
                if (parts.Length != 3) return null;
                var year = int.Parse(parts[0]);
                var month = int.Parse(parts[1]);
                var day = int.Parse(parts[2]);
                actualValue = new DateTime(year, month, day, new PersianCalendar());
            }
            catch (FormatException e)
            {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }
}