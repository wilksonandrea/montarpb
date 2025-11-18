using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful;

public sealed class Styler : StyleClass<TextPattern>, IEquatable<Styler>
{
	public delegate string MatchFound(string unstyledInput, MatchLocation matchLocation, string match);

	public delegate string MatchFoundLite(string match);

	[CompilerGenerated]
	private MatchFound matchFound_0;

	public MatchFound MatchFoundHandler
	{
		[CompilerGenerated]
		get
		{
			return matchFound_0;
		}
		[CompilerGenerated]
		private set
		{
			matchFound_0 = value;
		}
	}

	public Styler(string string_0, Color color_1, MatchFound matchFound_1)
	{
		base.Target = new TextPattern(string_0);
		base.Color = color_1;
		MatchFoundHandler = matchFound_1;
	}

	public bool Equals(Styler other)
	{
		if (other == null)
		{
			return false;
		}
		if (Equals((StyleClass<TextPattern>)other))
		{
			return MatchFoundHandler == other.MatchFoundHandler;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		return Equals(obj as Styler);
	}

	public override int GetHashCode()
	{
		return base.GetHashCode() * (79 + MatchFoundHandler.GetHashCode());
	}
}
