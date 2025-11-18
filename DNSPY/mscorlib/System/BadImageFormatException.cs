using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System
{
	// Token: 0x020000B0 RID: 176
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class BadImageFormatException : SystemException
	{
		// Token: 0x06000A0D RID: 2573 RVA: 0x000207FC File Offset: 0x0001E9FC
		[__DynamicallyInvokable]
		public BadImageFormatException()
			: base(Environment.GetResourceString("Arg_BadImageFormatException"))
		{
			base.SetErrorCode(-2147024885);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00020819 File Offset: 0x0001EA19
		[__DynamicallyInvokable]
		public BadImageFormatException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024885);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0002082D File Offset: 0x0001EA2D
		[__DynamicallyInvokable]
		public BadImageFormatException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147024885);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00020842 File Offset: 0x0001EA42
		[__DynamicallyInvokable]
		public BadImageFormatException(string message, string fileName)
			: base(message)
		{
			base.SetErrorCode(-2147024885);
			this._fileName = fileName;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002085D File Offset: 0x0001EA5D
		[__DynamicallyInvokable]
		public BadImageFormatException(string message, string fileName, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147024885);
			this._fileName = fileName;
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x00020879 File Offset: 0x0001EA79
		[__DynamicallyInvokable]
		public override string Message
		{
			[__DynamicallyInvokable]
			get
			{
				this.SetMessageField();
				return this._message;
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00020888 File Offset: 0x0001EA88
		private void SetMessageField()
		{
			if (this._message == null)
			{
				if (this._fileName == null && base.HResult == -2146233088)
				{
					this._message = Environment.GetResourceString("Arg_BadImageFormatException");
					return;
				}
				this._message = FileLoadException.FormatFileLoadExceptionMessage(this._fileName, base.HResult);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x000208DA File Offset: 0x0001EADA
		[__DynamicallyInvokable]
		public string FileName
		{
			[__DynamicallyInvokable]
			get
			{
				return this._fileName;
			}
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x000208E4 File Offset: 0x0001EAE4
		[__DynamicallyInvokable]
		public override string ToString()
		{
			string text = base.GetType().FullName + ": " + this.Message;
			if (this._fileName != null && this._fileName.Length != 0)
			{
				text = text + Environment.NewLine + Environment.GetResourceString("IO.FileName_Name", new object[] { this._fileName });
			}
			if (base.InnerException != null)
			{
				text = text + " ---> " + base.InnerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + Environment.NewLine + this.StackTrace;
			}
			try
			{
				if (this.FusionLog != null)
				{
					if (text == null)
					{
						text = " ";
					}
					text += Environment.NewLine;
					text += Environment.NewLine;
					text += this.FusionLog;
				}
			}
			catch (SecurityException)
			{
			}
			return text;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x000209D0 File Offset: 0x0001EBD0
		protected BadImageFormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._fileName = info.GetString("BadImageFormat_FileName");
			try
			{
				this._fusionLog = info.GetString("BadImageFormat_FusionLog");
			}
			catch
			{
				this._fusionLog = null;
			}
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00020A24 File Offset: 0x0001EC24
		private BadImageFormatException(string fileName, string fusionLog, int hResult)
			: base(null)
		{
			base.SetErrorCode(hResult);
			this._fileName = fileName;
			this._fusionLog = fusionLog;
			this.SetMessageField();
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x00020A48 File Offset: 0x0001EC48
		public string FusionLog
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)]
			get
			{
				return this._fusionLog;
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00020A50 File Offset: 0x0001EC50
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("BadImageFormat_FileName", this._fileName, typeof(string));
			try
			{
				info.AddValue("BadImageFormat_FusionLog", this.FusionLog, typeof(string));
			}
			catch (SecurityException)
			{
			}
		}

		// Token: 0x040003EA RID: 1002
		private string _fileName;

		// Token: 0x040003EB RID: 1003
		private string _fusionLog;
	}
}
