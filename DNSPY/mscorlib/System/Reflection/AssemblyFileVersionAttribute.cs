using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005BF RID: 1471
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyFileVersionAttribute : Attribute
	{
		// Token: 0x0600446A RID: 17514 RVA: 0x000FC267 File Offset: 0x000FA467
		[__DynamicallyInvokable]
		public AssemblyFileVersionAttribute(string version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this._version = version;
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x0600446B RID: 17515 RVA: 0x000FC284 File Offset: 0x000FA484
		[__DynamicallyInvokable]
		public string Version
		{
			[__DynamicallyInvokable]
			get
			{
				return this._version;
			}
		}

		// Token: 0x04001C0E RID: 7182
		private string _version;
	}
}
