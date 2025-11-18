using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008B0 RID: 2224
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class CustomConstantAttribute : Attribute
	{
		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x06005D9F RID: 23967
		[__DynamicallyInvokable]
		public abstract object Value
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x06005DA0 RID: 23968 RVA: 0x00149254 File Offset: 0x00147454
		internal static object GetRawConstant(CustomAttributeData attr)
		{
			foreach (CustomAttributeNamedArgument customAttributeNamedArgument in attr.NamedArguments)
			{
				if (customAttributeNamedArgument.MemberInfo.Name.Equals("Value"))
				{
					return customAttributeNamedArgument.TypedValue.Value;
				}
			}
			return DBNull.Value;
		}

		// Token: 0x06005DA1 RID: 23969 RVA: 0x001492CC File Offset: 0x001474CC
		[__DynamicallyInvokable]
		protected CustomConstantAttribute()
		{
		}
	}
}
