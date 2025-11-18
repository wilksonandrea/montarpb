using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008C1 RID: 2241
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class MethodImplAttribute : Attribute
	{
		// Token: 0x06005DBA RID: 23994 RVA: 0x00149644 File Offset: 0x00147844
		internal MethodImplAttribute(MethodImplAttributes methodImplAttributes)
		{
			MethodImplOptions methodImplOptions = MethodImplOptions.Unmanaged | MethodImplOptions.ForwardRef | MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall | MethodImplOptions.Synchronized | MethodImplOptions.NoInlining | MethodImplOptions.AggressiveInlining | MethodImplOptions.NoOptimization | MethodImplOptions.SecurityMitigations;
			this._val = (MethodImplOptions)(methodImplAttributes & (MethodImplAttributes)methodImplOptions);
		}

		// Token: 0x06005DBB RID: 23995 RVA: 0x00149666 File Offset: 0x00147866
		[__DynamicallyInvokable]
		public MethodImplAttribute(MethodImplOptions methodImplOptions)
		{
			this._val = methodImplOptions;
		}

		// Token: 0x06005DBC RID: 23996 RVA: 0x00149675 File Offset: 0x00147875
		public MethodImplAttribute(short value)
		{
			this._val = (MethodImplOptions)value;
		}

		// Token: 0x06005DBD RID: 23997 RVA: 0x00149684 File Offset: 0x00147884
		public MethodImplAttribute()
		{
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06005DBE RID: 23998 RVA: 0x0014968C File Offset: 0x0014788C
		[__DynamicallyInvokable]
		public MethodImplOptions Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A31 RID: 10801
		internal MethodImplOptions _val;

		// Token: 0x04002A32 RID: 10802
		public MethodCodeType MethodCodeType;
	}
}
