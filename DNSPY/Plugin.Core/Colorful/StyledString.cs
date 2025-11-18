using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	// Token: 0x0200010D RID: 269
	public sealed class StyledString : IEquatable<StyledString>
	{
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00007D3B File Offset: 0x00005F3B
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x00007D43 File Offset: 0x00005F43
		public string AbstractValue
		{
			[CompilerGenerated]
			get
			{
				return this.string_0;
			}
			[CompilerGenerated]
			private set
			{
				this.string_0 = value;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00007D4C File Offset: 0x00005F4C
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x00007D54 File Offset: 0x00005F54
		public string ConcreteValue
		{
			[CompilerGenerated]
			get
			{
				return this.string_1;
			}
			[CompilerGenerated]
			private set
			{
				this.string_1 = value;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00007D5D File Offset: 0x00005F5D
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x00007D65 File Offset: 0x00005F65
		public Color[,] ColorGeometry
		{
			[CompilerGenerated]
			get
			{
				return this.color_0;
			}
			[CompilerGenerated]
			set
			{
				this.color_0 = value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00007D6E File Offset: 0x00005F6E
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x00007D76 File Offset: 0x00005F76
		public char[,] CharacterGeometry
		{
			[CompilerGenerated]
			get
			{
				return this.char_0;
			}
			[CompilerGenerated]
			set
			{
				this.char_0 = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x00007D7F File Offset: 0x00005F7F
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x00007D87 File Offset: 0x00005F87
		public int[,] CharacterIndexGeometry
		{
			[CompilerGenerated]
			get
			{
				return this.int_0;
			}
			[CompilerGenerated]
			set
			{
				this.int_0 = value;
			}
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00007D90 File Offset: 0x00005F90
		public StyledString(string string_2)
		{
			this.AbstractValue = string_2;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00007D9F File Offset: 0x00005F9F
		public StyledString(string string_2, string string_3)
		{
			this.AbstractValue = string_2;
			this.ConcreteValue = string_3;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00007DB5 File Offset: 0x00005FB5
		public bool Equals(StyledString other)
		{
			return other != null && this.AbstractValue == other.AbstractValue && this.ConcreteValue == other.ConcreteValue;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00007DE2 File Offset: 0x00005FE2
		public override bool Equals(object obj)
		{
			return this.Equals(obj as StyledString);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00007DF0 File Offset: 0x00005FF0
		public override int GetHashCode()
		{
			return 163 * (79 + this.AbstractValue.GetHashCode()) * (79 + this.ConcreteValue.GetHashCode());
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00007E15 File Offset: 0x00006015
		public override string ToString()
		{
			return this.ConcreteValue;
		}

		// Token: 0x0400071E RID: 1822
		[CompilerGenerated]
		private string string_0;

		// Token: 0x0400071F RID: 1823
		[CompilerGenerated]
		private string string_1;

		// Token: 0x04000720 RID: 1824
		[CompilerGenerated]
		private Color[,] color_0;

		// Token: 0x04000721 RID: 1825
		[CompilerGenerated]
		private char[,] char_0;

		// Token: 0x04000722 RID: 1826
		[CompilerGenerated]
		private int[,] int_0;
	}
}
