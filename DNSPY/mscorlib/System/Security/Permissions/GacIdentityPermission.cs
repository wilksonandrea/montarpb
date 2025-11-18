using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000312 RID: 786
	[ComVisible(true)]
	[Serializable]
	public sealed class GacIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x060027B1 RID: 10161 RVA: 0x0009046F File Offset: 0x0008E66F
		public GacIdentityPermission(PermissionState state)
		{
			if (state != PermissionState.Unrestricted && state != PermissionState.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
			}
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x0009048E File Offset: 0x0008E68E
		public GacIdentityPermission()
		{
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x00090496 File Offset: 0x0008E696
		public override IPermission Copy()
		{
			return new GacIdentityPermission();
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x0009049D File Offset: 0x0008E69D
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return false;
			}
			if (!(target is GacIdentityPermission))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			return true;
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x000904D1 File Offset: 0x0008E6D1
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (!(target is GacIdentityPermission))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			return this.Copy();
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x0009050A File Offset: 0x0008E70A
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (!(target is GacIdentityPermission))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			return this.Copy();
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x00090548 File Offset: 0x0008E748
		public override SecurityElement ToXml()
		{
			return CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.GacIdentityPermission");
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x00090562 File Offset: 0x0008E762
		public override void FromXml(SecurityElement securityElement)
		{
			CodeAccessPermission.ValidateElement(securityElement, this);
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x0009056B File Offset: 0x0008E76B
		int IBuiltInPermission.GetTokenIndex()
		{
			return GacIdentityPermission.GetTokenIndex();
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x00090572 File Offset: 0x0008E772
		internal static int GetTokenIndex()
		{
			return 15;
		}
	}
}
