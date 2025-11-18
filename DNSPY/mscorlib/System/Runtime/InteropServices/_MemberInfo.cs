using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000906 RID: 2310
	[Guid("f7102fa9-cabb-3a74-a6da-b4567ef1b079")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[TypeLibImportClass(typeof(MemberInfo))]
	[CLSCompliant(false)]
	[ComVisible(true)]
	public interface _MemberInfo
	{
		// Token: 0x06005F05 RID: 24325
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06005F06 RID: 24326
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06005F07 RID: 24327
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06005F08 RID: 24328
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		// Token: 0x06005F09 RID: 24329
		string ToString();

		// Token: 0x06005F0A RID: 24330
		bool Equals(object other);

		// Token: 0x06005F0B RID: 24331
		int GetHashCode();

		// Token: 0x06005F0C RID: 24332
		Type GetType();

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x06005F0D RID: 24333
		MemberTypes MemberType { get; }

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x06005F0E RID: 24334
		string Name { get; }

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x06005F0F RID: 24335
		Type DeclaringType { get; }

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x06005F10 RID: 24336
		Type ReflectedType { get; }

		// Token: 0x06005F11 RID: 24337
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x06005F12 RID: 24338
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06005F13 RID: 24339
		bool IsDefined(Type attributeType, bool inherit);
	}
}
