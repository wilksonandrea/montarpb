using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002FE RID: 766
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class PublisherIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060026F3 RID: 9971 RVA: 0x0008CD28 File Offset: 0x0008AF28
		public PublisherIdentityPermissionAttribute(SecurityAction action)
			: base(action)
		{
			this.m_x509cert = null;
			this.m_certFile = null;
			this.m_signedFile = null;
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060026F4 RID: 9972 RVA: 0x0008CD46 File Offset: 0x0008AF46
		// (set) Token: 0x060026F5 RID: 9973 RVA: 0x0008CD4E File Offset: 0x0008AF4E
		public string X509Certificate
		{
			get
			{
				return this.m_x509cert;
			}
			set
			{
				this.m_x509cert = value;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060026F6 RID: 9974 RVA: 0x0008CD57 File Offset: 0x0008AF57
		// (set) Token: 0x060026F7 RID: 9975 RVA: 0x0008CD5F File Offset: 0x0008AF5F
		public string CertFile
		{
			get
			{
				return this.m_certFile;
			}
			set
			{
				this.m_certFile = value;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060026F8 RID: 9976 RVA: 0x0008CD68 File Offset: 0x0008AF68
		// (set) Token: 0x060026F9 RID: 9977 RVA: 0x0008CD70 File Offset: 0x0008AF70
		public string SignedFile
		{
			get
			{
				return this.m_signedFile;
			}
			set
			{
				this.m_signedFile = value;
			}
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x0008CD7C File Offset: 0x0008AF7C
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new PublisherIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.m_x509cert != null)
			{
				return new PublisherIdentityPermission(new X509Certificate(Hex.DecodeHexString(this.m_x509cert)));
			}
			if (this.m_certFile != null)
			{
				return new PublisherIdentityPermission(System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromCertFile(this.m_certFile));
			}
			if (this.m_signedFile != null)
			{
				return new PublisherIdentityPermission(System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromSignedFile(this.m_signedFile));
			}
			return new PublisherIdentityPermission(PermissionState.None);
		}

		// Token: 0x04000F0C RID: 3852
		private string m_x509cert;

		// Token: 0x04000F0D RID: 3853
		private string m_certFile;

		// Token: 0x04000F0E RID: 3854
		private string m_signedFile;
	}
}
