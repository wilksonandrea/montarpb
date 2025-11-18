using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x0200010F RID: 271
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class MissingFieldException : MissingMemberException, ISerializable
	{
		// Token: 0x06001058 RID: 4184 RVA: 0x00031181 File Offset: 0x0002F381
		[__DynamicallyInvokable]
		public MissingFieldException()
			: base(Environment.GetResourceString("Arg_MissingFieldException"))
		{
			base.SetErrorCode(-2146233071);
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0003119E File Offset: 0x0002F39E
		[__DynamicallyInvokable]
		public MissingFieldException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233071);
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x000311B2 File Offset: 0x0002F3B2
		[__DynamicallyInvokable]
		public MissingFieldException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233071);
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x000311C7 File Offset: 0x0002F3C7
		protected MissingFieldException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x000311D4 File Offset: 0x0002F3D4
		[__DynamicallyInvokable]
		public override string Message
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				if (this.ClassName == null)
				{
					return base.Message;
				}
				return Environment.GetResourceString("MissingField_Name", new object[] { ((this.Signature != null) ? (MissingMemberException.FormatSignature(this.Signature) + " ") : "") + this.ClassName + "." + this.MemberName });
			}
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0003123D File Offset: 0x0002F43D
		private MissingFieldException(string className, string fieldName, byte[] signature)
		{
			this.ClassName = className;
			this.MemberName = fieldName;
			this.Signature = signature;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0003125A File Offset: 0x0002F45A
		public MissingFieldException(string className, string fieldName)
		{
			this.ClassName = className;
			this.MemberName = fieldName;
		}
	}
}
