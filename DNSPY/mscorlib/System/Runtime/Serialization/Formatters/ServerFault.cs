using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x02000769 RID: 1897
	[SoapType(Embedded = true)]
	[ComVisible(true)]
	[Serializable]
	public sealed class ServerFault
	{
		// Token: 0x06005334 RID: 21300 RVA: 0x00123D09 File Offset: 0x00121F09
		internal ServerFault(Exception exception)
		{
			this.exception = exception;
		}

		// Token: 0x06005335 RID: 21301 RVA: 0x00123D18 File Offset: 0x00121F18
		public ServerFault(string exceptionType, string message, string stackTrace)
		{
			this.exceptionType = exceptionType;
			this.message = message;
			this.stackTrace = stackTrace;
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06005336 RID: 21302 RVA: 0x00123D35 File Offset: 0x00121F35
		// (set) Token: 0x06005337 RID: 21303 RVA: 0x00123D3D File Offset: 0x00121F3D
		public string ExceptionType
		{
			get
			{
				return this.exceptionType;
			}
			set
			{
				this.exceptionType = value;
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06005338 RID: 21304 RVA: 0x00123D46 File Offset: 0x00121F46
		// (set) Token: 0x06005339 RID: 21305 RVA: 0x00123D4E File Offset: 0x00121F4E
		public string ExceptionMessage
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x0600533A RID: 21306 RVA: 0x00123D57 File Offset: 0x00121F57
		// (set) Token: 0x0600533B RID: 21307 RVA: 0x00123D5F File Offset: 0x00121F5F
		public string StackTrace
		{
			get
			{
				return this.stackTrace;
			}
			set
			{
				this.stackTrace = value;
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x0600533C RID: 21308 RVA: 0x00123D68 File Offset: 0x00121F68
		internal Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x040024E8 RID: 9448
		private string exceptionType;

		// Token: 0x040024E9 RID: 9449
		private string message;

		// Token: 0x040024EA RID: 9450
		private string stackTrace;

		// Token: 0x040024EB RID: 9451
		private Exception exception;
	}
}
