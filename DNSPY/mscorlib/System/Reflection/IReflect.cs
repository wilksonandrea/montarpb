using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005F2 RID: 1522
	[Guid("AFBF15E5-C37C-11d2-B88E-00A0C9B471B8")]
	[ComVisible(true)]
	public interface IReflect
	{
		// Token: 0x0600466B RID: 18027
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x0600466C RID: 18028
		MethodInfo GetMethod(string name, BindingFlags bindingAttr);

		// Token: 0x0600466D RID: 18029
		MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x0600466E RID: 18030
		FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x0600466F RID: 18031
		FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x06004670 RID: 18032
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

		// Token: 0x06004671 RID: 18033
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06004672 RID: 18034
		PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x06004673 RID: 18035
		MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

		// Token: 0x06004674 RID: 18036
		MemberInfo[] GetMembers(BindingFlags bindingAttr);

		// Token: 0x06004675 RID: 18037
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06004676 RID: 18038
		Type UnderlyingSystemType { get; }
	}
}
