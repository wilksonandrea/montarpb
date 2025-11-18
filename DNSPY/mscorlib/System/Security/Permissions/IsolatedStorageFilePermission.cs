using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002EC RID: 748
	[ComVisible(true)]
	[Serializable]
	public sealed class IsolatedStorageFilePermission : IsolatedStoragePermission, IBuiltInPermission
	{
		// Token: 0x06002652 RID: 9810 RVA: 0x0008BE26 File Offset: 0x0008A026
		public IsolatedStorageFilePermission(PermissionState state)
			: base(state)
		{
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x0008BE2F File Offset: 0x0008A02F
		internal IsolatedStorageFilePermission(IsolatedStorageContainment UsageAllowed, long ExpirationDays, bool PermanentData)
			: base(UsageAllowed, ExpirationDays, PermanentData)
		{
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x0008BE3C File Offset: 0x0008A03C
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (!base.VerifyType(target))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			IsolatedStorageFilePermission isolatedStorageFilePermission = (IsolatedStorageFilePermission)target;
			if (base.IsUnrestricted() || isolatedStorageFilePermission.IsUnrestricted())
			{
				return new IsolatedStorageFilePermission(PermissionState.Unrestricted);
			}
			return new IsolatedStorageFilePermission(PermissionState.None)
			{
				m_userQuota = IsolatedStoragePermission.max(this.m_userQuota, isolatedStorageFilePermission.m_userQuota),
				m_machineQuota = IsolatedStoragePermission.max(this.m_machineQuota, isolatedStorageFilePermission.m_machineQuota),
				m_expirationDays = IsolatedStoragePermission.max(this.m_expirationDays, isolatedStorageFilePermission.m_expirationDays),
				m_permanentData = (this.m_permanentData || isolatedStorageFilePermission.m_permanentData),
				m_allowed = (IsolatedStorageContainment)IsolatedStoragePermission.max((long)this.m_allowed, (long)isolatedStorageFilePermission.m_allowed)
			};
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x0008BF1C File Offset: 0x0008A11C
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_userQuota == 0L && this.m_machineQuota == 0L && this.m_expirationDays == 0L && !this.m_permanentData && this.m_allowed == IsolatedStorageContainment.None;
			}
			bool flag;
			try
			{
				IsolatedStorageFilePermission isolatedStorageFilePermission = (IsolatedStorageFilePermission)target;
				if (isolatedStorageFilePermission.IsUnrestricted())
				{
					flag = true;
				}
				else
				{
					flag = isolatedStorageFilePermission.m_userQuota >= this.m_userQuota && isolatedStorageFilePermission.m_machineQuota >= this.m_machineQuota && isolatedStorageFilePermission.m_expirationDays >= this.m_expirationDays && (isolatedStorageFilePermission.m_permanentData || !this.m_permanentData) && isolatedStorageFilePermission.m_allowed >= this.m_allowed;
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			return flag;
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x0008BFF4 File Offset: 0x0008A1F4
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (!base.VerifyType(target))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			IsolatedStorageFilePermission isolatedStorageFilePermission = (IsolatedStorageFilePermission)target;
			if (isolatedStorageFilePermission.IsUnrestricted())
			{
				return this.Copy();
			}
			if (base.IsUnrestricted())
			{
				return target.Copy();
			}
			IsolatedStorageFilePermission isolatedStorageFilePermission2 = new IsolatedStorageFilePermission(PermissionState.None);
			isolatedStorageFilePermission2.m_userQuota = IsolatedStoragePermission.min(this.m_userQuota, isolatedStorageFilePermission.m_userQuota);
			isolatedStorageFilePermission2.m_machineQuota = IsolatedStoragePermission.min(this.m_machineQuota, isolatedStorageFilePermission.m_machineQuota);
			isolatedStorageFilePermission2.m_expirationDays = IsolatedStoragePermission.min(this.m_expirationDays, isolatedStorageFilePermission.m_expirationDays);
			isolatedStorageFilePermission2.m_permanentData = this.m_permanentData && isolatedStorageFilePermission.m_permanentData;
			isolatedStorageFilePermission2.m_allowed = (IsolatedStorageContainment)IsolatedStoragePermission.min((long)this.m_allowed, (long)isolatedStorageFilePermission.m_allowed);
			if (isolatedStorageFilePermission2.m_userQuota == 0L && isolatedStorageFilePermission2.m_machineQuota == 0L && isolatedStorageFilePermission2.m_expirationDays == 0L && !isolatedStorageFilePermission2.m_permanentData && isolatedStorageFilePermission2.m_allowed == IsolatedStorageContainment.None)
			{
				return null;
			}
			return isolatedStorageFilePermission2;
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x0008C100 File Offset: 0x0008A300
		public override IPermission Copy()
		{
			IsolatedStorageFilePermission isolatedStorageFilePermission = new IsolatedStorageFilePermission(PermissionState.Unrestricted);
			if (!base.IsUnrestricted())
			{
				isolatedStorageFilePermission.m_userQuota = this.m_userQuota;
				isolatedStorageFilePermission.m_machineQuota = this.m_machineQuota;
				isolatedStorageFilePermission.m_expirationDays = this.m_expirationDays;
				isolatedStorageFilePermission.m_permanentData = this.m_permanentData;
				isolatedStorageFilePermission.m_allowed = this.m_allowed;
			}
			return isolatedStorageFilePermission;
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x0008C159 File Offset: 0x0008A359
		int IBuiltInPermission.GetTokenIndex()
		{
			return IsolatedStorageFilePermission.GetTokenIndex();
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x0008C160 File Offset: 0x0008A360
		internal static int GetTokenIndex()
		{
			return 3;
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x0008C163 File Offset: 0x0008A363
		[ComVisible(false)]
		public override SecurityElement ToXml()
		{
			return base.ToXml("System.Security.Permissions.IsolatedStorageFilePermission");
		}
	}
}
