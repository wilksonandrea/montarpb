using System;
using System.Runtime.CompilerServices;

namespace System.StubHelpers
{
	// Token: 0x020005A5 RID: 1445
	internal static class WinRTTypeNameConverter
	{
		// Token: 0x06004327 RID: 17191
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string ConvertToWinRTTypeName(Type managedType, out bool isPrimitive);

		// Token: 0x06004328 RID: 17192
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Type GetTypeFromWinRTTypeName(string typeName, out bool isPrimitive);
	}
}
