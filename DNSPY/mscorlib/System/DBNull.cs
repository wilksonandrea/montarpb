using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x020000D2 RID: 210
	[ComVisible(true)]
	[Serializable]
	public sealed class DBNull : ISerializable, IConvertible
	{
		// Token: 0x06000CFB RID: 3323 RVA: 0x00027B5C File Offset: 0x00025D5C
		private DBNull()
		{
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00027B64 File Offset: 0x00025D64
		private DBNull(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DBNullSerial"));
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00027B7B File Offset: 0x00025D7B
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			UnitySerializationHolder.GetUnitySerializationInfo(info, 2, null, null);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00027B86 File Offset: 0x00025D86
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00027B8D File Offset: 0x00025D8D
		public string ToString(IFormatProvider provider)
		{
			return string.Empty;
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00027B94 File Offset: 0x00025D94
		public TypeCode GetTypeCode()
		{
			return TypeCode.DBNull;
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00027B97 File Offset: 0x00025D97
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00027BA8 File Offset: 0x00025DA8
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00027BB9 File Offset: 0x00025DB9
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00027BCA File Offset: 0x00025DCA
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00027BDB File Offset: 0x00025DDB
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00027BEC File Offset: 0x00025DEC
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00027BFD File Offset: 0x00025DFD
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00027C0E File Offset: 0x00025E0E
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00027C1F File Offset: 0x00025E1F
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00027C30 File Offset: 0x00025E30
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00027C41 File Offset: 0x00025E41
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00027C52 File Offset: 0x00025E52
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00027C63 File Offset: 0x00025E63
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00027C74 File Offset: 0x00025E74
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00027C85 File Offset: 0x00025E85
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00027C8F File Offset: 0x00025E8F
		// Note: this type is marked as 'beforefieldinit'.
		static DBNull()
		{
		}

		// Token: 0x0400054A RID: 1354
		public static readonly DBNull Value = new DBNull();
	}
}
