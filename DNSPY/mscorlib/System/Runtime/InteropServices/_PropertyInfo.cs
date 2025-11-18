using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200090B RID: 2315
	[Guid("F59ED4E4-E68F-3218-BD77-061AA82824BF")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(PropertyInfo))]
	[ComVisible(true)]
	public interface _PropertyInfo
	{
		// Token: 0x06005FA5 RID: 24485
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06005FA6 RID: 24486
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06005FA7 RID: 24487
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06005FA8 RID: 24488
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		// Token: 0x06005FA9 RID: 24489
		string ToString();

		// Token: 0x06005FAA RID: 24490
		bool Equals(object other);

		// Token: 0x06005FAB RID: 24491
		int GetHashCode();

		// Token: 0x06005FAC RID: 24492
		Type GetType();

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x06005FAD RID: 24493
		MemberTypes MemberType { get; }

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x06005FAE RID: 24494
		string Name { get; }

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x06005FAF RID: 24495
		Type DeclaringType { get; }

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x06005FB0 RID: 24496
		Type ReflectedType { get; }

		// Token: 0x06005FB1 RID: 24497
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x06005FB2 RID: 24498
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06005FB3 RID: 24499
		bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x06005FB4 RID: 24500
		Type PropertyType { get; }

		// Token: 0x06005FB5 RID: 24501
		object GetValue(object obj, object[] index);

		// Token: 0x06005FB6 RID: 24502
		object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		// Token: 0x06005FB7 RID: 24503
		void SetValue(object obj, object value, object[] index);

		// Token: 0x06005FB8 RID: 24504
		void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		// Token: 0x06005FB9 RID: 24505
		MethodInfo[] GetAccessors(bool nonPublic);

		// Token: 0x06005FBA RID: 24506
		MethodInfo GetGetMethod(bool nonPublic);

		// Token: 0x06005FBB RID: 24507
		MethodInfo GetSetMethod(bool nonPublic);

		// Token: 0x06005FBC RID: 24508
		ParameterInfo[] GetIndexParameters();

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x06005FBD RID: 24509
		PropertyAttributes Attributes { get; }

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x06005FBE RID: 24510
		bool CanRead { get; }

		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x06005FBF RID: 24511
		bool CanWrite { get; }

		// Token: 0x06005FC0 RID: 24512
		MethodInfo[] GetAccessors();

		// Token: 0x06005FC1 RID: 24513
		MethodInfo GetGetMethod();

		// Token: 0x06005FC2 RID: 24514
		MethodInfo GetSetMethod();

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x06005FC3 RID: 24515
		bool IsSpecialName { get; }
	}
}
