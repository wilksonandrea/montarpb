using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x02000648 RID: 1608
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_MethodBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class MethodBuilder : MethodInfo, _MethodBuilder
	{
		// Token: 0x06004B21 RID: 19233 RVA: 0x0010FDCC File Offset: 0x0010DFCC
		internal MethodBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, ModuleBuilder mod, TypeBuilder type, bool bIsGlobalMethod)
		{
			this.Init(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null, mod, type, bIsGlobalMethod);
		}

		// Token: 0x06004B22 RID: 19234 RVA: 0x0010FE00 File Offset: 0x0010E000
		internal MethodBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, ModuleBuilder mod, TypeBuilder type, bool bIsGlobalMethod)
		{
			this.Init(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, mod, type, bIsGlobalMethod);
		}

		// Token: 0x06004B23 RID: 19235 RVA: 0x0010FE38 File Offset: 0x0010E038
		private void Init(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, ModuleBuilder mod, TypeBuilder type, bool bIsGlobalMethod)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
			}
			if (name[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), "name");
			}
			if (mod == null)
			{
				throw new ArgumentNullException("mod");
			}
			if (parameterTypes != null)
			{
				foreach (Type type2 in parameterTypes)
				{
					if (type2 == null)
					{
						throw new ArgumentNullException("parameterTypes");
					}
				}
			}
			this.m_strName = name;
			this.m_module = mod;
			this.m_containingType = type;
			this.m_returnType = returnType;
			if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				callingConvention |= CallingConventions.HasThis;
			}
			else if ((attributes & MethodAttributes.Virtual) != MethodAttributes.PrivateScope)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_NoStaticVirtual"));
			}
			if ((attributes & MethodAttributes.SpecialName) != MethodAttributes.SpecialName && (type.Attributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask && (attributes & (MethodAttributes.Virtual | MethodAttributes.Abstract)) != (MethodAttributes.Virtual | MethodAttributes.Abstract) && (attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadAttributeOnInterfaceMethod"));
			}
			this.m_callingConvention = callingConvention;
			if (parameterTypes != null)
			{
				this.m_parameterTypes = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.m_parameterTypes, parameterTypes.Length);
			}
			else
			{
				this.m_parameterTypes = null;
			}
			this.m_returnTypeRequiredCustomModifiers = returnTypeRequiredCustomModifiers;
			this.m_returnTypeOptionalCustomModifiers = returnTypeOptionalCustomModifiers;
			this.m_parameterTypeRequiredCustomModifiers = parameterTypeRequiredCustomModifiers;
			this.m_parameterTypeOptionalCustomModifiers = parameterTypeOptionalCustomModifiers;
			this.m_iAttributes = attributes;
			this.m_bIsGlobalMethod = bIsGlobalMethod;
			this.m_bIsBaked = false;
			this.m_fInitLocals = true;
			this.m_localSymInfo = new LocalSymInfo();
			this.m_ubBody = null;
			this.m_ilGenerator = null;
			this.m_dwMethodImplFlags = MethodImplAttributes.IL;
		}

		// Token: 0x06004B24 RID: 19236 RVA: 0x0010FFE4 File Offset: 0x0010E1E4
		internal void CheckContext(params Type[][] typess)
		{
			this.m_module.CheckContext(typess);
		}

		// Token: 0x06004B25 RID: 19237 RVA: 0x0010FFF2 File Offset: 0x0010E1F2
		internal void CheckContext(params Type[] types)
		{
			this.m_module.CheckContext(types);
		}

		// Token: 0x06004B26 RID: 19238 RVA: 0x00110000 File Offset: 0x0010E200
		[SecurityCritical]
		internal void CreateMethodBodyHelper(ILGenerator il)
		{
			if (il == null)
			{
				throw new ArgumentNullException("il");
			}
			int num = 0;
			ModuleBuilder module = this.m_module;
			this.m_containingType.ThrowIfCreated();
			if (this.m_bIsBaked)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodHasBody"));
			}
			if (il.m_methodBuilder != this && il.m_methodBuilder != null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadILGeneratorUsage"));
			}
			this.ThrowIfShouldNotHaveBody();
			if (il.m_ScopeTree.m_iOpenScopeCount != 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OpenLocalVariableScope"));
			}
			this.m_ubBody = il.BakeByteArray();
			this.m_mdMethodFixups = il.GetTokenFixups();
			__ExceptionInfo[] exceptions = il.GetExceptions();
			int num2 = this.CalculateNumberOfExceptions(exceptions);
			if (num2 > 0)
			{
				this.m_exceptions = new ExceptionHandler[num2];
				for (int i = 0; i < exceptions.Length; i++)
				{
					int[] filterAddresses = exceptions[i].GetFilterAddresses();
					int[] catchAddresses = exceptions[i].GetCatchAddresses();
					int[] catchEndAddresses = exceptions[i].GetCatchEndAddresses();
					Type[] catchClass = exceptions[i].GetCatchClass();
					int numberOfCatches = exceptions[i].GetNumberOfCatches();
					int startAddress = exceptions[i].GetStartAddress();
					int endAddress = exceptions[i].GetEndAddress();
					int[] exceptionTypes = exceptions[i].GetExceptionTypes();
					for (int j = 0; j < numberOfCatches; j++)
					{
						int num3 = 0;
						if (catchClass[j] != null)
						{
							num3 = module.GetTypeTokenInternal(catchClass[j]).Token;
						}
						switch (exceptionTypes[j])
						{
						case 0:
						case 1:
						case 4:
							this.m_exceptions[num++] = new ExceptionHandler(startAddress, endAddress, filterAddresses[j], catchAddresses[j], catchEndAddresses[j], exceptionTypes[j], num3);
							break;
						case 2:
							this.m_exceptions[num++] = new ExceptionHandler(startAddress, exceptions[i].GetFinallyEndAddress(), filterAddresses[j], catchAddresses[j], catchEndAddresses[j], exceptionTypes[j], num3);
							break;
						}
					}
				}
			}
			this.m_bIsBaked = true;
			if (module.GetSymWriter() != null)
			{
				SymbolToken symbolToken = new SymbolToken(this.MetadataTokenInternal);
				ISymbolWriter symWriter = module.GetSymWriter();
				symWriter.OpenMethod(symbolToken);
				symWriter.OpenScope(0);
				if (this.m_symCustomAttrs != null)
				{
					foreach (MethodBuilder.SymCustomAttr symCustomAttr in this.m_symCustomAttrs)
					{
						module.GetSymWriter().SetSymAttribute(new SymbolToken(this.MetadataTokenInternal), symCustomAttr.m_name, symCustomAttr.m_data);
					}
				}
				if (this.m_localSymInfo != null)
				{
					this.m_localSymInfo.EmitLocalSymInfo(symWriter);
				}
				il.m_ScopeTree.EmitScopeTree(symWriter);
				il.m_LineNumberInfo.EmitLineNumberInfo(symWriter);
				symWriter.CloseScope(il.ILOffset);
				symWriter.CloseMethod();
			}
		}

		// Token: 0x06004B27 RID: 19239 RVA: 0x001102F4 File Offset: 0x0010E4F4
		internal void ReleaseBakedStructures()
		{
			if (!this.m_bIsBaked)
			{
				return;
			}
			this.m_ubBody = null;
			this.m_localSymInfo = null;
			this.m_mdMethodFixups = null;
			this.m_localSignature = null;
			this.m_exceptions = null;
		}

		// Token: 0x06004B28 RID: 19240 RVA: 0x00110322 File Offset: 0x0010E522
		internal override Type[] GetParameterTypes()
		{
			if (this.m_parameterTypes == null)
			{
				this.m_parameterTypes = EmptyArray<Type>.Value;
			}
			return this.m_parameterTypes;
		}

		// Token: 0x06004B29 RID: 19241 RVA: 0x00110340 File Offset: 0x0010E540
		internal static Type GetMethodBaseReturnType(MethodBase method)
		{
			MethodInfo methodInfo;
			if ((methodInfo = method as MethodInfo) != null)
			{
				return methodInfo.ReturnType;
			}
			ConstructorInfo constructorInfo;
			if ((constructorInfo = method as ConstructorInfo) != null)
			{
				return constructorInfo.GetReturnType();
			}
			return null;
		}

		// Token: 0x06004B2A RID: 19242 RVA: 0x00110380 File Offset: 0x0010E580
		internal void SetToken(MethodToken token)
		{
			this.m_tkMethod = token;
		}

		// Token: 0x06004B2B RID: 19243 RVA: 0x00110389 File Offset: 0x0010E589
		internal byte[] GetBody()
		{
			return this.m_ubBody;
		}

		// Token: 0x06004B2C RID: 19244 RVA: 0x00110391 File Offset: 0x0010E591
		internal int[] GetTokenFixups()
		{
			return this.m_mdMethodFixups;
		}

		// Token: 0x06004B2D RID: 19245 RVA: 0x0011039C File Offset: 0x0010E59C
		[SecurityCritical]
		internal SignatureHelper GetMethodSignature()
		{
			if (this.m_parameterTypes == null)
			{
				this.m_parameterTypes = EmptyArray<Type>.Value;
			}
			this.m_signature = SignatureHelper.GetMethodSigHelper(this.m_module, this.m_callingConvention, (this.m_inst != null) ? this.m_inst.Length : 0, (this.m_returnType == null) ? typeof(void) : this.m_returnType, this.m_returnTypeRequiredCustomModifiers, this.m_returnTypeOptionalCustomModifiers, this.m_parameterTypes, this.m_parameterTypeRequiredCustomModifiers, this.m_parameterTypeOptionalCustomModifiers);
			return this.m_signature;
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x0011042C File Offset: 0x0010E62C
		internal byte[] GetLocalSignature(out int signatureLength)
		{
			if (this.m_localSignature != null)
			{
				signatureLength = this.m_localSignature.Length;
				return this.m_localSignature;
			}
			if (this.m_ilGenerator != null && this.m_ilGenerator.m_localCount != 0)
			{
				return this.m_ilGenerator.m_localSignature.InternalGetSignature(out signatureLength);
			}
			return SignatureHelper.GetLocalVarSigHelper(this.m_module).InternalGetSignature(out signatureLength);
		}

		// Token: 0x06004B2F RID: 19247 RVA: 0x0011048A File Offset: 0x0010E68A
		internal int GetMaxStack()
		{
			if (this.m_ilGenerator != null)
			{
				return this.m_ilGenerator.GetMaxStackSize() + this.ExceptionHandlerCount;
			}
			return this.m_maxStack;
		}

		// Token: 0x06004B30 RID: 19248 RVA: 0x001104AD File Offset: 0x0010E6AD
		internal ExceptionHandler[] GetExceptionHandlers()
		{
			return this.m_exceptions;
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06004B31 RID: 19249 RVA: 0x001104B5 File Offset: 0x0010E6B5
		internal int ExceptionHandlerCount
		{
			get
			{
				if (this.m_exceptions == null)
				{
					return 0;
				}
				return this.m_exceptions.Length;
			}
		}

		// Token: 0x06004B32 RID: 19250 RVA: 0x001104CC File Offset: 0x0010E6CC
		internal int CalculateNumberOfExceptions(__ExceptionInfo[] excp)
		{
			int num = 0;
			if (excp == null)
			{
				return 0;
			}
			for (int i = 0; i < excp.Length; i++)
			{
				num += excp[i].GetNumberOfCatches();
			}
			return num;
		}

		// Token: 0x06004B33 RID: 19251 RVA: 0x001104FA File Offset: 0x0010E6FA
		internal bool IsTypeCreated()
		{
			return this.m_containingType != null && this.m_containingType.IsCreated();
		}

		// Token: 0x06004B34 RID: 19252 RVA: 0x00110517 File Offset: 0x0010E717
		internal TypeBuilder GetTypeBuilder()
		{
			return this.m_containingType;
		}

		// Token: 0x06004B35 RID: 19253 RVA: 0x0011051F File Offset: 0x0010E71F
		internal ModuleBuilder GetModuleBuilder()
		{
			return this.m_module;
		}

		// Token: 0x06004B36 RID: 19254 RVA: 0x00110528 File Offset: 0x0010E728
		[SecuritySafeCritical]
		public override bool Equals(object obj)
		{
			if (!(obj is MethodBuilder))
			{
				return false;
			}
			if (!this.m_strName.Equals(((MethodBuilder)obj).m_strName))
			{
				return false;
			}
			if (this.m_iAttributes != ((MethodBuilder)obj).m_iAttributes)
			{
				return false;
			}
			SignatureHelper methodSignature = ((MethodBuilder)obj).GetMethodSignature();
			return methodSignature.Equals(this.GetMethodSignature());
		}

		// Token: 0x06004B37 RID: 19255 RVA: 0x0011058B File Offset: 0x0010E78B
		public override int GetHashCode()
		{
			return this.m_strName.GetHashCode();
		}

		// Token: 0x06004B38 RID: 19256 RVA: 0x00110598 File Offset: 0x0010E798
		[SecuritySafeCritical]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(1000);
			stringBuilder.Append("Name: " + this.m_strName + " " + Environment.NewLine);
			StringBuilder stringBuilder2 = stringBuilder;
			string text = "Attributes: ";
			int iAttributes = (int)this.m_iAttributes;
			stringBuilder2.Append(text + iAttributes.ToString() + Environment.NewLine);
			StringBuilder stringBuilder3 = stringBuilder;
			string text2 = "Method Signature: ";
			SignatureHelper methodSignature = this.GetMethodSignature();
			stringBuilder3.Append(text2 + ((methodSignature != null) ? methodSignature.ToString() : null) + Environment.NewLine);
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06004B39 RID: 19257 RVA: 0x0011062F File Offset: 0x0010E82F
		public override string Name
		{
			get
			{
				return this.m_strName;
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06004B3A RID: 19258 RVA: 0x00110638 File Offset: 0x0010E838
		internal int MetadataTokenInternal
		{
			get
			{
				return this.GetToken().Token;
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06004B3B RID: 19259 RVA: 0x00110653 File Offset: 0x0010E853
		public override Module Module
		{
			get
			{
				return this.m_containingType.Module;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06004B3C RID: 19260 RVA: 0x00110660 File Offset: 0x0010E860
		public override Type DeclaringType
		{
			get
			{
				if (this.m_containingType.m_isHiddenGlobalType)
				{
					return null;
				}
				return this.m_containingType;
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06004B3D RID: 19261 RVA: 0x00110677 File Offset: 0x0010E877
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06004B3E RID: 19262 RVA: 0x0011067A File Offset: 0x0010E87A
		public override Type ReflectedType
		{
			get
			{
				return this.DeclaringType;
			}
		}

		// Token: 0x06004B3F RID: 19263 RVA: 0x00110682 File Offset: 0x0010E882
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004B40 RID: 19264 RVA: 0x00110693 File Offset: 0x0010E893
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_dwMethodImplFlags;
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06004B41 RID: 19265 RVA: 0x0011069B File Offset: 0x0010E89B
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_iAttributes;
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06004B42 RID: 19266 RVA: 0x001106A3 File Offset: 0x0010E8A3
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_callingConvention;
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06004B43 RID: 19267 RVA: 0x001106AB File Offset: 0x0010E8AB
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06004B44 RID: 19268 RVA: 0x001106BC File Offset: 0x0010E8BC
		public override bool IsSecurityCritical
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06004B45 RID: 19269 RVA: 0x001106CD File Offset: 0x0010E8CD
		public override bool IsSecuritySafeCritical
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06004B46 RID: 19270 RVA: 0x001106DE File Offset: 0x0010E8DE
		public override bool IsSecurityTransparent
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
			}
		}

		// Token: 0x06004B47 RID: 19271 RVA: 0x001106EF File Offset: 0x0010E8EF
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06004B48 RID: 19272 RVA: 0x001106F2 File Offset: 0x0010E8F2
		public override Type ReturnType
		{
			get
			{
				return this.m_returnType;
			}
		}

		// Token: 0x06004B49 RID: 19273 RVA: 0x001106FC File Offset: 0x0010E8FC
		public override ParameterInfo[] GetParameters()
		{
			if (!this.m_bIsBaked || this.m_containingType == null || this.m_containingType.BakedRuntimeType == null)
			{
				throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_TypeNotCreated"));
			}
			MethodInfo method = this.m_containingType.GetMethod(this.m_strName, this.m_parameterTypes);
			return method.GetParameters();
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06004B4A RID: 19274 RVA: 0x00110760 File Offset: 0x0010E960
		public override ParameterInfo ReturnParameter
		{
			get
			{
				if (!this.m_bIsBaked || this.m_containingType == null || this.m_containingType.BakedRuntimeType == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeNotCreated"));
				}
				MethodInfo method = this.m_containingType.GetMethod(this.m_strName, this.m_parameterTypes);
				return method.ReturnParameter;
			}
		}

		// Token: 0x06004B4B RID: 19275 RVA: 0x001107C4 File Offset: 0x0010E9C4
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004B4C RID: 19276 RVA: 0x001107D5 File Offset: 0x0010E9D5
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004B4D RID: 19277 RVA: 0x001107E6 File Offset: 0x0010E9E6
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06004B4E RID: 19278 RVA: 0x001107F7 File Offset: 0x0010E9F7
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.m_bIsGenMethDef;
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06004B4F RID: 19279 RVA: 0x001107FF File Offset: 0x0010E9FF
		public override bool ContainsGenericParameters
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004B50 RID: 19280 RVA: 0x00110806 File Offset: 0x0010EA06
		public override MethodInfo GetGenericMethodDefinition()
		{
			if (!this.IsGenericMethod)
			{
				throw new InvalidOperationException();
			}
			return this;
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06004B51 RID: 19281 RVA: 0x00110817 File Offset: 0x0010EA17
		public override bool IsGenericMethod
		{
			get
			{
				return this.m_inst != null;
			}
		}

		// Token: 0x06004B52 RID: 19282 RVA: 0x00110824 File Offset: 0x0010EA24
		public override Type[] GetGenericArguments()
		{
			return this.m_inst;
		}

		// Token: 0x06004B53 RID: 19283 RVA: 0x00110839 File Offset: 0x0010EA39
		public override MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			return MethodBuilderInstantiation.MakeGenericMethod(this, typeArguments);
		}

		// Token: 0x06004B54 RID: 19284 RVA: 0x00110844 File Offset: 0x0010EA44
		public GenericTypeParameterBuilder[] DefineGenericParameters(params string[] names)
		{
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			if (names.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EmptyArray"), "names");
			}
			if (this.m_inst != null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GenericParametersAlreadySet"));
			}
			for (int i = 0; i < names.Length; i++)
			{
				if (names[i] == null)
				{
					throw new ArgumentNullException("names");
				}
			}
			if (this.m_tkMethod.Token != 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodBuilderBaked"));
			}
			this.m_bIsGenMethDef = true;
			this.m_inst = new GenericTypeParameterBuilder[names.Length];
			for (int j = 0; j < names.Length; j++)
			{
				this.m_inst[j] = new GenericTypeParameterBuilder(new TypeBuilder(names[j], j, this));
			}
			return this.m_inst;
		}

		// Token: 0x06004B55 RID: 19285 RVA: 0x0011090B File Offset: 0x0010EB0B
		internal void ThrowIfGeneric()
		{
			if (this.IsGenericMethod && !this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06004B56 RID: 19286 RVA: 0x00110924 File Offset: 0x0010EB24
		[SecuritySafeCritical]
		public MethodToken GetToken()
		{
			if (this.m_tkMethod.Token != 0)
			{
				return this.m_tkMethod;
			}
			MethodToken tokenNoLock = new MethodToken(0);
			List<MethodBuilder> listMethods = this.m_containingType.m_listMethods;
			lock (listMethods)
			{
				if (this.m_tkMethod.Token != 0)
				{
					return this.m_tkMethod;
				}
				int i;
				for (i = this.m_containingType.m_lastTokenizedMethod + 1; i < this.m_containingType.m_listMethods.Count; i++)
				{
					MethodBuilder methodBuilder = this.m_containingType.m_listMethods[i];
					tokenNoLock = methodBuilder.GetTokenNoLock();
					if (methodBuilder == this)
					{
						break;
					}
				}
				this.m_containingType.m_lastTokenizedMethod = i;
			}
			return tokenNoLock;
		}

		// Token: 0x06004B57 RID: 19287 RVA: 0x001109F4 File Offset: 0x0010EBF4
		[SecurityCritical]
		private MethodToken GetTokenNoLock()
		{
			int num;
			byte[] array = this.GetMethodSignature().InternalGetSignature(out num);
			int num2 = TypeBuilder.DefineMethod(this.m_module.GetNativeHandle(), this.m_containingType.MetadataTokenInternal, this.m_strName, array, num, this.Attributes);
			this.m_tkMethod = new MethodToken(num2);
			if (this.m_inst != null)
			{
				foreach (GenericTypeParameterBuilder genericTypeParameterBuilder in this.m_inst)
				{
					if (!genericTypeParameterBuilder.m_type.IsCreated())
					{
						genericTypeParameterBuilder.m_type.CreateType();
					}
				}
			}
			TypeBuilder.SetMethodImpl(this.m_module.GetNativeHandle(), num2, this.m_dwMethodImplFlags);
			return this.m_tkMethod;
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x00110AA4 File Offset: 0x0010ECA4
		public void SetParameters(params Type[] parameterTypes)
		{
			this.CheckContext(parameterTypes);
			this.SetSignature(null, null, null, parameterTypes, null, null);
		}

		// Token: 0x06004B59 RID: 19289 RVA: 0x00110AB9 File Offset: 0x0010ECB9
		public void SetReturnType(Type returnType)
		{
			this.CheckContext(new Type[] { returnType });
			this.SetSignature(returnType, null, null, null, null, null);
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x00110AD8 File Offset: 0x0010ECD8
		public void SetSignature(Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			if (this.m_tkMethod.Token != 0)
			{
				return;
			}
			this.CheckContext(new Type[] { returnType });
			this.CheckContext(new Type[][] { returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes });
			this.CheckContext(parameterTypeRequiredCustomModifiers);
			this.CheckContext(parameterTypeOptionalCustomModifiers);
			this.ThrowIfGeneric();
			if (returnType != null)
			{
				this.m_returnType = returnType;
			}
			if (parameterTypes != null)
			{
				this.m_parameterTypes = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.m_parameterTypes, parameterTypes.Length);
			}
			this.m_returnTypeRequiredCustomModifiers = returnTypeRequiredCustomModifiers;
			this.m_returnTypeOptionalCustomModifiers = returnTypeOptionalCustomModifiers;
			this.m_parameterTypeRequiredCustomModifiers = parameterTypeRequiredCustomModifiers;
			this.m_parameterTypeOptionalCustomModifiers = parameterTypeOptionalCustomModifiers;
		}

		// Token: 0x06004B5B RID: 19291 RVA: 0x00110B84 File Offset: 0x0010ED84
		[SecuritySafeCritical]
		public ParameterBuilder DefineParameter(int position, ParameterAttributes attributes, string strParamName)
		{
			if (position < 0)
			{
				throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_ParamSequence"));
			}
			this.ThrowIfGeneric();
			this.m_containingType.ThrowIfCreated();
			if (position > 0 && (this.m_parameterTypes == null || position > this.m_parameterTypes.Length))
			{
				throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_ParamSequence"));
			}
			attributes &= ~ParameterAttributes.ReservedMask;
			return new ParameterBuilder(this, position, attributes, strParamName);
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x00110BEF File Offset: 0x0010EDEF
		[SecuritySafeCritical]
		[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public void SetMarshal(UnmanagedMarshal unmanagedMarshal)
		{
			this.ThrowIfGeneric();
			this.m_containingType.ThrowIfCreated();
			if (this.m_retParam == null)
			{
				this.m_retParam = new ParameterBuilder(this, 0, ParameterAttributes.None, null);
			}
			this.m_retParam.SetMarshal(unmanagedMarshal);
		}

		// Token: 0x06004B5D RID: 19293 RVA: 0x00110C28 File Offset: 0x0010EE28
		public void SetSymCustomAttribute(string name, byte[] data)
		{
			this.ThrowIfGeneric();
			this.m_containingType.ThrowIfCreated();
			ModuleBuilder module = this.m_module;
			if (module.GetSymWriter() == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
			}
			if (this.m_symCustomAttrs == null)
			{
				this.m_symCustomAttrs = new List<MethodBuilder.SymCustomAttr>();
			}
			this.m_symCustomAttrs.Add(new MethodBuilder.SymCustomAttr(name, data));
		}

		// Token: 0x06004B5E RID: 19294 RVA: 0x00110C8C File Offset: 0x0010EE8C
		[SecuritySafeCritical]
		public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
		{
			if (pset == null)
			{
				throw new ArgumentNullException("pset");
			}
			this.ThrowIfGeneric();
			if (!Enum.IsDefined(typeof(SecurityAction), action) || action == SecurityAction.RequestMinimum || action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse)
			{
				throw new ArgumentOutOfRangeException("action");
			}
			this.m_containingType.ThrowIfCreated();
			byte[] array = null;
			int num = 0;
			if (!pset.IsEmpty())
			{
				array = pset.EncodeXml();
				num = array.Length;
			}
			TypeBuilder.AddDeclarativeSecurity(this.m_module.GetNativeHandle(), this.MetadataTokenInternal, action, array, num);
		}

		// Token: 0x06004B5F RID: 19295 RVA: 0x00110D18 File Offset: 0x0010EF18
		public void SetMethodBody(byte[] il, int maxStack, byte[] localSignature, IEnumerable<ExceptionHandler> exceptionHandlers, IEnumerable<int> tokenFixups)
		{
			if (il == null)
			{
				throw new ArgumentNullException("il", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (maxStack < 0)
			{
				throw new ArgumentOutOfRangeException("maxStack", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (this.m_bIsBaked)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodBaked"));
			}
			this.m_containingType.ThrowIfCreated();
			this.ThrowIfGeneric();
			byte[] array = null;
			ExceptionHandler[] array2 = null;
			int[] array3 = null;
			byte[] array4 = (byte[])il.Clone();
			if (localSignature != null)
			{
				array = (byte[])localSignature.Clone();
			}
			if (exceptionHandlers != null)
			{
				array2 = MethodBuilder.ToArray<ExceptionHandler>(exceptionHandlers);
				MethodBuilder.CheckExceptionHandlerRanges(array2, array4.Length);
			}
			if (tokenFixups != null)
			{
				array3 = MethodBuilder.ToArray<int>(tokenFixups);
				int num = array4.Length - 4;
				for (int i = 0; i < array3.Length; i++)
				{
					if (array3[i] < 0 || array3[i] > num)
					{
						throw new ArgumentOutOfRangeException("tokenFixups[" + i.ToString() + "]", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[] { 0, num }));
					}
				}
			}
			this.m_ubBody = array4;
			this.m_localSignature = array;
			this.m_exceptions = array2;
			this.m_mdMethodFixups = array3;
			this.m_maxStack = maxStack;
			this.m_ilGenerator = null;
			this.m_bIsBaked = true;
		}

		// Token: 0x06004B60 RID: 19296 RVA: 0x00110E5C File Offset: 0x0010F05C
		private static T[] ToArray<T>(IEnumerable<T> sequence)
		{
			T[] array = sequence as T[];
			if (array != null)
			{
				return (T[])array.Clone();
			}
			return new List<T>(sequence).ToArray();
		}

		// Token: 0x06004B61 RID: 19297 RVA: 0x00110E8C File Offset: 0x0010F08C
		private static void CheckExceptionHandlerRanges(ExceptionHandler[] exceptionHandlers, int maxOffset)
		{
			for (int i = 0; i < exceptionHandlers.Length; i++)
			{
				ExceptionHandler exceptionHandler = exceptionHandlers[i];
				if (exceptionHandler.m_filterOffset > maxOffset || exceptionHandler.m_tryEndOffset > maxOffset || exceptionHandler.m_handlerEndOffset > maxOffset)
				{
					throw new ArgumentOutOfRangeException("exceptionHandlers[" + i.ToString() + "]", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[] { 0, maxOffset }));
				}
				if (exceptionHandler.Kind == ExceptionHandlingClauseOptions.Clause && exceptionHandler.ExceptionTypeToken == 0)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidTypeToken", new object[] { exceptionHandler.ExceptionTypeToken }), "exceptionHandlers[" + i.ToString() + "]");
				}
			}
		}

		// Token: 0x06004B62 RID: 19298 RVA: 0x00110F5C File Offset: 0x0010F15C
		public void CreateMethodBody(byte[] il, int count)
		{
			this.ThrowIfGeneric();
			if (this.m_bIsBaked)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodBaked"));
			}
			this.m_containingType.ThrowIfCreated();
			if (il != null && (count < 0 || count > il.Length))
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (il == null)
			{
				this.m_ubBody = null;
				return;
			}
			this.m_ubBody = new byte[count];
			Array.Copy(il, this.m_ubBody, count);
			this.m_localSignature = null;
			this.m_exceptions = null;
			this.m_mdMethodFixups = null;
			this.m_maxStack = 16;
			this.m_bIsBaked = true;
		}

		// Token: 0x06004B63 RID: 19299 RVA: 0x00110FFC File Offset: 0x0010F1FC
		[SecuritySafeCritical]
		public void SetImplementationFlags(MethodImplAttributes attributes)
		{
			this.ThrowIfGeneric();
			this.m_containingType.ThrowIfCreated();
			this.m_dwMethodImplFlags = attributes;
			this.m_canBeRuntimeImpl = true;
			TypeBuilder.SetMethodImpl(this.m_module.GetNativeHandle(), this.MetadataTokenInternal, attributes);
		}

		// Token: 0x06004B64 RID: 19300 RVA: 0x00111034 File Offset: 0x0010F234
		public ILGenerator GetILGenerator()
		{
			this.ThrowIfGeneric();
			this.ThrowIfShouldNotHaveBody();
			if (this.m_ilGenerator == null)
			{
				this.m_ilGenerator = new ILGenerator(this);
			}
			return this.m_ilGenerator;
		}

		// Token: 0x06004B65 RID: 19301 RVA: 0x0011105C File Offset: 0x0010F25C
		public ILGenerator GetILGenerator(int size)
		{
			this.ThrowIfGeneric();
			this.ThrowIfShouldNotHaveBody();
			if (this.m_ilGenerator == null)
			{
				this.m_ilGenerator = new ILGenerator(this, size);
			}
			return this.m_ilGenerator;
		}

		// Token: 0x06004B66 RID: 19302 RVA: 0x00111085 File Offset: 0x0010F285
		private void ThrowIfShouldNotHaveBody()
		{
			if ((this.m_dwMethodImplFlags & MethodImplAttributes.CodeTypeMask) != MethodImplAttributes.IL || (this.m_dwMethodImplFlags & MethodImplAttributes.ManagedMask) != MethodImplAttributes.IL || (this.m_iAttributes & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope || this.m_isDllImport)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ShouldNotHaveMethodBody"));
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06004B67 RID: 19303 RVA: 0x001110C1 File Offset: 0x0010F2C1
		// (set) Token: 0x06004B68 RID: 19304 RVA: 0x001110CF File Offset: 0x0010F2CF
		public bool InitLocals
		{
			get
			{
				this.ThrowIfGeneric();
				return this.m_fInitLocals;
			}
			set
			{
				this.ThrowIfGeneric();
				this.m_fInitLocals = value;
			}
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x001110DE File Offset: 0x0010F2DE
		public Module GetModule()
		{
			return this.GetModuleBuilder();
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06004B6A RID: 19306 RVA: 0x001110E6 File Offset: 0x0010F2E6
		public string Signature
		{
			[SecuritySafeCritical]
			get
			{
				return this.GetMethodSignature().ToString();
			}
		}

		// Token: 0x06004B6B RID: 19307 RVA: 0x001110F4 File Offset: 0x0010F2F4
		[SecuritySafeCritical]
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			this.ThrowIfGeneric();
			TypeBuilder.DefineCustomAttribute(this.m_module, this.MetadataTokenInternal, this.m_module.GetConstructorToken(con).Token, binaryAttribute, false, false);
			if (this.IsKnownCA(con))
			{
				this.ParseCA(con, binaryAttribute);
			}
		}

		// Token: 0x06004B6C RID: 19308 RVA: 0x00111164 File Offset: 0x0010F364
		[SecuritySafeCritical]
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			this.ThrowIfGeneric();
			customBuilder.CreateCustomAttribute(this.m_module, this.MetadataTokenInternal);
			if (this.IsKnownCA(customBuilder.m_con))
			{
				this.ParseCA(customBuilder.m_con, customBuilder.m_blob);
			}
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x001111B8 File Offset: 0x0010F3B8
		private bool IsKnownCA(ConstructorInfo con)
		{
			Type declaringType = con.DeclaringType;
			return declaringType == typeof(MethodImplAttribute) || declaringType == typeof(DllImportAttribute);
		}

		// Token: 0x06004B6E RID: 19310 RVA: 0x001111F8 File Offset: 0x0010F3F8
		private void ParseCA(ConstructorInfo con, byte[] blob)
		{
			Type declaringType = con.DeclaringType;
			if (declaringType == typeof(MethodImplAttribute))
			{
				this.m_canBeRuntimeImpl = true;
				return;
			}
			if (declaringType == typeof(DllImportAttribute))
			{
				this.m_canBeRuntimeImpl = true;
				this.m_isDllImport = true;
			}
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x00111246 File Offset: 0x0010F446
		void _MethodBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B70 RID: 19312 RVA: 0x0011124D File Offset: 0x0010F44D
		void _MethodBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x00111254 File Offset: 0x0010F454
		void _MethodBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x0011125B File Offset: 0x0010F45B
		void _MethodBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001F12 RID: 7954
		internal string m_strName;

		// Token: 0x04001F13 RID: 7955
		private MethodToken m_tkMethod;

		// Token: 0x04001F14 RID: 7956
		private ModuleBuilder m_module;

		// Token: 0x04001F15 RID: 7957
		internal TypeBuilder m_containingType;

		// Token: 0x04001F16 RID: 7958
		private int[] m_mdMethodFixups;

		// Token: 0x04001F17 RID: 7959
		private byte[] m_localSignature;

		// Token: 0x04001F18 RID: 7960
		internal LocalSymInfo m_localSymInfo;

		// Token: 0x04001F19 RID: 7961
		internal ILGenerator m_ilGenerator;

		// Token: 0x04001F1A RID: 7962
		private byte[] m_ubBody;

		// Token: 0x04001F1B RID: 7963
		private ExceptionHandler[] m_exceptions;

		// Token: 0x04001F1C RID: 7964
		private const int DefaultMaxStack = 16;

		// Token: 0x04001F1D RID: 7965
		private int m_maxStack = 16;

		// Token: 0x04001F1E RID: 7966
		internal bool m_bIsBaked;

		// Token: 0x04001F1F RID: 7967
		private bool m_bIsGlobalMethod;

		// Token: 0x04001F20 RID: 7968
		private bool m_fInitLocals;

		// Token: 0x04001F21 RID: 7969
		private MethodAttributes m_iAttributes;

		// Token: 0x04001F22 RID: 7970
		private CallingConventions m_callingConvention;

		// Token: 0x04001F23 RID: 7971
		private MethodImplAttributes m_dwMethodImplFlags;

		// Token: 0x04001F24 RID: 7972
		private SignatureHelper m_signature;

		// Token: 0x04001F25 RID: 7973
		internal Type[] m_parameterTypes;

		// Token: 0x04001F26 RID: 7974
		private ParameterBuilder m_retParam;

		// Token: 0x04001F27 RID: 7975
		private Type m_returnType;

		// Token: 0x04001F28 RID: 7976
		private Type[] m_returnTypeRequiredCustomModifiers;

		// Token: 0x04001F29 RID: 7977
		private Type[] m_returnTypeOptionalCustomModifiers;

		// Token: 0x04001F2A RID: 7978
		private Type[][] m_parameterTypeRequiredCustomModifiers;

		// Token: 0x04001F2B RID: 7979
		private Type[][] m_parameterTypeOptionalCustomModifiers;

		// Token: 0x04001F2C RID: 7980
		private GenericTypeParameterBuilder[] m_inst;

		// Token: 0x04001F2D RID: 7981
		private bool m_bIsGenMethDef;

		// Token: 0x04001F2E RID: 7982
		private List<MethodBuilder.SymCustomAttr> m_symCustomAttrs;

		// Token: 0x04001F2F RID: 7983
		internal bool m_canBeRuntimeImpl;

		// Token: 0x04001F30 RID: 7984
		internal bool m_isDllImport;

		// Token: 0x02000C44 RID: 3140
		private struct SymCustomAttr
		{
			// Token: 0x0600706F RID: 28783 RVA: 0x00183098 File Offset: 0x00181298
			public SymCustomAttr(string name, byte[] data)
			{
				this.m_name = name;
				this.m_data = data;
			}

			// Token: 0x04003761 RID: 14177
			public string m_name;

			// Token: 0x04003762 RID: 14178
			public byte[] m_data;
		}
	}
}
