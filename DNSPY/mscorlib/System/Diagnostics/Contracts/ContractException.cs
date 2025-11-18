using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Diagnostics.Contracts
{
	// Token: 0x02000418 RID: 1048
	[Serializable]
	internal sealed class ContractException : Exception
	{
		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06003428 RID: 13352 RVA: 0x000C69F3 File Offset: 0x000C4BF3
		public ContractFailureKind Kind
		{
			get
			{
				return this._Kind;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06003429 RID: 13353 RVA: 0x000C69FB File Offset: 0x000C4BFB
		public string Failure
		{
			get
			{
				return this.Message;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x0600342A RID: 13354 RVA: 0x000C6A03 File Offset: 0x000C4C03
		public string UserMessage
		{
			get
			{
				return this._UserMessage;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x0600342B RID: 13355 RVA: 0x000C6A0B File Offset: 0x000C4C0B
		public string Condition
		{
			get
			{
				return this._Condition;
			}
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x000C6A13 File Offset: 0x000C4C13
		private ContractException()
		{
			base.HResult = -2146233022;
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x000C6A26 File Offset: 0x000C4C26
		public ContractException(ContractFailureKind kind, string failure, string userMessage, string condition, Exception innerException)
			: base(failure, innerException)
		{
			base.HResult = -2146233022;
			this._Kind = kind;
			this._UserMessage = userMessage;
			this._Condition = condition;
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x000C6A52 File Offset: 0x000C4C52
		private ContractException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._Kind = (ContractFailureKind)info.GetInt32("Kind");
			this._UserMessage = info.GetString("UserMessage");
			this._Condition = info.GetString("Condition");
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x000C6A90 File Offset: 0x000C4C90
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Kind", this._Kind);
			info.AddValue("UserMessage", this._UserMessage);
			info.AddValue("Condition", this._Condition);
		}

		// Token: 0x0400171F RID: 5919
		private readonly ContractFailureKind _Kind;

		// Token: 0x04001720 RID: 5920
		private readonly string _UserMessage;

		// Token: 0x04001721 RID: 5921
		private readonly string _Condition;
	}
}
