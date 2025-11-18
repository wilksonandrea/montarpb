using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200092E RID: 2350
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class ComImportAttribute : Attribute
	{
		// Token: 0x0600602D RID: 24621 RVA: 0x0014B88A File Offset: 0x00149A8A
		internal static Attribute GetCustomAttribute(RuntimeType type)
		{
			if ((type.Attributes & TypeAttributes.Import) == TypeAttributes.NotPublic)
			{
				return null;
			}
			return new ComImportAttribute();
		}

		// Token: 0x0600602E RID: 24622 RVA: 0x0014B8A1 File Offset: 0x00149AA1
		internal static bool IsDefined(RuntimeType type)
		{
			return (type.Attributes & TypeAttributes.Import) > TypeAttributes.NotPublic;
		}

		// Token: 0x0600602F RID: 24623 RVA: 0x0014B8B2 File Offset: 0x00149AB2
		[__DynamicallyInvokable]
		public ComImportAttribute()
		{
		}
	}
}
