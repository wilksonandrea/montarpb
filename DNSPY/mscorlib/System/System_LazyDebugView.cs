using System;
using System.Threading;

namespace System
{
	// Token: 0x020000F9 RID: 249
	internal sealed class System_LazyDebugView<T>
	{
		// Token: 0x06000F24 RID: 3876 RVA: 0x0002F2C7 File Offset: 0x0002D4C7
		public System_LazyDebugView(Lazy<T> lazy)
		{
			this.m_lazy = lazy;
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x0002F2D6 File Offset: 0x0002D4D6
		public bool IsValueCreated
		{
			get
			{
				return this.m_lazy.IsValueCreated;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x0002F2E3 File Offset: 0x0002D4E3
		public T Value
		{
			get
			{
				return this.m_lazy.ValueForDebugDisplay;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0002F2F0 File Offset: 0x0002D4F0
		public LazyThreadSafetyMode Mode
		{
			get
			{
				return this.m_lazy.Mode;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0002F2FD File Offset: 0x0002D4FD
		public bool IsValueFaulted
		{
			get
			{
				return this.m_lazy.IsValueFaulted;
			}
		}

		// Token: 0x0400059E RID: 1438
		private readonly Lazy<T> m_lazy;
	}
}
