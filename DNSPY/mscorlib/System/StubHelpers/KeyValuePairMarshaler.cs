using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x020005A8 RID: 1448
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class KeyValuePairMarshaler
	{
		// Token: 0x0600432E RID: 17198 RVA: 0x000FA098 File Offset: 0x000F8298
		[SecurityCritical]
		internal static IntPtr ConvertToNative<K, V>([In] ref KeyValuePair<K, V> pair)
		{
			IKeyValuePair<K, V> keyValuePair = new CLRIKeyValuePairImpl<K, V>(ref pair);
			return Marshal.GetComInterfaceForObject(keyValuePair, typeof(IKeyValuePair<K, V>));
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x000FA0BC File Offset: 0x000F82BC
		[SecurityCritical]
		internal static KeyValuePair<K, V> ConvertToManaged<K, V>(IntPtr pInsp)
		{
			object obj = InterfaceMarshaler.ConvertToManagedWithoutUnboxing(pInsp);
			IKeyValuePair<K, V> keyValuePair = (IKeyValuePair<K, V>)obj;
			return new KeyValuePair<K, V>(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x06004330 RID: 17200 RVA: 0x000FA0E8 File Offset: 0x000F82E8
		[SecurityCritical]
		internal static object ConvertToManagedBox<K, V>(IntPtr pInsp)
		{
			return KeyValuePairMarshaler.ConvertToManaged<K, V>(pInsp);
		}
	}
}
