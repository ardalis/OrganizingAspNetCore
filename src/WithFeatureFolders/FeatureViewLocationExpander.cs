using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Linq;

namespace WithFeatureFolders
{
    public class FeatureViewLocationExpander : IViewLocationExpander
    {
        private const string FEATURE_FOLDER_NAME = "Features";
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (viewLocations == null)
            {
                throw new ArgumentNullException(nameof(viewLocations));
            }
            // check for areas
            object area;
            if (context.ActionContext.RouteData.Values.TryGetValue("area", out area))
            {
                yield return "/Areas/{2}/{1}/{0}.cshtml";
                yield return "/Areas/{2}/Shared/{0}.cshtml";
                yield return "/Areas/Shared/{0}.cshtml";
            }

            var controllerActionDescriptor = context.ActionContext.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null &&
                controllerActionDescriptor.ControllerTypeInfo.FullName.Contains(FEATURE_FOLDER_NAME))
            {
                yield return GetFeatureLocation(controllerActionDescriptor.ControllerTypeInfo.FullName);
            }
            foreach (var location in viewLocations)
            {
                yield return location;
            }
        }

        private string GetFeatureLocation(string fullControllerName)
        {
            var folderName = fullControllerName.Split('.')
                .SkipWhile(w => !w.Equals(FEATURE_FOLDER_NAME.ToLowerInvariant(), StringComparison.CurrentCultureIgnoreCase))
                .Skip(1) // Features
                .Take(1).FirstOrDefault(); // Feature folder name
            return System.IO.Path.Combine(FEATURE_FOLDER_NAME, folderName, "{0}.cshtml");
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}