using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000667 RID: 1639
	[ComVisible(true)]
	public sealed class GenericTypeParameterBuilder : TypeInfo
	{
		// Token: 0x06004E35 RID: 20021 RVA: 0x0011B305 File Offset: 0x00119505
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x0011B31E File Offset: 0x0011951E
		internal GenericTypeParameterBuilder(TypeBuilder type)
		{
			this.m_type = type;
		}

		// Token: 0x06004E37 RID: 20023 RVA: 0x0011B32D File Offset: 0x0011952D
		public override string ToString()
		{
			return this.m_type.Name;
		}

		// Token: 0x06004E38 RID: 20024 RVA: 0x0011B33C File Offset: 0x0011953C
		public override bool Equals(object o)
		{
			GenericTypeParameterBuilder genericTypeParameterBuilder = o as GenericTypeParameterBuilder;
			return !(genericTypeParameterBuilder == null) && genericTypeParameterBuilder.m_type == this.m_type;
		}

		// Token: 0x06004E39 RID: 20025 RVA: 0x0011B369 File Offset: 0x00119569
		public override int GetHashCode()
		{
			return this.m_type.GetHashCode();
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06004E3A RID: 20026 RVA: 0x0011B376 File Offset: 0x00119576
		public override Type DeclaringType
		{
			get
			{
				return this.m_type.DeclaringType;
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06004E3B RID: 20027 RVA: 0x0011B383 File Offset: 0x00119583
		public override Type ReflectedType
		{
			get
			{
				return this.m_type.ReflectedType;
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06004E3C RID: 20028 RVA: 0x0011B390 File Offset: 0x00119590
		public override string Name
		{
			get
			{
				return this.m_type.Name;
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06004E3D RID: 20029 RVA: 0x0011B39D File Offset: 0x0011959D
		public override Module Module
		{
			get
			{
				return this.m_type.Module;
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06004E3E RID: 20030 RVA: 0x0011B3AA File Offset: 0x001195AA
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_type.MetadataTokenInternal;
			}
		}

		// Token: 0x06004E3F RID: 20031 RVA: 0x0011B3B7 File Offset: 0x001195B7
		public override Type MakePointerType()
		{
			return SymbolType.FormCompoundType("*".ToCharArray(), this, 0);
		}

		// Token: 0x06004E40 RID: 20032 RVA: 0x0011B3CA File Offset: 0x001195CA
		public override Type MakeByRefType()
		{
			return SymbolType.FormCompoundType("&".ToCharArray(), this, 0);
		}

		// Token: 0x06004E41 RID: 20033 RVA: 0x0011B3DD File Offset: 0x001195DD
		public override Type MakeArrayType()
		{
			return SymbolType.FormCompoundType("[]".ToCharArray(), this, 0);
		}

		// Token: 0x06004E42 RID: 20034 RVA: 0x0011B3F0 File Offset: 0x001195F0
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
			return SymbolType.FormCompoundType(text2.ToCharArray(), this, 0) as SymbolType;
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06004E43 RID: 20035 RVA: 0x0011B456 File Offset: 0x00119656
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004E44 RID: 20036 RVA: 0x0011B45D File Offset: 0x0011965D
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06004E45 RID: 20037 RVA: 0x0011B464 File Offset: 0x00119664
		public override Assembly Assembly
		{
			get
			{
				return this.m_type.Assembly;
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06004E46 RID: 20038 RVA: 0x0011B471 File Offset: 0x00119671
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06004E47 RID: 20039 RVA: 0x0011B478 File Offset: 0x00119678
		public override string FullName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06004E48 RID: 20040 RVA: 0x0011B47B File Offset: 0x0011967B
		public override string Namespace
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06004E49 RID: 20041 RVA: 0x0011B47E File Offset: 0x0011967E
		public override string AssemblyQualifiedName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06004E4A RID: 20042 RVA: 0x0011B481 File Offset: 0x00119681
		public override Type BaseType
		{
			get
			{
				return this.m_type.BaseType;
			}
		}

		// Token: 0x06004E4B RID: 20043 RVA: 0x0011B48E File Offset: 0x0011968E
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E4C RID: 20044 RVA: 0x0011B495 File Offset: 0x00119695
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E4D RID: 20045 RVA: 0x0011B49C File Offset: 0x0011969C
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E4E RID: 20046 RVA: 0x0011B4A3 File Offset: 0x001196A3
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E4F RID: 20047 RVA: 0x0011B4AA File Offset: 0x001196AA
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E50 RID: 20048 RVA: 0x0011B4B1 File Offset: 0x001196B1
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E51 RID: 20049 RVA: 0x0011B4B8 File Offset: 0x001196B8
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E52 RID: 20050 RVA: 0x0011B4BF File Offset: 0x001196BF
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E53 RID: 20051 RVA: 0x0011B4C6 File Offset: 0x001196C6
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E54 RID: 20052 RVA: 0x0011B4CD File Offset: 0x001196CD
		public override EventInfo[] GetEvents()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E55 RID: 20053 RVA: 0x0011B4D4 File Offset: 0x001196D4
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E56 RID: 20054 RVA: 0x0011B4DB File Offset: 0x001196DB
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E57 RID: 20055 RVA: 0x0011B4E2 File Offset: 0x001196E2
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E58 RID: 20056 RVA: 0x0011B4E9 File Offset: 0x001196E9
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E59 RID: 20057 RVA: 0x0011B4F0 File Offset: 0x001196F0
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E5A RID: 20058 RVA: 0x0011B4F7 File Offset: 0x001196F7
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E5B RID: 20059 RVA: 0x0011B4FE File Offset: 0x001196FE
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E5C RID: 20060 RVA: 0x0011B505 File Offset: 0x00119705
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E5D RID: 20061 RVA: 0x0011B50C File Offset: 0x0011970C
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return TypeAttributes.Public;
		}

		// Token: 0x06004E5E RID: 20062 RVA: 0x0011B50F File Offset: 0x0011970F
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004E5F RID: 20063 RVA: 0x0011B512 File Offset: 0x00119712
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004E60 RID: 20064 RVA: 0x0011B515 File Offset: 0x00119715
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06004E61 RID: 20065 RVA: 0x0011B518 File Offset: 0x00119718
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004E62 RID: 20066 RVA: 0x0011B51B File Offset: 0x0011971B
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x06004E63 RID: 20067 RVA: 0x0011B51E File Offset: 0x0011971E
		public override Type GetElementType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E64 RID: 20068 RVA: 0x0011B525 File Offset: 0x00119725
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06004E65 RID: 20069 RVA: 0x0011B528 File Offset: 0x00119728
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06004E66 RID: 20070 RVA: 0x0011B52B File Offset: 0x0011972B
		public override Type[] GetGenericArguments()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06004E67 RID: 20071 RVA: 0x0011B532 File Offset: 0x00119732
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06004E68 RID: 20072 RVA: 0x0011B535 File Offset: 0x00119735
		public override bool IsGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06004E69 RID: 20073 RVA: 0x0011B538 File Offset: 0x00119738
		public override bool IsGenericParameter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06004E6A RID: 20074 RVA: 0x0011B53B File Offset: 0x0011973B
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06004E6B RID: 20075 RVA: 0x0011B53E File Offset: 0x0011973E
		public override int GenericParameterPosition
		{
			get
			{
				return this.m_type.GenericParameterPosition;
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06004E6C RID: 20076 RVA: 0x0011B54B File Offset: 0x0011974B
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.m_type.ContainsGenericParameters;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06004E6D RID: 20077 RVA: 0x0011B558 File Offset: 0x00119758
		public override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				return this.m_type.GenericParameterAttributes;
			}
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06004E6E RID: 20078 RVA: 0x0011B565 File Offset: 0x00119765
		public override MethodBase DeclaringMethod
		{
			get
			{
				return this.m_type.DeclaringMethod;
			}
		}

		// Token: 0x06004E6F RID: 20079 RVA: 0x0011B572 File Offset: 0x00119772
		public override Type GetGenericTypeDefinition()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06004E70 RID: 20080 RVA: 0x0011B579 File Offset: 0x00119779
		public override Type MakeGenericType(params Type[] typeArguments)
		{
			throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericTypeDefinition"));
		}

		// Token: 0x06004E71 RID: 20081 RVA: 0x0011B58A File Offset: 0x0011978A
		protected override bool IsValueTypeImpl()
		{
			return false;
		}

		// Token: 0x06004E72 RID: 20082 RVA: 0x0011B58D File Offset: 0x0011978D
		public override bool IsAssignableFrom(Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E73 RID: 20083 RVA: 0x0011B594 File Offset: 0x00119794
		[ComVisible(true)]
		public override bool IsSubclassOf(Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E74 RID: 20084 RVA: 0x0011B59B File Offset: 0x0011979B
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E75 RID: 20085 RVA: 0x0011B5A2 File Offset: 0x001197A2
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E76 RID: 20086 RVA: 0x0011B5A9 File Offset: 0x001197A9
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E77 RID: 20087 RVA: 0x0011B5B0 File Offset: 0x001197B0
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.m_type.SetGenParamCustomAttribute(con, binaryAttribute);
		}

		// Token: 0x06004E78 RID: 20088 RVA: 0x0011B5BF File Offset: 0x001197BF
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.m_type.SetGenParamCustomAttribute(customBuilder);
		}

		// Token: 0x06004E79 RID: 20089 RVA: 0x0011B5CD File Offset: 0x001197CD
		public void SetBaseTypeConstraint(Type baseTypeConstraint)
		{
			this.m_type.CheckContext(new Type[] { baseTypeConstraint });
			this.m_type.SetParent(baseTypeConstraint);
		}

		// Token: 0x06004E7A RID: 20090 RVA: 0x0011B5F0 File Offset: 0x001197F0
		[ComVisible(true)]
		public void SetInterfaceConstraints(params Type[] interfaceConstraints)
		{
			this.m_type.CheckContext(interfaceConstraints);
			this.m_type.SetInterfaces(interfaceConstraints);
		}

		// Token: 0x06004E7B RID: 20091 RVA: 0x0011B60A File Offset: 0x0011980A
		public void SetGenericParameterAttributes(GenericParameterAttributes genericParameterAttributes)
		{
			this.m_type.SetGenParamAttributes(genericParameterAttributes);
		}

		// Token: 0x040021D8 RID: 8664
		internal TypeBuilder m_type;
	}
}
