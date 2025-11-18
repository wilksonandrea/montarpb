using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005D7 RID: 1495
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct CustomAttributeNamedArgument
	{
		// Token: 0x0600454F RID: 17743 RVA: 0x000FEA4F File Offset: 0x000FCC4F
		public static bool operator ==(CustomAttributeNamedArgument left, CustomAttributeNamedArgument right)
		{
			return left.Equals(right);
		}

		// Token: 0x06004550 RID: 17744 RVA: 0x000FEA64 File Offset: 0x000FCC64
		public static bool operator !=(CustomAttributeNamedArgument left, CustomAttributeNamedArgument right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06004551 RID: 17745 RVA: 0x000FEA7C File Offset: 0x000FCC7C
		public CustomAttributeNamedArgument(MemberInfo memberInfo, object value)
		{
			if (memberInfo == null)
			{
				throw new ArgumentNullException("memberInfo");
			}
			FieldInfo fieldInfo = memberInfo as FieldInfo;
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			Type type;
			if (fieldInfo != null)
			{
				type = fieldInfo.FieldType;
			}
			else
			{
				if (!(propertyInfo != null))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidMemberForNamedArgument"));
				}
				type = propertyInfo.PropertyType;
			}
			this.m_memberInfo = memberInfo;
			this.m_value = new CustomAttributeTypedArgument(type, value);
		}

		// Token: 0x06004552 RID: 17746 RVA: 0x000FEAF5 File Offset: 0x000FCCF5
		public CustomAttributeNamedArgument(MemberInfo memberInfo, CustomAttributeTypedArgument typedArgument)
		{
			if (memberInfo == null)
			{
				throw new ArgumentNullException("memberInfo");
			}
			this.m_memberInfo = memberInfo;
			this.m_value = typedArgument;
		}

		// Token: 0x06004553 RID: 17747 RVA: 0x000FEB1C File Offset: 0x000FCD1C
		public override string ToString()
		{
			if (this.m_memberInfo == null)
			{
				return base.ToString();
			}
			return string.Format(CultureInfo.CurrentCulture, "{0} = {1}", this.MemberInfo.Name, this.TypedValue.ToString(this.ArgumentType != typeof(object)));
		}

		// Token: 0x06004554 RID: 17748 RVA: 0x000FEB85 File Offset: 0x000FCD85
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004555 RID: 17749 RVA: 0x000FEB97 File Offset: 0x000FCD97
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06004556 RID: 17750 RVA: 0x000FEBA7 File Offset: 0x000FCDA7
		internal Type ArgumentType
		{
			get
			{
				if (!(this.m_memberInfo is FieldInfo))
				{
					return ((PropertyInfo)this.m_memberInfo).PropertyType;
				}
				return ((FieldInfo)this.m_memberInfo).FieldType;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06004557 RID: 17751 RVA: 0x000FEBD7 File Offset: 0x000FCDD7
		public MemberInfo MemberInfo
		{
			get
			{
				return this.m_memberInfo;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06004558 RID: 17752 RVA: 0x000FEBDF File Offset: 0x000FCDDF
		[__DynamicallyInvokable]
		public CustomAttributeTypedArgument TypedValue
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06004559 RID: 17753 RVA: 0x000FEBE7 File Offset: 0x000FCDE7
		[__DynamicallyInvokable]
		public string MemberName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.MemberInfo.Name;
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x0600455A RID: 17754 RVA: 0x000FEBF4 File Offset: 0x000FCDF4
		[__DynamicallyInvokable]
		public bool IsField
		{
			[__DynamicallyInvokable]
			get
			{
				return this.MemberInfo is FieldInfo;
			}
		}

		// Token: 0x04001C68 RID: 7272
		private MemberInfo m_memberInfo;

		// Token: 0x04001C69 RID: 7273
		private CustomAttributeTypedArgument m_value;
	}
}
