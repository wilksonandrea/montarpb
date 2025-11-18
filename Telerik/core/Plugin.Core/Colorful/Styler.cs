using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	public sealed class Styler : StyleClass<TextPattern>, IEquatable<Styler>
	{
		public Styler.MatchFound MatchFoundHandler
		{
			get;
			private set;
		}

		public Styler(string string_0, System.Drawing.Color color_1, Styler.MatchFound matchFound_1)
		{
			base.Target = new TextPattern(string_0);
			base.Color = color_1;
			this.MatchFoundHandler = matchFound_1;
		}

		public bool Equals(Styler other)
		{
			if (other == null)
			{
				return false;
			}
			if (!base.Equals(other))
			{
				return false;
			}
			return this.MatchFoundHandler == other.MatchFoundHandler;
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as Styler);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() * (79 + this.MatchFoundHandler.GetHashCode());
		}

		public delegate string MatchFound(string unstyledInput, MatchLocation matchLocation, string match);

		public delegate string MatchFoundLite(string match);
	}
}