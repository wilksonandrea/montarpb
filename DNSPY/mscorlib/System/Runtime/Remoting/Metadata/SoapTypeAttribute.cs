using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007D9 RID: 2009
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	[ComVisible(true)]
	public sealed class SoapTypeAttribute : SoapAttribute
	{
		// Token: 0x060056F0 RID: 22256 RVA: 0x00134796 File Offset: 0x00132996
		internal bool IsInteropXmlElement()
		{
			return (this._explicitlySet & (SoapTypeAttribute.ExplicitlySet.XmlElementName | SoapTypeAttribute.ExplicitlySet.XmlNamespace)) > SoapTypeAttribute.ExplicitlySet.None;
		}

		// Token: 0x060056F1 RID: 22257 RVA: 0x001347A3 File Offset: 0x001329A3
		internal bool IsInteropXmlType()
		{
			return (this._explicitlySet & (SoapTypeAttribute.ExplicitlySet.XmlTypeName | SoapTypeAttribute.ExplicitlySet.XmlTypeNamespace)) > SoapTypeAttribute.ExplicitlySet.None;
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x060056F2 RID: 22258 RVA: 0x001347B1 File Offset: 0x001329B1
		// (set) Token: 0x060056F3 RID: 22259 RVA: 0x001347B9 File Offset: 0x001329B9
		public SoapOption SoapOptions
		{
			get
			{
				return this._SoapOptions;
			}
			set
			{
				this._SoapOptions = value;
			}
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x060056F4 RID: 22260 RVA: 0x001347C2 File Offset: 0x001329C2
		// (set) Token: 0x060056F5 RID: 22261 RVA: 0x001347F0 File Offset: 0x001329F0
		public string XmlElementName
		{
			get
			{
				if (this._XmlElementName == null && this.ReflectInfo != null)
				{
					this._XmlElementName = SoapTypeAttribute.GetTypeName((Type)this.ReflectInfo);
				}
				return this._XmlElementName;
			}
			set
			{
				this._XmlElementName = value;
				this._explicitlySet |= SoapTypeAttribute.ExplicitlySet.XmlElementName;
			}
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x060056F6 RID: 22262 RVA: 0x00134807 File Offset: 0x00132A07
		// (set) Token: 0x060056F7 RID: 22263 RVA: 0x0013482B File Offset: 0x00132A2B
		public override string XmlNamespace
		{
			get
			{
				if (this.ProtXmlNamespace == null && this.ReflectInfo != null)
				{
					this.ProtXmlNamespace = this.XmlTypeNamespace;
				}
				return this.ProtXmlNamespace;
			}
			set
			{
				this.ProtXmlNamespace = value;
				this._explicitlySet |= SoapTypeAttribute.ExplicitlySet.XmlNamespace;
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x060056F8 RID: 22264 RVA: 0x00134842 File Offset: 0x00132A42
		// (set) Token: 0x060056F9 RID: 22265 RVA: 0x00134870 File Offset: 0x00132A70
		public string XmlTypeName
		{
			get
			{
				if (this._XmlTypeName == null && this.ReflectInfo != null)
				{
					this._XmlTypeName = SoapTypeAttribute.GetTypeName((Type)this.ReflectInfo);
				}
				return this._XmlTypeName;
			}
			set
			{
				this._XmlTypeName = value;
				this._explicitlySet |= SoapTypeAttribute.ExplicitlySet.XmlTypeName;
			}
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x060056FA RID: 22266 RVA: 0x00134887 File Offset: 0x00132A87
		// (set) Token: 0x060056FB RID: 22267 RVA: 0x001348B6 File Offset: 0x00132AB6
		public string XmlTypeNamespace
		{
			[SecuritySafeCritical]
			get
			{
				if (this._XmlTypeNamespace == null && this.ReflectInfo != null)
				{
					this._XmlTypeNamespace = XmlNamespaceEncoder.GetXmlNamespaceForTypeNamespace((RuntimeType)this.ReflectInfo, null);
				}
				return this._XmlTypeNamespace;
			}
			set
			{
				this._XmlTypeNamespace = value;
				this._explicitlySet |= SoapTypeAttribute.ExplicitlySet.XmlTypeNamespace;
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x060056FC RID: 22268 RVA: 0x001348CD File Offset: 0x00132ACD
		// (set) Token: 0x060056FD RID: 22269 RVA: 0x001348D5 File Offset: 0x00132AD5
		public XmlFieldOrderOption XmlFieldOrder
		{
			get
			{
				return this._XmlFieldOrder;
			}
			set
			{
				this._XmlFieldOrder = value;
			}
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x060056FE RID: 22270 RVA: 0x001348DE File Offset: 0x00132ADE
		// (set) Token: 0x060056FF RID: 22271 RVA: 0x001348E1 File Offset: 0x00132AE1
		public override bool UseAttribute
		{
			get
			{
				return false;
			}
			set
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Attribute_UseAttributeNotsettable"));
			}
		}

		// Token: 0x06005700 RID: 22272 RVA: 0x001348F4 File Offset: 0x00132AF4
		private static string GetTypeName(Type t)
		{
			if (!t.IsNested)
			{
				return t.Name;
			}
			string fullName = t.FullName;
			string @namespace = t.Namespace;
			if (@namespace == null || @namespace.Length == 0)
			{
				return fullName;
			}
			return fullName.Substring(@namespace.Length + 1);
		}

		// Token: 0x06005701 RID: 22273 RVA: 0x0013493B File Offset: 0x00132B3B
		public SoapTypeAttribute()
		{
		}

		// Token: 0x040027DD RID: 10205
		private SoapTypeAttribute.ExplicitlySet _explicitlySet;

		// Token: 0x040027DE RID: 10206
		private SoapOption _SoapOptions;

		// Token: 0x040027DF RID: 10207
		private string _XmlElementName;

		// Token: 0x040027E0 RID: 10208
		private string _XmlTypeName;

		// Token: 0x040027E1 RID: 10209
		private string _XmlTypeNamespace;

		// Token: 0x040027E2 RID: 10210
		private XmlFieldOrderOption _XmlFieldOrder;

		// Token: 0x02000C72 RID: 3186
		[Flags]
		[Serializable]
		private enum ExplicitlySet
		{
			// Token: 0x040037F7 RID: 14327
			None = 0,
			// Token: 0x040037F8 RID: 14328
			XmlElementName = 1,
			// Token: 0x040037F9 RID: 14329
			XmlNamespace = 2,
			// Token: 0x040037FA RID: 14330
			XmlTypeName = 4,
			// Token: 0x040037FB RID: 14331
			XmlTypeNamespace = 8
		}
	}
}
