using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008B1 RID: 2225
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DateTimeConstantAttribute : CustomConstantAttribute
	{
		// Token: 0x06005DA2 RID: 23970 RVA: 0x001492D4 File Offset: 0x001474D4
		[__DynamicallyInvokable]
		public DateTimeConstantAttribute(long ticks)
		{
			this.date = new DateTime(ticks);
		}

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x06005DA3 RID: 23971 RVA: 0x001492E8 File Offset: 0x001474E8
		[__DynamicallyInvokable]
		public override object Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.date;
			}
		}

		// Token: 0x06005DA4 RID: 23972 RVA: 0x001492F8 File Offset: 0x001474F8
		internal static DateTime GetRawDateTimeConstant(CustomAttributeData attr)
		{
			foreach (CustomAttributeNamedArgument customAttributeNamedArgument in attr.NamedArguments)
			{
				if (customAttributeNamedArgument.MemberInfo.Name.Equals("Value"))
				{
					return new DateTime((long)customAttributeNamedArgument.TypedValue.Value);
				}
			}
			return new DateTime((long)attr.ConstructorArguments[0].Value);
		}

		// Token: 0x04002A19 RID: 10777
		private DateTime date;
	}
}
