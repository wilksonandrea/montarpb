using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful;

public sealed class StyledString : IEquatable<StyledString>
{
	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private string string_1;

	[CompilerGenerated]
	private Color[,] color_0;

	[CompilerGenerated]
	private char[,] char_0;

	[CompilerGenerated]
	private int[,] int_0;

	public string AbstractValue
	{
		[CompilerGenerated]
		get
		{
			return string_0;
		}
		[CompilerGenerated]
		private set
		{
			string_0 = value;
		}
	}

	public string ConcreteValue
	{
		[CompilerGenerated]
		get
		{
			return string_1;
		}
		[CompilerGenerated]
		private set
		{
			string_1 = value;
		}
	}

	public Color[,] ColorGeometry
	{
		[CompilerGenerated]
		get
		{
			return color_0;
		}
		[CompilerGenerated]
		set
		{
			color_0 = value;
		}
	}

	public char[,] CharacterGeometry
	{
		[CompilerGenerated]
		get
		{
			return char_0;
		}
		[CompilerGenerated]
		set
		{
			char_0 = value;
		}
	}

	public int[,] CharacterIndexGeometry
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		set
		{
			int_0 = value;
		}
	}

	public StyledString(string string_2)
	{
		AbstractValue = string_2;
	}

	public StyledString(string string_2, string string_3)
	{
		AbstractValue = string_2;
		ConcreteValue = string_3;
	}

	public bool Equals(StyledString other)
	{
		if (other == null)
		{
			return false;
		}
		if (AbstractValue == other.AbstractValue)
		{
			return ConcreteValue == other.ConcreteValue;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		return Equals(obj as StyledString);
	}

	public override int GetHashCode()
	{
		return 163 * (79 + AbstractValue.GetHashCode()) * (79 + ConcreteValue.GetHashCode());
	}

	public override string ToString()
	{
		return ConcreteValue;
	}
}
