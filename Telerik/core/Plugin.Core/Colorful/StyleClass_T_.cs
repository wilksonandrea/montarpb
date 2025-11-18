using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	public class StyleClass<T> : IEquatable<StyleClass<T>>
	{
		public System.Drawing.Color Color
		{
			get;
			protected set;
		}

		public T Target
		{
			get;
			protected set;
		}

		public StyleClass()
		{
		}

		public StyleClass(T gparam_1, System.Drawing.Color color_1)
		{
			this.Target = gparam_1;
			this.Color = color_1;
		}

		public bool Equals(StyleClass<T> other)
		{
			if (other == null)
			{
				return false;
			}
			if (!this.Target.Equals(other.Target))
			{
				return false;
			}
			return this.Color == other.Color;
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as StyleClass<T>);
		}

		public override int GetHashCode()
		{
			T target = this.Target;
			return 163 * (79 + target.GetHashCode()) * (79 + this.Color.GetHashCode());
		}
	}
}