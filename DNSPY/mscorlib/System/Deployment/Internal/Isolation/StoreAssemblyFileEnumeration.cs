using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200068A RID: 1674
	internal class StoreAssemblyFileEnumeration : IEnumerator
	{
		// Token: 0x06004F5F RID: 20319 RVA: 0x0011C374 File Offset: 0x0011A574
		public StoreAssemblyFileEnumeration(IEnumSTORE_ASSEMBLY_FILE pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x0011C383 File Offset: 0x0011A583
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x0011C386 File Offset: 0x0011A586
		private STORE_ASSEMBLY_FILE GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06004F62 RID: 20322 RVA: 0x0011C39C File Offset: 0x0011A59C
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06004F63 RID: 20323 RVA: 0x0011C3A9 File Offset: 0x0011A5A9
		public STORE_ASSEMBLY_FILE Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004F64 RID: 20324 RVA: 0x0011C3B4 File Offset: 0x0011A5B4
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			STORE_ASSEMBLY_FILE[] array = new STORE_ASSEMBLY_FILE[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = num == 1U;
		}

		// Token: 0x06004F65 RID: 20325 RVA: 0x0011C3F4 File Offset: 0x0011A5F4
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x0400220C RID: 8716
		private IEnumSTORE_ASSEMBLY_FILE _enum;

		// Token: 0x0400220D RID: 8717
		private bool _fValid;

		// Token: 0x0400220E RID: 8718
		private STORE_ASSEMBLY_FILE _current;
	}
}
