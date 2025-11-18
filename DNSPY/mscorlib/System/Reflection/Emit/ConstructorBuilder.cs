using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x02000632 RID: 1586
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ConstructorBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ConstructorBuilder : ConstructorInfo, _ConstructorBuilder
	{
		// Token: 0x060049B3 RID: 18867 RVA: 0x0010A9E7 File Offset: 0x00108BE7
		private ConstructorBuilder()
		{
		}

		// Token: 0x060049B4 RID: 18868 RVA: 0x0010A9F0 File Offset: 0x00108BF0
		[SecurityCritical]
		internal ConstructorBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers, ModuleBuilder mod, TypeBuilder type)
		{
			this.m_methodBuilder = new MethodBuilder(name, attributes, callingConvention, null, null, null, parameterTypes, requiredCustomModifiers, optionalCustomModifiers, mod, type, false);
			type.m_listMethods.Add(this.m_methodBuilder);
			int num;
			byte[] array = this.m_methodBuilder.GetMethodSignature().InternalGetSignature(out num);
			MethodToken token = this.m_methodBuilder.GetToken();
		}

		// Token: 0x060049B5 RID: 18869 RVA: 0x0010AA50 File Offset: 0x00108C50
		[SecurityCritical]
		internal ConstructorBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, ModuleBuilder mod, TypeBuilder type)
			: this(name, attributes, callingConvention, parameterTypes, null, null, mod, type)
		{
		}

		// Token: 0x060049B6 RID: 18870 RVA: 0x0010AA6E File Offset: 0x00108C6E
		internal override Type[] GetParameterTypes()
		{
			return this.m_methodBuilder.GetParameterTypes();
		}

		// Token: 0x060049B7 RID: 18871 RVA: 0x0010AA7B File Offset: 0x00108C7B
		private TypeBuilder GetTypeBuilder()
		{
			return this.m_methodBuilder.GetTypeBuilder();
		}

		// Token: 0x060049B8 RID: 18872 RVA: 0x0010AA88 File Offset: 0x00108C88
		internal ModuleBuilder GetModuleBuilder()
		{
			return this.GetTypeBuilder().GetModuleBuilder();
		}

		// Token: 0x060049B9 RID: 18873 RVA: 0x0010AA95 File Offset: 0x00108C95
		public override string ToString()
		{
			return this.m_methodBuilder.ToString();
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x060049BA RID: 18874 RVA: 0x0010AAA2 File Offset: 0x00108CA2
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_methodBuilder.MetadataTokenInternal;
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x060049BB RID: 18875 RVA: 0x0010AAAF File Offset: 0x00108CAF
		public override Module Module
		{
			get
			{
				return this.m_methodBuilder.Module;
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x060049BC RID: 18876 RVA: 0x0010AABC File Offset: 0x00108CBC
		public override Type ReflectedType
		{
			get
			{
				return this.m_methodBuilder.ReflectedType;
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x060049BD RID: 18877 RVA: 0x0010AAC9 File Offset: 0x00108CC9
		public override Type DeclaringType
		{
			get
			{
				return this.m_methodBuilder.DeclaringType;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x060049BE RID: 18878 RVA: 0x0010AAD6 File Offset: 0x00108CD6
		public override string Name
		{
			get
			{
				return this.m_methodBuilder.Name;
			}
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x0010AAE3 File Offset: 0x00108CE3
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x060049C0 RID: 18880 RVA: 0x0010AAF4 File Offset: 0x00108CF4
		public override ParameterInfo[] GetParameters()
		{
			ConstructorInfo constructor = this.GetTypeBuilder().GetConstructor(this.m_methodBuilder.m_parameterTypes);
			return constructor.GetParameters();
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x060049C1 RID: 18881 RVA: 0x0010AB1E File Offset: 0x00108D1E
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_methodBuilder.Attributes;
			}
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x0010AB2B File Offset: 0x00108D2B
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_methodBuilder.GetMethodImplementationFlags();
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x060049C3 RID: 18883 RVA: 0x0010AB38 File Offset: 0x00108D38
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.m_methodBuilder.MethodHandle;
			}
		}

		// Token: 0x060049C4 RID: 18884 RVA: 0x0010AB45 File Offset: 0x00108D45
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x060049C5 RID: 18885 RVA: 0x0010AB56 File Offset: 0x00108D56
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_methodBuilder.GetCustomAttributes(inherit);
		}

		// Token: 0x060049C6 RID: 18886 RVA: 0x0010AB64 File Offset: 0x00108D64
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_methodBuilder.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x060049C7 RID: 18887 RVA: 0x0010AB73 File Offset: 0x00108D73
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_methodBuilder.IsDefined(attributeType, inherit);
		}

		// Token: 0x060049C8 RID: 18888 RVA: 0x0010AB82 File Offset: 0x00108D82
		public MethodToken GetToken()
		{
			return this.m_methodBuilder.GetToken();
		}

		// Token: 0x060049C9 RID: 18889 RVA: 0x0010AB8F File Offset: 0x00108D8F
		public ParameterBuilder DefineParameter(int iSequence, ParameterAttributes attributes, string strParamName)
		{
			attributes &= ~ParameterAttributes.ReservedMask;
			return this.m_methodBuilder.DefineParameter(iSequence, attributes, strParamName);
		}

		// Token: 0x060049CA RID: 18890 RVA: 0x0010ABA8 File Offset: 0x00108DA8
		public void SetSymCustomAttribute(string name, byte[] data)
		{
			this.m_methodBuilder.SetSymCustomAttribute(name, data);
		}

		// Token: 0x060049CB RID: 18891 RVA: 0x0010ABB7 File Offset: 0x00108DB7
		public ILGenerator GetILGenerator()
		{
			if (this.m_isDefaultConstructor)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DefaultConstructorILGen"));
			}
			return this.m_methodBuilder.GetILGenerator();
		}

		// Token: 0x060049CC RID: 18892 RVA: 0x0010ABDC File Offset: 0x00108DDC
		public ILGenerator GetILGenerator(int streamSize)
		{
			if (this.m_isDefaultConstructor)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DefaultConstructorILGen"));
			}
			return this.m_methodBuilder.GetILGenerator(streamSize);
		}

		// Token: 0x060049CD RID: 18893 RVA: 0x0010AC02 File Offset: 0x00108E02
		public void SetMethodBody(byte[] il, int maxStack, byte[] localSignature, IEnumerable<ExceptionHandler> exceptionHandlers, IEnumerable<int> tokenFixups)
		{
			if (this.m_isDefaultConstructor)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DefaultConstructorDefineBody"));
			}
			this.m_methodBuilder.SetMethodBody(il, maxStack, localSignature, exceptionHandlers, tokenFixups);
		}

		// Token: 0x060049CE RID: 18894 RVA: 0x0010AC30 File Offset: 0x00108E30
		[SecuritySafeCritical]
		public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
		{
			if (pset == null)
			{
				throw new ArgumentNullException("pset");
			}
			if (!Enum.IsDefined(typeof(SecurityAction), action) || action == SecurityAction.RequestMinimum || action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse)
			{
				throw new ArgumentOutOfRangeException("action");
			}
			if (this.m_methodBuilder.IsTypeCreated())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeHasBeenCreated"));
			}
			byte[] array = pset.EncodeXml();
			TypeBuilder.AddDeclarativeSecurity(this.GetModuleBuilder().GetNativeHandle(), this.GetToken().Token, action, array, array.Length);
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x060049CF RID: 18895 RVA: 0x0010ACC2 File Offset: 0x00108EC2
		public override CallingConventions CallingConvention
		{
			get
			{
				if (this.DeclaringType.IsGenericType)
				{
					return CallingConventions.HasThis;
				}
				return CallingConventions.Standard;
			}
		}

		// Token: 0x060049D0 RID: 18896 RVA: 0x0010ACD5 File Offset: 0x00108ED5
		public Module GetModule()
		{
			return this.m_methodBuilder.GetModule();
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x060049D1 RID: 18897 RVA: 0x0010ACE2 File Offset: 0x00108EE2
		[Obsolete("This property has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		public Type ReturnType
		{
			get
			{
				return this.GetReturnType();
			}
		}

		// Token: 0x060049D2 RID: 18898 RVA: 0x0010ACEA File Offset: 0x00108EEA
		internal override Type GetReturnType()
		{
			return this.m_methodBuilder.ReturnType;
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x060049D3 RID: 18899 RVA: 0x0010ACF7 File Offset: 0x00108EF7
		public string Signature
		{
			get
			{
				return this.m_methodBuilder.Signature;
			}
		}

		// Token: 0x060049D4 RID: 18900 RVA: 0x0010AD04 File Offset: 0x00108F04
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.m_methodBuilder.SetCustomAttribute(con, binaryAttribute);
		}

		// Token: 0x060049D5 RID: 18901 RVA: 0x0010AD13 File Offset: 0x00108F13
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.m_methodBuilder.SetCustomAttribute(customBuilder);
		}

		// Token: 0x060049D6 RID: 18902 RVA: 0x0010AD21 File Offset: 0x00108F21
		public void SetImplementationFlags(MethodImplAttributes attributes)
		{
			this.m_methodBuilder.SetImplementationFlags(attributes);
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x060049D7 RID: 18903 RVA: 0x0010AD2F File Offset: 0x00108F2F
		// (set) Token: 0x060049D8 RID: 18904 RVA: 0x0010AD3C File Offset: 0x00108F3C
		public bool InitLocals
		{
			get
			{
				return this.m_methodBuilder.InitLocals;
			}
			set
			{
				this.m_methodBuilder.InitLocals = value;
			}
		}

		// Token: 0x060049D9 RID: 18905 RVA: 0x0010AD4A File Offset: 0x00108F4A
		void _ConstructorBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060049DA RID: 18906 RVA: 0x0010AD51 File Offset: 0x00108F51
		void _ConstructorBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060049DB RID: 18907 RVA: 0x0010AD58 File Offset: 0x00108F58
		void _ConstructorBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060049DC RID: 18908 RVA: 0x0010AD5F File Offset: 0x00108F5F
		void _ConstructorBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001E8B RID: 7819
		private readonly MethodBuilder m_methodBuilder;

		// Token: 0x04001E8C RID: 7820
		internal bool m_isDefaultConstructor;
	}
}
