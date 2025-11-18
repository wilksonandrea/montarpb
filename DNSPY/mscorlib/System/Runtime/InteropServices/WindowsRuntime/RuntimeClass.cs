using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A01 RID: 2561
	internal abstract class RuntimeClass : __ComObject
	{
		// Token: 0x060064FA RID: 25850
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetRedirectedGetHashCodeMD();

		// Token: 0x060064FB RID: 25851
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int RedirectGetHashCode(IntPtr pMD);

		// Token: 0x060064FC RID: 25852 RVA: 0x00157B40 File Offset: 0x00155D40
		[SecuritySafeCritical]
		public override int GetHashCode()
		{
			IntPtr redirectedGetHashCodeMD = this.GetRedirectedGetHashCodeMD();
			if (redirectedGetHashCodeMD == IntPtr.Zero)
			{
				return base.GetHashCode();
			}
			return this.RedirectGetHashCode(redirectedGetHashCodeMD);
		}

		// Token: 0x060064FD RID: 25853
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetRedirectedToStringMD();

		// Token: 0x060064FE RID: 25854
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string RedirectToString(IntPtr pMD);

		// Token: 0x060064FF RID: 25855 RVA: 0x00157B70 File Offset: 0x00155D70
		[SecuritySafeCritical]
		public override string ToString()
		{
			IStringable stringable = this as IStringable;
			if (stringable != null)
			{
				return stringable.ToString();
			}
			IntPtr redirectedToStringMD = this.GetRedirectedToStringMD();
			if (redirectedToStringMD == IntPtr.Zero)
			{
				return base.ToString();
			}
			return this.RedirectToString(redirectedToStringMD);
		}

		// Token: 0x06006500 RID: 25856
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetRedirectedEqualsMD();

		// Token: 0x06006501 RID: 25857
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool RedirectEquals(object obj, IntPtr pMD);

		// Token: 0x06006502 RID: 25858 RVA: 0x00157BB0 File Offset: 0x00155DB0
		[SecuritySafeCritical]
		public override bool Equals(object obj)
		{
			IntPtr redirectedEqualsMD = this.GetRedirectedEqualsMD();
			if (redirectedEqualsMD == IntPtr.Zero)
			{
				return base.Equals(obj);
			}
			return this.RedirectEquals(obj, redirectedEqualsMD);
		}

		// Token: 0x06006503 RID: 25859 RVA: 0x00157BE1 File Offset: 0x00155DE1
		protected RuntimeClass()
		{
		}
	}
}
