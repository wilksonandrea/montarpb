using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	// Token: 0x0200010C RID: 268
	public class StyleClass<T> : IEquatable<StyleClass<T>>
	{
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00007CF5 File Offset: 0x00005EF5
		// (set) Token: 0x060009C8 RID: 2504 RVA: 0x00007CFD File Offset: 0x00005EFD
		public T Target
		{
			[CompilerGenerated]
			get
			{
				return this.gparam_0;
			}
			[CompilerGenerated]
			protected set
			{
				this.gparam_0 = value;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00007D06 File Offset: 0x00005F06
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x00007D0E File Offset: 0x00005F0E
		public Color Color
		{
			[CompilerGenerated]
			get
			{
				return this.color_0;
			}
			[CompilerGenerated]
			protected set
			{
				this.color_0 = value;
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00002116 File Offset: 0x00000316
		public StyleClass()
		{
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00007D17 File Offset: 0x00005F17
		public StyleClass(T gparam_1, Color color_1)
		{
			this.Target = gparam_1;
			this.Color = color_1;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x000220D0 File Offset: 0x000202D0
		public bool Equals(StyleClass<T> other)
		{
			if (other == null)
			{
				return false;
			}
			T target = this.Target;
			return target.Equals(other.Target) && this.Color == other.Color;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00007D2D File Offset: 0x00005F2D
		public override bool Equals(object obj)
		{
			return this.Equals(obj as StyleClass<T>);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00022118 File Offset: 0x00020318
		public override int GetHashCode()
		{
			int num = 163;
			int num2 = 79;
			T target = this.Target;
			return num * (num2 + target.GetHashCode()) * (79 + this.Color.GetHashCode());
		}

		// Token: 0x0400071C RID: 1820
		[CompilerGenerated]
		private T gparam_0;

		// Token: 0x0400071D RID: 1821
		[CompilerGenerated]
		private Color color_0;
	}
}
