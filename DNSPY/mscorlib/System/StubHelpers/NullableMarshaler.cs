using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x020005A2 RID: 1442
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class NullableMarshaler
	{
		// Token: 0x06004324 RID: 17188 RVA: 0x000F9E60 File Offset: 0x000F8060
		[SecurityCritical]
		internal static IntPtr ConvertToNative<T>(ref T? pManaged) where T : struct
		{
			if (pManaged != null)
			{
				object obj = IReferenceFactory.CreateIReference(pManaged);
				return Marshal.GetComInterfaceForObject(obj, typeof(IReference<T>));
			}
			return IntPtr.Zero;
		}

		// Token: 0x06004325 RID: 17189 RVA: 0x000F9E9C File Offset: 0x000F809C
		[SecurityCritical]
		internal static void ConvertToManagedRetVoid<T>(IntPtr pNative, ref T? retObj) where T : struct
		{
			retObj = NullableMarshaler.ConvertToManaged<T>(pNative);
		}

		// Token: 0x06004326 RID: 17190 RVA: 0x000F9EAC File Offset: 0x000F80AC
		[SecurityCritical]
		internal static T? ConvertToManaged<T>(IntPtr pNative) where T : struct
		{
			if (pNative != IntPtr.Zero)
			{
				object obj = InterfaceMarshaler.ConvertToManagedWithoutUnboxing(pNative);
				return (T?)CLRIReferenceImpl<T>.UnboxHelper(obj);
			}
			return null;
		}
	}
}
