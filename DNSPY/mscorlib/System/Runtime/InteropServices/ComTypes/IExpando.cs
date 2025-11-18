using System;
using System.Reflection;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A31 RID: 2609
	[Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface IExpando : IReflect
	{
		// Token: 0x06006639 RID: 26169
		FieldInfo AddField(string name);

		// Token: 0x0600663A RID: 26170
		PropertyInfo AddProperty(string name);

		// Token: 0x0600663B RID: 26171
		MethodInfo AddMethod(string name, Delegate method);

		// Token: 0x0600663C RID: 26172
		void RemoveMember(MemberInfo m);
	}
}
