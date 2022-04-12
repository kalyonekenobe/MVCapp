using Microsoft.AspNetCore.Mvc.Razor;

namespace MVCapp.Custom
{
	public class ViewLocationExpander : IViewLocationExpander
	{
		public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
		{
			List<string> routes = new List<string> { "/Views/{1}/{0}.cshtml", "/Views/Shared/{0}.cshtml" };
			string controllerName = ReverseControllerName(context.ControllerName);
			return new List<string>(routes.Select(route => string.Format(route, context.ViewName, controllerName)));
		}

		public void PopulateValues(ViewLocationExpanderContext context) { }

		private static string ReverseControllerName(string controllerName) => new string(controllerName.ToCharArray().Reverse().ToArray());
	}
}
