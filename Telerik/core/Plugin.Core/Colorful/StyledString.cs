using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	public sealed class StyledString : IEquatable<StyledString>
	{
		public string AbstractValue
		{
			get;
			private set;
		}

		public char[,] CharacterGeometry
		{
			get;
			set;
		}

		public int[,] CharacterIndexGeometry
		{
			get;
			set;
		}

		public Color[,] ColorGeometry
		{
			get;
			set;
		}

		public string ConcreteValue
		{
			get;
			private set;
		}

		public StyledString(string string_2)
		{
			this.AbstractValue = string_2;
		}

		public StyledString(string string_2, string string_3)
		{
			this.AbstractValue = string_2;
			this.ConcreteValue = string_3;
		}

		public bool Equals(StyledString other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.AbstractValue != other.AbstractValue)
			{
				return false;
			}
			return this.ConcreteValue == other.ConcreteValue;
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as StyledString);
		}

		public override int GetHashCode()
		{
			return 163 * (79 + this.AbstractValue.GetHashCode()) * (79 + this.ConcreteValue.GetHashCode());
		}

		public override string ToString()
		{
			return this.ConcreteValue;
		}
	}
}