using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200068E RID: 1678
	internal class StoreSubcategoryEnumeration : IEnumerator
	{
		// Token: 0x06004F75 RID: 20341 RVA: 0x0011C49C File Offset: 0x0011A69C
		public StoreSubcategoryEnumeration(IEnumSTORE_CATEGORY_SUBCATEGORY pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004F76 RID: 20342 RVA: 0x0011C4AB File Offset: 0x0011A6AB
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004F77 RID: 20343 RVA: 0x0011C4AE File Offset: 0x0011A6AE
		private STORE_CATEGORY_SUBCATEGORY GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06004F78 RID: 20344 RVA: 0x0011C4C4 File Offset: 0x0011A6C4
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06004F79 RID: 20345 RVA: 0x0011C4D1 File Offset: 0x0011A6D1
		public STORE_CATEGORY_SUBCATEGORY Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004F7A RID: 20346 RVA: 0x0011C4DC File Offset: 0x0011A6DC
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			STORE_CATEGORY_SUBCATEGORY[] array = new STORE_CATEGORY_SUBCATEGORY[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = num == 1U;
		}

		// Token: 0x06004F7B RID: 20347 RVA: 0x0011C51C File Offset: 0x0011A71C
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04002212 RID: 8722
		private IEnumSTORE_CATEGORY_SUBCATEGORY _enum;

		// Token: 0x04002213 RID: 8723
		private bool _fValid;

		// Token: 0x04002214 RID: 8724
		private STORE_CATEGORY_SUBCATEGORY _current;
	}
}
