using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000684 RID: 1668
	internal class StoreDeploymentMetadataEnumeration : IEnumerator
	{
		// Token: 0x06004F3E RID: 20286 RVA: 0x0011C1C1 File Offset: 0x0011A3C1
		public StoreDeploymentMetadataEnumeration(IEnumSTORE_DEPLOYMENT_METADATA pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004F3F RID: 20287 RVA: 0x0011C1D0 File Offset: 0x0011A3D0
		private IDefinitionAppId GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06004F40 RID: 20288 RVA: 0x0011C1E6 File Offset: 0x0011A3E6
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06004F41 RID: 20289 RVA: 0x0011C1EE File Offset: 0x0011A3EE
		public IDefinitionAppId Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004F42 RID: 20290 RVA: 0x0011C1F6 File Offset: 0x0011A3F6
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004F43 RID: 20291 RVA: 0x0011C1FC File Offset: 0x0011A3FC
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			IDefinitionAppId[] array = new IDefinitionAppId[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = num == 1U;
		}

		// Token: 0x06004F44 RID: 20292 RVA: 0x0011C238 File Offset: 0x0011A438
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04002203 RID: 8707
		private IEnumSTORE_DEPLOYMENT_METADATA _enum;

		// Token: 0x04002204 RID: 8708
		private bool _fValid;

		// Token: 0x04002205 RID: 8709
		private IDefinitionAppId _current;
	}
}
