using System;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	// Token: 0x0200013F RID: 319
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct Single : IComparable, IFormattable, IConvertible, IComparable<float>, IEquatable<float>
	{
		// Token: 0x06001315 RID: 4885 RVA: 0x00038510 File Offset: 0x00036710
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public unsafe static bool IsInfinity(float f)
		{
			return (*(int*)(&f) & int.MaxValue) == 2139095040;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00038523 File Offset: 0x00036723
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public unsafe static bool IsPositiveInfinity(float f)
		{
			return *(int*)(&f) == 2139095040;
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00038530 File Offset: 0x00036730
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public unsafe static bool IsNegativeInfinity(float f)
		{
			return *(int*)(&f) == -8388608;
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0003853D File Offset: 0x0003673D
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public unsafe static bool IsNaN(float f)
		{
			return (*(int*)(&f) & int.MaxValue) > 2139095040;
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00038550 File Offset: 0x00036750
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is float))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeSingle"));
			}
			float num = (float)value;
			if (this < num)
			{
				return -1;
			}
			if (this > num)
			{
				return 1;
			}
			if (this == num)
			{
				return 0;
			}
			if (!float.IsNaN(this))
			{
				return 1;
			}
			if (!float.IsNaN(num))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x000385AC File Offset: 0x000367AC
		[__DynamicallyInvokable]
		public int CompareTo(float value)
		{
			if (this < value)
			{
				return -1;
			}
			if (this > value)
			{
				return 1;
			}
			if (this == value)
			{
				return 0;
			}
			if (!float.IsNaN(this))
			{
				return 1;
			}
			if (!float.IsNaN(value))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x000385D9 File Offset: 0x000367D9
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator ==(float left, float right)
		{
			return left == right;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x000385DF File Offset: 0x000367DF
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator !=(float left, float right)
		{
			return left != right;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x000385E8 File Offset: 0x000367E8
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator <(float left, float right)
		{
			return left < right;
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x000385EE File Offset: 0x000367EE
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator >(float left, float right)
		{
			return left > right;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x000385F4 File Offset: 0x000367F4
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator <=(float left, float right)
		{
			return left <= right;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x000385FD File Offset: 0x000367FD
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator >=(float left, float right)
		{
			return left >= right;
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00038608 File Offset: 0x00036808
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			if (!(obj is float))
			{
				return false;
			}
			float num = (float)obj;
			return num == this || (float.IsNaN(num) && float.IsNaN(this));
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0003863E File Offset: 0x0003683E
		[__DynamicallyInvokable]
		public bool Equals(float obj)
		{
			return obj == this || (float.IsNaN(obj) && float.IsNaN(this));
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00038658 File Offset: 0x00036858
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetHashCode()
		{
			float num = this;
			if (num == 0f)
			{
				return 0;
			}
			return *(int*)(&num);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00038678 File Offset: 0x00036878
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Number.FormatSingle(this, null, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00038687 File Offset: 0x00036887
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatSingle(this, null, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00038697 File Offset: 0x00036897
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return Number.FormatSingle(this, format, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x000386A6 File Offset: 0x000368A6
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatSingle(this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x000386B6 File Offset: 0x000368B6
		[__DynamicallyInvokable]
		public static float Parse(string s)
		{
			return float.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x000386C8 File Offset: 0x000368C8
		[__DynamicallyInvokable]
		public static float Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return float.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x000386DC File Offset: 0x000368DC
		[__DynamicallyInvokable]
		public static float Parse(string s, IFormatProvider provider)
		{
			return float.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x000386EF File Offset: 0x000368EF
		[__DynamicallyInvokable]
		public static float Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return float.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x00038704 File Offset: 0x00036904
		private static float Parse(string s, NumberStyles style, NumberFormatInfo info)
		{
			return Number.ParseSingle(s, style, info);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0003870E File Offset: 0x0003690E
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out float result)
		{
			return float.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x00038721 File Offset: 0x00036921
		[__DynamicallyInvokable]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out float result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return float.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00038738 File Offset: 0x00036938
		private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out float result)
		{
			if (s == null)
			{
				result = 0f;
				return false;
			}
			if (!Number.TryParseSingle(s, style, info, out result))
			{
				string text = s.Trim();
				if (text.Equals(info.PositiveInfinitySymbol))
				{
					result = float.PositiveInfinity;
				}
				else if (text.Equals(info.NegativeInfinitySymbol))
				{
					result = float.NegativeInfinity;
				}
				else
				{
					if (!text.Equals(info.NaNSymbol))
					{
						return false;
					}
					result = float.NaN;
				}
			}
			return true;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x000387AD File Offset: 0x000369AD
		public TypeCode GetTypeCode()
		{
			return TypeCode.Single;
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x000387B1 File Offset: 0x000369B1
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x000387BA File Offset: 0x000369BA
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Single", "Char" }));
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x000387E1 File Offset: 0x000369E1
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x000387EA File Offset: 0x000369EA
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x000387F3 File Offset: 0x000369F3
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x000387FC File Offset: 0x000369FC
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00038805 File Offset: 0x00036A05
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0003880E File Offset: 0x00036A0E
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00038817 File Offset: 0x00036A17
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x00038820 File Offset: 0x00036A20
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00038829 File Offset: 0x00036A29
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0003882D File Offset: 0x00036A2D
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00038836 File Offset: 0x00036A36
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0003883F File Offset: 0x00036A3F
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Single", "DateTime" }));
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00038866 File Offset: 0x00036A66
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x04000688 RID: 1672
		internal float m_value;

		// Token: 0x04000689 RID: 1673
		[__DynamicallyInvokable]
		public const float MinValue = -3.40282347E+38f;

		// Token: 0x0400068A RID: 1674
		[__DynamicallyInvokable]
		public const float Epsilon = 1.401298E-45f;

		// Token: 0x0400068B RID: 1675
		[__DynamicallyInvokable]
		public const float MaxValue = 3.40282347E+38f;

		// Token: 0x0400068C RID: 1676
		[__DynamicallyInvokable]
		public const float PositiveInfinity = float.PositiveInfinity;

		// Token: 0x0400068D RID: 1677
		[__DynamicallyInvokable]
		public const float NegativeInfinity = float.NegativeInfinity;

		// Token: 0x0400068E RID: 1678
		[__DynamicallyInvokable]
		public const float NaN = float.NaN;
	}
}
