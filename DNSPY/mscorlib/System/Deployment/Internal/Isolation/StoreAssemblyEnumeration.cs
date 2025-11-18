using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000688 RID: 1672
	internal class StoreAssemblyEnumeration : IEnumerator
	{
		// Token: 0x06004F54 RID: 20308 RVA: 0x0011C2E0 File Offset: 0x0011A4E0
		public StoreAssemblyEnumeration(IEnumSTORE_ASSEMBLY pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004F55 RID: 20309 RVA: 0x0011C2EF File Offset: 0x0011A4EF
		private STORE_ASSEMBLY GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06004F56 RID: 20310 RVA: 0x0011C305 File Offset: 0x0011A505
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06004F57 RID: 20311 RVA: 0x0011C312 File Offset: 0x0011A512
		public STORE_ASSEMBLY Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x0011C31A File Offset: 0x0011A51A
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x0011C320 File Offset: 0x0011A520
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			STORE_ASSEMBLY[] array = new STORE_ASSEMBLY[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = num == 1U;
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x0011C360 File Offset: 0x0011A560
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04002209 RID: 8713
		private IEnumSTORE_ASSEMBLY _enum;

		// Token: 0x0400220A RID: 8714
		private bool _fValid;

		// Token: 0x0400220B RID: 8715
		private STORE_ASSEMBLY _current;
	}
}
