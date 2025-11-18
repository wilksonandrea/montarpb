using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x02000346 RID: 838
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationTrust : EvidenceBase, ISecurityEncodable
	{
		// Token: 0x06002997 RID: 10647 RVA: 0x0009976B File Offset: 0x0009796B
		public ApplicationTrust(ApplicationIdentity applicationIdentity)
			: this()
		{
			this.ApplicationIdentity = applicationIdentity;
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x0009977A File Offset: 0x0009797A
		public ApplicationTrust()
			: this(new PermissionSet(PermissionState.None))
		{
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x00099788 File Offset: 0x00097988
		internal ApplicationTrust(PermissionSet defaultGrantSet)
		{
			this.InitDefaultGrantSet(defaultGrantSet);
			this.m_fullTrustAssemblies = new List<StrongName>().AsReadOnly();
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x000997A8 File Offset: 0x000979A8
		public ApplicationTrust(PermissionSet defaultGrantSet, IEnumerable<StrongName> fullTrustAssemblies)
		{
			if (fullTrustAssemblies == null)
			{
				throw new ArgumentNullException("fullTrustAssemblies");
			}
			this.InitDefaultGrantSet(defaultGrantSet);
			List<StrongName> list = new List<StrongName>();
			foreach (StrongName strongName in fullTrustAssemblies)
			{
				if (strongName == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_NullFullTrustAssembly"));
				}
				list.Add(new StrongName(strongName.PublicKey, strongName.Name, strongName.Version));
			}
			this.m_fullTrustAssemblies = list.AsReadOnly();
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x00099848 File Offset: 0x00097A48
		private void InitDefaultGrantSet(PermissionSet defaultGrantSet)
		{
			if (defaultGrantSet == null)
			{
				throw new ArgumentNullException("defaultGrantSet");
			}
			this.DefaultGrantSet = new PolicyStatement(defaultGrantSet);
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x0600299C RID: 10652 RVA: 0x00099864 File Offset: 0x00097A64
		// (set) Token: 0x0600299D RID: 10653 RVA: 0x0009986C File Offset: 0x00097A6C
		public ApplicationIdentity ApplicationIdentity
		{
			get
			{
				return this.m_appId;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(Environment.GetResourceString("Argument_InvalidAppId"));
				}
				this.m_appId = value;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600299E RID: 10654 RVA: 0x00099888 File Offset: 0x00097A88
		// (set) Token: 0x0600299F RID: 10655 RVA: 0x000998A4 File Offset: 0x00097AA4
		public PolicyStatement DefaultGrantSet
		{
			get
			{
				if (this.m_psDefaultGrant == null)
				{
					return new PolicyStatement(new PermissionSet(PermissionState.None));
				}
				return this.m_psDefaultGrant;
			}
			set
			{
				if (value == null)
				{
					this.m_psDefaultGrant = null;
					this.m_grantSetSpecialFlags = 0;
					return;
				}
				this.m_psDefaultGrant = value;
				this.m_grantSetSpecialFlags = SecurityManager.GetSpecialFlags(this.m_psDefaultGrant.PermissionSet, null);
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060029A0 RID: 10656 RVA: 0x000998D6 File Offset: 0x00097AD6
		public IList<StrongName> FullTrustAssemblies
		{
			get
			{
				return this.m_fullTrustAssemblies;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060029A1 RID: 10657 RVA: 0x000998DE File Offset: 0x00097ADE
		// (set) Token: 0x060029A2 RID: 10658 RVA: 0x000998E6 File Offset: 0x00097AE6
		public bool IsApplicationTrustedToRun
		{
			get
			{
				return this.m_appTrustedToRun;
			}
			set
			{
				this.m_appTrustedToRun = value;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060029A3 RID: 10659 RVA: 0x000998EF File Offset: 0x00097AEF
		// (set) Token: 0x060029A4 RID: 10660 RVA: 0x000998F7 File Offset: 0x00097AF7
		public bool Persist
		{
			get
			{
				return this.m_persist;
			}
			set
			{
				this.m_persist = value;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060029A5 RID: 10661 RVA: 0x00099900 File Offset: 0x00097B00
		// (set) Token: 0x060029A6 RID: 10662 RVA: 0x00099928 File Offset: 0x00097B28
		public object ExtraInfo
		{
			get
			{
				if (this.m_elExtraInfo != null)
				{
					this.m_extraInfo = ApplicationTrust.ObjectFromXml(this.m_elExtraInfo);
					this.m_elExtraInfo = null;
				}
				return this.m_extraInfo;
			}
			set
			{
				this.m_elExtraInfo = null;
				this.m_extraInfo = value;
			}
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x00099938 File Offset: 0x00097B38
		public SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("ApplicationTrust");
			securityElement.AddAttribute("version", "1");
			if (this.m_appId != null)
			{
				securityElement.AddAttribute("FullName", SecurityElement.Escape(this.m_appId.FullName));
			}
			if (this.m_appTrustedToRun)
			{
				securityElement.AddAttribute("TrustedToRun", "true");
			}
			if (this.m_persist)
			{
				securityElement.AddAttribute("Persist", "true");
			}
			if (this.m_psDefaultGrant != null)
			{
				SecurityElement securityElement2 = new SecurityElement("DefaultGrant");
				securityElement2.AddChild(this.m_psDefaultGrant.ToXml());
				securityElement.AddChild(securityElement2);
			}
			if (this.m_fullTrustAssemblies.Count > 0)
			{
				SecurityElement securityElement3 = new SecurityElement("FullTrustAssemblies");
				foreach (StrongName strongName in this.m_fullTrustAssemblies)
				{
					securityElement3.AddChild(strongName.ToXml());
				}
				securityElement.AddChild(securityElement3);
			}
			if (this.ExtraInfo != null)
			{
				securityElement.AddChild(ApplicationTrust.ObjectToXml("ExtraInfo", this.ExtraInfo));
			}
			return securityElement;
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x00099A64 File Offset: 0x00097C64
		public void FromXml(SecurityElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (string.Compare(element.Tag, "ApplicationTrust", StringComparison.Ordinal) != 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
			}
			this.m_appTrustedToRun = false;
			string text = element.Attribute("TrustedToRun");
			if (text != null && string.Compare(text, "true", StringComparison.Ordinal) == 0)
			{
				this.m_appTrustedToRun = true;
			}
			this.m_persist = false;
			string text2 = element.Attribute("Persist");
			if (text2 != null && string.Compare(text2, "true", StringComparison.Ordinal) == 0)
			{
				this.m_persist = true;
			}
			this.m_appId = null;
			string text3 = element.Attribute("FullName");
			if (text3 != null && text3.Length > 0)
			{
				this.m_appId = new ApplicationIdentity(text3);
			}
			this.m_psDefaultGrant = null;
			this.m_grantSetSpecialFlags = 0;
			SecurityElement securityElement = element.SearchForChildByTag("DefaultGrant");
			if (securityElement != null)
			{
				SecurityElement securityElement2 = securityElement.SearchForChildByTag("PolicyStatement");
				if (securityElement2 != null)
				{
					PolicyStatement policyStatement = new PolicyStatement(null);
					policyStatement.FromXml(securityElement2);
					this.m_psDefaultGrant = policyStatement;
					this.m_grantSetSpecialFlags = SecurityManager.GetSpecialFlags(policyStatement.PermissionSet, null);
				}
			}
			List<StrongName> list = new List<StrongName>();
			SecurityElement securityElement3 = element.SearchForChildByTag("FullTrustAssemblies");
			if (securityElement3 != null && securityElement3.InternalChildren != null)
			{
				IEnumerator enumerator = securityElement3.Children.GetEnumerator();
				while (enumerator.MoveNext())
				{
					StrongName strongName = new StrongName();
					strongName.FromXml(enumerator.Current as SecurityElement);
					list.Add(strongName);
				}
			}
			this.m_fullTrustAssemblies = list.AsReadOnly();
			this.m_elExtraInfo = element.SearchForChildByTag("ExtraInfo");
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x00099BF8 File Offset: 0x00097DF8
		private static SecurityElement ObjectToXml(string tag, object obj)
		{
			ISecurityEncodable securityEncodable = obj as ISecurityEncodable;
			SecurityElement securityElement;
			if (securityEncodable != null)
			{
				securityElement = securityEncodable.ToXml();
				if (!securityElement.Tag.Equals(tag))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
				}
			}
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, obj);
			byte[] array = memoryStream.ToArray();
			securityElement = new SecurityElement(tag);
			securityElement.AddAttribute("Data", Hex.EncodeHexString(array));
			return securityElement;
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x00099C6C File Offset: 0x00097E6C
		private static object ObjectFromXml(SecurityElement elObject)
		{
			if (elObject.Attribute("class") != null)
			{
				ISecurityEncodable securityEncodable = XMLUtil.CreateCodeGroup(elObject) as ISecurityEncodable;
				if (securityEncodable != null)
				{
					securityEncodable.FromXml(elObject);
					return securityEncodable;
				}
			}
			string text = elObject.Attribute("Data");
			MemoryStream memoryStream = new MemoryStream(Hex.DecodeHexString(text));
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			return binaryFormatter.Deserialize(memoryStream);
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x00099CC3 File Offset: 0x00097EC3
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override EvidenceBase Clone()
		{
			return base.Clone();
		}

		// Token: 0x04001115 RID: 4373
		private ApplicationIdentity m_appId;

		// Token: 0x04001116 RID: 4374
		private bool m_appTrustedToRun;

		// Token: 0x04001117 RID: 4375
		private bool m_persist;

		// Token: 0x04001118 RID: 4376
		private object m_extraInfo;

		// Token: 0x04001119 RID: 4377
		private SecurityElement m_elExtraInfo;

		// Token: 0x0400111A RID: 4378
		private PolicyStatement m_psDefaultGrant;

		// Token: 0x0400111B RID: 4379
		private IList<StrongName> m_fullTrustAssemblies;

		// Token: 0x0400111C RID: 4380
		[NonSerialized]
		private int m_grantSetSpecialFlags;
	}
}
