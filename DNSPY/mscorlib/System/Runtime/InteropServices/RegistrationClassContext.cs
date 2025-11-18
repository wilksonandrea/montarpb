using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000976 RID: 2422
	[Flags]
	public enum RegistrationClassContext
	{
		// Token: 0x04002BC3 RID: 11203
		InProcessServer = 1,
		// Token: 0x04002BC4 RID: 11204
		InProcessHandler = 2,
		// Token: 0x04002BC5 RID: 11205
		LocalServer = 4,
		// Token: 0x04002BC6 RID: 11206
		InProcessServer16 = 8,
		// Token: 0x04002BC7 RID: 11207
		RemoteServer = 16,
		// Token: 0x04002BC8 RID: 11208
		InProcessHandler16 = 32,
		// Token: 0x04002BC9 RID: 11209
		Reserved1 = 64,
		// Token: 0x04002BCA RID: 11210
		Reserved2 = 128,
		// Token: 0x04002BCB RID: 11211
		Reserved3 = 256,
		// Token: 0x04002BCC RID: 11212
		Reserved4 = 512,
		// Token: 0x04002BCD RID: 11213
		NoCodeDownload = 1024,
		// Token: 0x04002BCE RID: 11214
		Reserved5 = 2048,
		// Token: 0x04002BCF RID: 11215
		NoCustomMarshal = 4096,
		// Token: 0x04002BD0 RID: 11216
		EnableCodeDownload = 8192,
		// Token: 0x04002BD1 RID: 11217
		NoFailureLog = 16384,
		// Token: 0x04002BD2 RID: 11218
		DisableActivateAsActivator = 32768,
		// Token: 0x04002BD3 RID: 11219
		EnableActivateAsActivator = 65536,
		// Token: 0x04002BD4 RID: 11220
		FromDefaultContext = 131072
	}
}
