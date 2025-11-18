using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x0200066A RID: 1642
	internal sealed class MethodOnTypeBuilderInstantiation : MethodInfo
	{
		// Token: 0x06004EC4 RID: 20164 RVA: 0x0011BA8E File Offset: 0x00119C8E
		internal static MethodInfo GetMethod(MethodInfo method, TypeBuilderInstantiation type)
		{
			return new MethodOnTypeBuilderInstantiation(method, type);
		}

		// Token: 0x06004EC5 RID: 20165 RVA: 0x0011BA97 File Offset: 0x00119C97
		internal MethodOnTypeBuilderInstantiation(MethodInfo method, TypeBuilderInstantiation type)
		{
			this.m_method = method;
			this.m_type = type;
		}

		// Token: 0x06004EC6 RID: 20166 RVA: 0x0011BAAD File Offset: 0x00119CAD
		internal override Type[] GetParameterTypes()
		{
			return this.m_method.GetParameterTypes();
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06004EC7 RID: 20167 RVA: 0x0011BABA File Offset: 0x00119CBA
		public override MemberTypes MemberType
		{
			get
			{
				return this.m_method.MemberType;
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06004EC8 RID: 20168 RVA: 0x0011BAC7 File Offset: 0x00119CC7
		public override string Name
		{
			get
			{
				return this.m_method.Name;
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06004EC9 RID: 20169 RVA: 0x0011BAD4 File Offset: 0x00119CD4
		public override Type DeclaringType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06004ECA RID: 20170 RVA: 0x0011BADC File Offset: 0x00119CDC
		public override Type ReflectedType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x06004ECB RID: 20171 RVA: 0x0011BAE4 File Offset: 0x00119CE4
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_method.GetCustomAttributes(inherit);
		}

		// Token: 0x06004ECC RID: 20172 RVA: 0x0011BAF2 File Offset: 0x00119CF2
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_method.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004ECD RID: 20173 RVA: 0x0011BB01 File Offset: 0x00119D01
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_method.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06004ECE RID: 20174 RVA: 0x0011BB10 File Offset: 0x00119D10
		internal int MetadataTokenInternal
		{
			get
			{
				MethodBuilder methodBuilder = this.m_method as MethodBuilder;
				if (methodBuilder != null)
				{
					return methodBuilder.MetadataTokenInternal;
				}
				return this.m_method.MetadataToken;
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06004ECF RID: 20175 RVA: 0x0011BB44 File Offset: 0x00119D44
		public override Module Module
		{
			get
			{
				return this.m_method.Module;
			}
		}

		// Token: 0x06004ED0 RID: 20176 RVA: 0x0011BB51 File Offset: 0x00119D51
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004ED1 RID: 20177 RVA: 0x0011BB59 File Offset: 0x00119D59
		public override ParameterInfo[] GetParameters()
		{
			return this.m_method.GetParameters();
		}

		// Token: 0x06004ED2 RID: 20178 RVA: 0x0011BB66 File Offset: 0x00119D66
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_method.GetMethodImplementationFlags();
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06004ED3 RID: 20179 RVA: 0x0011BB73 File Offset: 0x00119D73
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.m_method.MethodHandle;
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06004ED4 RID: 20180 RVA: 0x0011BB80 File Offset: 0x00119D80
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_method.Attributes;
			}
		}

		// Token: 0x06004ED5 RID: 20181 RVA: 0x0011BB8D File Offset: 0x00119D8D
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06004ED6 RID: 20182 RVA: 0x0011BB94 File Offset: 0x00119D94
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_method.CallingConvention;
			}
		}

		// Token: 0x06004ED7 RID: 20183 RVA: 0x0011BBA1 File Offset: 0x00119DA1
		public override Type[] GetGenericArguments()
		{
			return this.m_method.GetGenericArguments();
		}

		// Token: 0x06004ED8 RID: 20184 RVA: 0x0011BBAE File Offset: 0x00119DAE
		public override MethodInfo GetGenericMethodDefinition()
		{
			return this.m_method;
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06004ED9 RID: 20185 RVA: 0x0011BBB6 File Offset: 0x00119DB6
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.m_method.IsGenericMethodDefinition;
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06004EDA RID: 20186 RVA: 0x0011BBC3 File Offset: 0x00119DC3
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.m_method.ContainsGenericParameters;
			}
		}

		// Token: 0x06004EDB RID: 20187 RVA: 0x0011BBD0 File Offset: 0x00119DD0
		public override MethodInfo MakeGenericMethod(params Type[] typeArgs)
		{
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericMethodDefinition"));
			}
			return MethodBuilderInstantiation.MakeGenericMethod(this, typeArgs);
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06004EDC RID: 20188 RVA: 0x0011BBF1 File Offset: 0x00119DF1
		public override bool IsGenericMethod
		{
			get
			{
				return this.m_method.IsGenericMethod;
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06004EDD RID: 20189 RVA: 0x0011BBFE File Offset: 0x00119DFE
		public override Type ReturnType
		{
			get
			{
				return this.m_method.ReturnType;
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06004EDE RID: 20190 RVA: 0x0011BC0B File Offset: 0x00119E0B
		public override ParameterInfo ReturnParameter
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06004EDF RID: 20191 RVA: 0x0011BC12 File Offset: 0x00119E12
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004EE0 RID: 20192 RVA: 0x0011BC19 File Offset: 0x00119E19
		public override MethodInfo GetBaseDefinition()
		{
			throw new NotSupportedException();
		}

		// Token: 0x040021DD RID: 8669
		internal MethodInfo m_method;

		// Token: 0x040021DE RID: 8670
		private TypeBuilderInstantiation m_type;
	}
}
