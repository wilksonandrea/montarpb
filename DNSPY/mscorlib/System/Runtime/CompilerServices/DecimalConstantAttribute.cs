using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008B3 RID: 2227
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DecimalConstantAttribute : Attribute
	{
		// Token: 0x06005DA6 RID: 23974 RVA: 0x0014939C File Offset: 0x0014759C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public DecimalConstantAttribute(byte scale, byte sign, uint hi, uint mid, uint low)
		{
			this.dec = new decimal((int)low, (int)mid, (int)hi, sign > 0, scale);
		}

		// Token: 0x06005DA7 RID: 23975 RVA: 0x001493B9 File Offset: 0x001475B9
		[__DynamicallyInvokable]
		public DecimalConstantAttribute(byte scale, byte sign, int hi, int mid, int low)
		{
			this.dec = new decimal(low, mid, hi, sign > 0, scale);
		}

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x06005DA8 RID: 23976 RVA: 0x001493D6 File Offset: 0x001475D6
		[__DynamicallyInvokable]
		public decimal Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.dec;
			}
		}

		// Token: 0x06005DA9 RID: 23977 RVA: 0x001493E0 File Offset: 0x001475E0
		internal static decimal GetRawDecimalConstant(CustomAttributeData attr)
		{
			foreach (CustomAttributeNamedArgument customAttributeNamedArgument in attr.NamedArguments)
			{
				if (customAttributeNamedArgument.MemberInfo.Name.Equals("Value"))
				{
					return (decimal)customAttributeNamedArgument.TypedValue.Value;
				}
			}
			ParameterInfo[] parameters = attr.Constructor.GetParameters();
			IList<CustomAttributeTypedArgument> constructorArguments = attr.ConstructorArguments;
			if (parameters[2].ParameterType == typeof(uint))
			{
				int num = (int)((uint)constructorArguments[4].Value);
				int num2 = (int)((uint)constructorArguments[3].Value);
				int num3 = (int)((uint)constructorArguments[2].Value);
				byte b = (byte)constructorArguments[1].Value;
				byte b2 = (byte)constructorArguments[0].Value;
				return new decimal(num, num2, num3, b > 0, b2);
			}
			int num4 = (int)constructorArguments[4].Value;
			int num5 = (int)constructorArguments[3].Value;
			int num6 = (int)constructorArguments[2].Value;
			byte b3 = (byte)constructorArguments[1].Value;
			byte b4 = (byte)constructorArguments[0].Value;
			return new decimal(num4, num5, num6, b3 > 0, b4);
		}

		// Token: 0x04002A1A RID: 10778
		private decimal dec;
	}
}
