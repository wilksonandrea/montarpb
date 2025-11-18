using System;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008AD RID: 2221
	[__DynamicallyInvokable]
	public static class RuntimeHelpers
	{
		// Token: 0x06005D88 RID: 23944
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void InitializeArray(Array array, RuntimeFieldHandle fldHandle);

		// Token: 0x06005D89 RID: 23945
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object GetObjectValue(object obj);

		// Token: 0x06005D8A RID: 23946
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _RunClassConstructor(RuntimeType type);

		// Token: 0x06005D8B RID: 23947 RVA: 0x001491A5 File Offset: 0x001473A5
		[__DynamicallyInvokable]
		public static void RunClassConstructor(RuntimeTypeHandle type)
		{
			RuntimeHelpers._RunClassConstructor(type.GetRuntimeType());
		}

		// Token: 0x06005D8C RID: 23948
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _RunModuleConstructor(RuntimeModule module);

		// Token: 0x06005D8D RID: 23949 RVA: 0x001491B3 File Offset: 0x001473B3
		public static void RunModuleConstructor(ModuleHandle module)
		{
			RuntimeHelpers._RunModuleConstructor(module.GetRuntimeModule());
		}

		// Token: 0x06005D8E RID: 23950
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _PrepareMethod(IRuntimeMethodInfo method, IntPtr* pInstantiation, int cInstantiation);

		// Token: 0x06005D8F RID: 23951
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void _CompileMethod(IRuntimeMethodInfo method);

		// Token: 0x06005D90 RID: 23952 RVA: 0x001491C1 File Offset: 0x001473C1
		[SecurityCritical]
		public static void PrepareMethod(RuntimeMethodHandle method)
		{
			RuntimeHelpers._PrepareMethod(method.GetMethodInfo(), null, 0);
		}

		// Token: 0x06005D91 RID: 23953 RVA: 0x001491D4 File Offset: 0x001473D4
		[SecurityCritical]
		public unsafe static void PrepareMethod(RuntimeMethodHandle method, RuntimeTypeHandle[] instantiation)
		{
			int num;
			IntPtr[] array = RuntimeTypeHandle.CopyRuntimeTypeHandles(instantiation, out num);
			IntPtr[] array2;
			IntPtr* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			RuntimeHelpers._PrepareMethod(method.GetMethodInfo(), ptr, num);
			GC.KeepAlive(instantiation);
			array2 = null;
		}

		// Token: 0x06005D92 RID: 23954
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PrepareDelegate(Delegate d);

		// Token: 0x06005D93 RID: 23955
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PrepareContractedDelegate(Delegate d);

		// Token: 0x06005D94 RID: 23956
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetHashCode(object o);

		// Token: 0x06005D95 RID: 23957
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public new static extern bool Equals(object o1, object o2);

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x06005D96 RID: 23958 RVA: 0x00149218 File Offset: 0x00147418
		[__DynamicallyInvokable]
		public static int OffsetToStringData
		{
			[NonVersionable]
			[__DynamicallyInvokable]
			get
			{
				return 8;
			}
		}

		// Token: 0x06005D97 RID: 23959
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EnsureSufficientExecutionStack();

		// Token: 0x06005D98 RID: 23960
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ProbeForSufficientStack();

		// Token: 0x06005D99 RID: 23961 RVA: 0x0014921B File Offset: 0x0014741B
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void PrepareConstrainedRegions()
		{
			RuntimeHelpers.ProbeForSufficientStack();
		}

		// Token: 0x06005D9A RID: 23962 RVA: 0x00149222 File Offset: 0x00147422
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void PrepareConstrainedRegionsNoOP()
		{
		}

		// Token: 0x06005D9B RID: 23963
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ExecuteCodeWithGuaranteedCleanup(RuntimeHelpers.TryCode code, RuntimeHelpers.CleanupCode backoutCode, object userData);

		// Token: 0x06005D9C RID: 23964 RVA: 0x00149224 File Offset: 0x00147424
		[PrePrepareMethod]
		internal static void ExecuteBackoutCodeHelper(object backoutCode, object userData, bool exceptionThrown)
		{
			((RuntimeHelpers.CleanupCode)backoutCode)(userData, exceptionThrown);
		}

		// Token: 0x02000C8C RID: 3212
		// (Invoke) Token: 0x060070EA RID: 28906
		public delegate void TryCode(object userData);

		// Token: 0x02000C8D RID: 3213
		// (Invoke) Token: 0x060070EE RID: 28910
		public delegate void CleanupCode(object userData, bool exceptionThrown);
	}
}
