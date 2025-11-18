using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	public sealed class ColorManager
	{
		private ColorMapper colorMapper_0;

		private ColorStore colorStore_0;

		private int int_0;

		private int int_1;

		public bool IsInCompatibilityMode
		{
			get;
			private set;
		}

		public ColorManager(ColorStore colorStore_1, ColorMapper colorMapper_1, int int_2, int int_3, bool bool_1)
		{
			this.colorStore_0 = colorStore_1;
			this.colorMapper_0 = colorMapper_1;
			this.int_0 = int_3;
			this.int_1 = int_2;
			this.IsInCompatibilityMode = bool_1;
		}

		public Color GetColor(ConsoleColor color)
		{
			return this.colorStore_0.ConsoleColors[color];
		}

		public ConsoleColor GetConsoleColor(Color color)
		{
			ConsoleColor nearestConsoleColor;
			if (this.IsInCompatibilityMode)
			{
				return color.ToNearestConsoleColor();
			}
			try
			{
				nearestConsoleColor = this.method_0(color);
			}
			catch
			{
				nearestConsoleColor = color.ToNearestConsoleColor();
			}
			return nearestConsoleColor;
		}

		private ConsoleColor method_0(Color color_0)
		{
			if (this.method_1() && this.colorStore_0.RequiresUpdate(color_0))
			{
				ConsoleColor int0 = (ConsoleColor)this.int_0;
				this.colorMapper_0.MapColor(int0, color_0);
				this.colorStore_0.Update(int0, color_0);
				this.int_0++;
			}
			if (this.colorStore_0.Colors.ContainsKey(color_0))
			{
				return this.colorStore_0.Colors[color_0];
			}
			return this.colorStore_0.Colors.Last<KeyValuePair<Color, ConsoleColor>>().Value;
		}

		private bool method_1()
		{
			return this.int_0 < this.int_1;
		}

		public void ReplaceColor(Color oldColor, Color newColor)
		{
			if (!this.IsInCompatibilityMode)
			{
				ConsoleColor consoleColor = this.colorStore_0.Replace(oldColor, newColor);
				this.colorMapper_0.MapColor(consoleColor, newColor);
			}
		}
	}
}