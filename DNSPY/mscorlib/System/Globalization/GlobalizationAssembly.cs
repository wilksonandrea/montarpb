using System;
using System.IO;
using System.Reflection;
using System.Security;

namespace System.Globalization
{
	// Token: 0x020003BB RID: 955
	internal sealed class GlobalizationAssembly
	{
		// Token: 0x06002F5D RID: 12125 RVA: 0x000B5B60 File Offset: 0x000B3D60
		[SecurityCritical]
		internal unsafe static byte* GetGlobalizationResourceBytePtr(Assembly assembly, string tableName)
		{
			Stream manifestResourceStream = assembly.GetManifestResourceStream(tableName);
			UnmanagedMemoryStream unmanagedMemoryStream = manifestResourceStream as UnmanagedMemoryStream;
			if (unmanagedMemoryStream != null)
			{
				byte* positionPointer = unmanagedMemoryStream.PositionPointer;
				if (positionPointer != null)
				{
					return positionPointer;
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x000B5B92 File Offset: 0x000B3D92
		public GlobalizationAssembly()
		{
		}
	}
}
