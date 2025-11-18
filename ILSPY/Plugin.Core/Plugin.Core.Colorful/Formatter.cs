using System.Drawing;

namespace Plugin.Core.Colorful;

public sealed class Formatter
{
	private StyleClass<object> styleClass_0;

	public object Target => styleClass_0.Target;

	public Color Color => styleClass_0.Color;

	public Formatter(object object_0, Color color_0)
	{
		styleClass_0 = new StyleClass<object>(object_0, color_0);
	}
}
