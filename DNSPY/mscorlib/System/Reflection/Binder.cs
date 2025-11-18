using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005D1 RID: 1489
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	[Serializable]
	public abstract class Binder
	{
		// Token: 0x060044E6 RID: 17638
		public abstract MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] names, out object state);

		// Token: 0x060044E7 RID: 17639
		public abstract FieldInfo BindToField(BindingFlags bindingAttr, FieldInfo[] match, object value, CultureInfo culture);

		// Token: 0x060044E8 RID: 17640
		public abstract MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060044E9 RID: 17641
		public abstract PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type returnType, Type[] indexes, ParameterModifier[] modifiers);

		// Token: 0x060044EA RID: 17642
		public abstract object ChangeType(object value, Type type, CultureInfo culture);

		// Token: 0x060044EB RID: 17643
		public abstract void ReorderArgumentArray(ref object[] args, object state);

		// Token: 0x060044EC RID: 17644 RVA: 0x000FD0FC File Offset: 0x000FB2FC
		protected Binder()
		{
		}
	}
}
