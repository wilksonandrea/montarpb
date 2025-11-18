using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000735 RID: 1845
	[CLSCompliant(false)]
	[ComVisible(true)]
	public interface IFormatterConverter
	{
		// Token: 0x060051B5 RID: 20917
		object Convert(object value, Type type);

		// Token: 0x060051B6 RID: 20918
		object Convert(object value, TypeCode typeCode);

		// Token: 0x060051B7 RID: 20919
		bool ToBoolean(object value);

		// Token: 0x060051B8 RID: 20920
		char ToChar(object value);

		// Token: 0x060051B9 RID: 20921
		sbyte ToSByte(object value);

		// Token: 0x060051BA RID: 20922
		byte ToByte(object value);

		// Token: 0x060051BB RID: 20923
		short ToInt16(object value);

		// Token: 0x060051BC RID: 20924
		ushort ToUInt16(object value);

		// Token: 0x060051BD RID: 20925
		int ToInt32(object value);

		// Token: 0x060051BE RID: 20926
		uint ToUInt32(object value);

		// Token: 0x060051BF RID: 20927
		long ToInt64(object value);

		// Token: 0x060051C0 RID: 20928
		ulong ToUInt64(object value);

		// Token: 0x060051C1 RID: 20929
		float ToSingle(object value);

		// Token: 0x060051C2 RID: 20930
		double ToDouble(object value);

		// Token: 0x060051C3 RID: 20931
		decimal ToDecimal(object value);

		// Token: 0x060051C4 RID: 20932
		DateTime ToDateTime(object value);

		// Token: 0x060051C5 RID: 20933
		string ToString(object value);
	}
}
