using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Principal
{
	// Token: 0x0200033E RID: 830
	[ComVisible(false)]
	[Serializable]
	public sealed class IdentityNotMappedException : SystemException
	{
		// Token: 0x0600295A RID: 10586 RVA: 0x00098C34 File Offset: 0x00096E34
		public IdentityNotMappedException()
			: base(Environment.GetResourceString("IdentityReference_IdentityNotMapped"))
		{
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x00098C46 File Offset: 0x00096E46
		public IdentityNotMappedException(string message)
			: base(message)
		{
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x00098C4F File Offset: 0x00096E4F
		public IdentityNotMappedException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x00098C59 File Offset: 0x00096E59
		internal IdentityNotMappedException(string message, IdentityReferenceCollection unmappedIdentities)
			: this(message)
		{
			this.unmappedIdentities = unmappedIdentities;
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x00098C69 File Offset: 0x00096E69
		internal IdentityNotMappedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x00098C73 File Offset: 0x00096E73
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06002960 RID: 10592 RVA: 0x00098C7D File Offset: 0x00096E7D
		public IdentityReferenceCollection UnmappedIdentities
		{
			get
			{
				if (this.unmappedIdentities == null)
				{
					this.unmappedIdentities = new IdentityReferenceCollection();
				}
				return this.unmappedIdentities;
			}
		}

		// Token: 0x04001107 RID: 4359
		private IdentityReferenceCollection unmappedIdentities;
	}
}
