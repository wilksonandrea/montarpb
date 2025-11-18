using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x02000767 RID: 1895
	[ComVisible(true)]
	[Serializable]
	public class SoapMessage : ISoapMessage
	{
		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x0600531B RID: 21275 RVA: 0x00123ADA File Offset: 0x00121CDA
		// (set) Token: 0x0600531C RID: 21276 RVA: 0x00123AE2 File Offset: 0x00121CE2
		public string[] ParamNames
		{
			get
			{
				return this.paramNames;
			}
			set
			{
				this.paramNames = value;
			}
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x0600531D RID: 21277 RVA: 0x00123AEB File Offset: 0x00121CEB
		// (set) Token: 0x0600531E RID: 21278 RVA: 0x00123AF3 File Offset: 0x00121CF3
		public object[] ParamValues
		{
			get
			{
				return this.paramValues;
			}
			set
			{
				this.paramValues = value;
			}
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x0600531F RID: 21279 RVA: 0x00123AFC File Offset: 0x00121CFC
		// (set) Token: 0x06005320 RID: 21280 RVA: 0x00123B04 File Offset: 0x00121D04
		public Type[] ParamTypes
		{
			get
			{
				return this.paramTypes;
			}
			set
			{
				this.paramTypes = value;
			}
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x06005321 RID: 21281 RVA: 0x00123B0D File Offset: 0x00121D0D
		// (set) Token: 0x06005322 RID: 21282 RVA: 0x00123B15 File Offset: 0x00121D15
		public string MethodName
		{
			get
			{
				return this.methodName;
			}
			set
			{
				this.methodName = value;
			}
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06005323 RID: 21283 RVA: 0x00123B1E File Offset: 0x00121D1E
		// (set) Token: 0x06005324 RID: 21284 RVA: 0x00123B26 File Offset: 0x00121D26
		public string XmlNameSpace
		{
			get
			{
				return this.xmlNameSpace;
			}
			set
			{
				this.xmlNameSpace = value;
			}
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06005325 RID: 21285 RVA: 0x00123B2F File Offset: 0x00121D2F
		// (set) Token: 0x06005326 RID: 21286 RVA: 0x00123B37 File Offset: 0x00121D37
		public Header[] Headers
		{
			get
			{
				return this.headers;
			}
			set
			{
				this.headers = value;
			}
		}

		// Token: 0x06005327 RID: 21287 RVA: 0x00123B40 File Offset: 0x00121D40
		public SoapMessage()
		{
		}

		// Token: 0x040024DE RID: 9438
		internal string[] paramNames;

		// Token: 0x040024DF RID: 9439
		internal object[] paramValues;

		// Token: 0x040024E0 RID: 9440
		internal Type[] paramTypes;

		// Token: 0x040024E1 RID: 9441
		internal string methodName;

		// Token: 0x040024E2 RID: 9442
		internal string xmlNameSpace;

		// Token: 0x040024E3 RID: 9443
		internal Header[] headers;
	}
}
