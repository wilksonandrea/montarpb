using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007D3 RID: 2003
	internal class RemotingFieldCachedData : RemotingCachedData
	{
		// Token: 0x060056D3 RID: 22227 RVA: 0x00134228 File Offset: 0x00132428
		internal RemotingFieldCachedData(RuntimeFieldInfo ri)
		{
			this.RI = ri;
		}

		// Token: 0x060056D4 RID: 22228 RVA: 0x00134237 File Offset: 0x00132437
		internal RemotingFieldCachedData(SerializationFieldInfo ri)
		{
			this.RI = ri;
		}

		// Token: 0x060056D5 RID: 22229 RVA: 0x00134248 File Offset: 0x00132448
		internal override SoapAttribute GetSoapAttributeNoLock()
		{
			object[] customAttributes = this.RI.GetCustomAttributes(typeof(SoapFieldAttribute), false);
			SoapAttribute soapAttribute;
			if (customAttributes != null && customAttributes.Length != 0)
			{
				soapAttribute = (SoapAttribute)customAttributes[0];
			}
			else
			{
				soapAttribute = new SoapFieldAttribute();
			}
			soapAttribute.SetReflectInfo(this.RI);
			return soapAttribute;
		}

		// Token: 0x040027BE RID: 10174
		private FieldInfo RI;
	}
}
