using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x0200066C RID: 1644
	internal sealed class FieldOnTypeBuilderInstantiation : FieldInfo
	{
		// Token: 0x06004EFA RID: 20218 RVA: 0x0011BD5C File Offset: 0x00119F5C
		internal static FieldInfo GetField(FieldInfo Field, TypeBuilderInstantiation type)
		{
			FieldInfo fieldInfo;
			if (type.m_hashtable.Contains(Field))
			{
				fieldInfo = type.m_hashtable[Field] as FieldInfo;
			}
			else
			{
				fieldInfo = new FieldOnTypeBuilderInstantiation(Field, type);
				type.m_hashtable[Field] = fieldInfo;
			}
			return fieldInfo;
		}

		// Token: 0x06004EFB RID: 20219 RVA: 0x0011BDA3 File Offset: 0x00119FA3
		internal FieldOnTypeBuilderInstantiation(FieldInfo field, TypeBuilderInstantiation type)
		{
			this.m_field = field;
			this.m_type = type;
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06004EFC RID: 20220 RVA: 0x0011BDB9 File Offset: 0x00119FB9
		internal FieldInfo FieldInfo
		{
			get
			{
				return this.m_field;
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06004EFD RID: 20221 RVA: 0x0011BDC1 File Offset: 0x00119FC1
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Field;
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06004EFE RID: 20222 RVA: 0x0011BDC4 File Offset: 0x00119FC4
		public override string Name
		{
			get
			{
				return this.m_field.Name;
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06004EFF RID: 20223 RVA: 0x0011BDD1 File Offset: 0x00119FD1
		public override Type DeclaringType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06004F00 RID: 20224 RVA: 0x0011BDD9 File Offset: 0x00119FD9
		public override Type ReflectedType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x06004F01 RID: 20225 RVA: 0x0011BDE1 File Offset: 0x00119FE1
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_field.GetCustomAttributes(inherit);
		}

		// Token: 0x06004F02 RID: 20226 RVA: 0x0011BDEF File Offset: 0x00119FEF
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_field.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004F03 RID: 20227 RVA: 0x0011BDFE File Offset: 0x00119FFE
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_field.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06004F04 RID: 20228 RVA: 0x0011BE10 File Offset: 0x0011A010
		internal int MetadataTokenInternal
		{
			get
			{
				FieldBuilder fieldBuilder = this.m_field as FieldBuilder;
				if (fieldBuilder != null)
				{
					return fieldBuilder.MetadataTokenInternal;
				}
				return this.m_field.MetadataToken;
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06004F05 RID: 20229 RVA: 0x0011BE44 File Offset: 0x0011A044
		public override Module Module
		{
			get
			{
				return this.m_field.Module;
			}
		}

		// Token: 0x06004F06 RID: 20230 RVA: 0x0011BE51 File Offset: 0x0011A051
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004F07 RID: 20231 RVA: 0x0011BE59 File Offset: 0x0011A059
		public override Type[] GetRequiredCustomModifiers()
		{
			return this.m_field.GetRequiredCustomModifiers();
		}

		// Token: 0x06004F08 RID: 20232 RVA: 0x0011BE66 File Offset: 0x0011A066
		public override Type[] GetOptionalCustomModifiers()
		{
			return this.m_field.GetOptionalCustomModifiers();
		}

		// Token: 0x06004F09 RID: 20233 RVA: 0x0011BE73 File Offset: 0x0011A073
		public override void SetValueDirect(TypedReference obj, object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004F0A RID: 20234 RVA: 0x0011BE7A File Offset: 0x0011A07A
		public override object GetValueDirect(TypedReference obj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06004F0B RID: 20235 RVA: 0x0011BE81 File Offset: 0x0011A081
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06004F0C RID: 20236 RVA: 0x0011BE88 File Offset: 0x0011A088
		public override Type FieldType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x0011BE8F File Offset: 0x0011A08F
		public override object GetValue(object obj)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06004F0E RID: 20238 RVA: 0x0011BE96 File Offset: 0x0011A096
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06004F0F RID: 20239 RVA: 0x0011BE9D File Offset: 0x0011A09D
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_field.Attributes;
			}
		}

		// Token: 0x040021E1 RID: 8673
		private FieldInfo m_field;

		// Token: 0x040021E2 RID: 8674
		private TypeBuilderInstantiation m_type;
	}
}
