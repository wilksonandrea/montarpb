using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200026D RID: 621
	[ComVisible(true)]
	public interface ICspAsymmetricAlgorithm
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06002209 RID: 8713
		CspKeyContainerInfo CspKeyContainerInfo { get; }

		// Token: 0x0600220A RID: 8714
		byte[] ExportCspBlob(bool includePrivateParameters);

		// Token: 0x0600220B RID: 8715
		void ImportCspBlob(byte[] rawData);
	}
}
