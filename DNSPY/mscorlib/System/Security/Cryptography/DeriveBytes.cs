using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200025B RID: 603
	[ComVisible(true)]
	public abstract class DeriveBytes : IDisposable
	{
		// Token: 0x06002168 RID: 8552
		public abstract byte[] GetBytes(int cb);

		// Token: 0x06002169 RID: 8553
		public abstract void Reset();

		// Token: 0x0600216A RID: 8554 RVA: 0x00076219 File Offset: 0x00074419
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x00076228 File Offset: 0x00074428
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0007622A File Offset: 0x0007442A
		protected DeriveBytes()
		{
		}
	}
}
