using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	// Token: 0x020000F0 RID: 240
	public sealed class ColorManager
	{
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x000076D4 File Offset: 0x000058D4
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x000076DC File Offset: 0x000058DC
		public bool IsInCompatibilityMode
		{
			[CompilerGenerated]
			get
			{
				return this.bool_0;
			}
			[CompilerGenerated]
			private set
			{
				this.bool_0 = value;
			}
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x000076E5 File Offset: 0x000058E5
		public ColorManager(ColorStore colorStore_1, ColorMapper colorMapper_1, int int_2, int int_3, bool bool_1)
		{
			this.colorStore_0 = colorStore_1;
			this.colorMapper_0 = colorMapper_1;
			this.int_0 = int_3;
			this.int_1 = int_2;
			this.IsInCompatibilityMode = bool_1;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00007712 File Offset: 0x00005912
		public Color GetColor(ConsoleColor color)
		{
			return this.colorStore_0.ConsoleColors[color];
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00020EE0 File Offset: 0x0001F0E0
		public void ReplaceColor(Color oldColor, Color newColor)
		{
			if (!this.IsInCompatibilityMode)
			{
				ConsoleColor consoleColor = this.colorStore_0.Replace(oldColor, newColor);
				this.colorMapper_0.MapColor(consoleColor, newColor);
			}
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00020F10 File Offset: 0x0001F110
		public ConsoleColor GetConsoleColor(Color color)
		{
			if (this.IsInCompatibilityMode)
			{
				return color.ToNearestConsoleColor();
			}
			ConsoleColor consoleColor;
			try
			{
				consoleColor = this.method_0(color);
			}
			catch
			{
				consoleColor = color.ToNearestConsoleColor();
			}
			return consoleColor;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00020F54 File Offset: 0x0001F154
		private ConsoleColor method_0(Color color_0)
		{
			if (this.method_1() && this.colorStore_0.RequiresUpdate(color_0))
			{
				ConsoleColor consoleColor = (ConsoleColor)this.int_0;
				this.colorMapper_0.MapColor(consoleColor, color_0);
				this.colorStore_0.Update(consoleColor, color_0);
				this.int_0++;
			}
			if (this.colorStore_0.Colors.ContainsKey(color_0))
			{
				return this.colorStore_0.Colors[color_0];
			}
			return this.colorStore_0.Colors.Last<KeyValuePair<Color, ConsoleColor>>().Value;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00007725 File Offset: 0x00005925
		private bool method_1()
		{
			return this.int_0 < this.int_1;
		}

		// Token: 0x040006C5 RID: 1733
		[CompilerGenerated]
		private bool bool_0;

		// Token: 0x040006C6 RID: 1734
		private ColorMapper colorMapper_0;

		// Token: 0x040006C7 RID: 1735
		private ColorStore colorStore_0;

		// Token: 0x040006C8 RID: 1736
		private int int_0;

		// Token: 0x040006C9 RID: 1737
		private int int_1;
	}
}
