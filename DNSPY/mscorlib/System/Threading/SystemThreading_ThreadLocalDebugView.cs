using System;
using System.Collections.Generic;

namespace System.Threading
{
	// Token: 0x02000541 RID: 1345
	internal sealed class SystemThreading_ThreadLocalDebugView<T>
	{
		// Token: 0x06003F03 RID: 16131 RVA: 0x000EA4FB File Offset: 0x000E86FB
		public SystemThreading_ThreadLocalDebugView(ThreadLocal<T> tlocal)
		{
			this.m_tlocal = tlocal;
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06003F04 RID: 16132 RVA: 0x000EA50A File Offset: 0x000E870A
		public bool IsValueCreated
		{
			get
			{
				return this.m_tlocal.IsValueCreated;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06003F05 RID: 16133 RVA: 0x000EA517 File Offset: 0x000E8717
		public T Value
		{
			get
			{
				return this.m_tlocal.ValueForDebugDisplay;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06003F06 RID: 16134 RVA: 0x000EA524 File Offset: 0x000E8724
		public List<T> Values
		{
			get
			{
				return this.m_tlocal.ValuesForDebugDisplay;
			}
		}

		// Token: 0x04001A80 RID: 6784
		private readonly ThreadLocal<T> m_tlocal;
	}
}
