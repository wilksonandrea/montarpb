using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000627 RID: 1575
	[ComVisible(true)]
	[Serializable]
	public class TypeDelegator : TypeInfo
	{
		// Token: 0x060048C6 RID: 18630 RVA: 0x001077FB File Offset: 0x001059FB
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x060048C7 RID: 18631 RVA: 0x00107814 File Offset: 0x00105A14
		protected TypeDelegator()
		{
		}

		// Token: 0x060048C8 RID: 18632 RVA: 0x0010781C File Offset: 0x00105A1C
		public TypeDelegator(Type delegatingType)
		{
			if (delegatingType == null)
			{
				throw new ArgumentNullException("delegatingType");
			}
			this.typeImpl = delegatingType;
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x060048C9 RID: 18633 RVA: 0x0010783F File Offset: 0x00105A3F
		public override Guid GUID
		{
			get
			{
				return this.typeImpl.GUID;
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x060048CA RID: 18634 RVA: 0x0010784C File Offset: 0x00105A4C
		public override int MetadataToken
		{
			get
			{
				return this.typeImpl.MetadataToken;
			}
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x0010785C File Offset: 0x00105A5C
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			return this.typeImpl.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x060048CC RID: 18636 RVA: 0x00107881 File Offset: 0x00105A81
		public override Module Module
		{
			get
			{
				return this.typeImpl.Module;
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x060048CD RID: 18637 RVA: 0x0010788E File Offset: 0x00105A8E
		public override Assembly Assembly
		{
			get
			{
				return this.typeImpl.Assembly;
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x060048CE RID: 18638 RVA: 0x0010789B File Offset: 0x00105A9B
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return this.typeImpl.TypeHandle;
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x060048CF RID: 18639 RVA: 0x001078A8 File Offset: 0x00105AA8
		public override string Name
		{
			get
			{
				return this.typeImpl.Name;
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x060048D0 RID: 18640 RVA: 0x001078B5 File Offset: 0x00105AB5
		public override string FullName
		{
			get
			{
				return this.typeImpl.FullName;
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x060048D1 RID: 18641 RVA: 0x001078C2 File Offset: 0x00105AC2
		public override string Namespace
		{
			get
			{
				return this.typeImpl.Namespace;
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x060048D2 RID: 18642 RVA: 0x001078CF File Offset: 0x00105ACF
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.typeImpl.AssemblyQualifiedName;
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x060048D3 RID: 18643 RVA: 0x001078DC File Offset: 0x00105ADC
		public override Type BaseType
		{
			get
			{
				return this.typeImpl.BaseType;
			}
		}

		// Token: 0x060048D4 RID: 18644 RVA: 0x001078E9 File Offset: 0x00105AE9
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.typeImpl.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x060048D5 RID: 18645 RVA: 0x001078FD File Offset: 0x00105AFD
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetConstructors(bindingAttr);
		}

		// Token: 0x060048D6 RID: 18646 RVA: 0x0010790B File Offset: 0x00105B0B
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				return this.typeImpl.GetMethod(name, bindingAttr);
			}
			return this.typeImpl.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x060048D7 RID: 18647 RVA: 0x00107933 File Offset: 0x00105B33
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMethods(bindingAttr);
		}

		// Token: 0x060048D8 RID: 18648 RVA: 0x00107941 File Offset: 0x00105B41
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetField(name, bindingAttr);
		}

		// Token: 0x060048D9 RID: 18649 RVA: 0x00107950 File Offset: 0x00105B50
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetFields(bindingAttr);
		}

		// Token: 0x060048DA RID: 18650 RVA: 0x0010795E File Offset: 0x00105B5E
		public override Type GetInterface(string name, bool ignoreCase)
		{
			return this.typeImpl.GetInterface(name, ignoreCase);
		}

		// Token: 0x060048DB RID: 18651 RVA: 0x0010796D File Offset: 0x00105B6D
		public override Type[] GetInterfaces()
		{
			return this.typeImpl.GetInterfaces();
		}

		// Token: 0x060048DC RID: 18652 RVA: 0x0010797A File Offset: 0x00105B7A
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetEvent(name, bindingAttr);
		}

		// Token: 0x060048DD RID: 18653 RVA: 0x00107989 File Offset: 0x00105B89
		public override EventInfo[] GetEvents()
		{
			return this.typeImpl.GetEvents();
		}

		// Token: 0x060048DE RID: 18654 RVA: 0x00107996 File Offset: 0x00105B96
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			if (returnType == null && types == null)
			{
				return this.typeImpl.GetProperty(name, bindingAttr);
			}
			return this.typeImpl.GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
		}

		// Token: 0x060048DF RID: 18655 RVA: 0x001079C8 File Offset: 0x00105BC8
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetProperties(bindingAttr);
		}

		// Token: 0x060048E0 RID: 18656 RVA: 0x001079D6 File Offset: 0x00105BD6
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetEvents(bindingAttr);
		}

		// Token: 0x060048E1 RID: 18657 RVA: 0x001079E4 File Offset: 0x00105BE4
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetNestedTypes(bindingAttr);
		}

		// Token: 0x060048E2 RID: 18658 RVA: 0x001079F2 File Offset: 0x00105BF2
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetNestedType(name, bindingAttr);
		}

		// Token: 0x060048E3 RID: 18659 RVA: 0x00107A01 File Offset: 0x00105C01
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMember(name, type, bindingAttr);
		}

		// Token: 0x060048E4 RID: 18660 RVA: 0x00107A11 File Offset: 0x00105C11
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMembers(bindingAttr);
		}

		// Token: 0x060048E5 RID: 18661 RVA: 0x00107A1F File Offset: 0x00105C1F
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.typeImpl.Attributes;
		}

		// Token: 0x060048E6 RID: 18662 RVA: 0x00107A2C File Offset: 0x00105C2C
		protected override bool IsArrayImpl()
		{
			return this.typeImpl.IsArray;
		}

		// Token: 0x060048E7 RID: 18663 RVA: 0x00107A39 File Offset: 0x00105C39
		protected override bool IsPrimitiveImpl()
		{
			return this.typeImpl.IsPrimitive;
		}

		// Token: 0x060048E8 RID: 18664 RVA: 0x00107A46 File Offset: 0x00105C46
		protected override bool IsByRefImpl()
		{
			return this.typeImpl.IsByRef;
		}

		// Token: 0x060048E9 RID: 18665 RVA: 0x00107A53 File Offset: 0x00105C53
		protected override bool IsPointerImpl()
		{
			return this.typeImpl.IsPointer;
		}

		// Token: 0x060048EA RID: 18666 RVA: 0x00107A60 File Offset: 0x00105C60
		protected override bool IsValueTypeImpl()
		{
			return this.typeImpl.IsValueType;
		}

		// Token: 0x060048EB RID: 18667 RVA: 0x00107A6D File Offset: 0x00105C6D
		protected override bool IsCOMObjectImpl()
		{
			return this.typeImpl.IsCOMObject;
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x060048EC RID: 18668 RVA: 0x00107A7A File Offset: 0x00105C7A
		public override bool IsConstructedGenericType
		{
			get
			{
				return this.typeImpl.IsConstructedGenericType;
			}
		}

		// Token: 0x060048ED RID: 18669 RVA: 0x00107A87 File Offset: 0x00105C87
		public override Type GetElementType()
		{
			return this.typeImpl.GetElementType();
		}

		// Token: 0x060048EE RID: 18670 RVA: 0x00107A94 File Offset: 0x00105C94
		protected override bool HasElementTypeImpl()
		{
			return this.typeImpl.HasElementType;
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x060048EF RID: 18671 RVA: 0x00107AA1 File Offset: 0x00105CA1
		public override Type UnderlyingSystemType
		{
			get
			{
				return this.typeImpl.UnderlyingSystemType;
			}
		}

		// Token: 0x060048F0 RID: 18672 RVA: 0x00107AAE File Offset: 0x00105CAE
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.typeImpl.GetCustomAttributes(inherit);
		}

		// Token: 0x060048F1 RID: 18673 RVA: 0x00107ABC File Offset: 0x00105CBC
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.typeImpl.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x060048F2 RID: 18674 RVA: 0x00107ACB File Offset: 0x00105CCB
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.typeImpl.IsDefined(attributeType, inherit);
		}

		// Token: 0x060048F3 RID: 18675 RVA: 0x00107ADA File Offset: 0x00105CDA
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			return this.typeImpl.GetInterfaceMap(interfaceType);
		}

		// Token: 0x04001E49 RID: 7753
		protected Type typeImpl;
	}
}
