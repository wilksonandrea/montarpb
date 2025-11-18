using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x02000610 RID: 1552
	[Serializable]
	internal class RuntimeModule : Module
	{
		// Token: 0x060047C7 RID: 18375 RVA: 0x00104E43 File Offset: 0x00103043
		internal RuntimeModule()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060047C8 RID: 18376
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetType(RuntimeModule module, string className, bool ignoreCase, bool throwOnError, ObjectHandleOnStack type);

		// Token: 0x060047C9 RID: 18377
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall")]
		private static extern bool nIsTransientInternal(RuntimeModule module);

		// Token: 0x060047CA RID: 18378
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetScopeName(RuntimeModule module, StringHandleOnStack retString);

		// Token: 0x060047CB RID: 18379
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetFullyQualifiedName(RuntimeModule module, StringHandleOnStack retString);

		// Token: 0x060047CC RID: 18380
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RuntimeType[] GetTypes(RuntimeModule module);

		// Token: 0x060047CD RID: 18381 RVA: 0x00104E50 File Offset: 0x00103050
		[SecuritySafeCritical]
		internal RuntimeType[] GetDefinedTypes()
		{
			return RuntimeModule.GetTypes(this.GetNativeHandle());
		}

		// Token: 0x060047CE RID: 18382
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsResource(RuntimeModule module);

		// Token: 0x060047CF RID: 18383
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetSignerCertificate(RuntimeModule module, ObjectHandleOnStack retData);

		// Token: 0x060047D0 RID: 18384 RVA: 0x00104E60 File Offset: 0x00103060
		private static RuntimeTypeHandle[] ConvertToTypeHandleArray(Type[] genericArguments)
		{
			if (genericArguments == null)
			{
				return null;
			}
			int num = genericArguments.Length;
			RuntimeTypeHandle[] array = new RuntimeTypeHandle[num];
			for (int i = 0; i < num; i++)
			{
				Type type = genericArguments[i];
				if (type == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidGenericInstArray"));
				}
				type = type.UnderlyingSystemType;
				if (type == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidGenericInstArray"));
				}
				if (!(type is RuntimeType))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidGenericInstArray"));
				}
				array[i] = type.GetTypeHandleInternal();
			}
			return array;
		}

		// Token: 0x060047D1 RID: 18385 RVA: 0x00104EEC File Offset: 0x001030EC
		[SecuritySafeCritical]
		public override byte[] ResolveSignature(int metadataToken)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", new object[] { metadataToken2, this }));
			}
			if (!metadataToken2.IsMemberRef && !metadataToken2.IsMethodDef && !metadataToken2.IsTypeSpec && !metadataToken2.IsSignature && !metadataToken2.IsFieldDef)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidToken", new object[] { metadataToken2, this }), "metadataToken");
			}
			ConstArray constArray;
			if (metadataToken2.IsMemberRef)
			{
				constArray = this.MetadataImport.GetMemberRefProps(metadataToken);
			}
			else
			{
				constArray = this.MetadataImport.GetSignatureFromToken(metadataToken);
			}
			byte[] array = new byte[constArray.Length];
			for (int i = 0; i < constArray.Length; i++)
			{
				array[i] = constArray[i];
			}
			return array;
		}

		// Token: 0x060047D2 RID: 18386 RVA: 0x00104FF0 File Offset: 0x001031F0
		[SecuritySafeCritical]
		public unsafe override MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", new object[] { metadataToken2, this }));
			}
			RuntimeTypeHandle[] array = RuntimeModule.ConvertToTypeHandleArray(genericTypeArguments);
			RuntimeTypeHandle[] array2 = RuntimeModule.ConvertToTypeHandleArray(genericMethodArguments);
			MethodBase methodBase;
			try
			{
				if (!metadataToken2.IsMethodDef && !metadataToken2.IsMethodSpec)
				{
					if (!metadataToken2.IsMemberRef)
					{
						throw new ArgumentException("metadataToken", Environment.GetResourceString("Argument_ResolveMethod", new object[] { metadataToken2, this }));
					}
					if (*(byte*)this.MetadataImport.GetMemberRefProps(metadataToken2).Signature.ToPointer() == 6)
					{
						throw new ArgumentException("metadataToken", Environment.GetResourceString("Argument_ResolveMethod", new object[] { metadataToken2, this }));
					}
				}
				IRuntimeMethodInfo runtimeMethodInfo = ModuleHandle.ResolveMethodHandleInternal(this.GetNativeHandle(), metadataToken2, array, array2);
				Type type = RuntimeMethodHandle.GetDeclaringType(runtimeMethodInfo);
				if (type.IsGenericType || type.IsArray)
				{
					MetadataToken metadataToken3 = new MetadataToken(this.MetadataImport.GetParentToken(metadataToken2));
					if (metadataToken2.IsMethodSpec)
					{
						metadataToken3 = new MetadataToken(this.MetadataImport.GetParentToken(metadataToken3));
					}
					type = this.ResolveType(metadataToken3, genericTypeArguments, genericMethodArguments);
				}
				methodBase = RuntimeType.GetMethodBase(type as RuntimeType, runtimeMethodInfo);
			}
			catch (BadImageFormatException ex)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadImageFormatExceptionResolve"), ex);
			}
			return methodBase;
		}

		// Token: 0x060047D3 RID: 18387 RVA: 0x001051B4 File Offset: 0x001033B4
		[SecurityCritical]
		private FieldInfo ResolveLiteralField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (!this.MetadataImport.IsValidToken(metadataToken2) || !metadataToken2.IsFieldDef)
			{
				throw new ArgumentOutOfRangeException("metadataToken", string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Argument_InvalidToken", new object[] { metadataToken2, this }), Array.Empty<object>()));
			}
			string text = this.MetadataImport.GetName(metadataToken2).ToString();
			int parentToken = this.MetadataImport.GetParentToken(metadataToken2);
			Type type = this.ResolveType(parentToken, genericTypeArguments, genericMethodArguments);
			type.GetFields();
			FieldInfo field;
			try
			{
				field = type.GetField(text, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
			catch
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ResolveField", new object[] { metadataToken2, this }), "metadataToken");
			}
			return field;
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x001052B4 File Offset: 0x001034B4
		[SecuritySafeCritical]
		public unsafe override FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", new object[] { metadataToken2, this }));
			}
			RuntimeTypeHandle[] array = RuntimeModule.ConvertToTypeHandleArray(genericTypeArguments);
			RuntimeTypeHandle[] array2 = RuntimeModule.ConvertToTypeHandleArray(genericMethodArguments);
			FieldInfo fieldInfo;
			try
			{
				IRuntimeFieldInfo runtimeFieldInfo;
				if (!metadataToken2.IsFieldDef)
				{
					if (!metadataToken2.IsMemberRef)
					{
						throw new ArgumentException("metadataToken", Environment.GetResourceString("Argument_ResolveField", new object[] { metadataToken2, this }));
					}
					if (*(byte*)this.MetadataImport.GetMemberRefProps(metadataToken2).Signature.ToPointer() != 6)
					{
						throw new ArgumentException("metadataToken", Environment.GetResourceString("Argument_ResolveField", new object[] { metadataToken2, this }));
					}
					runtimeFieldInfo = ModuleHandle.ResolveFieldHandleInternal(this.GetNativeHandle(), metadataToken2, array, array2);
				}
				runtimeFieldInfo = ModuleHandle.ResolveFieldHandleInternal(this.GetNativeHandle(), metadataToken, array, array2);
				RuntimeType runtimeType = RuntimeFieldHandle.GetApproxDeclaringType(runtimeFieldInfo.Value);
				if (runtimeType.IsGenericType || runtimeType.IsArray)
				{
					int parentToken = ModuleHandle.GetMetadataImport(this.GetNativeHandle()).GetParentToken(metadataToken);
					runtimeType = (RuntimeType)this.ResolveType(parentToken, genericTypeArguments, genericMethodArguments);
				}
				fieldInfo = RuntimeType.GetFieldInfo(runtimeType, runtimeFieldInfo);
			}
			catch (MissingFieldException)
			{
				fieldInfo = this.ResolveLiteralField(metadataToken2, genericTypeArguments, genericMethodArguments);
			}
			catch (BadImageFormatException ex)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadImageFormatExceptionResolve"), ex);
			}
			return fieldInfo;
		}

		// Token: 0x060047D5 RID: 18389 RVA: 0x00105480 File Offset: 0x00103680
		[SecuritySafeCritical]
		public override Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (metadataToken2.IsGlobalTypeDefToken)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ResolveModuleType", new object[] { metadataToken2 }), "metadataToken");
			}
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", new object[] { metadataToken2, this }));
			}
			if (!metadataToken2.IsTypeDef && !metadataToken2.IsTypeSpec && !metadataToken2.IsTypeRef)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ResolveType", new object[] { metadataToken2, this }), "metadataToken");
			}
			RuntimeTypeHandle[] array = RuntimeModule.ConvertToTypeHandleArray(genericTypeArguments);
			RuntimeTypeHandle[] array2 = RuntimeModule.ConvertToTypeHandleArray(genericMethodArguments);
			Type type;
			try
			{
				Type runtimeType = this.GetModuleHandle().ResolveTypeHandle(metadataToken, array, array2).GetRuntimeType();
				if (runtimeType == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_ResolveType", new object[] { metadataToken2, this }), "metadataToken");
				}
				type = runtimeType;
			}
			catch (BadImageFormatException ex)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadImageFormatExceptionResolve"), ex);
			}
			return type;
		}

		// Token: 0x060047D6 RID: 18390 RVA: 0x001055CC File Offset: 0x001037CC
		[SecuritySafeCritical]
		public unsafe override MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (metadataToken2.IsProperty)
			{
				throw new ArgumentException(Environment.GetResourceString("InvalidOperation_PropertyInfoNotAvailable"));
			}
			if (metadataToken2.IsEvent)
			{
				throw new ArgumentException(Environment.GetResourceString("InvalidOperation_EventInfoNotAvailable"));
			}
			if (metadataToken2.IsMethodSpec || metadataToken2.IsMethodDef)
			{
				return this.ResolveMethod(metadataToken, genericTypeArguments, genericMethodArguments);
			}
			if (metadataToken2.IsFieldDef)
			{
				return this.ResolveField(metadataToken, genericTypeArguments, genericMethodArguments);
			}
			if (metadataToken2.IsTypeRef || metadataToken2.IsTypeDef || metadataToken2.IsTypeSpec)
			{
				return this.ResolveType(metadataToken, genericTypeArguments, genericMethodArguments);
			}
			if (!metadataToken2.IsMemberRef)
			{
				throw new ArgumentException("metadataToken", Environment.GetResourceString("Argument_ResolveMember", new object[] { metadataToken2, this }));
			}
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", new object[] { metadataToken2, this }));
			}
			if (*(byte*)this.MetadataImport.GetMemberRefProps(metadataToken2).Signature.ToPointer() == 6)
			{
				return this.ResolveField(metadataToken2, genericTypeArguments, genericMethodArguments);
			}
			return this.ResolveMethod(metadataToken2, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x060047D7 RID: 18391 RVA: 0x00105720 File Offset: 0x00103920
		[SecuritySafeCritical]
		public override string ResolveString(int metadataToken)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (!metadataToken2.IsString)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Argument_ResolveString"), metadataToken, this.ToString()));
			}
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Argument_InvalidToken", new object[] { metadataToken2, this }), Array.Empty<object>()));
			}
			string userString = this.MetadataImport.GetUserString(metadataToken);
			if (userString == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Argument_ResolveString"), metadataToken, this.ToString()));
			}
			return userString;
		}

		// Token: 0x060047D8 RID: 18392 RVA: 0x001057EB File Offset: 0x001039EB
		public override void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			ModuleHandle.GetPEKind(this.GetNativeHandle(), out peKind, out machine);
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x060047D9 RID: 18393 RVA: 0x001057FA File Offset: 0x001039FA
		public override int MDStreamVersion
		{
			[SecuritySafeCritical]
			get
			{
				return ModuleHandle.GetMDStreamVersion(this.GetNativeHandle());
			}
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x00105807 File Offset: 0x00103A07
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethodInternal(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x060047DB RID: 18395 RVA: 0x00105818 File Offset: 0x00103A18
		internal MethodInfo GetMethodInternal(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (this.RuntimeType == null)
			{
				return null;
			}
			if (types == null)
			{
				return this.RuntimeType.GetMethod(name, bindingAttr);
			}
			return this.RuntimeType.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x060047DC RID: 18396 RVA: 0x00105850 File Offset: 0x00103A50
		internal RuntimeType RuntimeType
		{
			get
			{
				if (this.m_runtimeType == null)
				{
					this.m_runtimeType = ModuleHandle.GetModuleType(this.GetNativeHandle());
				}
				return this.m_runtimeType;
			}
		}

		// Token: 0x060047DD RID: 18397 RVA: 0x00105877 File Offset: 0x00103A77
		[SecuritySafeCritical]
		internal bool IsTransientInternal()
		{
			return RuntimeModule.nIsTransientInternal(this.GetNativeHandle());
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x060047DE RID: 18398 RVA: 0x00105884 File Offset: 0x00103A84
		internal MetadataImport MetadataImport
		{
			[SecurityCritical]
			get
			{
				return ModuleHandle.GetMetadataImport(this.GetNativeHandle());
			}
		}

		// Token: 0x060047DF RID: 18399 RVA: 0x00105891 File Offset: 0x00103A91
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x060047E0 RID: 18400 RVA: 0x001058A8 File Offset: 0x00103AA8
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.GetCustomAttributes(this, runtimeType);
		}

		// Token: 0x060047E1 RID: 18401 RVA: 0x001058FC File Offset: 0x00103AFC
		[SecuritySafeCritical]
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.IsDefined(this, runtimeType);
		}

		// Token: 0x060047E2 RID: 18402 RVA: 0x0010594E File Offset: 0x00103B4E
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x060047E3 RID: 18403 RVA: 0x00105956 File Offset: 0x00103B56
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, 5, this.ScopeName, this.GetRuntimeAssembly());
		}

		// Token: 0x060047E4 RID: 18404 RVA: 0x0010597C File Offset: 0x00103B7C
		[SecuritySafeCritical]
		[ComVisible(true)]
		public override Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			if (className == null)
			{
				throw new ArgumentNullException("className");
			}
			RuntimeType runtimeType = null;
			RuntimeModule.GetType(this.GetNativeHandle(), className, throwOnError, ignoreCase, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			return runtimeType;
		}

		// Token: 0x060047E5 RID: 18405 RVA: 0x001059B0 File Offset: 0x00103BB0
		[SecurityCritical]
		internal string GetFullyQualifiedName()
		{
			string text = null;
			RuntimeModule.GetFullyQualifiedName(this.GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref text));
			return text;
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x060047E6 RID: 18406 RVA: 0x001059D4 File Offset: 0x00103BD4
		public override string FullyQualifiedName
		{
			[SecuritySafeCritical]
			get
			{
				string fullyQualifiedName = this.GetFullyQualifiedName();
				if (fullyQualifiedName != null)
				{
					bool flag = true;
					try
					{
						Path.GetFullPathInternal(fullyQualifiedName);
					}
					catch (ArgumentException)
					{
						flag = false;
					}
					if (flag)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, fullyQualifiedName).Demand();
					}
				}
				return fullyQualifiedName;
			}
		}

		// Token: 0x060047E7 RID: 18407 RVA: 0x00105A1C File Offset: 0x00103C1C
		[SecuritySafeCritical]
		public override Type[] GetTypes()
		{
			return RuntimeModule.GetTypes(this.GetNativeHandle());
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x060047E8 RID: 18408 RVA: 0x00105A38 File Offset: 0x00103C38
		public override Guid ModuleVersionId
		{
			[SecuritySafeCritical]
			get
			{
				Guid guid;
				this.MetadataImport.GetScopeProps(out guid);
				return guid;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x060047E9 RID: 18409 RVA: 0x00105A56 File Offset: 0x00103C56
		public override int MetadataToken
		{
			[SecuritySafeCritical]
			get
			{
				return ModuleHandle.GetToken(this.GetNativeHandle());
			}
		}

		// Token: 0x060047EA RID: 18410 RVA: 0x00105A63 File Offset: 0x00103C63
		public override bool IsResource()
		{
			return RuntimeModule.IsResource(this.GetNativeHandle());
		}

		// Token: 0x060047EB RID: 18411 RVA: 0x00105A70 File Offset: 0x00103C70
		public override FieldInfo[] GetFields(BindingFlags bindingFlags)
		{
			if (this.RuntimeType == null)
			{
				return new FieldInfo[0];
			}
			return this.RuntimeType.GetFields(bindingFlags);
		}

		// Token: 0x060047EC RID: 18412 RVA: 0x00105A93 File Offset: 0x00103C93
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.RuntimeType == null)
			{
				return null;
			}
			return this.RuntimeType.GetField(name, bindingAttr);
		}

		// Token: 0x060047ED RID: 18413 RVA: 0x00105AC0 File Offset: 0x00103CC0
		public override MethodInfo[] GetMethods(BindingFlags bindingFlags)
		{
			if (this.RuntimeType == null)
			{
				return new MethodInfo[0];
			}
			return this.RuntimeType.GetMethods(bindingFlags);
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x060047EE RID: 18414 RVA: 0x00105AE4 File Offset: 0x00103CE4
		public override string ScopeName
		{
			[SecuritySafeCritical]
			get
			{
				string text = null;
				RuntimeModule.GetScopeName(this.GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref text));
				return text;
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x060047EF RID: 18415 RVA: 0x00105B08 File Offset: 0x00103D08
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				string fullyQualifiedName = this.GetFullyQualifiedName();
				int num = fullyQualifiedName.LastIndexOf('\\');
				if (num == -1)
				{
					return fullyQualifiedName;
				}
				return new string(fullyQualifiedName.ToCharArray(), num + 1, fullyQualifiedName.Length - num - 1);
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x060047F0 RID: 18416 RVA: 0x00105B43 File Offset: 0x00103D43
		public override Assembly Assembly
		{
			get
			{
				return this.GetRuntimeAssembly();
			}
		}

		// Token: 0x060047F1 RID: 18417 RVA: 0x00105B4B File Offset: 0x00103D4B
		internal RuntimeAssembly GetRuntimeAssembly()
		{
			return this.m_runtimeAssembly;
		}

		// Token: 0x060047F2 RID: 18418 RVA: 0x00105B53 File Offset: 0x00103D53
		internal override ModuleHandle GetModuleHandle()
		{
			return new ModuleHandle(this);
		}

		// Token: 0x060047F3 RID: 18419 RVA: 0x00105B5B File Offset: 0x00103D5B
		internal RuntimeModule GetNativeHandle()
		{
			return this;
		}

		// Token: 0x060047F4 RID: 18420 RVA: 0x00105B60 File Offset: 0x00103D60
		[SecuritySafeCritical]
		public override X509Certificate GetSignerCertificate()
		{
			byte[] array = null;
			RuntimeModule.GetSignerCertificate(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<byte[]>(ref array));
			if (array == null)
			{
				return null;
			}
			return new X509Certificate(array);
		}

		// Token: 0x04001DC5 RID: 7621
		private RuntimeType m_runtimeType;

		// Token: 0x04001DC6 RID: 7622
		private RuntimeAssembly m_runtimeAssembly;

		// Token: 0x04001DC7 RID: 7623
		private IntPtr m_pRefClass;

		// Token: 0x04001DC8 RID: 7624
		private IntPtr m_pData;

		// Token: 0x04001DC9 RID: 7625
		private IntPtr m_pGlobals;

		// Token: 0x04001DCA RID: 7626
		private IntPtr m_pFields;
	}
}
