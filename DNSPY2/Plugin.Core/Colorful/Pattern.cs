using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	// Token: 0x02000109 RID: 265
	public abstract class Pattern<T> : IEquatable<Pattern<T>>
	{
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x00007C41 File Offset: 0x00005E41
		// (set) Token: 0x060009B7 RID: 2487 RVA: 0x00007C49 File Offset: 0x00005E49
		public T Value
		{
			[CompilerGenerated]
			get
			{
				return this.gparam_0;
			}
			[CompilerGenerated]
			private set
			{
				this.gparam_0 = value;
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00007C52 File Offset: 0x00005E52
		public Pattern(T gparam_1)
		{
			this.Value = gparam_1;
		}

		// Token: 0x060009B9 RID: 2489
		public abstract IEnumerable<MatchLocation> GetMatchLocations(T input);

		// Token: 0x060009BA RID: 2490
		public abstract IEnumerable<T> GetMatches(T input);

		// Token: 0x060009BB RID: 2491 RVA: 0x00022008 File Offset: 0x00020208
		public bool Equals(Pattern<T> other)
		{
			if (other == null)
			{
				return false;
			}
			T value = this.Value;
			return value.Equals(other.Value);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00007C61 File Offset: 0x00005E61
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Pattern<T>);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0002203C File Offset: 0x0002023C
		public override int GetHashCode()
		{
			int num = 163;
			int num2 = 79;
			T value = this.Value;
			return num * (num2 + value.GetHashCode());
		}

		// Token: 0x04000718 RID: 1816
		[CompilerGenerated]
		private T gparam_0;
	}
}
