using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200041A RID: 1050
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	[Conditional("CODE_ANALYSIS")]
	[__DynamicallyInvokable]
	public sealed class SuppressMessageAttribute : Attribute
	{
		// Token: 0x06003432 RID: 13362 RVA: 0x000C6AF5 File Offset: 0x000C4CF5
		[__DynamicallyInvokable]
		public SuppressMessageAttribute(string category, string checkId)
		{
			this.category = category;
			this.checkId = checkId;
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06003433 RID: 13363 RVA: 0x000C6B0B File Offset: 0x000C4D0B
		[__DynamicallyInvokable]
		public string Category
		{
			[__DynamicallyInvokable]
			get
			{
				return this.category;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06003434 RID: 13364 RVA: 0x000C6B13 File Offset: 0x000C4D13
		[__DynamicallyInvokable]
		public string CheckId
		{
			[__DynamicallyInvokable]
			get
			{
				return this.checkId;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06003435 RID: 13365 RVA: 0x000C6B1B File Offset: 0x000C4D1B
		// (set) Token: 0x06003436 RID: 13366 RVA: 0x000C6B23 File Offset: 0x000C4D23
		[__DynamicallyInvokable]
		public string Scope
		{
			[__DynamicallyInvokable]
			get
			{
				return this.scope;
			}
			[__DynamicallyInvokable]
			set
			{
				this.scope = value;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06003437 RID: 13367 RVA: 0x000C6B2C File Offset: 0x000C4D2C
		// (set) Token: 0x06003438 RID: 13368 RVA: 0x000C6B34 File Offset: 0x000C4D34
		[__DynamicallyInvokable]
		public string Target
		{
			[__DynamicallyInvokable]
			get
			{
				return this.target;
			}
			[__DynamicallyInvokable]
			set
			{
				this.target = value;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06003439 RID: 13369 RVA: 0x000C6B3D File Offset: 0x000C4D3D
		// (set) Token: 0x0600343A RID: 13370 RVA: 0x000C6B45 File Offset: 0x000C4D45
		[__DynamicallyInvokable]
		public string MessageId
		{
			[__DynamicallyInvokable]
			get
			{
				return this.messageId;
			}
			[__DynamicallyInvokable]
			set
			{
				this.messageId = value;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x0600343B RID: 13371 RVA: 0x000C6B4E File Offset: 0x000C4D4E
		// (set) Token: 0x0600343C RID: 13372 RVA: 0x000C6B56 File Offset: 0x000C4D56
		[__DynamicallyInvokable]
		public string Justification
		{
			[__DynamicallyInvokable]
			get
			{
				return this.justification;
			}
			[__DynamicallyInvokable]
			set
			{
				this.justification = value;
			}
		}

		// Token: 0x04001722 RID: 5922
		private string category;

		// Token: 0x04001723 RID: 5923
		private string justification;

		// Token: 0x04001724 RID: 5924
		private string checkId;

		// Token: 0x04001725 RID: 5925
		private string scope;

		// Token: 0x04001726 RID: 5926
		private string target;

		// Token: 0x04001727 RID: 5927
		private string messageId;
	}
}
