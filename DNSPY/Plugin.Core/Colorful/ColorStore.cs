using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	// Token: 0x020000F8 RID: 248
	public sealed class ColorStore
	{
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00007820 File Offset: 0x00005A20
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x00007828 File Offset: 0x00005A28
		public ConcurrentDictionary<Color, ConsoleColor> Colors
		{
			[CompilerGenerated]
			get
			{
				return this.concurrentDictionary_0;
			}
			[CompilerGenerated]
			private set
			{
				this.concurrentDictionary_0 = value;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x00007831 File Offset: 0x00005A31
		// (set) Token: 0x06000953 RID: 2387 RVA: 0x00007839 File Offset: 0x00005A39
		public ConcurrentDictionary<ConsoleColor, Color> ConsoleColors
		{
			[CompilerGenerated]
			get
			{
				return this.concurrentDictionary_1;
			}
			[CompilerGenerated]
			private set
			{
				this.concurrentDictionary_1 = value;
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00007842 File Offset: 0x00005A42
		public ColorStore(ConcurrentDictionary<Color, ConsoleColor> concurrentDictionary_2, ConcurrentDictionary<ConsoleColor, Color> concurrentDictionary_3)
		{
			this.Colors = concurrentDictionary_2;
			this.ConsoleColors = concurrentDictionary_3;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00007858 File Offset: 0x00005A58
		public void Update(ConsoleColor oldColor, Color newColor)
		{
			this.Colors.TryAdd(newColor, oldColor);
			this.ConsoleColors[oldColor] = newColor;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000214A4 File Offset: 0x0001F6A4
		public ConsoleColor Replace(Color oldColor, Color newColor)
		{
			ConsoleColor consoleColor;
			if (!this.Colors.TryRemove(oldColor, out consoleColor))
			{
				throw new ArgumentException("An attempt was made to replace a nonexistent color in the ColorStore!");
			}
			this.Colors.TryAdd(newColor, consoleColor);
			this.ConsoleColors[consoleColor] = newColor;
			return consoleColor;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00007875 File Offset: 0x00005A75
		public bool RequiresUpdate(Color color)
		{
			return !this.Colors.ContainsKey(color);
		}

		// Token: 0x040006EC RID: 1772
		[CompilerGenerated]
		private ConcurrentDictionary<Color, ConsoleColor> concurrentDictionary_0;

		// Token: 0x040006ED RID: 1773
		[CompilerGenerated]
		private ConcurrentDictionary<ConsoleColor, Color> concurrentDictionary_1;
	}
}
