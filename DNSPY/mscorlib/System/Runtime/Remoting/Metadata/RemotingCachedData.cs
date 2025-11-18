using System;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007D2 RID: 2002
	internal abstract class RemotingCachedData
	{
		// Token: 0x060056D0 RID: 22224 RVA: 0x001341C8 File Offset: 0x001323C8
		internal SoapAttribute GetSoapAttribute()
		{
			if (this._soapAttr == null)
			{
				lock (this)
				{
					if (this._soapAttr == null)
					{
						this._soapAttr = this.GetSoapAttributeNoLock();
					}
				}
			}
			return this._soapAttr;
		}

		// Token: 0x060056D1 RID: 22225
		internal abstract SoapAttribute GetSoapAttributeNoLock();

		// Token: 0x060056D2 RID: 22226 RVA: 0x00134220 File Offset: 0x00132420
		protected RemotingCachedData()
		{
		}

		// Token: 0x040027BD RID: 10173
		private SoapAttribute _soapAttr;
	}
}
