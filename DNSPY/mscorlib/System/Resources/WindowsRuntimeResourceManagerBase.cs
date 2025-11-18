using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Resources
{
	// Token: 0x02000395 RID: 917
	[FriendAccessAllowed]
	[SecurityCritical]
	internal class WindowsRuntimeResourceManagerBase
	{
		// Token: 0x06002D18 RID: 11544 RVA: 0x000AA13D File Offset: 0x000A833D
		[SecurityCritical]
		public virtual bool Initialize(string libpath, string reswFilename, out PRIExceptionInfo exceptionInfo)
		{
			exceptionInfo = null;
			return false;
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x000AA143 File Offset: 0x000A8343
		[SecurityCritical]
		public virtual string GetString(string stringName, string startingCulture, string neutralResourcesCulture)
		{
			return null;
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06002D1A RID: 11546 RVA: 0x000AA146 File Offset: 0x000A8346
		public virtual CultureInfo GlobalResourceContextBestFitCultureInfo
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x000AA149 File Offset: 0x000A8349
		[SecurityCritical]
		public virtual bool SetGlobalResourceContextDefaultCulture(CultureInfo ci)
		{
			return false;
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x000AA14C File Offset: 0x000A834C
		public WindowsRuntimeResourceManagerBase()
		{
		}
	}
}
