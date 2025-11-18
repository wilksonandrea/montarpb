using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000988 RID: 2440
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IExpando instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface UCOMIExpando : UCOMIReflect
	{
		// Token: 0x060062A6 RID: 25254
		FieldInfo AddField(string name);

		// Token: 0x060062A7 RID: 25255
		PropertyInfo AddProperty(string name);

		// Token: 0x060062A8 RID: 25256
		MethodInfo AddMethod(string name, Delegate method);

		// Token: 0x060062A9 RID: 25257
		void RemoveMember(MemberInfo m);
	}
}
