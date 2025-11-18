using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006BA RID: 1722
	[Guid("07662534-750b-4ed5-9cfb-1c5bc5acfd07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IStateManager
	{
		// Token: 0x06005042 RID: 20546
		[SecurityCritical]
		void PrepareApplicationState([In] UIntPtr Inputs, ref UIntPtr Outputs);

		// Token: 0x06005043 RID: 20547
		[SecurityCritical]
		void SetApplicationRunningState([In] uint Flags, [In] IActContext Context, [In] uint RunningState, out uint Disposition);

		// Token: 0x06005044 RID: 20548
		[SecurityCritical]
		void GetApplicationStateFilesystemLocation([In] uint Flags, [In] IDefinitionAppId Appidentity, [In] IDefinitionIdentity ComponentIdentity, [In] UIntPtr Coordinates, [MarshalAs(UnmanagedType.LPWStr)] out string Path);

		// Token: 0x06005045 RID: 20549
		[SecurityCritical]
		void Scavenge([In] uint Flags, out uint Disposition);
	}
}
