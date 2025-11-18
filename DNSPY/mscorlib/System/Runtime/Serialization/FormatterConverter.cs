using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000730 RID: 1840
	[ComVisible(true)]
	public class FormatterConverter : IFormatterConverter
	{
		// Token: 0x0600517E RID: 20862 RVA: 0x0011EF8E File Offset: 0x0011D18E
		public FormatterConverter()
		{
		}

		// Token: 0x0600517F RID: 20863 RVA: 0x0011EF96 File Offset: 0x0011D196
		public object Convert(object value, Type type)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
		}

		// Token: 0x06005180 RID: 20864 RVA: 0x0011EFB2 File Offset: 0x0011D1B2
		public object Convert(object value, TypeCode typeCode)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ChangeType(value, typeCode, CultureInfo.InvariantCulture);
		}

		// Token: 0x06005181 RID: 20865 RVA: 0x0011EFCE File Offset: 0x0011D1CE
		public bool ToBoolean(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToBoolean(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06005182 RID: 20866 RVA: 0x0011EFE9 File Offset: 0x0011D1E9
		public char ToChar(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToChar(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06005183 RID: 20867 RVA: 0x0011F004 File Offset: 0x0011D204
		[CLSCompliant(false)]
		public sbyte ToSByte(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToSByte(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06005184 RID: 20868 RVA: 0x0011F01F File Offset: 0x0011D21F
		public byte ToByte(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToByte(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06005185 RID: 20869 RVA: 0x0011F03A File Offset: 0x0011D23A
		public short ToInt16(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToInt16(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06005186 RID: 20870 RVA: 0x0011F055 File Offset: 0x0011D255
		[CLSCompliant(false)]
		public ushort ToUInt16(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToUInt16(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06005187 RID: 20871 RVA: 0x0011F070 File Offset: 0x0011D270
		public int ToInt32(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToInt32(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06005188 RID: 20872 RVA: 0x0011F08B File Offset: 0x0011D28B
		[CLSCompliant(false)]
		public uint ToUInt32(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToUInt32(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06005189 RID: 20873 RVA: 0x0011F0A6 File Offset: 0x0011D2A6
		public long ToInt64(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToInt64(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x0011F0C1 File Offset: 0x0011D2C1
		[CLSCompliant(false)]
		public ulong ToUInt64(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToUInt64(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600518B RID: 20875 RVA: 0x0011F0DC File Offset: 0x0011D2DC
		public float ToSingle(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToSingle(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600518C RID: 20876 RVA: 0x0011F0F7 File Offset: 0x0011D2F7
		public double ToDouble(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToDouble(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600518D RID: 20877 RVA: 0x0011F112 File Offset: 0x0011D312
		public decimal ToDecimal(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToDecimal(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600518E RID: 20878 RVA: 0x0011F12D File Offset: 0x0011D32D
		public DateTime ToDateTime(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToDateTime(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600518F RID: 20879 RVA: 0x0011F148 File Offset: 0x0011D348
		public string ToString(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToString(value, CultureInfo.InvariantCulture);
		}
	}
}
