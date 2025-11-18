using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Reflection.Emit
{
	// Token: 0x0200063A RID: 1594
	[ComVisible(true)]
	public sealed class DynamicMethod : MethodInfo
	{
		// Token: 0x06004A38 RID: 19000 RVA: 0x0010C51D File Offset: 0x0010A71D
		private DynamicMethod()
		{
		}

		// Token: 0x06004A39 RID: 19001 RVA: 0x0010C528 File Offset: 0x0010A728
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.Init(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, null, null, false, true, ref stackCrawlMark);
		}

		// Token: 0x06004A3A RID: 19002 RVA: 0x0010C550 File Offset: 0x0010A750
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, bool restrictedSkipVisibility)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.Init(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, null, null, restrictedSkipVisibility, true, ref stackCrawlMark);
		}

		// Token: 0x06004A3B RID: 19003 RVA: 0x0010C578 File Offset: 0x0010A778
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Module m)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PerformSecurityCheck(m, ref stackCrawlMark, false);
			this.Init(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, null, m, false, false, ref stackCrawlMark);
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x0010C5AC File Offset: 0x0010A7AC
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Module m, bool skipVisibility)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PerformSecurityCheck(m, ref stackCrawlMark, skipVisibility);
			this.Init(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, null, m, skipVisibility, false, ref stackCrawlMark);
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x0010C5E4 File Offset: 0x0010A7E4
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DynamicMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Module m, bool skipVisibility)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PerformSecurityCheck(m, ref stackCrawlMark, skipVisibility);
			this.Init(name, attributes, callingConvention, returnType, parameterTypes, null, m, skipVisibility, false, ref stackCrawlMark);
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x0010C61C File Offset: 0x0010A81C
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Type owner)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PerformSecurityCheck(owner, ref stackCrawlMark, false);
			this.Init(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, owner, null, false, false, ref stackCrawlMark);
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x0010C650 File Offset: 0x0010A850
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Type owner, bool skipVisibility)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PerformSecurityCheck(owner, ref stackCrawlMark, skipVisibility);
			this.Init(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, owner, null, skipVisibility, false, ref stackCrawlMark);
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x0010C688 File Offset: 0x0010A888
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DynamicMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type owner, bool skipVisibility)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PerformSecurityCheck(owner, ref stackCrawlMark, skipVisibility);
			this.Init(name, attributes, callingConvention, returnType, parameterTypes, owner, null, skipVisibility, false, ref stackCrawlMark);
		}

		// Token: 0x06004A41 RID: 19009 RVA: 0x0010C6C0 File Offset: 0x0010A8C0
		private static void CheckConsistency(MethodAttributes attributes, CallingConventions callingConvention)
		{
			if ((attributes & ~MethodAttributes.MemberAccessMask) != MethodAttributes.Static)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicMethodFlags"));
			}
			if ((attributes & MethodAttributes.MemberAccessMask) != MethodAttributes.Public)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicMethodFlags"));
			}
			if (callingConvention != CallingConventions.Standard && callingConvention != CallingConventions.VarArgs)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicMethodFlags"));
			}
			if (callingConvention == CallingConventions.VarArgs)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicMethodFlags"));
			}
		}

		// Token: 0x06004A42 RID: 19010 RVA: 0x0010C728 File Offset: 0x0010A928
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static RuntimeModule GetDynamicMethodsModule()
		{
			if (DynamicMethod.s_anonymouslyHostedDynamicMethodsModule != null)
			{
				return DynamicMethod.s_anonymouslyHostedDynamicMethodsModule;
			}
			object obj = DynamicMethod.s_anonymouslyHostedDynamicMethodsModuleLock;
			lock (obj)
			{
				if (DynamicMethod.s_anonymouslyHostedDynamicMethodsModule != null)
				{
					return DynamicMethod.s_anonymouslyHostedDynamicMethodsModule;
				}
				ConstructorInfo constructor = typeof(SecurityTransparentAttribute).GetConstructor(Type.EmptyTypes);
				CustomAttributeBuilder customAttributeBuilder = new CustomAttributeBuilder(constructor, EmptyArray<object>.Value);
				List<CustomAttributeBuilder> list = new List<CustomAttributeBuilder>();
				list.Add(customAttributeBuilder);
				ConstructorInfo constructor2 = typeof(SecurityRulesAttribute).GetConstructor(new Type[] { typeof(SecurityRuleSet) });
				CustomAttributeBuilder customAttributeBuilder2 = new CustomAttributeBuilder(constructor2, new object[] { SecurityRuleSet.Level1 });
				list.Add(customAttributeBuilder2);
				AssemblyName assemblyName = new AssemblyName("Anonymously Hosted DynamicMethods Assembly");
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMe;
				AssemblyBuilder assemblyBuilder = AssemblyBuilder.InternalDefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run, null, null, null, null, null, ref stackCrawlMark, list, SecurityContextSource.CurrentAssembly);
				AppDomain.PublishAnonymouslyHostedDynamicMethodsAssembly(assemblyBuilder.GetNativeHandle());
				DynamicMethod.s_anonymouslyHostedDynamicMethodsModule = (InternalModuleBuilder)assemblyBuilder.ManifestModule;
			}
			return DynamicMethod.s_anonymouslyHostedDynamicMethodsModule;
		}

		// Token: 0x06004A43 RID: 19011 RVA: 0x0010C858 File Offset: 0x0010AA58
		[SecurityCritical]
		private void Init(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] signature, Type owner, Module m, bool skipVisibility, bool transparentMethod, ref StackCrawlMark stackMark)
		{
			DynamicMethod.CheckConsistency(attributes, callingConvention);
			if (signature != null)
			{
				this.m_parameterTypes = new RuntimeType[signature.Length];
				for (int i = 0; i < signature.Length; i++)
				{
					if (signature[i] == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Arg_InvalidTypeInSignature"));
					}
					this.m_parameterTypes[i] = signature[i].UnderlyingSystemType as RuntimeType;
					if (this.m_parameterTypes[i] == null || this.m_parameterTypes[i] == null || this.m_parameterTypes[i] == (RuntimeType)typeof(void))
					{
						throw new ArgumentException(Environment.GetResourceString("Arg_InvalidTypeInSignature"));
					}
				}
			}
			else
			{
				this.m_parameterTypes = new RuntimeType[0];
			}
			this.m_returnType = ((returnType == null) ? ((RuntimeType)typeof(void)) : (returnType.UnderlyingSystemType as RuntimeType));
			if (this.m_returnType == null || this.m_returnType == null || this.m_returnType.IsByRef)
			{
				throw new NotSupportedException(Environment.GetResourceString("Arg_InvalidTypeInRetType"));
			}
			if (transparentMethod)
			{
				this.m_module = DynamicMethod.GetDynamicMethodsModule();
				if (skipVisibility)
				{
					this.m_restrictedSkipVisibility = true;
				}
				this.m_creationContext = CompressedStack.Capture();
			}
			else
			{
				if (m != null)
				{
					this.m_module = m.ModuleHandle.GetRuntimeModule();
				}
				else
				{
					RuntimeType runtimeType = null;
					if (owner != null)
					{
						runtimeType = owner.UnderlyingSystemType as RuntimeType;
					}
					if (runtimeType != null)
					{
						if (runtimeType.HasElementType || runtimeType.ContainsGenericParameters || runtimeType.IsGenericParameter || runtimeType.IsInterface)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidTypeForDynamicMethod"));
						}
						this.m_typeOwner = runtimeType;
						this.m_module = runtimeType.GetRuntimeModule();
					}
				}
				this.m_skipVisibility = skipVisibility;
			}
			this.m_ilGenerator = null;
			this.m_fInitLocals = true;
			this.m_methodHandle = null;
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (AppDomain.ProfileAPICheck)
			{
				if (this.m_creatorAssembly == null)
				{
					this.m_creatorAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
				}
				if (this.m_creatorAssembly != null && !this.m_creatorAssembly.IsFrameworkAssembly())
				{
					this.m_profileAPICheck = true;
				}
			}
			this.m_dynMethod = new DynamicMethod.RTDynamicMethod(this, name, attributes, callingConvention);
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x0010CAAC File Offset: 0x0010ACAC
		[SecurityCritical]
		private void PerformSecurityCheck(Module m, ref StackCrawlMark stackMark, bool skipVisibility)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			ModuleBuilder moduleBuilder = m as ModuleBuilder;
			RuntimeModule runtimeModule;
			if (moduleBuilder != null)
			{
				runtimeModule = moduleBuilder.InternalModule;
			}
			else
			{
				runtimeModule = m as RuntimeModule;
			}
			if (runtimeModule == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeModule"), "m");
			}
			if (runtimeModule == DynamicMethod.s_anonymouslyHostedDynamicMethodsModule)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"), "m");
			}
			if (skipVisibility)
			{
				new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
			}
			RuntimeType callerType = RuntimeMethodHandle.GetCallerType(ref stackMark);
			this.m_creatorAssembly = callerType.GetRuntimeAssembly();
			if (m.Assembly != this.m_creatorAssembly)
			{
				CodeAccessSecurityEngine.ReflectionTargetDemandHelper(PermissionType.SecurityControlEvidence, m.Assembly.PermissionSet);
			}
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x0010CB78 File Offset: 0x0010AD78
		[SecurityCritical]
		private void PerformSecurityCheck(Type owner, ref StackCrawlMark stackMark, bool skipVisibility)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			RuntimeType runtimeType = owner as RuntimeType;
			if (runtimeType == null)
			{
				runtimeType = owner.UnderlyingSystemType as RuntimeType;
			}
			if (runtimeType == null)
			{
				throw new ArgumentNullException("owner", Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			RuntimeType callerType = RuntimeMethodHandle.GetCallerType(ref stackMark);
			if (skipVisibility)
			{
				new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
			}
			else if (callerType != runtimeType)
			{
				new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
			}
			this.m_creatorAssembly = callerType.GetRuntimeAssembly();
			if (runtimeType.Assembly != this.m_creatorAssembly)
			{
				CodeAccessSecurityEngine.ReflectionTargetDemandHelper(PermissionType.SecurityControlEvidence, owner.Assembly.PermissionSet);
			}
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x0010CC30 File Offset: 0x0010AE30
		[SecuritySafeCritical]
		[ComVisible(true)]
		public sealed override Delegate CreateDelegate(Type delegateType)
		{
			if (this.m_restrictedSkipVisibility)
			{
				this.GetMethodDescriptor();
				RuntimeHelpers._CompileMethod(this.m_methodHandle);
			}
			MulticastDelegate multicastDelegate = (MulticastDelegate)Delegate.CreateDelegateNoSecurityCheck(delegateType, null, this.GetMethodDescriptor());
			multicastDelegate.StoreDynamicMethod(this.GetMethodInfo());
			return multicastDelegate;
		}

		// Token: 0x06004A47 RID: 19015 RVA: 0x0010CC78 File Offset: 0x0010AE78
		[SecuritySafeCritical]
		[ComVisible(true)]
		public sealed override Delegate CreateDelegate(Type delegateType, object target)
		{
			if (this.m_restrictedSkipVisibility)
			{
				this.GetMethodDescriptor();
				RuntimeHelpers._CompileMethod(this.m_methodHandle);
			}
			MulticastDelegate multicastDelegate = (MulticastDelegate)Delegate.CreateDelegateNoSecurityCheck(delegateType, target, this.GetMethodDescriptor());
			multicastDelegate.StoreDynamicMethod(this.GetMethodInfo());
			return multicastDelegate;
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06004A48 RID: 19016 RVA: 0x0010CCBF File Offset: 0x0010AEBF
		// (set) Token: 0x06004A49 RID: 19017 RVA: 0x0010CCC7 File Offset: 0x0010AEC7
		internal bool ProfileAPICheck
		{
			get
			{
				return this.m_profileAPICheck;
			}
			[FriendAccessAllowed]
			set
			{
				this.m_profileAPICheck = value;
			}
		}

		// Token: 0x06004A4A RID: 19018 RVA: 0x0010CCD0 File Offset: 0x0010AED0
		[SecurityCritical]
		internal RuntimeMethodHandle GetMethodDescriptor()
		{
			if (this.m_methodHandle == null)
			{
				lock (this)
				{
					if (this.m_methodHandle == null)
					{
						if (this.m_DynamicILInfo != null)
						{
							this.m_DynamicILInfo.GetCallableMethod(this.m_module, this);
						}
						else
						{
							if (this.m_ilGenerator == null || this.m_ilGenerator.ILOffset == 0)
							{
								throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadEmptyMethodBody", new object[] { this.Name }));
							}
							this.m_ilGenerator.GetCallableMethod(this.m_module, this);
						}
					}
				}
			}
			return new RuntimeMethodHandle(this.m_methodHandle);
		}

		// Token: 0x06004A4B RID: 19019 RVA: 0x0010CD88 File Offset: 0x0010AF88
		public override string ToString()
		{
			return this.m_dynMethod.ToString();
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06004A4C RID: 19020 RVA: 0x0010CD95 File Offset: 0x0010AF95
		public override string Name
		{
			get
			{
				return this.m_dynMethod.Name;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06004A4D RID: 19021 RVA: 0x0010CDA2 File Offset: 0x0010AFA2
		public override Type DeclaringType
		{
			get
			{
				return this.m_dynMethod.DeclaringType;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06004A4E RID: 19022 RVA: 0x0010CDAF File Offset: 0x0010AFAF
		public override Type ReflectedType
		{
			get
			{
				return this.m_dynMethod.ReflectedType;
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06004A4F RID: 19023 RVA: 0x0010CDBC File Offset: 0x0010AFBC
		public override Module Module
		{
			get
			{
				return this.m_dynMethod.Module;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06004A50 RID: 19024 RVA: 0x0010CDC9 File Offset: 0x0010AFC9
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06004A51 RID: 19025 RVA: 0x0010CDDA File Offset: 0x0010AFDA
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_dynMethod.Attributes;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06004A52 RID: 19026 RVA: 0x0010CDE7 File Offset: 0x0010AFE7
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_dynMethod.CallingConvention;
			}
		}

		// Token: 0x06004A53 RID: 19027 RVA: 0x0010CDF4 File Offset: 0x0010AFF4
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		// Token: 0x06004A54 RID: 19028 RVA: 0x0010CDF7 File Offset: 0x0010AFF7
		public override ParameterInfo[] GetParameters()
		{
			return this.m_dynMethod.GetParameters();
		}

		// Token: 0x06004A55 RID: 19029 RVA: 0x0010CE04 File Offset: 0x0010B004
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_dynMethod.GetMethodImplementationFlags();
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06004A56 RID: 19030 RVA: 0x0010CE14 File Offset: 0x0010B014
		public override bool IsSecurityCritical
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_methodHandle != null)
				{
					return RuntimeMethodHandle.IsSecurityCritical(this.m_methodHandle);
				}
				if (this.m_typeOwner != null)
				{
					RuntimeAssembly runtimeAssembly = this.m_typeOwner.Assembly as RuntimeAssembly;
					return runtimeAssembly.IsAllSecurityCritical();
				}
				RuntimeAssembly runtimeAssembly2 = this.m_module.Assembly as RuntimeAssembly;
				return runtimeAssembly2.IsAllSecurityCritical();
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06004A57 RID: 19031 RVA: 0x0010CE74 File Offset: 0x0010B074
		public override bool IsSecuritySafeCritical
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_methodHandle != null)
				{
					return RuntimeMethodHandle.IsSecuritySafeCritical(this.m_methodHandle);
				}
				if (this.m_typeOwner != null)
				{
					RuntimeAssembly runtimeAssembly = this.m_typeOwner.Assembly as RuntimeAssembly;
					return runtimeAssembly.IsAllPublicAreaSecuritySafeCritical();
				}
				RuntimeAssembly runtimeAssembly2 = this.m_module.Assembly as RuntimeAssembly;
				return runtimeAssembly2.IsAllSecuritySafeCritical();
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06004A58 RID: 19032 RVA: 0x0010CED4 File Offset: 0x0010B0D4
		public override bool IsSecurityTransparent
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_methodHandle != null)
				{
					return RuntimeMethodHandle.IsSecurityTransparent(this.m_methodHandle);
				}
				if (this.m_typeOwner != null)
				{
					RuntimeAssembly runtimeAssembly = this.m_typeOwner.Assembly as RuntimeAssembly;
					return !runtimeAssembly.IsAllSecurityCritical();
				}
				RuntimeAssembly runtimeAssembly2 = this.m_module.Assembly as RuntimeAssembly;
				return !runtimeAssembly2.IsAllSecurityCritical();
			}
		}

		// Token: 0x06004A59 RID: 19033 RVA: 0x0010CF38 File Offset: 0x0010B138
		[SecuritySafeCritical]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			if ((this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_CallToVarArg"));
			}
			RuntimeMethodHandle methodDescriptor = this.GetMethodDescriptor();
			Signature signature = new Signature(this.m_methodHandle, this.m_parameterTypes, this.m_returnType, this.CallingConvention);
			int num = signature.Arguments.Length;
			int num2 = ((parameters != null) ? parameters.Length : 0);
			if (num != num2)
			{
				throw new TargetParameterCountException(Environment.GetResourceString("Arg_ParmCnt"));
			}
			object obj2;
			if (num2 > 0)
			{
				object[] array = base.CheckArguments(parameters, binder, invokeAttr, culture, signature);
				obj2 = RuntimeMethodHandle.InvokeMethod(null, array, signature, false);
				for (int i = 0; i < array.Length; i++)
				{
					parameters[i] = array[i];
				}
			}
			else
			{
				obj2 = RuntimeMethodHandle.InvokeMethod(null, null, signature, false);
			}
			GC.KeepAlive(this);
			return obj2;
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x0010D002 File Offset: 0x0010B202
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_dynMethod.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004A5B RID: 19035 RVA: 0x0010D011 File Offset: 0x0010B211
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_dynMethod.GetCustomAttributes(inherit);
		}

		// Token: 0x06004A5C RID: 19036 RVA: 0x0010D01F File Offset: 0x0010B21F
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_dynMethod.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06004A5D RID: 19037 RVA: 0x0010D02E File Offset: 0x0010B22E
		public override Type ReturnType
		{
			get
			{
				return this.m_dynMethod.ReturnType;
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06004A5E RID: 19038 RVA: 0x0010D03B File Offset: 0x0010B23B
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return this.m_dynMethod.ReturnParameter;
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06004A5F RID: 19039 RVA: 0x0010D048 File Offset: 0x0010B248
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return this.m_dynMethod.ReturnTypeCustomAttributes;
			}
		}

		// Token: 0x06004A60 RID: 19040 RVA: 0x0010D058 File Offset: 0x0010B258
		public ParameterBuilder DefineParameter(int position, ParameterAttributes attributes, string parameterName)
		{
			if (position < 0 || position > this.m_parameterTypes.Length)
			{
				throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_ParamSequence"));
			}
			position--;
			if (position >= 0)
			{
				ParameterInfo[] array = this.m_dynMethod.LoadParameters();
				array[position].SetName(parameterName);
				array[position].SetAttributes(attributes);
			}
			return null;
		}

		// Token: 0x06004A61 RID: 19041 RVA: 0x0010D0AC File Offset: 0x0010B2AC
		[SecuritySafeCritical]
		public DynamicILInfo GetDynamicILInfo()
		{
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			if (this.m_DynamicILInfo != null)
			{
				return this.m_DynamicILInfo;
			}
			return this.GetDynamicILInfo(new DynamicScope());
		}

		// Token: 0x06004A62 RID: 19042 RVA: 0x0010D0D4 File Offset: 0x0010B2D4
		[SecurityCritical]
		internal DynamicILInfo GetDynamicILInfo(DynamicScope scope)
		{
			if (this.m_DynamicILInfo == null)
			{
				Module module = null;
				CallingConventions callingConvention = this.CallingConvention;
				Type returnType = this.ReturnType;
				Type[] array = null;
				Type[] array2 = null;
				Type[] parameterTypes = this.m_parameterTypes;
				byte[] signature = SignatureHelper.GetMethodSigHelper(module, callingConvention, returnType, array, array2, parameterTypes, null, null).GetSignature(true);
				this.m_DynamicILInfo = new DynamicILInfo(scope, this, signature);
			}
			return this.m_DynamicILInfo;
		}

		// Token: 0x06004A63 RID: 19043 RVA: 0x0010D122 File Offset: 0x0010B322
		public ILGenerator GetILGenerator()
		{
			return this.GetILGenerator(64);
		}

		// Token: 0x06004A64 RID: 19044 RVA: 0x0010D12C File Offset: 0x0010B32C
		[SecuritySafeCritical]
		public ILGenerator GetILGenerator(int streamSize)
		{
			if (this.m_ilGenerator == null)
			{
				Module module = null;
				CallingConventions callingConvention = this.CallingConvention;
				Type returnType = this.ReturnType;
				Type[] array = null;
				Type[] array2 = null;
				Type[] parameterTypes = this.m_parameterTypes;
				byte[] signature = SignatureHelper.GetMethodSigHelper(module, callingConvention, returnType, array, array2, parameterTypes, null, null).GetSignature(true);
				this.m_ilGenerator = new DynamicILGenerator(this, signature, streamSize);
			}
			return this.m_ilGenerator;
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06004A65 RID: 19045 RVA: 0x0010D17A File Offset: 0x0010B37A
		// (set) Token: 0x06004A66 RID: 19046 RVA: 0x0010D182 File Offset: 0x0010B382
		public bool InitLocals
		{
			get
			{
				return this.m_fInitLocals;
			}
			set
			{
				this.m_fInitLocals = value;
			}
		}

		// Token: 0x06004A67 RID: 19047 RVA: 0x0010D18B File Offset: 0x0010B38B
		internal MethodInfo GetMethodInfo()
		{
			return this.m_dynMethod;
		}

		// Token: 0x06004A68 RID: 19048 RVA: 0x0010D193 File Offset: 0x0010B393
		// Note: this type is marked as 'beforefieldinit'.
		static DynamicMethod()
		{
		}

		// Token: 0x04001EA5 RID: 7845
		private RuntimeType[] m_parameterTypes;

		// Token: 0x04001EA6 RID: 7846
		internal IRuntimeMethodInfo m_methodHandle;

		// Token: 0x04001EA7 RID: 7847
		private RuntimeType m_returnType;

		// Token: 0x04001EA8 RID: 7848
		private DynamicILGenerator m_ilGenerator;

		// Token: 0x04001EA9 RID: 7849
		private DynamicILInfo m_DynamicILInfo;

		// Token: 0x04001EAA RID: 7850
		private bool m_fInitLocals;

		// Token: 0x04001EAB RID: 7851
		private RuntimeModule m_module;

		// Token: 0x04001EAC RID: 7852
		internal bool m_skipVisibility;

		// Token: 0x04001EAD RID: 7853
		internal RuntimeType m_typeOwner;

		// Token: 0x04001EAE RID: 7854
		private DynamicMethod.RTDynamicMethod m_dynMethod;

		// Token: 0x04001EAF RID: 7855
		internal DynamicResolver m_resolver;

		// Token: 0x04001EB0 RID: 7856
		private bool m_profileAPICheck;

		// Token: 0x04001EB1 RID: 7857
		private RuntimeAssembly m_creatorAssembly;

		// Token: 0x04001EB2 RID: 7858
		internal bool m_restrictedSkipVisibility;

		// Token: 0x04001EB3 RID: 7859
		internal CompressedStack m_creationContext;

		// Token: 0x04001EB4 RID: 7860
		private static volatile InternalModuleBuilder s_anonymouslyHostedDynamicMethodsModule;

		// Token: 0x04001EB5 RID: 7861
		private static readonly object s_anonymouslyHostedDynamicMethodsModuleLock = new object();

		// Token: 0x02000C43 RID: 3139
		internal class RTDynamicMethod : MethodInfo
		{
			// Token: 0x06007056 RID: 28758 RVA: 0x00182E94 File Offset: 0x00181094
			private RTDynamicMethod()
			{
			}

			// Token: 0x06007057 RID: 28759 RVA: 0x00182E9C File Offset: 0x0018109C
			internal RTDynamicMethod(DynamicMethod owner, string name, MethodAttributes attributes, CallingConventions callingConvention)
			{
				this.m_owner = owner;
				this.m_name = name;
				this.m_attributes = attributes;
				this.m_callingConvention = callingConvention;
			}

			// Token: 0x06007058 RID: 28760 RVA: 0x00182EC1 File Offset: 0x001810C1
			public override string ToString()
			{
				return this.ReturnType.FormatTypeName() + " " + base.FormatNameAndSig();
			}

			// Token: 0x17001342 RID: 4930
			// (get) Token: 0x06007059 RID: 28761 RVA: 0x00182EDE File Offset: 0x001810DE
			public override string Name
			{
				get
				{
					return this.m_name;
				}
			}

			// Token: 0x17001343 RID: 4931
			// (get) Token: 0x0600705A RID: 28762 RVA: 0x00182EE6 File Offset: 0x001810E6
			public override Type DeclaringType
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001344 RID: 4932
			// (get) Token: 0x0600705B RID: 28763 RVA: 0x00182EE9 File Offset: 0x001810E9
			public override Type ReflectedType
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001345 RID: 4933
			// (get) Token: 0x0600705C RID: 28764 RVA: 0x00182EEC File Offset: 0x001810EC
			public override Module Module
			{
				get
				{
					return this.m_owner.m_module;
				}
			}

			// Token: 0x17001346 RID: 4934
			// (get) Token: 0x0600705D RID: 28765 RVA: 0x00182EF9 File Offset: 0x001810F9
			public override RuntimeMethodHandle MethodHandle
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
				}
			}

			// Token: 0x17001347 RID: 4935
			// (get) Token: 0x0600705E RID: 28766 RVA: 0x00182F0A File Offset: 0x0018110A
			public override MethodAttributes Attributes
			{
				get
				{
					return this.m_attributes;
				}
			}

			// Token: 0x17001348 RID: 4936
			// (get) Token: 0x0600705F RID: 28767 RVA: 0x00182F12 File Offset: 0x00181112
			public override CallingConventions CallingConvention
			{
				get
				{
					return this.m_callingConvention;
				}
			}

			// Token: 0x06007060 RID: 28768 RVA: 0x00182F1A File Offset: 0x0018111A
			public override MethodInfo GetBaseDefinition()
			{
				return this;
			}

			// Token: 0x06007061 RID: 28769 RVA: 0x00182F20 File Offset: 0x00181120
			public override ParameterInfo[] GetParameters()
			{
				ParameterInfo[] array = this.LoadParameters();
				ParameterInfo[] array2 = new ParameterInfo[array.Length];
				Array.Copy(array, array2, array.Length);
				return array2;
			}

			// Token: 0x06007062 RID: 28770 RVA: 0x00182F48 File Offset: 0x00181148
			public override MethodImplAttributes GetMethodImplementationFlags()
			{
				return MethodImplAttributes.NoInlining;
			}

			// Token: 0x06007063 RID: 28771 RVA: 0x00182F4B File Offset: 0x0018114B
			public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), "this");
			}

			// Token: 0x06007064 RID: 28772 RVA: 0x00182F64 File Offset: 0x00181164
			public override object[] GetCustomAttributes(Type attributeType, bool inherit)
			{
				if (attributeType == null)
				{
					throw new ArgumentNullException("attributeType");
				}
				if (attributeType.IsAssignableFrom(typeof(MethodImplAttribute)))
				{
					return new object[]
					{
						new MethodImplAttribute(this.GetMethodImplementationFlags())
					};
				}
				return EmptyArray<object>.Value;
			}

			// Token: 0x06007065 RID: 28773 RVA: 0x00182FB1 File Offset: 0x001811B1
			public override object[] GetCustomAttributes(bool inherit)
			{
				return new object[]
				{
					new MethodImplAttribute(this.GetMethodImplementationFlags())
				};
			}

			// Token: 0x06007066 RID: 28774 RVA: 0x00182FC7 File Offset: 0x001811C7
			public override bool IsDefined(Type attributeType, bool inherit)
			{
				if (attributeType == null)
				{
					throw new ArgumentNullException("attributeType");
				}
				return attributeType.IsAssignableFrom(typeof(MethodImplAttribute));
			}

			// Token: 0x17001349 RID: 4937
			// (get) Token: 0x06007067 RID: 28775 RVA: 0x00182FF2 File Offset: 0x001811F2
			public override bool IsSecurityCritical
			{
				get
				{
					return this.m_owner.IsSecurityCritical;
				}
			}

			// Token: 0x1700134A RID: 4938
			// (get) Token: 0x06007068 RID: 28776 RVA: 0x00182FFF File Offset: 0x001811FF
			public override bool IsSecuritySafeCritical
			{
				get
				{
					return this.m_owner.IsSecuritySafeCritical;
				}
			}

			// Token: 0x1700134B RID: 4939
			// (get) Token: 0x06007069 RID: 28777 RVA: 0x0018300C File Offset: 0x0018120C
			public override bool IsSecurityTransparent
			{
				get
				{
					return this.m_owner.IsSecurityTransparent;
				}
			}

			// Token: 0x1700134C RID: 4940
			// (get) Token: 0x0600706A RID: 28778 RVA: 0x00183019 File Offset: 0x00181219
			public override Type ReturnType
			{
				get
				{
					return this.m_owner.m_returnType;
				}
			}

			// Token: 0x1700134D RID: 4941
			// (get) Token: 0x0600706B RID: 28779 RVA: 0x00183026 File Offset: 0x00181226
			public override ParameterInfo ReturnParameter
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700134E RID: 4942
			// (get) Token: 0x0600706C RID: 28780 RVA: 0x00183029 File Offset: 0x00181229
			public override ICustomAttributeProvider ReturnTypeCustomAttributes
			{
				get
				{
					return this.GetEmptyCAHolder();
				}
			}

			// Token: 0x0600706D RID: 28781 RVA: 0x00183034 File Offset: 0x00181234
			internal ParameterInfo[] LoadParameters()
			{
				if (this.m_parameters == null)
				{
					Type[] parameterTypes = this.m_owner.m_parameterTypes;
					Type[] array = parameterTypes;
					ParameterInfo[] array2 = new ParameterInfo[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array2[i] = new RuntimeParameterInfo(this, null, array[i], i);
					}
					if (this.m_parameters == null)
					{
						this.m_parameters = array2;
					}
				}
				return this.m_parameters;
			}

			// Token: 0x0600706E RID: 28782 RVA: 0x00183091 File Offset: 0x00181291
			private ICustomAttributeProvider GetEmptyCAHolder()
			{
				return new DynamicMethod.RTDynamicMethod.EmptyCAHolder();
			}

			// Token: 0x0400375C RID: 14172
			internal DynamicMethod m_owner;

			// Token: 0x0400375D RID: 14173
			private ParameterInfo[] m_parameters;

			// Token: 0x0400375E RID: 14174
			private string m_name;

			// Token: 0x0400375F RID: 14175
			private MethodAttributes m_attributes;

			// Token: 0x04003760 RID: 14176
			private CallingConventions m_callingConvention;

			// Token: 0x02000D12 RID: 3346
			private class EmptyCAHolder : ICustomAttributeProvider
			{
				// Token: 0x0600722E RID: 29230 RVA: 0x00189523 File Offset: 0x00187723
				internal EmptyCAHolder()
				{
				}

				// Token: 0x0600722F RID: 29231 RVA: 0x0018952B File Offset: 0x0018772B
				object[] ICustomAttributeProvider.GetCustomAttributes(Type attributeType, bool inherit)
				{
					return EmptyArray<object>.Value;
				}

				// Token: 0x06007230 RID: 29232 RVA: 0x00189532 File Offset: 0x00187732
				object[] ICustomAttributeProvider.GetCustomAttributes(bool inherit)
				{
					return EmptyArray<object>.Value;
				}

				// Token: 0x06007231 RID: 29233 RVA: 0x00189539 File Offset: 0x00187739
				bool ICustomAttributeProvider.IsDefined(Type attributeType, bool inherit)
				{
					return false;
				}
			}
		}
	}
}
