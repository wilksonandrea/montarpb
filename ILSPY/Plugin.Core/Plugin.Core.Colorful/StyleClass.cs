using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful;

public class StyleClass<T> : IEquatable<StyleClass<T>>
{
	[CompilerGenerated]
	private T gparam_0;

	[CompilerGenerated]
	private Color color_0;

	public T Target
	{
		[CompilerGenerated]
		get
		{
			return gparam_0;
		}
		[CompilerGenerated]
		protected set
		{
			gparam_0 = value;
		}
	}

	public Color Color
	{
		[CompilerGenerated]
		get
		{
			return color_0;
		}
		[CompilerGenerated]
		protected set
		{
			color_0 = value;
		}
	}

	public StyleClass()
	{
	}

	public StyleClass(T gparam_1, Color color_1)
	{
		Target = gparam_1;
		Color = color_1;
	}

	public bool Equals(StyleClass<T> other)
	{
		if (other == null)
		{
			return false;
		}
		if (Target.Equals(other.Target))
		{
			return Color == other.Color;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		return Equals(obj as StyleClass<T>);
	}

	public override int GetHashCode()
	{
		return 163 * (79 + Target.GetHashCode()) * (79 + Color.GetHashCode());
	}
}
