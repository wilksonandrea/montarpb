using System;
using System.Runtime.InteropServices.ComTypes;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009B2 RID: 2482
	internal static class NativeMethods
	{
		// Token: 0x0600633A RID: 25402
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("oleaut32.dll", PreserveSig = false)]
		internal static extern void VariantClear(IntPtr variant);

		// Token: 0x02000C9F RID: 3231
		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("00020400-0000-0000-C000-000000000046")]
		[ComImport]
		internal interface IDispatch
		{
			// Token: 0x0600712B RID: 28971
			[SecurityCritical]
			void GetTypeInfoCount(out uint pctinfo);

			// Token: 0x0600712C RID: 28972
			[SecurityCritical]
			void GetTypeInfo(uint iTInfo, int lcid, out IntPtr info);

			// Token: 0x0600712D RID: 28973
			[SecurityCritical]
			void GetIDsOfNames(ref Guid iid, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 2)] string[] names, uint cNames, int lcid, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 2)] [Out] int[] rgDispId);

			// Token: 0x0600712E RID: 28974
			[SecurityCritical]
			void Invoke(int dispIdMember, ref Guid riid, int lcid, INVOKEKIND wFlags, ref DISPPARAMS pDispParams, IntPtr pvarResult, IntPtr pExcepInfo, IntPtr puArgErr);
		}
	}
}
