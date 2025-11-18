using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x02000668 RID: 1640
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_EnumBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EnumBuilder : TypeInfo, _EnumBuilder
	{
		// Token: 0x06004E7C RID: 20092 RVA: 0x0011B618 File Offset: 0x00119818
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x06004E7D RID: 20093 RVA: 0x0011B634 File Offset: 0x00119834
		public FieldBuilder DefineLiteral(string literalName, object literalValue)
		{
			FieldBuilder fieldBuilder = this.m_typeBuilder.DefineField(literalName, this, FieldAttributes.FamANDAssem | FieldAttributes.Family | FieldAttributes.Static | FieldAttributes.Literal);
			fieldBuilder.SetConstant(literalValue);
			return fieldBuilder;
		}

		// Token: 0x06004E7E RID: 20094 RVA: 0x0011B659 File Offset: 0x00119859
		public TypeInfo CreateTypeInfo()
		{
			return this.m_typeBuilder.CreateTypeInfo();
		}

		// Token: 0x06004E7F RID: 20095 RVA: 0x0011B666 File Offset: 0x00119866
		public Type CreateType()
		{
			return this.m_typeBuilder.CreateType();
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06004E80 RID: 20096 RVA: 0x0011B673 File Offset: 0x00119873
		public TypeToken TypeToken
		{
			get
			{
				return this.m_typeBuilder.TypeToken;
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06004E81 RID: 20097 RVA: 0x0011B680 File Offset: 0x00119880
		public FieldBuilder UnderlyingField
		{
			get
			{
				return this.m_underlyingField;
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06004E82 RID: 20098 RVA: 0x0011B688 File Offset: 0x00119888
		public override string Name
		{
			get
			{
				return this.m_typeBuilder.Name;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06004E83 RID: 20099 RVA: 0x0011B695 File Offset: 0x00119895
		public override Guid GUID
		{
			get
			{
				return this.m_typeBuilder.GUID;
			}
		}

		// Token: 0x06004E84 RID: 20100 RVA: 0x0011B6A4 File Offset: 0x001198A4
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			return this.m_typeBuilder.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06004E85 RID: 20101 RVA: 0x0011B6C9 File Offset: 0x001198C9
		public override Module Module
		{
			get
			{
				return this.m_typeBuilder.Module;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06004E86 RID: 20102 RVA: 0x0011B6D6 File Offset: 0x001198D6
		public override Assembly Assembly
		{
			get
			{
				return this.m_typeBuilder.Assembly;
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06004E87 RID: 20103 RVA: 0x0011B6E3 File Offset: 0x001198E3
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return this.m_typeBuilder.TypeHandle;
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06004E88 RID: 20104 RVA: 0x0011B6F0 File Offset: 0x001198F0
		public override string FullName
		{
			get
			{
				return this.m_typeBuilder.FullName;
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06004E89 RID: 20105 RVA: 0x0011B6FD File Offset: 0x001198FD
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.m_typeBuilder.AssemblyQualifiedName;
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06004E8A RID: 20106 RVA: 0x0011B70A File Offset: 0x0011990A
		public override string Namespace
		{
			get
			{
				return this.m_typeBuilder.Namespace;
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06004E8B RID: 20107 RVA: 0x0011B717 File Offset: 0x00119917
		public override Type BaseType
		{
			get
			{
				return this.m_typeBuilder.BaseType;
			}
		}

		// Token: 0x06004E8C RID: 20108 RVA: 0x0011B724 File Offset: 0x00119924
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.m_typeBuilder.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06004E8D RID: 20109 RVA: 0x0011B738 File Offset: 0x00119938
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetConstructors(bindingAttr);
		}

		// Token: 0x06004E8E RID: 20110 RVA: 0x0011B746 File Offset: 0x00119946
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				return this.m_typeBuilder.GetMethod(name, bindingAttr);
			}
			return this.m_typeBuilder.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06004E8F RID: 20111 RVA: 0x0011B76E File Offset: 0x0011996E
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetMethods(bindingAttr);
		}

		// Token: 0x06004E90 RID: 20112 RVA: 0x0011B77C File Offset: 0x0011997C
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetField(name, bindingAttr);
		}

		// Token: 0x06004E91 RID: 20113 RVA: 0x0011B78B File Offset: 0x0011998B
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetFields(bindingAttr);
		}

		// Token: 0x06004E92 RID: 20114 RVA: 0x0011B799 File Offset: 0x00119999
		public override Type GetInterface(string name, bool ignoreCase)
		{
			return this.m_typeBuilder.GetInterface(name, ignoreCase);
		}

		// Token: 0x06004E93 RID: 20115 RVA: 0x0011B7A8 File Offset: 0x001199A8
		public override Type[] GetInterfaces()
		{
			return this.m_typeBuilder.GetInterfaces();
		}

		// Token: 0x06004E94 RID: 20116 RVA: 0x0011B7B5 File Offset: 0x001199B5
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetEvent(name, bindingAttr);
		}

		// Token: 0x06004E95 RID: 20117 RVA: 0x0011B7C4 File Offset: 0x001199C4
		public override EventInfo[] GetEvents()
		{
			return this.m_typeBuilder.GetEvents();
		}

		// Token: 0x06004E96 RID: 20118 RVA: 0x0011B7D1 File Offset: 0x001199D1
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x0011B7E2 File Offset: 0x001199E2
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetProperties(bindingAttr);
		}

		// Token: 0x06004E98 RID: 20120 RVA: 0x0011B7F0 File Offset: 0x001199F0
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetNestedTypes(bindingAttr);
		}

		// Token: 0x06004E99 RID: 20121 RVA: 0x0011B7FE File Offset: 0x001199FE
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetNestedType(name, bindingAttr);
		}

		// Token: 0x06004E9A RID: 20122 RVA: 0x0011B80D File Offset: 0x00119A0D
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetMember(name, type, bindingAttr);
		}

		// Token: 0x06004E9B RID: 20123 RVA: 0x0011B81D File Offset: 0x00119A1D
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetMembers(bindingAttr);
		}

		// Token: 0x06004E9C RID: 20124 RVA: 0x0011B82B File Offset: 0x00119A2B
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			return this.m_typeBuilder.GetInterfaceMap(interfaceType);
		}

		// Token: 0x06004E9D RID: 20125 RVA: 0x0011B839 File Offset: 0x00119A39
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetEvents(bindingAttr);
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x0011B847 File Offset: 0x00119A47
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.m_typeBuilder.Attributes;
		}

		// Token: 0x06004E9F RID: 20127 RVA: 0x0011B854 File Offset: 0x00119A54
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004EA0 RID: 20128 RVA: 0x0011B857 File Offset: 0x00119A57
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004EA1 RID: 20129 RVA: 0x0011B85A File Offset: 0x00119A5A
		protected override bool IsValueTypeImpl()
		{
			return true;
		}

		// Token: 0x06004EA2 RID: 20130 RVA: 0x0011B85D File Offset: 0x00119A5D
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004EA3 RID: 20131 RVA: 0x0011B860 File Offset: 0x00119A60
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x0011B863 File Offset: 0x00119A63
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06004EA5 RID: 20133 RVA: 0x0011B866 File Offset: 0x00119A66
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004EA6 RID: 20134 RVA: 0x0011B869 File Offset: 0x00119A69
		public override Type GetElementType()
		{
			return this.m_typeBuilder.GetElementType();
		}

		// Token: 0x06004EA7 RID: 20135 RVA: 0x0011B876 File Offset: 0x00119A76
		protected override bool HasElementTypeImpl()
		{
			return this.m_typeBuilder.HasElementType;
		}

		// Token: 0x06004EA8 RID: 20136 RVA: 0x0011B883 File Offset: 0x00119A83
		public override Type GetEnumUnderlyingType()
		{
			return this.m_underlyingField.FieldType;
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06004EA9 RID: 20137 RVA: 0x0011B890 File Offset: 0x00119A90
		public override Type UnderlyingSystemType
		{
			get
			{
				return this.GetEnumUnderlyingType();
			}
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x0011B898 File Offset: 0x00119A98
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_typeBuilder.GetCustomAttributes(inherit);
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x0011B8A6 File Offset: 0x00119AA6
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_typeBuilder.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004EAC RID: 20140 RVA: 0x0011B8B5 File Offset: 0x00119AB5
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.m_typeBuilder.SetCustomAttribute(con, binaryAttribute);
		}

		// Token: 0x06004EAD RID: 20141 RVA: 0x0011B8C4 File Offset: 0x00119AC4
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.m_typeBuilder.SetCustomAttribute(customBuilder);
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06004EAE RID: 20142 RVA: 0x0011B8D2 File Offset: 0x00119AD2
		public override Type DeclaringType
		{
			get
			{
				return this.m_typeBuilder.DeclaringType;
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06004EAF RID: 20143 RVA: 0x0011B8DF File Offset: 0x00119ADF
		public override Type ReflectedType
		{
			get
			{
				return this.m_typeBuilder.ReflectedType;
			}
		}

		// Token: 0x06004EB0 RID: 20144 RVA: 0x0011B8EC File Offset: 0x00119AEC
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_typeBuilder.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06004EB1 RID: 20145 RVA: 0x0011B8FB File Offset: 0x00119AFB
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_typeBuilder.MetadataTokenInternal;
			}
		}

		// Token: 0x06004EB2 RID: 20146 RVA: 0x0011B908 File Offset: 0x00119B08
		private EnumBuilder()
		{
		}

		// Token: 0x06004EB3 RID: 20147 RVA: 0x0011B910 File Offset: 0x00119B10
		public override Type MakePointerType()
		{
			return SymbolType.FormCompoundType("*".ToCharArray(), this, 0);
		}

		// Token: 0x06004EB4 RID: 20148 RVA: 0x0011B923 File Offset: 0x00119B23
		public override Type MakeByRefType()
		{
			return SymbolType.FormCompoundType("&".ToCharArray(), this, 0);
		}

		// Token: 0x06004EB5 RID: 20149 RVA: 0x0011B936 File Offset: 0x00119B36
		public override Type MakeArrayType()
		{
			return SymbolType.FormCompoundType("[]".ToCharArray(), this, 0);
		}

		// Token: 0x06004EB6 RID: 20150 RVA: 0x0011B94C File Offset: 0x00119B4C
		public override Type MakeArrayType(int rank)
		{
			if (rank <= 0)
			{
				throw new IndexOutOfRangeException();
			}
			string text = "";
			if (rank == 1)
			{
				text = "*";
			}
			else
			{
				for (int i = 1; i < rank; i++)
				{
					text += ",";
				}
			}
			string text2 = string.Format(CultureInfo.InvariantCulture, "[{0}]", text);
			return SymbolType.FormCompoundType(text2.ToCharArray(), this, 0);
		}

		// Token: 0x06004EB7 RID: 20151 RVA: 0x0011B9AC File Offset: 0x00119BAC
		[SecurityCritical]
		internal EnumBuilder(string name, Type underlyingType, TypeAttributes visibility, ModuleBuilder module)
		{
			if ((visibility & ~TypeAttributes.VisibilityMask) != TypeAttributes.NotPublic)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ShouldOnlySetVisibilityFlags"), "name");
			}
			this.m_typeBuilder = new TypeBuilder(name, visibility | TypeAttributes.Sealed, typeof(Enum), null, module, PackingSize.Unspecified, 0, null);
			this.m_underlyingField = this.m_typeBuilder.DefineField("value__", underlyingType, FieldAttributes.FamANDAssem | FieldAttributes.Family | FieldAttributes.SpecialName | FieldAttributes.RTSpecialName);
		}

		// Token: 0x06004EB8 RID: 20152 RVA: 0x0011BA19 File Offset: 0x00119C19
		void _EnumBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x0011BA20 File Offset: 0x00119C20
		void _EnumBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x0011BA27 File Offset: 0x00119C27
		void _EnumBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004EBB RID: 20155 RVA: 0x0011BA2E File Offset: 0x00119C2E
		void _EnumBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040021D9 RID: 8665
		internal TypeBuilder m_typeBuilder;

		// Token: 0x040021DA RID: 8666
		private FieldBuilder m_underlyingField;
	}
}
