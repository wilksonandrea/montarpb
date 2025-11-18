using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x0200064B RID: 1611
	internal sealed class MethodBuilderInstantiation : MethodInfo
	{
		// Token: 0x06004B88 RID: 19336 RVA: 0x001117E0 File Offset: 0x0010F9E0
		internal static MethodInfo MakeGenericMethod(MethodInfo method, Type[] inst)
		{
			if (!method.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException();
			}
			return new MethodBuilderInstantiation(method, inst);
		}

		// Token: 0x06004B89 RID: 19337 RVA: 0x001117F7 File Offset: 0x0010F9F7
		internal MethodBuilderInstantiation(MethodInfo method, Type[] inst)
		{
			this.m_method = method;
			this.m_inst = inst;
		}

		// Token: 0x06004B8A RID: 19338 RVA: 0x0011180D File Offset: 0x0010FA0D
		internal override Type[] GetParameterTypes()
		{
			return this.m_method.GetParameterTypes();
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06004B8B RID: 19339 RVA: 0x0011181A File Offset: 0x0010FA1A
		public override MemberTypes MemberType
		{
			get
			{
				return this.m_method.MemberType;
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06004B8C RID: 19340 RVA: 0x00111827 File Offset: 0x0010FA27
		public override string Name
		{
			get
			{
				return this.m_method.Name;
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06004B8D RID: 19341 RVA: 0x00111834 File Offset: 0x0010FA34
		public override Type DeclaringType
		{
			get
			{
				return this.m_method.DeclaringType;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06004B8E RID: 19342 RVA: 0x00111841 File Offset: 0x0010FA41
		public override Type ReflectedType
		{
			get
			{
				return this.m_method.ReflectedType;
			}
		}

		// Token: 0x06004B8F RID: 19343 RVA: 0x0011184E File Offset: 0x0010FA4E
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_method.GetCustomAttributes(inherit);
		}

		// Token: 0x06004B90 RID: 19344 RVA: 0x0011185C File Offset: 0x0010FA5C
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_method.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004B91 RID: 19345 RVA: 0x0011186B File Offset: 0x0010FA6B
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_method.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06004B92 RID: 19346 RVA: 0x0011187A File Offset: 0x0010FA7A
		public override Module Module
		{
			get
			{
				return this.m_method.Module;
			}
		}

		// Token: 0x06004B93 RID: 19347 RVA: 0x00111887 File Offset: 0x0010FA87
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004B94 RID: 19348 RVA: 0x0011188F File Offset: 0x0010FA8F
		public override ParameterInfo[] GetParameters()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004B95 RID: 19349 RVA: 0x00111896 File Offset: 0x0010FA96
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_method.GetMethodImplementationFlags();
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06004B96 RID: 19350 RVA: 0x001118A3 File Offset: 0x0010FAA3
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06004B97 RID: 19351 RVA: 0x001118B4 File Offset: 0x0010FAB4
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_method.Attributes;
			}
		}

		// Token: 0x06004B98 RID: 19352 RVA: 0x001118C1 File Offset: 0x0010FAC1
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06004B99 RID: 19353 RVA: 0x001118C8 File Offset: 0x0010FAC8
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_method.CallingConvention;
			}
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x001118D5 File Offset: 0x0010FAD5
		public override Type[] GetGenericArguments()
		{
			return this.m_inst;
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x001118DD File Offset: 0x0010FADD
		public override MethodInfo GetGenericMethodDefinition()
		{
			return this.m_method;
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06004B9C RID: 19356 RVA: 0x001118E5 File Offset: 0x0010FAE5
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06004B9D RID: 19357 RVA: 0x001118E8 File Offset: 0x0010FAE8
		public override bool ContainsGenericParameters
		{
			get
			{
				for (int i = 0; i < this.m_inst.Length; i++)
				{
					if (this.m_inst[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return this.DeclaringType != null && this.DeclaringType.ContainsGenericParameters;
			}
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x00111937 File Offset: 0x0010FB37
		public override MethodInfo MakeGenericMethod(params Type[] arguments)
		{
			throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericMethodDefinition"));
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06004B9F RID: 19359 RVA: 0x00111948 File Offset: 0x0010FB48
		public override bool IsGenericMethod
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06004BA0 RID: 19360 RVA: 0x0011194B File Offset: 0x0010FB4B
		public override Type ReturnType
		{
			get
			{
				return this.m_method.ReturnType;
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06004BA1 RID: 19361 RVA: 0x00111958 File Offset: 0x0010FB58
		public override ParameterInfo ReturnParameter
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06004BA2 RID: 19362 RVA: 0x0011195F File Offset: 0x0010FB5F
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004BA3 RID: 19363 RVA: 0x00111966 File Offset: 0x0010FB66
		public override MethodInfo GetBaseDefinition()
		{
			throw new NotSupportedException();
		}

		// Token: 0x04001F41 RID: 8001
		internal MethodInfo m_method;

		// Token: 0x04001F42 RID: 8002
		private Type[] m_inst;
	}
}
