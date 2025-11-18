using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000726 RID: 1830
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class TargetFrameworkAttribute : Attribute
	{
		// Token: 0x06005168 RID: 20840 RVA: 0x0011EE19 File Offset: 0x0011D019
		[__DynamicallyInvokable]
		public TargetFrameworkAttribute(string frameworkName)
		{
			if (frameworkName == null)
			{
				throw new ArgumentNullException("frameworkName");
			}
			this._frameworkName = frameworkName;
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06005169 RID: 20841 RVA: 0x0011EE36 File Offset: 0x0011D036
		[__DynamicallyInvokable]
		public string FrameworkName
		{
			[__DynamicallyInvokable]
			get
			{
				return this._frameworkName;
			}
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x0600516A RID: 20842 RVA: 0x0011EE3E File Offset: 0x0011D03E
		// (set) Token: 0x0600516B RID: 20843 RVA: 0x0011EE46 File Offset: 0x0011D046
		[__DynamicallyInvokable]
		public string FrameworkDisplayName
		{
			[__DynamicallyInvokable]
			get
			{
				return this._frameworkDisplayName;
			}
			[__DynamicallyInvokable]
			set
			{
				this._frameworkDisplayName = value;
			}
		}

		// Token: 0x0400242B RID: 9259
		private string _frameworkName;

		// Token: 0x0400242C RID: 9260
		private string _frameworkDisplayName;
	}
}
