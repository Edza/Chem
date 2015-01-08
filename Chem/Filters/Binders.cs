using Chem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chem.Filters
{
    public class HomeCustomDataBinder : DefaultModelBinder
    {

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(Reaction))
            {
                HttpRequestBase request = controllerContext.HttpContext.Request;

                string desc = request.Form.Get("desc");


                return new Reaction
                {
                    Desc = desc
                };

                //// call the default model binder this new binding context
                //return base.BindModel(controllerContext, newBindingContext);
            }
            else
            {
                return base.BindModel(controllerContext, bindingContext);
            }
        }

    } 
}