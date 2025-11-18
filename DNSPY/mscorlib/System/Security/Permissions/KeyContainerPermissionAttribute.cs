using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002F4 RID: 756
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002687 RID: 9863 RVA: 0x0008C4C0 File Offset: 0x0008A6C0
		public KeyContainerPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06002688 RID: 9864 RVA: 0x0008C4D7 File Offset: 0x0008A6D7
		// (set) Token: 0x06002689 RID: 9865 RVA: 0x0008C4DF File Offset: 0x0008A6DF
		public string KeyStore
		{
			get
			{
				return this.m_keyStore;
			}
			set
			{
				this.m_keyStore = value;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600268A RID: 9866 RVA: 0x0008C4E8 File Offset: 0x0008A6E8
		// (set) Token: 0x0600268B RID: 9867 RVA: 0x0008C4F0 File Offset: 0x0008A6F0
		public string ProviderName
		{
			get
			{
				return this.m_providerName;
			}
			set
			{
				this.m_providerName = value;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x0008C4F9 File Offset: 0x0008A6F9
		// (set) Token: 0x0600268D RID: 9869 RVA: 0x0008C501 File Offset: 0x0008A701
		public int ProviderType
		{
			get
			{
				return this.m_providerType;
			}
			set
			{
				this.m_providerType = value;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600268E RID: 9870 RVA: 0x0008C50A File Offset: 0x0008A70A
		// (set) Token: 0x0600268F RID: 9871 RVA: 0x0008C512 File Offset: 0x0008A712
		public string KeyContainerName
		{
			get
			{
				return this.m_keyContainerName;
			}
			set
			{
				this.m_keyContainerName = value;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06002690 RID: 9872 RVA: 0x0008C51B File Offset: 0x0008A71B
		// (set) Token: 0x06002691 RID: 9873 RVA: 0x0008C523 File Offset: 0x0008A723
		public int KeySpec
		{
			get
			{
				return this.m_keySpec;
			}
			set
			{
				this.m_keySpec = value;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06002692 RID: 9874 RVA: 0x0008C52C File Offset: 0x0008A72C
		// (set) Token: 0x06002693 RID: 9875 RVA: 0x0008C534 File Offset: 0x0008A734
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				this.m_flags = value;
			}
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x0008C540 File Offset: 0x0008A740
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new KeyContainerPermission(PermissionState.Unrestricted);
			}
			if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.m_keyStore, this.m_providerName, this.m_providerType, this.m_keyContainerName, this.m_keySpec))
			{
				return new KeyContainerPermission(this.m_flags);
			}
			KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
			KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(this.m_keyStore, this.m_providerName, this.m_providerType, this.m_keyContainerName, this.m_keySpec, this.m_flags);
			keyContainerPermission.AccessEntries.Add(keyContainerPermissionAccessEntry);
			return keyContainerPermission;
		}

		// Token: 0x04000EF4 RID: 3828
		private KeyContainerPermissionFlags m_flags;

		// Token: 0x04000EF5 RID: 3829
		private string m_keyStore;

		// Token: 0x04000EF6 RID: 3830
		private string m_providerName;

		// Token: 0x04000EF7 RID: 3831
		private int m_providerType = -1;

		// Token: 0x04000EF8 RID: 3832
		private string m_keyContainerName;

		// Token: 0x04000EF9 RID: 3833
		private int m_keySpec = -1;
	}
}
