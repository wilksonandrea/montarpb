using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007DB RID: 2011
	[AttributeUsage(AttributeTargets.Field)]
	[ComVisible(true)]
	public sealed class SoapFieldAttribute : SoapAttribute
	{
		// Token: 0x06005711 RID: 22289 RVA: 0x00134A90 File Offset: 0x00132C90
		public bool IsInteropXmlElement()
		{
			return (this._explicitlySet & SoapFieldAttribute.ExplicitlySet.XmlElementName) > SoapFieldAttribute.ExplicitlySet.None;
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06005712 RID: 22290 RVA: 0x00134A9D File Offset: 0x00132C9D
		// (set) Token: 0x06005713 RID: 22291 RVA: 0x00134ACB File Offset: 0x00132CCB
		public string XmlElementName
		{
			get
			{
				if (this._xmlElementName == null && this.ReflectInfo != null)
				{
					this._xmlElementName = ((FieldInfo)this.ReflectInfo).Name;
				}
				return this._xmlElementName;
			}
			set
			{
				this._xmlElementName = value;
				this._explicitlySet |= SoapFieldAttribute.ExplicitlySet.XmlElementName;
			}
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06005714 RID: 22292 RVA: 0x00134AE2 File Offset: 0x00132CE2
		// (set) Token: 0x06005715 RID: 22293 RVA: 0x00134AEA File Offset: 0x00132CEA
		public int Order
		{
			get
			{
				return this._order;
			}
			set
			{
				this._order = value;
			}
		}

		// Token: 0x06005716 RID: 22294 RVA: 0x00134AF3 File Offset: 0x00132CF3
		public SoapFieldAttribute()
		{
		}

		// Token: 0x040027E8 RID: 10216
		private SoapFieldAttribute.ExplicitlySet _explicitlySet;

		// Token: 0x040027E9 RID: 10217
		private string _xmlElementName;

		// Token: 0x040027EA RID: 10218
		private int _order;

		// Token: 0x02000C73 RID: 3187
		[Flags]
		[Serializable]
		private enum ExplicitlySet
		{
			// Token: 0x040037FD RID: 14333
			None = 0,
			// Token: 0x040037FE RID: 14334
			XmlElementName = 1
		}
	}
}
