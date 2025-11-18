using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000A1 RID: 161
	[AttributeUsage(AttributeTargets.Method)]
	[ComVisible(true)]
	public sealed class LoaderOptimizationAttribute : Attribute
	{
		// Token: 0x06000962 RID: 2402 RVA: 0x0001E9D6 File Offset: 0x0001CBD6
		public LoaderOptimizationAttribute(byte value)
		{
			this._val = value;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0001E9E5 File Offset: 0x0001CBE5
		public LoaderOptimizationAttribute(LoaderOptimization value)
		{
			this._val = (byte)value;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0001E9F5 File Offset: 0x0001CBF5
		public LoaderOptimization Value
		{
			get
			{
				return (LoaderOptimization)this._val;
			}
		}

		// Token: 0x040003BE RID: 958
		internal byte _val;
	}
}
