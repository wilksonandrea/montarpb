using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007DA RID: 2010
	[AttributeUsage(AttributeTargets.Method)]
	[ComVisible(true)]
	public sealed class SoapMethodAttribute : SoapAttribute
	{
		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06005702 RID: 22274 RVA: 0x00134943 File Offset: 0x00132B43
		internal bool SoapActionExplicitySet
		{
			get
			{
				return this._bSoapActionExplicitySet;
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06005703 RID: 22275 RVA: 0x0013494B File Offset: 0x00132B4B
		// (set) Token: 0x06005704 RID: 22276 RVA: 0x00134981 File Offset: 0x00132B81
		public string SoapAction
		{
			[SecuritySafeCritical]
			get
			{
				if (this._SoapAction == null)
				{
					this._SoapAction = this.XmlTypeNamespaceOfDeclaringType + "#" + ((MemberInfo)this.ReflectInfo).Name;
				}
				return this._SoapAction;
			}
			set
			{
				this._SoapAction = value;
				this._bSoapActionExplicitySet = true;
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06005705 RID: 22277 RVA: 0x00134991 File Offset: 0x00132B91
		// (set) Token: 0x06005706 RID: 22278 RVA: 0x00134994 File Offset: 0x00132B94
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

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06005707 RID: 22279 RVA: 0x001349A5 File Offset: 0x00132BA5
		// (set) Token: 0x06005708 RID: 22280 RVA: 0x001349C1 File Offset: 0x00132BC1
		public override string XmlNamespace
		{
			[SecuritySafeCritical]
			get
			{
				if (this.ProtXmlNamespace == null)
				{
					this.ProtXmlNamespace = this.XmlTypeNamespaceOfDeclaringType;
				}
				return this.ProtXmlNamespace;
			}
			set
			{
				this.ProtXmlNamespace = value;
			}
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06005709 RID: 22281 RVA: 0x001349CA File Offset: 0x00132BCA
		// (set) Token: 0x0600570A RID: 22282 RVA: 0x00134A02 File Offset: 0x00132C02
		public string ResponseXmlElementName
		{
			get
			{
				if (this._responseXmlElementName == null && this.ReflectInfo != null)
				{
					this._responseXmlElementName = ((MemberInfo)this.ReflectInfo).Name + "Response";
				}
				return this._responseXmlElementName;
			}
			set
			{
				this._responseXmlElementName = value;
			}
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x0600570B RID: 22283 RVA: 0x00134A0B File Offset: 0x00132C0B
		// (set) Token: 0x0600570C RID: 22284 RVA: 0x00134A27 File Offset: 0x00132C27
		public string ResponseXmlNamespace
		{
			get
			{
				if (this._responseXmlNamespace == null)
				{
					this._responseXmlNamespace = this.XmlNamespace;
				}
				return this._responseXmlNamespace;
			}
			set
			{
				this._responseXmlNamespace = value;
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x0600570D RID: 22285 RVA: 0x00134A30 File Offset: 0x00132C30
		// (set) Token: 0x0600570E RID: 22286 RVA: 0x00134A4B File Offset: 0x00132C4B
		public string ReturnXmlElementName
		{
			get
			{
				if (this._returnXmlElementName == null)
				{
					this._returnXmlElementName = "return";
				}
				return this._returnXmlElementName;
			}
			set
			{
				this._returnXmlElementName = value;
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x0600570F RID: 22287 RVA: 0x00134A54 File Offset: 0x00132C54
		private string XmlTypeNamespaceOfDeclaringType
		{
			[SecurityCritical]
			get
			{
				if (this.ReflectInfo != null)
				{
					Type declaringType = ((MemberInfo)this.ReflectInfo).DeclaringType;
					return XmlNamespaceEncoder.GetXmlNamespaceForType((RuntimeType)declaringType, null);
				}
				return null;
			}
		}

		// Token: 0x06005710 RID: 22288 RVA: 0x00134A88 File Offset: 0x00132C88
		public SoapMethodAttribute()
		{
		}

		// Token: 0x040027E3 RID: 10211
		private string _SoapAction;

		// Token: 0x040027E4 RID: 10212
		private string _responseXmlElementName;

		// Token: 0x040027E5 RID: 10213
		private string _responseXmlNamespace;

		// Token: 0x040027E6 RID: 10214
		private string _returnXmlElementName;

		// Token: 0x040027E7 RID: 10215
		private bool _bSoapActionExplicitySet;
	}
}
