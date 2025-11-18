using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Security.Permissions;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Principal
{
	// Token: 0x02000333 RID: 819
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SecurityInfrastructure = true)]
	[Serializable]
	public class WindowsPrincipal : ClaimsPrincipal
	{
		// Token: 0x060028F1 RID: 10481 RVA: 0x00096C5C File Offset: 0x00094E5C
		private WindowsPrincipal()
		{
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x00096C64 File Offset: 0x00094E64
		public WindowsPrincipal(WindowsIdentity ntIdentity)
			: base(ntIdentity)
		{
			if (ntIdentity == null)
			{
				throw new ArgumentNullException("ntIdentity");
			}
			this.m_identity = ntIdentity;
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x00096C84 File Offset: 0x00094E84
		[OnDeserialized]
		[SecuritySafeCritical]
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
			if (claimsIdentity == null)
			{
				base.AddIdentity(this.m_identity);
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060028F4 RID: 10484 RVA: 0x00096CE4 File Offset: 0x00094EE4
		public override IIdentity Identity
		{
			get
			{
				return this.m_identity;
			}
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x00096CEC File Offset: 0x00094EEC
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public override bool IsInRole(string role)
		{
			if (role == null || role.Length == 0)
			{
				return false;
			}
			NTAccount ntaccount = new NTAccount(role);
			IdentityReferenceCollection identityReferenceCollection = NTAccount.Translate(new IdentityReferenceCollection(1) { ntaccount }, typeof(SecurityIdentifier), false);
			SecurityIdentifier securityIdentifier = identityReferenceCollection[0] as SecurityIdentifier;
			return (securityIdentifier != null && this.IsInRole(securityIdentifier)) || base.IsInRole(role);
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060028F6 RID: 10486 RVA: 0x00096D58 File Offset: 0x00094F58
		public virtual IEnumerable<Claim> UserClaims
		{
			get
			{
				foreach (ClaimsIdentity claimsIdentity in this.Identities)
				{
					WindowsIdentity windowsIdentity = claimsIdentity as WindowsIdentity;
					if (windowsIdentity != null)
					{
						foreach (Claim claim in windowsIdentity.UserClaims)
						{
							yield return claim;
						}
						IEnumerator<Claim> enumerator2 = null;
					}
				}
				IEnumerator<ClaimsIdentity> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060028F7 RID: 10487 RVA: 0x00096D78 File Offset: 0x00094F78
		public virtual IEnumerable<Claim> DeviceClaims
		{
			get
			{
				foreach (ClaimsIdentity claimsIdentity in this.Identities)
				{
					WindowsIdentity windowsIdentity = claimsIdentity as WindowsIdentity;
					if (windowsIdentity != null)
					{
						foreach (Claim claim in windowsIdentity.DeviceClaims)
						{
							yield return claim;
						}
						IEnumerator<Claim> enumerator2 = null;
					}
				}
				IEnumerator<ClaimsIdentity> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x00096D95 File Offset: 0x00094F95
		public virtual bool IsInRole(WindowsBuiltInRole role)
		{
			if (role < WindowsBuiltInRole.Administrator || role > WindowsBuiltInRole.Replicator)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)role }), "role");
			}
			return this.IsInRole((int)role);
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x00096DD4 File Offset: 0x00094FD4
		public virtual bool IsInRole(int rid)
		{
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(IdentifierAuthority.NTAuthority, new int[] { 32, rid });
			return this.IsInRole(securityIdentifier);
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x00096E00 File Offset: 0x00095000
		[SecuritySafeCritical]
		[ComVisible(false)]
		public virtual bool IsInRole(SecurityIdentifier sid)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			if (this.m_identity.AccessToken.IsInvalid)
			{
				return false;
			}
			SafeAccessTokenHandle invalidHandle = SafeAccessTokenHandle.InvalidHandle;
			if (this.m_identity.ImpersonationLevel == TokenImpersonationLevel.None && !Win32Native.DuplicateTokenEx(this.m_identity.AccessToken, 8U, IntPtr.Zero, 2U, 2U, ref invalidHandle))
			{
				throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
			}
			bool flag = false;
			if (!Win32Native.CheckTokenMembership((this.m_identity.ImpersonationLevel != TokenImpersonationLevel.None) ? this.m_identity.AccessToken : invalidHandle, sid.BinaryForm, ref flag))
			{
				throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
			}
			invalidHandle.Dispose();
			return flag;
		}

		// Token: 0x0400108D RID: 4237
		private WindowsIdentity m_identity;

		// Token: 0x0400108E RID: 4238
		private string[] m_roles;

		// Token: 0x0400108F RID: 4239
		private Hashtable m_rolesTable;

		// Token: 0x04001090 RID: 4240
		private bool m_rolesLoaded;

		// Token: 0x02000B5A RID: 2906
		[CompilerGenerated]
		private sealed class <get_UserClaims>d__11 : IEnumerable<Claim>, IEnumerable, IEnumerator<Claim>, IDisposable, IEnumerator
		{
			// Token: 0x06006BD9 RID: 27609 RVA: 0x0017500F File Offset: 0x0017320F
			[DebuggerHidden]
			public <get_UserClaims>d__11(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06006BDA RID: 27610 RVA: 0x0017502C File Offset: 0x0017322C
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num - -4 <= 1 || num == 1)
				{
					try
					{
						if (num == -4 || num == 1)
						{
							try
							{
							}
							finally
							{
								this.<>m__Finally2();
							}
						}
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06006BDB RID: 27611 RVA: 0x00175084 File Offset: 0x00173284
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					int num = this.<>1__state;
					WindowsPrincipal windowsPrincipal = this;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -4;
						goto IL_9A;
					}
					else
					{
						this.<>1__state = -1;
						enumerator = windowsPrincipal.Identities.GetEnumerator();
						this.<>1__state = -3;
					}
					IL_B4:
					while (enumerator.MoveNext())
					{
						ClaimsIdentity claimsIdentity = enumerator.Current;
						WindowsIdentity windowsIdentity = claimsIdentity as WindowsIdentity;
						if (windowsIdentity != null)
						{
							enumerator2 = windowsIdentity.UserClaims.GetEnumerator();
							this.<>1__state = -4;
							goto IL_9A;
						}
					}
					this.<>m__Finally1();
					enumerator = null;
					return false;
					IL_9A:
					if (!enumerator2.MoveNext())
					{
						this.<>m__Finally2();
						enumerator2 = null;
						goto IL_B4;
					}
					Claim claim = enumerator2.Current;
					this.<>2__current = claim;
					this.<>1__state = 1;
					flag = true;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x06006BDC RID: 27612 RVA: 0x00175180 File Offset: 0x00173380
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x06006BDD RID: 27613 RVA: 0x0017519C File Offset: 0x0017339C
			private void <>m__Finally2()
			{
				this.<>1__state = -3;
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
			}

			// Token: 0x17001231 RID: 4657
			// (get) Token: 0x06006BDE RID: 27614 RVA: 0x001751B9 File Offset: 0x001733B9
			Claim IEnumerator<Claim>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006BDF RID: 27615 RVA: 0x001751C1 File Offset: 0x001733C1
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17001232 RID: 4658
			// (get) Token: 0x06006BE0 RID: 27616 RVA: 0x001751C8 File Offset: 0x001733C8
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006BE1 RID: 27617 RVA: 0x001751D0 File Offset: 0x001733D0
			[DebuggerHidden]
			IEnumerator<Claim> IEnumerable<Claim>.GetEnumerator()
			{
				WindowsPrincipal.<get_UserClaims>d__11 <get_UserClaims>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_UserClaims>d__ = this;
				}
				else
				{
					<get_UserClaims>d__ = new WindowsPrincipal.<get_UserClaims>d__11(0);
					<get_UserClaims>d__.<>4__this = this;
				}
				return <get_UserClaims>d__;
			}

			// Token: 0x06006BE2 RID: 27618 RVA: 0x00175213 File Offset: 0x00173413
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Security.Claims.Claim>.GetEnumerator();
			}

			// Token: 0x04003417 RID: 13335
			private int <>1__state;

			// Token: 0x04003418 RID: 13336
			private Claim <>2__current;

			// Token: 0x04003419 RID: 13337
			private int <>l__initialThreadId;

			// Token: 0x0400341A RID: 13338
			public WindowsPrincipal <>4__this;

			// Token: 0x0400341B RID: 13339
			private IEnumerator<ClaimsIdentity> <>7__wrap1;

			// Token: 0x0400341C RID: 13340
			private IEnumerator<Claim> <>7__wrap2;
		}

		// Token: 0x02000B5B RID: 2907
		[CompilerGenerated]
		private sealed class <get_DeviceClaims>d__13 : IEnumerable<Claim>, IEnumerable, IEnumerator<Claim>, IDisposable, IEnumerator
		{
			// Token: 0x06006BE3 RID: 27619 RVA: 0x0017521B File Offset: 0x0017341B
			[DebuggerHidden]
			public <get_DeviceClaims>d__13(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06006BE4 RID: 27620 RVA: 0x00175238 File Offset: 0x00173438
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num - -4 <= 1 || num == 1)
				{
					try
					{
						if (num == -4 || num == 1)
						{
							try
							{
							}
							finally
							{
								this.<>m__Finally2();
							}
						}
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06006BE5 RID: 27621 RVA: 0x00175290 File Offset: 0x00173490
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					int num = this.<>1__state;
					WindowsPrincipal windowsPrincipal = this;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -4;
						goto IL_9A;
					}
					else
					{
						this.<>1__state = -1;
						enumerator = windowsPrincipal.Identities.GetEnumerator();
						this.<>1__state = -3;
					}
					IL_B4:
					while (enumerator.MoveNext())
					{
						ClaimsIdentity claimsIdentity = enumerator.Current;
						WindowsIdentity windowsIdentity = claimsIdentity as WindowsIdentity;
						if (windowsIdentity != null)
						{
							enumerator2 = windowsIdentity.DeviceClaims.GetEnumerator();
							this.<>1__state = -4;
							goto IL_9A;
						}
					}
					this.<>m__Finally1();
					enumerator = null;
					return false;
					IL_9A:
					if (!enumerator2.MoveNext())
					{
						this.<>m__Finally2();
						enumerator2 = null;
						goto IL_B4;
					}
					Claim claim = enumerator2.Current;
					this.<>2__current = claim;
					this.<>1__state = 1;
					flag = true;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x06006BE6 RID: 27622 RVA: 0x0017538C File Offset: 0x0017358C
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x06006BE7 RID: 27623 RVA: 0x001753A8 File Offset: 0x001735A8
			private void <>m__Finally2()
			{
				this.<>1__state = -3;
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
			}

			// Token: 0x17001233 RID: 4659
			// (get) Token: 0x06006BE8 RID: 27624 RVA: 0x001753C5 File Offset: 0x001735C5
			Claim IEnumerator<Claim>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006BE9 RID: 27625 RVA: 0x001753CD File Offset: 0x001735CD
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17001234 RID: 4660
			// (get) Token: 0x06006BEA RID: 27626 RVA: 0x001753D4 File Offset: 0x001735D4
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006BEB RID: 27627 RVA: 0x001753DC File Offset: 0x001735DC
			[DebuggerHidden]
			IEnumerator<Claim> IEnumerable<Claim>.GetEnumerator()
			{
				WindowsPrincipal.<get_DeviceClaims>d__13 <get_DeviceClaims>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_DeviceClaims>d__ = this;
				}
				else
				{
					<get_DeviceClaims>d__ = new WindowsPrincipal.<get_DeviceClaims>d__13(0);
					<get_DeviceClaims>d__.<>4__this = this;
				}
				return <get_DeviceClaims>d__;
			}

			// Token: 0x06006BEC RID: 27628 RVA: 0x0017541F File Offset: 0x0017361F
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Security.Claims.Claim>.GetEnumerator();
			}

			// Token: 0x0400341D RID: 13341
			private int <>1__state;

			// Token: 0x0400341E RID: 13342
			private Claim <>2__current;

			// Token: 0x0400341F RID: 13343
			private int <>l__initialThreadId;

			// Token: 0x04003420 RID: 13344
			public WindowsPrincipal <>4__this;

			// Token: 0x04003421 RID: 13345
			private IEnumerator<ClaimsIdentity> <>7__wrap1;

			// Token: 0x04003422 RID: 13346
			private IEnumerator<Claim> <>7__wrap2;
		}
	}
}
