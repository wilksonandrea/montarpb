using System;
using System.Drawing;

namespace Plugin.Core.Colorful
{
	public sealed class Formatter
	{
		private StyleClass<object> styleClass_0;

		public System.Drawing.Color Color
		{
			get
			{
				return this.styleClass_0.Color;
			}
		}

		public object Target
		{
			get
			{
				return this.styleClass_0.Target;
			}
		}

		public Formatter(object object_0, System.Drawing.Color color_0)
		{
			this.styleClass_0 = new StyleClass<object>(object_0, color_0);
		}
	}
}