using System;

namespace System.Reflection
{
	// Token: 0x020005C6 RID: 1478
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class AssemblyMetadataAttribute : Attribute
	{
		// Token: 0x0600447C RID: 17532 RVA: 0x000FC34B File Offset: 0x000FA54B
		[__DynamicallyInvokable]
		public AssemblyMetadataAttribute(string key, string value)
		{
			this.m_key = key;
			this.m_value = value;
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x0600447D RID: 17533 RVA: 0x000FC361 File Offset: 0x000FA561
		[__DynamicallyInvokable]
		public string Key
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_key;
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x0600447E RID: 17534 RVA: 0x000FC369 File Offset: 0x000FA569
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x04001C15 RID: 7189
		private string m_key;

		// Token: 0x04001C16 RID: 7190
		private string m_value;
	}
}
