using System;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A15 RID: 2581
	internal sealed class WinRTClassActivator : MarshalByRefObject, IWinRTClassActivator
	{
		// Token: 0x060065C0 RID: 26048 RVA: 0x00159974 File Offset: 0x00157B74
		[SecurityCritical]
		public object ActivateInstance(string activatableClassId)
		{
			ManagedActivationFactory managedActivationFactory = WindowsRuntimeMarshal.GetManagedActivationFactory(this.LoadWinRTType(activatableClassId));
			return managedActivationFactory.ActivateInstance();
		}

		// Token: 0x060065C1 RID: 26049 RVA: 0x00159994 File Offset: 0x00157B94
		[SecurityCritical]
		public IntPtr GetActivationFactory(string activatableClassId, ref Guid iid)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2;
			try
			{
				intPtr = WindowsRuntimeMarshal.GetActivationFactoryForType(this.LoadWinRTType(activatableClassId));
				IntPtr zero = IntPtr.Zero;
				int num = Marshal.QueryInterface(intPtr, ref iid, out zero);
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
				intPtr2 = zero;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.Release(intPtr);
				}
			}
			return intPtr2;
		}

		// Token: 0x060065C2 RID: 26050 RVA: 0x001599F8 File Offset: 0x00157BF8
		private Type LoadWinRTType(string acid)
		{
			Type type = Type.GetType(acid + ", Windows, ContentType=WindowsRuntime");
			if (type == null)
			{
				throw new COMException(-2147221164);
			}
			return type;
		}

		// Token: 0x060065C3 RID: 26051 RVA: 0x00159A2B File Offset: 0x00157C2B
		[SecurityCritical]
		internal IntPtr GetIWinRTClassActivator()
		{
			return Marshal.GetComInterfaceForObject(this, typeof(IWinRTClassActivator));
		}

		// Token: 0x060065C4 RID: 26052 RVA: 0x00159A3D File Offset: 0x00157C3D
		public WinRTClassActivator()
		{
		}
	}
}
