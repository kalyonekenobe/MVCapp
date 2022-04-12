using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace MVCapp.Custom
{
	public class RouteNamingConvention : IControllerModelConvention
	{
		private static string _customControllerSuffix = "ControllerSasha";
		public void Apply(ControllerModel controller)
		{
			controller.ControllerName = controller.ControllerName.Replace(_customControllerSuffix, string.Empty);
		}
	}
}
