using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200068C RID: 1676
	internal class StoreCategoryEnumeration : IEnumerator
	{
		// Token: 0x06004F6A RID: 20330 RVA: 0x0011C408 File Offset: 0x0011A608
		public StoreCategoryEnumeration(IEnumSTORE_CATEGORY pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x0011C417 File Offset: 0x0011A617
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004F6C RID: 20332 RVA: 0x0011C41A File Offset: 0x0011A61A
		private STORE_CATEGORY GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06004F6D RID: 20333 RVA: 0x0011C430 File Offset: 0x0011A630
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06004F6E RID: 20334 RVA: 0x0011C43D File Offset: 0x0011A63D
		public STORE_CATEGORY Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004F6F RID: 20335 RVA: 0x0011C448 File Offset: 0x0011A648
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			STORE_CATEGORY[] array = new STORE_CATEGORY[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = num == 1U;
		}

		// Token: 0x06004F70 RID: 20336 RVA: 0x0011C488 File Offset: 0x0011A688
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x0400220F RID: 8719
		private IEnumSTORE_CATEGORY _enum;

		// Token: 0x04002210 RID: 8720
		private bool _fValid;

		// Token: 0x04002211 RID: 8721
		private STORE_CATEGORY _current;
	}
}
