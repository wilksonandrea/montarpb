using System;

namespace System.Threading
{
	// Token: 0x02000549 RID: 1353
	internal struct SparselyPopulatedArrayAddInfo<T> where T : class
	{
		// Token: 0x06003F73 RID: 16243 RVA: 0x000EC0EE File Offset: 0x000EA2EE
		internal SparselyPopulatedArrayAddInfo(SparselyPopulatedArrayFragment<T> source, int index)
		{
			this.m_source = source;
			this.m_index = index;
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06003F74 RID: 16244 RVA: 0x000EC0FE File Offset: 0x000EA2FE
		internal SparselyPopulatedArrayFragment<T> Source
		{
			get
			{
				return this.m_source;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06003F75 RID: 16245 RVA: 0x000EC106 File Offset: 0x000EA306
		internal int Index
		{
			get
			{
				return this.m_index;
			}
		}

		// Token: 0x04001AB7 RID: 6839
		private SparselyPopulatedArrayFragment<T> m_source;

		// Token: 0x04001AB8 RID: 6840
		private int m_index;
	}
}
