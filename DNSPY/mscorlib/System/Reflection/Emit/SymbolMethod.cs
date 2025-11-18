using System;
using System.Globalization;
using System.Security;

namespace System.Reflection.Emit
{
	// Token: 0x0200064E RID: 1614
	internal sealed class SymbolMethod : MethodInfo
	{
		// Token: 0x06004BDB RID: 19419 RVA: 0x001120BC File Offset: 0x001102BC
		[SecurityCritical]
		internal SymbolMethod(ModuleBuilder mod, MethodToken token, Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			this.m_mdMethod = token;
			this.m_returnType = returnType;
			if (parameterTypes != null)
			{
				this.m_parameterTypes = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.m_parameterTypes, parameterTypes.Length);
			}
			else
			{
				this.m_parameterTypes = EmptyArray<Type>.Value;
			}
			this.m_module = mod;
			this.m_containingType = arrayClass;
			this.m_name = methodName;
			this.m_callingConvention = callingConvention;
			this.m_signature = SignatureHelper.GetMethodSigHelper(mod, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x06004BDC RID: 19420 RVA: 0x00112143 File Offset: 0x00110343
		internal override Type[] GetParameterTypes()
		{
			return this.m_parameterTypes;
		}

		// Token: 0x06004BDD RID: 19421 RVA: 0x0011214B File Offset: 0x0011034B
		internal MethodToken GetToken(ModuleBuilder mod)
		{
			return mod.GetArrayMethodToken(this.m_containingType, this.m_name, this.m_callingConvention, this.m_returnType, this.m_parameterTypes);
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06004BDE RID: 19422 RVA: 0x00112171 File Offset: 0x00110371
		public override Module Module
		{
			get
			{
				return this.m_module;
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06004BDF RID: 19423 RVA: 0x00112179 File Offset: 0x00110379
		public override Type ReflectedType
		{
			get
			{
				return this.m_containingType;
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06004BE0 RID: 19424 RVA: 0x00112181 File Offset: 0x00110381
		public override string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06004BE1 RID: 19425 RVA: 0x00112189 File Offset: 0x00110389
		public override Type DeclaringType
		{
			get
			{
				return this.m_containingType;
			}
		}

		// Token: 0x06004BE2 RID: 19426 RVA: 0x00112191 File Offset: 0x00110391
		public override ParameterInfo[] GetParameters()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x001121A2 File Offset: 0x001103A2
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06004BE4 RID: 19428 RVA: 0x001121B3 File Offset: 0x001103B3
		public override MethodAttributes Attributes
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06004BE5 RID: 19429 RVA: 0x001121C4 File Offset: 0x001103C4
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_callingConvention;
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06004BE6 RID: 19430 RVA: 0x001121CC File Offset: 0x001103CC
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06004BE7 RID: 19431 RVA: 0x001121DD File Offset: 0x001103DD
		public override Type ReturnType
		{
			get
			{
				return this.m_returnType;
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06004BE8 RID: 19432 RVA: 0x001121E5 File Offset: 0x001103E5
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x001121E8 File Offset: 0x001103E8
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x001121F9 File Offset: 0x001103F9
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		// Token: 0x06004BEB RID: 19435 RVA: 0x001121FC File Offset: 0x001103FC
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x06004BEC RID: 19436 RVA: 0x0011220D File Offset: 0x0011040D
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x0011221E File Offset: 0x0011041E
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x06004BEE RID: 19438 RVA: 0x0011222F File Offset: 0x0011042F
		public Module GetModule()
		{
			return this.m_module;
		}

		// Token: 0x06004BEF RID: 19439 RVA: 0x00112237 File Offset: 0x00110437
		public MethodToken GetToken()
		{
			return this.m_mdMethod;
		}

		// Token: 0x04001F4E RID: 8014
		private ModuleBuilder m_module;

		// Token: 0x04001F4F RID: 8015
		private Type m_containingType;

		// Token: 0x04001F50 RID: 8016
		private string m_name;

		// Token: 0x04001F51 RID: 8017
		private CallingConventions m_callingConvention;

		// Token: 0x04001F52 RID: 8018
		private Type m_returnType;

		// Token: 0x04001F53 RID: 8019
		private MethodToken m_mdMethod;

		// Token: 0x04001F54 RID: 8020
		private Type[] m_parameterTypes;

		// Token: 0x04001F55 RID: 8021
		private SignatureHelper m_signature;
	}
}
