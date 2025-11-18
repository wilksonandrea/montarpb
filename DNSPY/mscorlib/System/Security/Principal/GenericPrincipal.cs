using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Claims;

namespace System.Security.Principal
{
	// Token: 0x02000323 RID: 803
	[ComVisible(true)]
	[Serializable]
	public class GenericPrincipal : ClaimsPrincipal
	{
		// Token: 0x0600289B RID: 10395 RVA: 0x00094B9C File Offset: 0x00092D9C
		public GenericPrincipal(IIdentity identity, string[] roles)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.m_identity = identity;
			if (roles != null)
			{
				this.m_roles = new string[roles.Length];
				for (int i = 0; i < roles.Length; i++)
				{
					this.m_roles[i] = roles[i];
				}
			}
			else
			{
				this.m_roles = null;
			}
			this.AddIdentityWithRoles(this.m_identity, this.m_roles);
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x00094C0C File Offset: 0x00092E0C
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			ClaimsIdentity claimsIdentity = null;
			foreach (ClaimsIdentity claimsIdentity2 in base.Identities)
			{
				if (claimsIdentity2 != null)
				{
					claimsIdentity = claimsIdentity2;
					break;
				}
			}
			if (this.m_roles != null && this.m_roles.Length != 0 && claimsIdentity != null)
			{
				claimsIdentity.ExternalClaims.Add(new RoleClaimProvider("LOCAL AUTHORITY", this.m_roles, claimsIdentity).Claims);
				return;
			}
			if (claimsIdentity == null)
			{
				this.AddIdentityWithRoles(this.m_identity, this.m_roles);
			}
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x00094CA8 File Offset: 0x00092EA8
		[SecuritySafeCritical]
		private void AddIdentityWithRoles(IIdentity identity, string[] roles)
		{
			ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
			if (claimsIdentity != null)
			{
				claimsIdentity = claimsIdentity.Clone();
			}
			else
			{
				claimsIdentity = new ClaimsIdentity(identity);
			}
			if (roles != null && roles.Length != 0)
			{
				claimsIdentity.ExternalClaims.Add(new RoleClaimProvider("LOCAL AUTHORITY", roles, claimsIdentity).Claims);
			}
			base.AddIdentity(claimsIdentity);
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x0600289E RID: 10398 RVA: 0x00094CF9 File Offset: 0x00092EF9
		public override IIdentity Identity
		{
			get
			{
				return this.m_identity;
			}
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x00094D04 File Offset: 0x00092F04
		public override bool IsInRole(string role)
		{
			if (role == null || this.m_roles == null)
			{
				return false;
			}
			for (int i = 0; i < this.m_roles.Length; i++)
			{
				if (this.m_roles[i] != null && string.Compare(this.m_roles[i], role, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return base.IsInRole(role);
		}

		// Token: 0x0400100D RID: 4109
		private IIdentity m_identity;

		// Token: 0x0400100E RID: 4110
		private string[] m_roles;
	}
}
