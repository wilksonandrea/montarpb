using System;

namespace System.Collections.Generic
{
	// Token: 0x020004BE RID: 1214
	[Serializable]
	internal class ComparisonComparer<T> : Comparer<T>
	{
		// Token: 0x06003A3B RID: 14907 RVA: 0x000DDCB6 File Offset: 0x000DBEB6
		public ComparisonComparer(Comparison<T> comparison)
		{
			this._comparison = comparison;
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x000DDCC5 File Offset: 0x000DBEC5
		public override int Compare(T x, T y)
		{
			return this._comparison(x, y);
		}

		// Token: 0x04001942 RID: 6466
		private readonly Comparison<T> _comparison;
	}
}
