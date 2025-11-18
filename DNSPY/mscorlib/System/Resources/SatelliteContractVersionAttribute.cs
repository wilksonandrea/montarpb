using System;
using System.Runtime.InteropServices;

namespace System.Resources
{
	// Token: 0x0200039E RID: 926
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class SatelliteContractVersionAttribute : Attribute
	{
		// Token: 0x06002DA0 RID: 11680 RVA: 0x000AE89C File Offset: 0x000ACA9C
		[__DynamicallyInvokable]
		public SatelliteContractVersionAttribute(string version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this._version = version;
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06002DA1 RID: 11681 RVA: 0x000AE8B9 File Offset: 0x000ACAB9
		[__DynamicallyInvokable]
		public string Version
		{
			[__DynamicallyInvokable]
			get
			{
				return this._version;
			}
		}

		// Token: 0x04001293 RID: 4755
		private string _version;
	}
}
