using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200098C RID: 2444
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IReflect instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("AFBF15E5-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface UCOMIReflect
	{
		// Token: 0x060062C4 RID: 25284
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060062C5 RID: 25285
		MethodInfo GetMethod(string name, BindingFlags bindingAttr);

		// Token: 0x060062C6 RID: 25286
		MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x060062C7 RID: 25287
		FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x060062C8 RID: 25288
		FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x060062C9 RID: 25289
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

		// Token: 0x060062CA RID: 25290
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060062CB RID: 25291
		PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x060062CC RID: 25292
		MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

		// Token: 0x060062CD RID: 25293
		MemberInfo[] GetMembers(BindingFlags bindingAttr);

		// Token: 0x060062CE RID: 25294
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x060062CF RID: 25295
		Type UnderlyingSystemType { get; }
	}
}
