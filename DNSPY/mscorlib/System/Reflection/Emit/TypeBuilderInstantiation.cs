using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000666 RID: 1638
	internal sealed class TypeBuilderInstantiation : TypeInfo
	{
		// Token: 0x06004DF5 RID: 19957 RVA: 0x0011AF53 File Offset: 0x00119153
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x06004DF6 RID: 19958 RVA: 0x0011AF6C File Offset: 0x0011916C
		internal static Type MakeGenericType(Type type, Type[] typeArguments)
		{
			if (!type.IsGenericTypeDefinition)
			{
				throw new InvalidOperationException();
			}
			if (typeArguments == null)
			{
				throw new ArgumentNullException("typeArguments");
			}
			foreach (Type type2 in typeArguments)
			{
				if (type2 == null)
				{
					throw new ArgumentNullException("typeArguments");
				}
			}
			return new TypeBuilderInstantiation(type, typeArguments);
		}

		// Token: 0x06004DF7 RID: 19959 RVA: 0x0011AFC4 File Offset: 0x001191C4
		private TypeBuilderInstantiation(Type type, Type[] inst)
		{
			this.m_type = type;
			this.m_inst = inst;
			this.m_hashtable = new Hashtable();
		}

		// Token: 0x06004DF8 RID: 19960 RVA: 0x0011AFF0 File Offset: 0x001191F0
		public override string ToString()
		{
			return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.ToString);
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06004DF9 RID: 19961 RVA: 0x0011AFF9 File Offset: 0x001191F9
		public override Type DeclaringType
		{
			get
			{
				return this.m_type.DeclaringType;
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06004DFA RID: 19962 RVA: 0x0011B006 File Offset: 0x00119206
		public override Type ReflectedType
		{
			get
			{
				return this.m_type.ReflectedType;
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06004DFB RID: 19963 RVA: 0x0011B013 File Offset: 0x00119213
		public override string Name
		{
			get
			{
				return this.m_type.Name;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06004DFC RID: 19964 RVA: 0x0011B020 File Offset: 0x00119220
		public override Module Module
		{
			get
			{
				return this.m_type.Module;
			}
		}

		// Token: 0x06004DFD RID: 19965 RVA: 0x0011B02D File Offset: 0x0011922D
		public override Type MakePointerType()
		{
			return SymbolType.FormCompoundType("*".ToCharArray(), this, 0);
		}

		// Token: 0x06004DFE RID: 19966 RVA: 0x0011B040 File Offset: 0x00119240
		public override Type MakeByRefType()
		{
			return SymbolType.FormCompoundType("&".ToCharArray(), this, 0);
		}

		// Token: 0x06004DFF RID: 19967 RVA: 0x0011B053 File Offset: 0x00119253
		public override Type MakeArrayType()
		{
			return SymbolType.FormCompoundType("[]".ToCharArray(), this, 0);
		}

		// Token: 0x06004E00 RID: 19968 RVA: 0x0011B068 File Offset: 0x00119268
		public override Type MakeArrayType(int rank)
		{
			if (rank <= 0)
			{
				throw new IndexOutOfRangeException();
			}
			string text = "";
			for (int i = 1; i < rank; i++)
			{
				text += ",";
			}
			string text2 = string.Format(CultureInfo.InvariantCulture, "[{0}]", text);
			return SymbolType.FormCompoundType(text2.ToCharArray(), this, 0);
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06004E01 RID: 19969 RVA: 0x0011B0BB File Offset: 0x001192BB
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004E02 RID: 19970 RVA: 0x0011B0C2 File Offset: 0x001192C2
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06004E03 RID: 19971 RVA: 0x0011B0C9 File Offset: 0x001192C9
		public override Assembly Assembly
		{
			get
			{
				return this.m_type.Assembly;
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06004E04 RID: 19972 RVA: 0x0011B0D6 File Offset: 0x001192D6
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06004E05 RID: 19973 RVA: 0x0011B0DD File Offset: 0x001192DD
		public override string FullName
		{
			get
			{
				if (this.m_strFullQualName == null)
				{
					this.m_strFullQualName = TypeNameBuilder.ToString(this, TypeNameBuilder.Format.FullName);
				}
				return this.m_strFullQualName;
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06004E06 RID: 19974 RVA: 0x0011B0FA File Offset: 0x001192FA
		public override string Namespace
		{
			get
			{
				return this.m_type.Namespace;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06004E07 RID: 19975 RVA: 0x0011B107 File Offset: 0x00119307
		public override string AssemblyQualifiedName
		{
			get
			{
				return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.AssemblyQualifiedName);
			}
		}

		// Token: 0x06004E08 RID: 19976 RVA: 0x0011B110 File Offset: 0x00119310
		private Type Substitute(Type[] substitutes)
		{
			Type[] genericArguments = this.GetGenericArguments();
			Type[] array = new Type[genericArguments.Length];
			for (int i = 0; i < array.Length; i++)
			{
				Type type = genericArguments[i];
				if (type is TypeBuilderInstantiation)
				{
					array[i] = (type as TypeBuilderInstantiation).Substitute(substitutes);
				}
				else if (type is GenericTypeParameterBuilder)
				{
					array[i] = substitutes[type.GenericParameterPosition];
				}
				else
				{
					array[i] = type;
				}
			}
			return this.GetGenericTypeDefinition().MakeGenericType(array);
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06004E09 RID: 19977 RVA: 0x0011B180 File Offset: 0x00119380
		public override Type BaseType
		{
			get
			{
				Type baseType = this.m_type.BaseType;
				if (baseType == null)
				{
					return null;
				}
				TypeBuilderInstantiation typeBuilderInstantiation = baseType as TypeBuilderInstantiation;
				if (typeBuilderInstantiation == null)
				{
					return baseType;
				}
				return typeBuilderInstantiation.Substitute(this.GetGenericArguments());
			}
		}

		// Token: 0x06004E0A RID: 19978 RVA: 0x0011B1C2 File Offset: 0x001193C2
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E0B RID: 19979 RVA: 0x0011B1C9 File Offset: 0x001193C9
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E0C RID: 19980 RVA: 0x0011B1D0 File Offset: 0x001193D0
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E0D RID: 19981 RVA: 0x0011B1D7 File Offset: 0x001193D7
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E0E RID: 19982 RVA: 0x0011B1DE File Offset: 0x001193DE
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E0F RID: 19983 RVA: 0x0011B1E5 File Offset: 0x001193E5
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E10 RID: 19984 RVA: 0x0011B1EC File Offset: 0x001193EC
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E11 RID: 19985 RVA: 0x0011B1F3 File Offset: 0x001193F3
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E12 RID: 19986 RVA: 0x0011B1FA File Offset: 0x001193FA
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E13 RID: 19987 RVA: 0x0011B201 File Offset: 0x00119401
		public override EventInfo[] GetEvents()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E14 RID: 19988 RVA: 0x0011B208 File Offset: 0x00119408
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E15 RID: 19989 RVA: 0x0011B20F File Offset: 0x0011940F
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E16 RID: 19990 RVA: 0x0011B216 File Offset: 0x00119416
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E17 RID: 19991 RVA: 0x0011B21D File Offset: 0x0011941D
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E18 RID: 19992 RVA: 0x0011B224 File Offset: 0x00119424
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E19 RID: 19993 RVA: 0x0011B22B File Offset: 0x0011942B
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E1A RID: 19994 RVA: 0x0011B232 File Offset: 0x00119432
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E1B RID: 19995 RVA: 0x0011B239 File Offset: 0x00119439
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E1C RID: 19996 RVA: 0x0011B240 File Offset: 0x00119440
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.m_type.Attributes;
		}

		// Token: 0x06004E1D RID: 19997 RVA: 0x0011B24D File Offset: 0x0011944D
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004E1E RID: 19998 RVA: 0x0011B250 File Offset: 0x00119450
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004E1F RID: 19999 RVA: 0x0011B253 File Offset: 0x00119453
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06004E20 RID: 20000 RVA: 0x0011B256 File Offset: 0x00119456
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004E21 RID: 20001 RVA: 0x0011B259 File Offset: 0x00119459
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x0011B25C File Offset: 0x0011945C
		public override Type GetElementType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E23 RID: 20003 RVA: 0x0011B263 File Offset: 0x00119463
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06004E24 RID: 20004 RVA: 0x0011B266 File Offset: 0x00119466
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06004E25 RID: 20005 RVA: 0x0011B269 File Offset: 0x00119469
		public override Type[] GetGenericArguments()
		{
			return this.m_inst;
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06004E26 RID: 20006 RVA: 0x0011B271 File Offset: 0x00119471
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06004E27 RID: 20007 RVA: 0x0011B274 File Offset: 0x00119474
		public override bool IsGenericType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06004E28 RID: 20008 RVA: 0x0011B277 File Offset: 0x00119477
		public override bool IsConstructedGenericType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06004E29 RID: 20009 RVA: 0x0011B27A File Offset: 0x0011947A
		public override bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06004E2A RID: 20010 RVA: 0x0011B27D File Offset: 0x0011947D
		public override int GenericParameterPosition
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06004E2B RID: 20011 RVA: 0x0011B284 File Offset: 0x00119484
		protected override bool IsValueTypeImpl()
		{
			return this.m_type.IsValueType;
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06004E2C RID: 20012 RVA: 0x0011B294 File Offset: 0x00119494
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
				return false;
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06004E2D RID: 20013 RVA: 0x0011B2C6 File Offset: 0x001194C6
		public override MethodBase DeclaringMethod
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004E2E RID: 20014 RVA: 0x0011B2C9 File Offset: 0x001194C9
		public override Type GetGenericTypeDefinition()
		{
			return this.m_type;
		}

		// Token: 0x06004E2F RID: 20015 RVA: 0x0011B2D1 File Offset: 0x001194D1
		public override Type MakeGenericType(params Type[] inst)
		{
			throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericTypeDefinition"));
		}

		// Token: 0x06004E30 RID: 20016 RVA: 0x0011B2E2 File Offset: 0x001194E2
		public override bool IsAssignableFrom(Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E31 RID: 20017 RVA: 0x0011B2E9 File Offset: 0x001194E9
		[ComVisible(true)]
		public override bool IsSubclassOf(Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E32 RID: 20018 RVA: 0x0011B2F0 File Offset: 0x001194F0
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E33 RID: 20019 RVA: 0x0011B2F7 File Offset: 0x001194F7
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E34 RID: 20020 RVA: 0x0011B2FE File Offset: 0x001194FE
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040021D4 RID: 8660
		private Type m_type;

		// Token: 0x040021D5 RID: 8661
		private Type[] m_inst;

		// Token: 0x040021D6 RID: 8662
		private string m_strFullQualName;

		// Token: 0x040021D7 RID: 8663
		internal Hashtable m_hashtable = new Hashtable();
	}
}
