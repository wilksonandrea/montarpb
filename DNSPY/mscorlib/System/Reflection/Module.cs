using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x0200060F RID: 1551
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Module))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	[Serializable]
	public abstract class Module : _Module, ISerializable, ICustomAttributeProvider
	{
		// Token: 0x06004791 RID: 18321 RVA: 0x001048A0 File Offset: 0x00102AA0
		static Module()
		{
			__Filters _Filters = new __Filters();
			Module.FilterTypeName = new TypeFilter(_Filters.FilterTypeName);
			Module.FilterTypeNameIgnoreCase = new TypeFilter(_Filters.FilterTypeNameIgnoreCase);
		}

		// Token: 0x06004792 RID: 18322 RVA: 0x001048D7 File Offset: 0x00102AD7
		protected Module()
		{
		}

		// Token: 0x06004793 RID: 18323 RVA: 0x001048DF File Offset: 0x00102ADF
		[__DynamicallyInvokable]
		public static bool operator ==(Module left, Module right)
		{
			return left == right || (left != null && right != null && !(left is RuntimeModule) && !(right is RuntimeModule) && left.Equals(right));
		}

		// Token: 0x06004794 RID: 18324 RVA: 0x00104906 File Offset: 0x00102B06
		[__DynamicallyInvokable]
		public static bool operator !=(Module left, Module right)
		{
			return !(left == right);
		}

		// Token: 0x06004795 RID: 18325 RVA: 0x00104912 File Offset: 0x00102B12
		[__DynamicallyInvokable]
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		// Token: 0x06004796 RID: 18326 RVA: 0x0010491B File Offset: 0x00102B1B
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x00104923 File Offset: 0x00102B23
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.ScopeName;
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06004798 RID: 18328 RVA: 0x0010492B File Offset: 0x00102B2B
		[__DynamicallyInvokable]
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x06004799 RID: 18329 RVA: 0x00104933 File Offset: 0x00102B33
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x0010493A File Offset: 0x00102B3A
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x00104941 File Offset: 0x00102B41
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x00104948 File Offset: 0x00102B48
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x0010494F File Offset: 0x00102B4F
		public MethodBase ResolveMethod(int metadataToken)
		{
			return this.ResolveMethod(metadataToken, null, null);
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x0010495C File Offset: 0x00102B5C
		public virtual MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				return runtimeModule.ResolveMethod(metadataToken, genericTypeArguments, genericMethodArguments);
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x00104988 File Offset: 0x00102B88
		public FieldInfo ResolveField(int metadataToken)
		{
			return this.ResolveField(metadataToken, null, null);
		}

		// Token: 0x060047A0 RID: 18336 RVA: 0x00104994 File Offset: 0x00102B94
		public virtual FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				return runtimeModule.ResolveField(metadataToken, genericTypeArguments, genericMethodArguments);
			}
			throw new NotImplementedException();
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x001049C0 File Offset: 0x00102BC0
		public Type ResolveType(int metadataToken)
		{
			return this.ResolveType(metadataToken, null, null);
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x001049CC File Offset: 0x00102BCC
		public virtual Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				return runtimeModule.ResolveType(metadataToken, genericTypeArguments, genericMethodArguments);
			}
			throw new NotImplementedException();
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x001049F8 File Offset: 0x00102BF8
		public MemberInfo ResolveMember(int metadataToken)
		{
			return this.ResolveMember(metadataToken, null, null);
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x00104A04 File Offset: 0x00102C04
		public virtual MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				return runtimeModule.ResolveMember(metadataToken, genericTypeArguments, genericMethodArguments);
			}
			throw new NotImplementedException();
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x00104A30 File Offset: 0x00102C30
		public virtual byte[] ResolveSignature(int metadataToken)
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				return runtimeModule.ResolveSignature(metadataToken);
			}
			throw new NotImplementedException();
		}

		// Token: 0x060047A6 RID: 18342 RVA: 0x00104A5C File Offset: 0x00102C5C
		public virtual string ResolveString(int metadataToken)
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				return runtimeModule.ResolveString(metadataToken);
			}
			throw new NotImplementedException();
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x00104A88 File Offset: 0x00102C88
		public virtual void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				runtimeModule.GetPEKind(out peKind, out machine);
			}
			throw new NotImplementedException();
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x060047A8 RID: 18344 RVA: 0x00104AB4 File Offset: 0x00102CB4
		public virtual int MDStreamVersion
		{
			get
			{
				RuntimeModule runtimeModule = this as RuntimeModule;
				if (runtimeModule != null)
				{
					return runtimeModule.MDStreamVersion;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x060047A9 RID: 18345 RVA: 0x00104ADD File Offset: 0x00102CDD
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060047AA RID: 18346 RVA: 0x00104AE4 File Offset: 0x00102CE4
		[ComVisible(true)]
		public virtual Type GetType(string className, bool ignoreCase)
		{
			return this.GetType(className, false, ignoreCase);
		}

		// Token: 0x060047AB RID: 18347 RVA: 0x00104AEF File Offset: 0x00102CEF
		[ComVisible(true)]
		public virtual Type GetType(string className)
		{
			return this.GetType(className, false, false);
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x00104AFA File Offset: 0x00102CFA
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public virtual Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x060047AD RID: 18349 RVA: 0x00104B01 File Offset: 0x00102D01
		[__DynamicallyInvokable]
		public virtual string FullyQualifiedName
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x00104B08 File Offset: 0x00102D08
		public virtual Type[] FindTypes(TypeFilter filter, object filterCriteria)
		{
			Type[] types = this.GetTypes();
			int num = 0;
			for (int i = 0; i < types.Length; i++)
			{
				if (filter != null && !filter(types[i], filterCriteria))
				{
					types[i] = null;
				}
				else
				{
					num++;
				}
			}
			if (num == types.Length)
			{
				return types;
			}
			Type[] array = new Type[num];
			num = 0;
			for (int j = 0; j < types.Length; j++)
			{
				if (types[j] != null)
				{
					array[num++] = types[j];
				}
			}
			return array;
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x00104B80 File Offset: 0x00102D80
		public virtual Type[] GetTypes()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x060047B0 RID: 18352 RVA: 0x00104B88 File Offset: 0x00102D88
		public virtual Guid ModuleVersionId
		{
			get
			{
				RuntimeModule runtimeModule = this as RuntimeModule;
				if (runtimeModule != null)
				{
					return runtimeModule.ModuleVersionId;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x060047B1 RID: 18353 RVA: 0x00104BB4 File Offset: 0x00102DB4
		public virtual int MetadataToken
		{
			get
			{
				RuntimeModule runtimeModule = this as RuntimeModule;
				if (runtimeModule != null)
				{
					return runtimeModule.MetadataToken;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x060047B2 RID: 18354 RVA: 0x00104BE0 File Offset: 0x00102DE0
		public virtual bool IsResource()
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				return runtimeModule.IsResource();
			}
			throw new NotImplementedException();
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x00104C09 File Offset: 0x00102E09
		public FieldInfo[] GetFields()
		{
			return this.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060047B4 RID: 18356 RVA: 0x00104C14 File Offset: 0x00102E14
		public virtual FieldInfo[] GetFields(BindingFlags bindingFlags)
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				return runtimeModule.GetFields(bindingFlags);
			}
			throw new NotImplementedException();
		}

		// Token: 0x060047B5 RID: 18357 RVA: 0x00104C3E File Offset: 0x00102E3E
		public FieldInfo GetField(string name)
		{
			return this.GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060047B6 RID: 18358 RVA: 0x00104C4C File Offset: 0x00102E4C
		public virtual FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				return runtimeModule.GetField(name, bindingAttr);
			}
			throw new NotImplementedException();
		}

		// Token: 0x060047B7 RID: 18359 RVA: 0x00104C77 File Offset: 0x00102E77
		public MethodInfo[] GetMethods()
		{
			return this.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060047B8 RID: 18360 RVA: 0x00104C84 File Offset: 0x00102E84
		public virtual MethodInfo[] GetMethods(BindingFlags bindingFlags)
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				return runtimeModule.GetMethods(bindingFlags);
			}
			throw new NotImplementedException();
		}

		// Token: 0x060047B9 RID: 18361 RVA: 0x00104CB0 File Offset: 0x00102EB0
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetMethodImpl(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x060047BA RID: 18362 RVA: 0x00104D10 File Offset: 0x00102F10
		public MethodInfo GetMethod(string name, Type[] types)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, types, null);
		}

		// Token: 0x060047BB RID: 18363 RVA: 0x00104D6A File Offset: 0x00102F6A
		public MethodInfo GetMethod(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, null, null);
		}

		// Token: 0x060047BC RID: 18364 RVA: 0x00104D87 File Offset: 0x00102F87
		protected virtual MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x060047BD RID: 18365 RVA: 0x00104D90 File Offset: 0x00102F90
		public virtual string ScopeName
		{
			get
			{
				RuntimeModule runtimeModule = this as RuntimeModule;
				if (runtimeModule != null)
				{
					return runtimeModule.ScopeName;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x060047BE RID: 18366 RVA: 0x00104DBC File Offset: 0x00102FBC
		[__DynamicallyInvokable]
		public virtual string Name
		{
			[__DynamicallyInvokable]
			get
			{
				RuntimeModule runtimeModule = this as RuntimeModule;
				if (runtimeModule != null)
				{
					return runtimeModule.Name;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x060047BF RID: 18367 RVA: 0x00104DE8 File Offset: 0x00102FE8
		[__DynamicallyInvokable]
		public virtual Assembly Assembly
		{
			[__DynamicallyInvokable]
			get
			{
				RuntimeModule runtimeModule = this as RuntimeModule;
				if (runtimeModule != null)
				{
					return runtimeModule.Assembly;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x060047C0 RID: 18368 RVA: 0x00104E11 File Offset: 0x00103011
		public ModuleHandle ModuleHandle
		{
			get
			{
				return this.GetModuleHandle();
			}
		}

		// Token: 0x060047C1 RID: 18369 RVA: 0x00104E19 File Offset: 0x00103019
		internal virtual ModuleHandle GetModuleHandle()
		{
			return ModuleHandle.EmptyHandle;
		}

		// Token: 0x060047C2 RID: 18370 RVA: 0x00104E20 File Offset: 0x00103020
		public virtual X509Certificate GetSignerCertificate()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x00104E27 File Offset: 0x00103027
		void _Module.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060047C4 RID: 18372 RVA: 0x00104E2E File Offset: 0x0010302E
		void _Module.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x00104E35 File Offset: 0x00103035
		void _Module.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060047C6 RID: 18374 RVA: 0x00104E3C File Offset: 0x0010303C
		void _Module.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001DC2 RID: 7618
		public static readonly TypeFilter FilterTypeName;

		// Token: 0x04001DC3 RID: 7619
		public static readonly TypeFilter FilterTypeNameIgnoreCase;

		// Token: 0x04001DC4 RID: 7620
		private const BindingFlags DefaultLookup = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
	}
}
