using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002EB RID: 747
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, ControlEvidence = true, ControlPolicy = true)]
	[Serializable]
	public abstract class IsolatedStoragePermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x06002645 RID: 9797 RVA: 0x0008BAD8 File Offset: 0x00089CD8
		protected IsolatedStoragePermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.m_userQuota = long.MaxValue;
				this.m_machineQuota = long.MaxValue;
				this.m_expirationDays = long.MaxValue;
				this.m_permanentData = true;
				this.m_allowed = IsolatedStorageContainment.UnrestrictedIsolatedStorage;
				return;
			}
			if (state == PermissionState.None)
			{
				this.m_userQuota = 0L;
				this.m_machineQuota = 0L;
				this.m_expirationDays = 0L;
				this.m_permanentData = false;
				this.m_allowed = IsolatedStorageContainment.None;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x0008BB68 File Offset: 0x00089D68
		internal IsolatedStoragePermission(IsolatedStorageContainment UsageAllowed, long ExpirationDays, bool PermanentData)
		{
			this.m_userQuota = 0L;
			this.m_machineQuota = 0L;
			this.m_expirationDays = ExpirationDays;
			this.m_permanentData = PermanentData;
			this.m_allowed = UsageAllowed;
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x0008BB95 File Offset: 0x00089D95
		internal IsolatedStoragePermission(IsolatedStorageContainment UsageAllowed, long ExpirationDays, bool PermanentData, long UserQuota)
		{
			this.m_machineQuota = 0L;
			this.m_userQuota = UserQuota;
			this.m_expirationDays = ExpirationDays;
			this.m_permanentData = PermanentData;
			this.m_allowed = UsageAllowed;
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06002649 RID: 9801 RVA: 0x0008BBCB File Offset: 0x00089DCB
		// (set) Token: 0x06002648 RID: 9800 RVA: 0x0008BBC2 File Offset: 0x00089DC2
		public long UserQuota
		{
			get
			{
				return this.m_userQuota;
			}
			set
			{
				this.m_userQuota = value;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x0008BBDC File Offset: 0x00089DDC
		// (set) Token: 0x0600264A RID: 9802 RVA: 0x0008BBD3 File Offset: 0x00089DD3
		public IsolatedStorageContainment UsageAllowed
		{
			get
			{
				return this.m_allowed;
			}
			set
			{
				this.m_allowed = value;
			}
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x0008BBE4 File Offset: 0x00089DE4
		public bool IsUnrestricted()
		{
			return this.m_allowed == IsolatedStorageContainment.UnrestrictedIsolatedStorage;
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x0008BBF3 File Offset: 0x00089DF3
		internal static long min(long x, long y)
		{
			if (x <= y)
			{
				return x;
			}
			return y;
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x0008BBFC File Offset: 0x00089DFC
		internal static long max(long x, long y)
		{
			if (x >= y)
			{
				return x;
			}
			return y;
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x0008BC05 File Offset: 0x00089E05
		public override SecurityElement ToXml()
		{
			return this.ToXml(base.GetType().FullName);
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x0008BC18 File Offset: 0x00089E18
		internal SecurityElement ToXml(string permName)
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, permName);
			if (!this.IsUnrestricted())
			{
				securityElement.AddAttribute("Allowed", Enum.GetName(typeof(IsolatedStorageContainment), this.m_allowed));
				if (this.m_userQuota > 0L)
				{
					securityElement.AddAttribute("UserQuota", this.m_userQuota.ToString(CultureInfo.InvariantCulture));
				}
				if (this.m_machineQuota > 0L)
				{
					securityElement.AddAttribute("MachineQuota", this.m_machineQuota.ToString(CultureInfo.InvariantCulture));
				}
				if (this.m_expirationDays > 0L)
				{
					securityElement.AddAttribute("Expiry", this.m_expirationDays.ToString(CultureInfo.InvariantCulture));
				}
				if (this.m_permanentData)
				{
					securityElement.AddAttribute("Permanent", this.m_permanentData.ToString());
				}
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x0008BD00 File Offset: 0x00089F00
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			this.m_allowed = IsolatedStorageContainment.None;
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.m_allowed = IsolatedStorageContainment.UnrestrictedIsolatedStorage;
			}
			else
			{
				string text = esd.Attribute("Allowed");
				if (text != null)
				{
					this.m_allowed = (IsolatedStorageContainment)Enum.Parse(typeof(IsolatedStorageContainment), text);
				}
			}
			if (this.m_allowed == IsolatedStorageContainment.UnrestrictedIsolatedStorage)
			{
				this.m_userQuota = long.MaxValue;
				this.m_machineQuota = long.MaxValue;
				this.m_expirationDays = long.MaxValue;
				this.m_permanentData = true;
				return;
			}
			string text2 = esd.Attribute("UserQuota");
			this.m_userQuota = ((text2 != null) ? long.Parse(text2, CultureInfo.InvariantCulture) : 0L);
			text2 = esd.Attribute("MachineQuota");
			this.m_machineQuota = ((text2 != null) ? long.Parse(text2, CultureInfo.InvariantCulture) : 0L);
			text2 = esd.Attribute("Expiry");
			this.m_expirationDays = ((text2 != null) ? long.Parse(text2, CultureInfo.InvariantCulture) : 0L);
			text2 = esd.Attribute("Permanent");
			this.m_permanentData = text2 != null && bool.Parse(text2);
		}

		// Token: 0x04000ED1 RID: 3793
		internal long m_userQuota;

		// Token: 0x04000ED2 RID: 3794
		internal long m_machineQuota;

		// Token: 0x04000ED3 RID: 3795
		internal long m_expirationDays;

		// Token: 0x04000ED4 RID: 3796
		internal bool m_permanentData;

		// Token: 0x04000ED5 RID: 3797
		internal IsolatedStorageContainment m_allowed;

		// Token: 0x04000ED6 RID: 3798
		private const string _strUserQuota = "UserQuota";

		// Token: 0x04000ED7 RID: 3799
		private const string _strMachineQuota = "MachineQuota";

		// Token: 0x04000ED8 RID: 3800
		private const string _strExpiry = "Expiry";

		// Token: 0x04000ED9 RID: 3801
		private const string _strPermDat = "Permanent";
	}
}
