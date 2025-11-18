using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Security.Claims
{
	// Token: 0x02000321 RID: 801
	[ComVisible(false)]
	internal class RoleClaimProvider
	{
		// Token: 0x0600288E RID: 10382 RVA: 0x00094A0D File Offset: 0x00092C0D
		public RoleClaimProvider(string issuer, string[] roles, ClaimsIdentity subject)
		{
			this.m_issuer = issuer;
			this.m_roles = roles;
			this.m_subject = subject;
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x0600288F RID: 10383 RVA: 0x00094A2C File Offset: 0x00092C2C
		public IEnumerable<Claim> Claims
		{
			get
			{
				int num;
				for (int i = 0; i < this.m_roles.Length; i = num + 1)
				{
					if (this.m_roles[i] != null)
					{
						yield return new Claim(this.m_subject.RoleClaimType, this.m_roles[i], "http://www.w3.org/2001/XMLSchema#string", this.m_issuer, this.m_issuer, this.m_subject);
					}
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x04001008 RID: 4104
		private string m_issuer;

		// Token: 0x04001009 RID: 4105
		private string[] m_roles;

		// Token: 0x0400100A RID: 4106
		private ClaimsIdentity m_subject;

		// Token: 0x02000B58 RID: 2904
		[CompilerGenerated]
		private sealed class <get_Claims>d__5 : IEnumerable<Claim>, IEnumerable, IEnumerator<Claim>, IDisposable, IEnumerator
		{
			// Token: 0x06006BC6 RID: 27590 RVA: 0x00174BEB File Offset: 0x00172DEB
			[DebuggerHidden]
			public <get_Claims>d__5(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06006BC7 RID: 27591 RVA: 0x00174C05 File Offset: 0x00172E05
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06006BC8 RID: 27592 RVA: 0x00174C08 File Offset: 0x00172E08
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				RoleClaimProvider roleClaimProvider = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					i = 0;
					goto IL_90;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				IL_80:
				int num2 = i;
				i = num2 + 1;
				IL_90:
				if (i >= roleClaimProvider.m_roles.Length)
				{
					return false;
				}
				if (roleClaimProvider.m_roles[i] != null)
				{
					this.<>2__current = new Claim(roleClaimProvider.m_subject.RoleClaimType, roleClaimProvider.m_roles[i], "http://www.w3.org/2001/XMLSchema#string", roleClaimProvider.m_issuer, roleClaimProvider.m_issuer, roleClaimProvider.m_subject);
					this.<>1__state = 1;
					return true;
				}
				goto IL_80;
			}

			// Token: 0x1700122D RID: 4653
			// (get) Token: 0x06006BC9 RID: 27593 RVA: 0x00174CB6 File Offset: 0x00172EB6
			Claim IEnumerator<Claim>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006BCA RID: 27594 RVA: 0x00174CBE File Offset: 0x00172EBE
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700122E RID: 4654
			// (get) Token: 0x06006BCB RID: 27595 RVA: 0x00174CC5 File Offset: 0x00172EC5
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006BCC RID: 27596 RVA: 0x00174CD0 File Offset: 0x00172ED0
			[DebuggerHidden]
			IEnumerator<Claim> IEnumerable<Claim>.GetEnumerator()
			{
				RoleClaimProvider.<get_Claims>d__5 <get_Claims>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_Claims>d__ = this;
				}
				else
				{
					<get_Claims>d__ = new RoleClaimProvider.<get_Claims>d__5(0);
					<get_Claims>d__.<>4__this = this;
				}
				return <get_Claims>d__;
			}

			// Token: 0x06006BCD RID: 27597 RVA: 0x00174D13 File Offset: 0x00172F13
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Security.Claims.Claim>.GetEnumerator();
			}

			// Token: 0x0400340C RID: 13324
			private int <>1__state;

			// Token: 0x0400340D RID: 13325
			private Claim <>2__current;

			// Token: 0x0400340E RID: 13326
			private int <>l__initialThreadId;

			// Token: 0x0400340F RID: 13327
			public RoleClaimProvider <>4__this;

			// Token: 0x04003410 RID: 13328
			private int <i>5__2;
		}
	}
}
