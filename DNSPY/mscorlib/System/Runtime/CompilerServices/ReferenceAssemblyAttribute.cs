using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008E4 RID: 2276
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class ReferenceAssemblyAttribute : Attribute
	{
		// Token: 0x06005DE4 RID: 24036 RVA: 0x00149852 File Offset: 0x00147A52
		[__DynamicallyInvokable]
		public ReferenceAssemblyAttribute()
		{
		}

		// Token: 0x06005DE5 RID: 24037 RVA: 0x0014985A File Offset: 0x00147A5A
		[__DynamicallyInvokable]
		public ReferenceAssemblyAttribute(string description)
		{
			this._description = description;
		}

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x06005DE6 RID: 24038 RVA: 0x00149869 File Offset: 0x00147A69
		[__DynamicallyInvokable]
		public string Description
		{
			[__DynamicallyInvokable]
			get
			{
				return this._description;
			}
		}

		// Token: 0x04002A43 RID: 10819
		private string _description;
	}
}
