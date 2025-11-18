using System;
using System.Reflection;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007D4 RID: 2004
	internal class RemotingParameterCachedData : RemotingCachedData
	{
		// Token: 0x060056D6 RID: 22230 RVA: 0x00134293 File Offset: 0x00132493
		internal RemotingParameterCachedData(RuntimeParameterInfo ri)
		{
			this.RI = ri;
		}

		// Token: 0x060056D7 RID: 22231 RVA: 0x001342A4 File Offset: 0x001324A4
		internal override SoapAttribute GetSoapAttributeNoLock()
		{
			object[] customAttributes = this.RI.GetCustomAttributes(typeof(SoapParameterAttribute), true);
			SoapAttribute soapAttribute;
			if (customAttributes != null && customAttributes.Length != 0)
			{
				soapAttribute = (SoapParameterAttribute)customAttributes[0];
			}
			else
			{
				soapAttribute = new SoapParameterAttribute();
			}
			soapAttribute.SetReflectInfo(this.RI);
			return soapAttribute;
		}

		// Token: 0x040027BF RID: 10175
		private RuntimeParameterInfo RI;
	}
}
