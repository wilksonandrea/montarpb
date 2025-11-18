using System;

namespace System.Reflection
{
	// Token: 0x020005C7 RID: 1479
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	[__DynamicallyInvokable]
	public sealed class AssemblySignatureKeyAttribute : Attribute
	{
		// Token: 0x0600447F RID: 17535 RVA: 0x000FC371 File Offset: 0x000FA571
		[__DynamicallyInvokable]
		public AssemblySignatureKeyAttribute(string publicKey, string countersignature)
		{
			this._publicKey = publicKey;
			this._countersignature = countersignature;
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06004480 RID: 17536 RVA: 0x000FC387 File Offset: 0x000FA587
		[__DynamicallyInvokable]
		public string PublicKey
		{
			[__DynamicallyInvokable]
			get
			{
				return this._publicKey;
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06004481 RID: 17537 RVA: 0x000FC38F File Offset: 0x000FA58F
		[__DynamicallyInvokable]
		public string Countersignature
		{
			[__DynamicallyInvokable]
			get
			{
				return this._countersignature;
			}
		}

		// Token: 0x04001C17 RID: 7191
		private string _publicKey;

		// Token: 0x04001C18 RID: 7192
		private string _countersignature;
	}
}
