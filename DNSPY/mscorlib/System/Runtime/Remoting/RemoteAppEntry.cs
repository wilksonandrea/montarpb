using System;

namespace System.Runtime.Remoting
{
	// Token: 0x020007C7 RID: 1991
	internal class RemoteAppEntry
	{
		// Token: 0x06005617 RID: 22039 RVA: 0x00131387 File Offset: 0x0012F587
		internal RemoteAppEntry(string appName, string appURI)
		{
			this._remoteAppName = appName;
			this._remoteAppURI = appURI;
		}

		// Token: 0x06005618 RID: 22040 RVA: 0x0013139D File Offset: 0x0012F59D
		internal string GetAppURI()
		{
			return this._remoteAppURI;
		}

		// Token: 0x04002790 RID: 10128
		private string _remoteAppName;

		// Token: 0x04002791 RID: 10129
		private string _remoteAppURI;
	}
}
