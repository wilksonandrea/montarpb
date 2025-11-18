using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008BB RID: 2235
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class InternalsVisibleToAttribute : Attribute
	{
		// Token: 0x06005DB4 RID: 23988 RVA: 0x00149604 File Offset: 0x00147804
		[__DynamicallyInvokable]
		public InternalsVisibleToAttribute(string assemblyName)
		{
			this._assemblyName = assemblyName;
		}

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06005DB5 RID: 23989 RVA: 0x0014961A File Offset: 0x0014781A
		[__DynamicallyInvokable]
		public string AssemblyName
		{
			[__DynamicallyInvokable]
			get
			{
				return this._assemblyName;
			}
		}

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06005DB6 RID: 23990 RVA: 0x00149622 File Offset: 0x00147822
		// (set) Token: 0x06005DB7 RID: 23991 RVA: 0x0014962A File Offset: 0x0014782A
		public bool AllInternalsVisible
		{
			get
			{
				return this._allInternalsVisible;
			}
			set
			{
				this._allInternalsVisible = value;
			}
		}

		// Token: 0x04002A20 RID: 10784
		private string _assemblyName;

		// Token: 0x04002A21 RID: 10785
		private bool _allInternalsVisible = true;
	}
}
