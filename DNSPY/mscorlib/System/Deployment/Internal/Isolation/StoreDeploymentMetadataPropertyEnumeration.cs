using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000686 RID: 1670
	internal class StoreDeploymentMetadataPropertyEnumeration : IEnumerator
	{
		// Token: 0x06004F49 RID: 20297 RVA: 0x0011C24C File Offset: 0x0011A44C
		public StoreDeploymentMetadataPropertyEnumeration(IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x0011C25B File Offset: 0x0011A45B
		private StoreOperationMetadataProperty GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06004F4B RID: 20299 RVA: 0x0011C271 File Offset: 0x0011A471
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06004F4C RID: 20300 RVA: 0x0011C27E File Offset: 0x0011A47E
		public StoreOperationMetadataProperty Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x0011C286 File Offset: 0x0011A486
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x0011C28C File Offset: 0x0011A48C
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			StoreOperationMetadataProperty[] array = new StoreOperationMetadataProperty[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = num == 1U;
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x0011C2CC File Offset: 0x0011A4CC
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04002206 RID: 8710
		private IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY _enum;

		// Token: 0x04002207 RID: 8711
		private bool _fValid;

		// Token: 0x04002208 RID: 8712
		private StoreOperationMetadataProperty _current;
	}
}
