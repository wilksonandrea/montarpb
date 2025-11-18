using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x02000372 RID: 882
	[ComVisible(true)]
	[Serializable]
	public sealed class ZoneMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
	{
		// Token: 0x06002BB0 RID: 11184 RVA: 0x000A29A7 File Offset: 0x000A0BA7
		internal ZoneMembershipCondition()
		{
			this.m_zone = SecurityZone.NoZone;
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x000A29B6 File Offset: 0x000A0BB6
		public ZoneMembershipCondition(SecurityZone zone)
		{
			ZoneMembershipCondition.VerifyZone(zone);
			this.SecurityZone = zone;
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06002BB3 RID: 11187 RVA: 0x000A29DA File Offset: 0x000A0BDA
		// (set) Token: 0x06002BB2 RID: 11186 RVA: 0x000A29CB File Offset: 0x000A0BCB
		public SecurityZone SecurityZone
		{
			get
			{
				if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
				{
					this.ParseZone();
				}
				return this.m_zone;
			}
			set
			{
				ZoneMembershipCondition.VerifyZone(value);
				this.m_zone = value;
			}
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000A29F9 File Offset: 0x000A0BF9
		private static void VerifyZone(SecurityZone zone)
		{
			if (zone < SecurityZone.MyComputer || zone > SecurityZone.Untrusted)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IllegalZone"));
			}
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x000A2A14 File Offset: 0x000A0C14
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x000A2A2C File Offset: 0x000A0C2C
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			if (evidence == null)
			{
				return false;
			}
			Zone hostEvidence = evidence.GetHostEvidence<Zone>();
			if (hostEvidence != null)
			{
				if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
				{
					this.ParseZone();
				}
				if (hostEvidence.SecurityZone == this.m_zone)
				{
					usedEvidence = hostEvidence;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x000A2A76 File Offset: 0x000A0C76
		public IMembershipCondition Copy()
		{
			if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
			{
				this.ParseZone();
			}
			return new ZoneMembershipCondition(this.m_zone);
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x000A2A9A File Offset: 0x000A0C9A
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x000A2AA3 File Offset: 0x000A0CA3
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x000A2AB0 File Offset: 0x000A0CB0
		public SecurityElement ToXml(PolicyLevel level)
		{
			if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
			{
				this.ParseZone();
			}
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), "System.Security.Policy.ZoneMembershipCondition");
			securityElement.AddAttribute("version", "1");
			if (this.m_zone != SecurityZone.NoZone)
			{
				securityElement.AddAttribute("Zone", Enum.GetName(typeof(SecurityZone), this.m_zone));
			}
			return securityElement;
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x000A2B30 File Offset: 0x000A0D30
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (!e.Tag.Equals("IMembershipCondition"))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"));
			}
			lock (this)
			{
				this.m_zone = SecurityZone.NoZone;
				this.m_element = e;
			}
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x000A2BA4 File Offset: 0x000A0DA4
		private void ParseZone()
		{
			lock (this)
			{
				if (this.m_element != null)
				{
					string text = this.m_element.Attribute("Zone");
					this.m_zone = SecurityZone.NoZone;
					if (text == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_ZoneCannotBeNull"));
					}
					this.m_zone = (SecurityZone)Enum.Parse(typeof(SecurityZone), text);
					ZoneMembershipCondition.VerifyZone(this.m_zone);
					this.m_element = null;
				}
			}
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x000A2C40 File Offset: 0x000A0E40
		public override bool Equals(object o)
		{
			ZoneMembershipCondition zoneMembershipCondition = o as ZoneMembershipCondition;
			if (zoneMembershipCondition != null)
			{
				if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
				{
					this.ParseZone();
				}
				if (zoneMembershipCondition.m_zone == SecurityZone.NoZone && zoneMembershipCondition.m_element != null)
				{
					zoneMembershipCondition.ParseZone();
				}
				if (this.m_zone == zoneMembershipCondition.m_zone)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000A2C96 File Offset: 0x000A0E96
		public override int GetHashCode()
		{
			if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
			{
				this.ParseZone();
			}
			return (int)this.m_zone;
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x000A2CB5 File Offset: 0x000A0EB5
		public override string ToString()
		{
			if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
			{
				this.ParseZone();
			}
			return string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Zone_ToString"), ZoneMembershipCondition.s_names[(int)this.m_zone]);
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x000A2CEE File Offset: 0x000A0EEE
		// Note: this type is marked as 'beforefieldinit'.
		static ZoneMembershipCondition()
		{
		}

		// Token: 0x040011AE RID: 4526
		private static readonly string[] s_names = new string[] { "MyComputer", "Intranet", "Trusted", "Internet", "Untrusted" };

		// Token: 0x040011AF RID: 4527
		private SecurityZone m_zone;

		// Token: 0x040011B0 RID: 4528
		private SecurityElement m_element;
	}
}
