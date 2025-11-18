using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000935 RID: 2357
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Method, AllowMultiple = false)]
	[ComVisible(false)]
	[__DynamicallyInvokable]
	public sealed class DefaultDllImportSearchPathsAttribute : Attribute
	{
		// Token: 0x0600603E RID: 24638 RVA: 0x0014B964 File Offset: 0x00149B64
		[__DynamicallyInvokable]
		public DefaultDllImportSearchPathsAttribute(DllImportSearchPath paths)
		{
			this._paths = paths;
		}

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x0600603F RID: 24639 RVA: 0x0014B973 File Offset: 0x00149B73
		[__DynamicallyInvokable]
		public DllImportSearchPath Paths
		{
			[__DynamicallyInvokable]
			get
			{
				return this._paths;
			}
		}

		// Token: 0x04002B19 RID: 11033
		internal DllImportSearchPath _paths;
	}
}
