using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000690 RID: 1680
	internal class StoreCategoryInstanceEnumeration : IEnumerator
	{
		// Token: 0x06004F80 RID: 20352 RVA: 0x0011C530 File Offset: 0x0011A730
		public StoreCategoryInstanceEnumeration(IEnumSTORE_CATEGORY_INSTANCE pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004F81 RID: 20353 RVA: 0x0011C53F File Offset: 0x0011A73F
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004F82 RID: 20354 RVA: 0x0011C542 File Offset: 0x0011A742
		private STORE_CATEGORY_INSTANCE GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06004F83 RID: 20355 RVA: 0x0011C558 File Offset: 0x0011A758
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06004F84 RID: 20356 RVA: 0x0011C565 File Offset: 0x0011A765
		public STORE_CATEGORY_INSTANCE Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004F85 RID: 20357 RVA: 0x0011C570 File Offset: 0x0011A770
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			STORE_CATEGORY_INSTANCE[] array = new STORE_CATEGORY_INSTANCE[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = num == 1U;
		}

		// Token: 0x06004F86 RID: 20358 RVA: 0x0011C5B0 File Offset: 0x0011A7B0
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04002215 RID: 8725
		private IEnumSTORE_CATEGORY_INSTANCE _enum;

		// Token: 0x04002216 RID: 8726
		private bool _fValid;

		// Token: 0x04002217 RID: 8727
		private STORE_CATEGORY_INSTANCE _current;
	}
}
