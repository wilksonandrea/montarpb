using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004FB RID: 1275
	public class HostExecutionContext : IDisposable
	{
		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06003C41 RID: 15425 RVA: 0x000E3D3D File Offset: 0x000E1F3D
		// (set) Token: 0x06003C42 RID: 15426 RVA: 0x000E3D45 File Offset: 0x000E1F45
		protected internal object State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x06003C43 RID: 15427 RVA: 0x000E3D4E File Offset: 0x000E1F4E
		public HostExecutionContext()
		{
		}

		// Token: 0x06003C44 RID: 15428 RVA: 0x000E3D56 File Offset: 0x000E1F56
		public HostExecutionContext(object state)
		{
			this.state = state;
		}

		// Token: 0x06003C45 RID: 15429 RVA: 0x000E3D68 File Offset: 0x000E1F68
		[SecuritySafeCritical]
		public virtual HostExecutionContext CreateCopy()
		{
			object obj = this.state;
			if (this.state is IUnknownSafeHandle)
			{
				obj = ((IUnknownSafeHandle)this.state).Clone();
			}
			return new HostExecutionContext(this.state);
		}

		// Token: 0x06003C46 RID: 15430 RVA: 0x000E3DA5 File Offset: 0x000E1FA5
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003C47 RID: 15431 RVA: 0x000E3DB4 File Offset: 0x000E1FB4
		public virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x0400199C RID: 6556
		private object state;
	}
}
