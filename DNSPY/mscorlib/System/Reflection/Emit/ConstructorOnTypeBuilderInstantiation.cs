using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x0200066B RID: 1643
	internal sealed class ConstructorOnTypeBuilderInstantiation : ConstructorInfo
	{
		// Token: 0x06004EE1 RID: 20193 RVA: 0x0011BC20 File Offset: 0x00119E20
		internal static ConstructorInfo GetConstructor(ConstructorInfo Constructor, TypeBuilderInstantiation type)
		{
			return new ConstructorOnTypeBuilderInstantiation(Constructor, type);
		}

		// Token: 0x06004EE2 RID: 20194 RVA: 0x0011BC29 File Offset: 0x00119E29
		internal ConstructorOnTypeBuilderInstantiation(ConstructorInfo constructor, TypeBuilderInstantiation type)
		{
			this.m_ctor = constructor;
			this.m_type = type;
		}

		// Token: 0x06004EE3 RID: 20195 RVA: 0x0011BC3F File Offset: 0x00119E3F
		internal override Type[] GetParameterTypes()
		{
			return this.m_ctor.GetParameterTypes();
		}

		// Token: 0x06004EE4 RID: 20196 RVA: 0x0011BC4C File Offset: 0x00119E4C
		internal override Type GetReturnType()
		{
			return this.DeclaringType;
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06004EE5 RID: 20197 RVA: 0x0011BC54 File Offset: 0x00119E54
		public override MemberTypes MemberType
		{
			get
			{
				return this.m_ctor.MemberType;
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06004EE6 RID: 20198 RVA: 0x0011BC61 File Offset: 0x00119E61
		public override string Name
		{
			get
			{
				return this.m_ctor.Name;
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06004EE7 RID: 20199 RVA: 0x0011BC6E File Offset: 0x00119E6E
		public override Type DeclaringType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06004EE8 RID: 20200 RVA: 0x0011BC76 File Offset: 0x00119E76
		public override Type ReflectedType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x06004EE9 RID: 20201 RVA: 0x0011BC7E File Offset: 0x00119E7E
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_ctor.GetCustomAttributes(inherit);
		}

		// Token: 0x06004EEA RID: 20202 RVA: 0x0011BC8C File Offset: 0x00119E8C
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_ctor.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004EEB RID: 20203 RVA: 0x0011BC9B File Offset: 0x00119E9B
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_ctor.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06004EEC RID: 20204 RVA: 0x0011BCAC File Offset: 0x00119EAC
		internal int MetadataTokenInternal
		{
			get
			{
				ConstructorBuilder constructorBuilder = this.m_ctor as ConstructorBuilder;
				if (constructorBuilder != null)
				{
					return constructorBuilder.MetadataTokenInternal;
				}
				return this.m_ctor.MetadataToken;
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06004EED RID: 20205 RVA: 0x0011BCE0 File Offset: 0x00119EE0
		public override Module Module
		{
			get
			{
				return this.m_ctor.Module;
			}
		}

		// Token: 0x06004EEE RID: 20206 RVA: 0x0011BCED File Offset: 0x00119EED
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004EEF RID: 20207 RVA: 0x0011BCF5 File Offset: 0x00119EF5
		public override ParameterInfo[] GetParameters()
		{
			return this.m_ctor.GetParameters();
		}

		// Token: 0x06004EF0 RID: 20208 RVA: 0x0011BD02 File Offset: 0x00119F02
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_ctor.GetMethodImplementationFlags();
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06004EF1 RID: 20209 RVA: 0x0011BD0F File Offset: 0x00119F0F
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.m_ctor.MethodHandle;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06004EF2 RID: 20210 RVA: 0x0011BD1C File Offset: 0x00119F1C
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_ctor.Attributes;
			}
		}

		// Token: 0x06004EF3 RID: 20211 RVA: 0x0011BD29 File Offset: 0x00119F29
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06004EF4 RID: 20212 RVA: 0x0011BD30 File Offset: 0x00119F30
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_ctor.CallingConvention;
			}
		}

		// Token: 0x06004EF5 RID: 20213 RVA: 0x0011BD3D File Offset: 0x00119F3D
		public override Type[] GetGenericArguments()
		{
			return this.m_ctor.GetGenericArguments();
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06004EF6 RID: 20214 RVA: 0x0011BD4A File Offset: 0x00119F4A
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06004EF7 RID: 20215 RVA: 0x0011BD4D File Offset: 0x00119F4D
		public override bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06004EF8 RID: 20216 RVA: 0x0011BD50 File Offset: 0x00119F50
		public override bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004EF9 RID: 20217 RVA: 0x0011BD53 File Offset: 0x00119F53
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x040021DF RID: 8671
		internal ConstructorInfo m_ctor;

		// Token: 0x040021E0 RID: 8672
		private TypeBuilderInstantiation m_type;
	}
}
