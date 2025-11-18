using System;
using System.Reflection;

namespace System.Runtime.InteropServices.Expando
{
	// Token: 0x02000A24 RID: 2596
	[Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
	[ComVisible(true)]
	public interface IExpando : IReflect
	{
		// Token: 0x0600660C RID: 26124
		FieldInfo AddField(string name);

		// Token: 0x0600660D RID: 26125
		PropertyInfo AddProperty(string name);

		// Token: 0x0600660E RID: 26126
		MethodInfo AddMethod(string name, Delegate method);

		// Token: 0x0600660F RID: 26127
		void RemoveMember(MemberInfo m);
	}
}
