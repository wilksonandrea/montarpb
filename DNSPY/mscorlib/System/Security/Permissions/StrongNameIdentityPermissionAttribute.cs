using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002FB RID: 763
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNameIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060026E3 RID: 9955 RVA: 0x0008CBC2 File Offset: 0x0008ADC2
		public StrongNameIdentityPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060026E4 RID: 9956 RVA: 0x0008CBCB File Offset: 0x0008ADCB
		// (set) Token: 0x060026E5 RID: 9957 RVA: 0x0008CBD3 File Offset: 0x0008ADD3
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060026E6 RID: 9958 RVA: 0x0008CBDC File Offset: 0x0008ADDC
		// (set) Token: 0x060026E7 RID: 9959 RVA: 0x0008CBE4 File Offset: 0x0008ADE4
		public string Version
		{
			get
			{
				return this.m_version;
			}
			set
			{
				this.m_version = value;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060026E8 RID: 9960 RVA: 0x0008CBED File Offset: 0x0008ADED
		// (set) Token: 0x060026E9 RID: 9961 RVA: 0x0008CBF5 File Offset: 0x0008ADF5
		public string PublicKey
		{
			get
			{
				return this.m_blob;
			}
			set
			{
				this.m_blob = value;
			}
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x0008CC00 File Offset: 0x0008AE00
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new StrongNameIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.m_blob == null && this.m_name == null && this.m_version == null)
			{
				return new StrongNameIdentityPermission(PermissionState.None);
			}
			if (this.m_blob == null)
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentNull_Key"));
			}
			StrongNamePublicKeyBlob strongNamePublicKeyBlob = new StrongNamePublicKeyBlob(this.m_blob);
			if (this.m_version == null || this.m_version.Equals(string.Empty))
			{
				return new StrongNameIdentityPermission(strongNamePublicKeyBlob, this.m_name, null);
			}
			return new StrongNameIdentityPermission(strongNamePublicKeyBlob, this.m_name, new Version(this.m_version));
		}

		// Token: 0x04000F07 RID: 3847
		private string m_name;

		// Token: 0x04000F08 RID: 3848
		private string m_version;

		// Token: 0x04000F09 RID: 3849
		private string m_blob;
	}
}
