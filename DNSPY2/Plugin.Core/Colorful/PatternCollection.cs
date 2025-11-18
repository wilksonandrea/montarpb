using System;
using System.Collections.Generic;

namespace Plugin.Core.Colorful
{
	// Token: 0x0200010B RID: 267
	public abstract class PatternCollection<T> : IPrototypable<PatternCollection<T>>
	{
		// Token: 0x060009C3 RID: 2499 RVA: 0x00007CDA File Offset: 0x00005EDA
		public PatternCollection()
		{
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00007CED File Offset: 0x00005EED
		public PatternCollection<T> Prototype()
		{
			return this.PrototypeCore();
		}

		// Token: 0x060009C5 RID: 2501
		protected abstract PatternCollection<T> PrototypeCore();

		// Token: 0x060009C6 RID: 2502
		public abstract bool MatchFound(string input);

		// Token: 0x0400071B RID: 1819
		protected List<Pattern<T>> patterns = new List<Pattern<T>>();
	}
}
