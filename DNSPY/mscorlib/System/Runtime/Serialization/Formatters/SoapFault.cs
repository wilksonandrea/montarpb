using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x02000768 RID: 1896
	[SoapType(Embedded = true)]
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapFault : ISerializable
	{
		// Token: 0x06005328 RID: 21288 RVA: 0x00123B48 File Offset: 0x00121D48
		public SoapFault()
		{
		}

		// Token: 0x06005329 RID: 21289 RVA: 0x00123B50 File Offset: 0x00121D50
		public SoapFault(string faultCode, string faultString, string faultActor, ServerFault serverFault)
		{
			this.faultCode = faultCode;
			this.faultString = faultString;
			this.faultActor = faultActor;
			this.detail = serverFault;
		}

		// Token: 0x0600532A RID: 21290 RVA: 0x00123B78 File Offset: 0x00121D78
		internal SoapFault(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				object value = enumerator.Value;
				if (string.Compare(name, "faultCode", true, CultureInfo.InvariantCulture) == 0)
				{
					int num = ((string)value).IndexOf(':');
					if (num > -1)
					{
						this.faultCode = ((string)value).Substring(num + 1);
					}
					else
					{
						this.faultCode = (string)value;
					}
				}
				else if (string.Compare(name, "faultString", true, CultureInfo.InvariantCulture) == 0)
				{
					this.faultString = (string)value;
				}
				else if (string.Compare(name, "faultActor", true, CultureInfo.InvariantCulture) == 0)
				{
					this.faultActor = (string)value;
				}
				else if (string.Compare(name, "detail", true, CultureInfo.InvariantCulture) == 0)
				{
					this.detail = value;
				}
			}
		}

		// Token: 0x0600532B RID: 21291 RVA: 0x00123C58 File Offset: 0x00121E58
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("faultcode", "SOAP-ENV:" + this.faultCode);
			info.AddValue("faultstring", this.faultString);
			if (this.faultActor != null)
			{
				info.AddValue("faultactor", this.faultActor);
			}
			info.AddValue("detail", this.detail, typeof(object));
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x0600532C RID: 21292 RVA: 0x00123CC5 File Offset: 0x00121EC5
		// (set) Token: 0x0600532D RID: 21293 RVA: 0x00123CCD File Offset: 0x00121ECD
		public string FaultCode
		{
			get
			{
				return this.faultCode;
			}
			set
			{
				this.faultCode = value;
			}
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x0600532E RID: 21294 RVA: 0x00123CD6 File Offset: 0x00121ED6
		// (set) Token: 0x0600532F RID: 21295 RVA: 0x00123CDE File Offset: 0x00121EDE
		public string FaultString
		{
			get
			{
				return this.faultString;
			}
			set
			{
				this.faultString = value;
			}
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06005330 RID: 21296 RVA: 0x00123CE7 File Offset: 0x00121EE7
		// (set) Token: 0x06005331 RID: 21297 RVA: 0x00123CEF File Offset: 0x00121EEF
		public string FaultActor
		{
			get
			{
				return this.faultActor;
			}
			set
			{
				this.faultActor = value;
			}
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06005332 RID: 21298 RVA: 0x00123CF8 File Offset: 0x00121EF8
		// (set) Token: 0x06005333 RID: 21299 RVA: 0x00123D00 File Offset: 0x00121F00
		public object Detail
		{
			get
			{
				return this.detail;
			}
			set
			{
				this.detail = value;
			}
		}

		// Token: 0x040024E4 RID: 9444
		private string faultCode;

		// Token: 0x040024E5 RID: 9445
		private string faultString;

		// Token: 0x040024E6 RID: 9446
		private string faultActor;

		// Token: 0x040024E7 RID: 9447
		[SoapField(Embedded = true)]
		private object detail;
	}
}
