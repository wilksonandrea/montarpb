using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A35 RID: 2613
	[Guid("AFBF15E5-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface IReflect
	{
		// Token: 0x06006657 RID: 26199
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06006658 RID: 26200
		MethodInfo GetMethod(string name, BindingFlags bindingAttr);

		// Token: 0x06006659 RID: 26201
		MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x0600665A RID: 26202
		FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x0600665B RID: 26203
		FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x0600665C RID: 26204
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

		// Token: 0x0600665D RID: 26205
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x0600665E RID: 26206
		PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x0600665F RID: 26207
		MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

		// Token: 0x06006660 RID: 26208
		MemberInfo[] GetMembers(BindingFlags bindingAttr);

		// Token: 0x06006661 RID: 26209
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x06006662 RID: 26210
		Type UnderlyingSystemType { get; }
	}
}
