using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace System
{
	// Token: 0x02000073 RID: 115
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class String : IComparable, ICloneable, IConvertible, IEnumerable, IComparable<string>, IEnumerable<char>, IEquatable<string>
	{
		// Token: 0x060004B4 RID: 1204 RVA: 0x0001090D File Offset: 0x0000EB0D
		[__DynamicallyInvokable]
		public static string Join(string separator, params string[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return string.Join(separator, value, 0, value.Length);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00010928 File Offset: 0x0000EB28
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public static string Join(string separator, params object[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length == 0 || values[0] == null)
			{
				return string.Empty;
			}
			if (separator == null)
			{
				separator = string.Empty;
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			string text = values[0].ToString();
			if (text != null)
			{
				stringBuilder.Append(text);
			}
			for (int i = 1; i < values.Length; i++)
			{
				stringBuilder.Append(separator);
				if (values[i] != null)
				{
					text = values[i].ToString();
					if (text != null)
					{
						stringBuilder.Append(text);
					}
				}
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000109B0 File Offset: 0x0000EBB0
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public static string Join<T>(string separator, IEnumerable<T> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (separator == null)
			{
				separator = string.Empty;
			}
			string text;
			using (IEnumerator<T> enumerator = values.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					text = string.Empty;
				}
				else
				{
					StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
					if (enumerator.Current != null)
					{
						T t = enumerator.Current;
						string text2 = t.ToString();
						if (text2 != null)
						{
							stringBuilder.Append(text2);
						}
					}
					while (enumerator.MoveNext())
					{
						stringBuilder.Append(separator);
						if (enumerator.Current != null)
						{
							T t = enumerator.Current;
							string text3 = t.ToString();
							if (text3 != null)
							{
								stringBuilder.Append(text3);
							}
						}
					}
					text = StringBuilderCache.GetStringAndRelease(stringBuilder);
				}
			}
			return text;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00010A8C File Offset: 0x0000EC8C
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public static string Join(string separator, IEnumerable<string> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (separator == null)
			{
				separator = string.Empty;
			}
			string text;
			using (IEnumerator<string> enumerator = values.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					text = string.Empty;
				}
				else
				{
					StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
					if (enumerator.Current != null)
					{
						stringBuilder.Append(enumerator.Current);
					}
					while (enumerator.MoveNext())
					{
						stringBuilder.Append(separator);
						if (enumerator.Current != null)
						{
							stringBuilder.Append(enumerator.Current);
						}
					}
					text = StringBuilderCache.GetStringAndRelease(stringBuilder);
				}
			}
			return text;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00010B30 File Offset: 0x0000ED30
		internal char FirstChar
		{
			get
			{
				return this.m_firstChar;
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00010B38 File Offset: 0x0000ED38
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe static string Join(string separator, string[] value, int startIndex, int count)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
			}
			if (startIndex > value.Length - count)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (separator == null)
			{
				separator = string.Empty;
			}
			if (count == 0)
			{
				return string.Empty;
			}
			int num = 0;
			int num2 = startIndex + count - 1;
			for (int i = startIndex; i <= num2; i++)
			{
				if (value[i] != null)
				{
					num += value[i].Length;
				}
			}
			num += (count - 1) * separator.Length;
			if (num < 0 || num + 1 < 0)
			{
				throw new OutOfMemoryException();
			}
			if (num == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(num);
			fixed (char* ptr = &text.m_firstChar)
			{
				char* ptr2 = ptr;
				UnSafeCharBuffer unSafeCharBuffer = new UnSafeCharBuffer(ptr2, num);
				unSafeCharBuffer.AppendString(value[startIndex]);
				for (int j = startIndex + 1; j <= num2; j++)
				{
					unSafeCharBuffer.AppendString(separator);
					unSafeCharBuffer.AppendString(value[j]);
				}
			}
			return text;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00010C54 File Offset: 0x0000EE54
		[SecuritySafeCritical]
		private unsafe static int CompareOrdinalIgnoreCaseHelper(string strA, string strB)
		{
			int num = Math.Min(strA.Length, strB.Length);
			fixed (char* ptr = &strA.m_firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &strB.m_firstChar)
				{
					char* ptr4 = ptr3;
					char* ptr5 = ptr2;
					char* ptr6 = ptr4;
					while (num != 0)
					{
						int num2 = (int)(*ptr5);
						int num3 = (int)(*ptr6);
						if (num2 - 97 <= 25)
						{
							num2 -= 32;
						}
						if (num3 - 97 <= 25)
						{
							num3 -= 32;
						}
						if (num2 != num3)
						{
							return num2 - num3;
						}
						ptr5++;
						ptr6++;
						num--;
					}
					return strA.Length - strB.Length;
				}
			}
		}

		// Token: 0x060004BB RID: 1211
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int nativeCompareOrdinalEx(string strA, int indexA, string strB, int indexB, int count);

		// Token: 0x060004BC RID: 1212
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern int nativeCompareOrdinalIgnoreCaseWC(string strA, sbyte* strBBytes);

		// Token: 0x060004BD RID: 1213 RVA: 0x00010CE8 File Offset: 0x0000EEE8
		[SecuritySafeCritical]
		internal unsafe static string SmallCharToUpper(string strIn)
		{
			int length = strIn.Length;
			string text = string.FastAllocateString(length);
			fixed (char* ptr = &strIn.m_firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &text.m_firstChar)
				{
					char* ptr4 = ptr3;
					for (int i = 0; i < length; i++)
					{
						int num = (int)ptr2[i];
						if (num - 97 <= 25)
						{
							num -= 32;
						}
						ptr4[i] = (char)num;
					}
					ptr = null;
				}
				return text;
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00010D5C File Offset: 0x0000EF5C
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private unsafe static bool EqualsHelper(string strA, string strB)
		{
			int i = strA.Length;
			fixed (char* ptr = &strA.m_firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &strB.m_firstChar)
				{
					char* ptr4 = ptr3;
					char* ptr5 = ptr2;
					char* ptr6 = ptr4;
					while (i >= 10)
					{
						if (*(int*)ptr5 != *(int*)ptr6)
						{
							return false;
						}
						if (*(int*)(ptr5 + 2) != *(int*)(ptr6 + 2))
						{
							return false;
						}
						if (*(int*)(ptr5 + 4) != *(int*)(ptr6 + 4))
						{
							return false;
						}
						if (*(int*)(ptr5 + 6) != *(int*)(ptr6 + 6))
						{
							return false;
						}
						if (*(int*)(ptr5 + 8) != *(int*)(ptr6 + 8))
						{
							return false;
						}
						ptr5 += 10;
						ptr6 += 10;
						i -= 10;
					}
					while (i > 0 && *(int*)ptr5 == *(int*)ptr6)
					{
						ptr5 += 2;
						ptr6 += 2;
						i -= 2;
					}
					return i <= 0;
				}
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00010E34 File Offset: 0x0000F034
		[SecuritySafeCritical]
		private unsafe static bool EqualsIgnoreCaseAsciiHelper(string strA, string strB)
		{
			int num = strA.Length;
			fixed (char* ptr = &strA.m_firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &strB.m_firstChar)
				{
					char* ptr4 = ptr3;
					char* ptr5 = ptr2;
					char* ptr6 = ptr4;
					while (num != 0)
					{
						int num2 = (int)(*ptr5);
						int num3 = (int)(*ptr6);
						if (num2 != num3 && ((num2 | 32) != (num3 | 32) || (num2 | 32) - 97 > 25))
						{
							return false;
						}
						ptr5++;
						ptr6++;
						num--;
					}
					return true;
				}
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00010EA4 File Offset: 0x0000F0A4
		[SecuritySafeCritical]
		private unsafe static int CompareOrdinalHelper(string strA, string strB)
		{
			int i = Math.Min(strA.Length, strB.Length);
			int num = -1;
			fixed (char* ptr = &strA.m_firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &strB.m_firstChar)
				{
					char* ptr4 = ptr3;
					char* ptr5 = ptr2;
					char* ptr6 = ptr4;
					while (i >= 10)
					{
						if (*(int*)ptr5 != *(int*)ptr6)
						{
							num = 0;
							break;
						}
						if (*(int*)(ptr5 + 2) != *(int*)(ptr6 + 2))
						{
							num = 2;
							break;
						}
						if (*(int*)(ptr5 + 4) != *(int*)(ptr6 + 4))
						{
							num = 4;
							break;
						}
						if (*(int*)(ptr5 + 6) != *(int*)(ptr6 + 6))
						{
							num = 6;
							break;
						}
						if (*(int*)(ptr5 + 8) != *(int*)(ptr6 + 8))
						{
							num = 8;
							break;
						}
						ptr5 += 10;
						ptr6 += 10;
						i -= 10;
					}
					if (num != -1)
					{
						ptr5 += num;
						ptr6 += num;
						int num2;
						if ((num2 = (int)(*ptr5 - *ptr6)) != 0)
						{
							return num2;
						}
						return (int)(ptr5[1] - ptr6[1]);
					}
					else
					{
						while (i > 0 && *(int*)ptr5 == *(int*)ptr6)
						{
							ptr5 += 2;
							ptr6 += 2;
							i -= 2;
						}
						if (i <= 0)
						{
							return strA.Length - strB.Length;
						}
						int num3;
						if ((num3 = (int)(*ptr5 - *ptr6)) != 0)
						{
							return num3;
						}
						return (int)(ptr5[1] - ptr6[1]);
					}
				}
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00010FEC File Offset: 0x0000F1EC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			if (this == null)
			{
				throw new NullReferenceException();
			}
			string text = obj as string;
			return text != null && (this == obj || (this.Length == text.Length && string.EqualsHelper(this, text)));
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001102B File Offset: 0x0000F22B
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public bool Equals(string value)
		{
			if (this == null)
			{
				throw new NullReferenceException();
			}
			return value != null && (this == value || (this.Length == value.Length && string.EqualsHelper(this, value)));
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00011058 File Offset: 0x0000F258
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public bool Equals(string value, StringComparison comparisonType)
		{
			if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
			{
				throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
			}
			if (this == value)
			{
				return true;
			}
			if (value == null)
			{
				return false;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(this, value, CompareOptions.None) == 0;
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(this, value, CompareOptions.IgnoreCase) == 0;
			case StringComparison.InvariantCulture:
				return CultureInfo.InvariantCulture.CompareInfo.Compare(this, value, CompareOptions.None) == 0;
			case StringComparison.InvariantCultureIgnoreCase:
				return CultureInfo.InvariantCulture.CompareInfo.Compare(this, value, CompareOptions.IgnoreCase) == 0;
			case StringComparison.Ordinal:
				return this.Length == value.Length && string.EqualsHelper(this, value);
			case StringComparison.OrdinalIgnoreCase:
				if (this.Length != value.Length)
				{
					return false;
				}
				if (this.IsAscii() && value.IsAscii())
				{
					return string.EqualsIgnoreCaseAsciiHelper(this, value);
				}
				return TextInfo.CompareOrdinalIgnoreCase(this, value) == 0;
			default:
				throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00011167 File Offset: 0x0000F367
		[__DynamicallyInvokable]
		public static bool Equals(string a, string b)
		{
			return a == b || (a != null && b != null && a.Length == b.Length && string.EqualsHelper(a, b));
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00011190 File Offset: 0x0000F390
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static bool Equals(string a, string b, StringComparison comparisonType)
		{
			if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
			{
				throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
			}
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(a, b, CompareOptions.None) == 0;
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(a, b, CompareOptions.IgnoreCase) == 0;
			case StringComparison.InvariantCulture:
				return CultureInfo.InvariantCulture.CompareInfo.Compare(a, b, CompareOptions.None) == 0;
			case StringComparison.InvariantCultureIgnoreCase:
				return CultureInfo.InvariantCulture.CompareInfo.Compare(a, b, CompareOptions.IgnoreCase) == 0;
			case StringComparison.Ordinal:
				return a.Length == b.Length && string.EqualsHelper(a, b);
			case StringComparison.OrdinalIgnoreCase:
				if (a.Length != b.Length)
				{
					return false;
				}
				if (a.IsAscii() && b.IsAscii())
				{
					return string.EqualsIgnoreCaseAsciiHelper(a, b);
				}
				return TextInfo.CompareOrdinalIgnoreCase(a, b) == 0;
			default:
				throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000112A2 File Offset: 0x0000F4A2
		[__DynamicallyInvokable]
		public static bool operator ==(string a, string b)
		{
			return string.Equals(a, b);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000112AB File Offset: 0x0000F4AB
		[__DynamicallyInvokable]
		public static bool operator !=(string a, string b)
		{
			return !string.Equals(a, b);
		}

		// Token: 0x17000085 RID: 133
		[__DynamicallyInvokable]
		[IndexerName("Chars")]
		public extern char this[int index]
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000112B8 File Offset: 0x0000F4B8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
			}
			if (sourceIndex < 0)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count > this.Length - sourceIndex)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", Environment.GetResourceString("ArgumentOutOfRange_IndexCount"));
			}
			if (destinationIndex > destination.Length - count || destinationIndex < 0)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", Environment.GetResourceString("ArgumentOutOfRange_IndexCount"));
			}
			if (count > 0)
			{
				fixed (char* ptr = &this.m_firstChar)
				{
					char* ptr2 = ptr;
					fixed (char[] array = destination)
					{
						char* ptr3;
						if (destination == null || array.Length == 0)
						{
							ptr3 = null;
						}
						else
						{
							ptr3 = &array[0];
						}
						string.wstrcpy(ptr3 + destinationIndex, ptr2 + sourceIndex, count);
					}
				}
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00011388 File Offset: 0x0000F588
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe char[] ToCharArray()
		{
			int length = this.Length;
			char[] array = new char[length];
			if (length > 0)
			{
				fixed (char* ptr = &this.m_firstChar)
				{
					char* ptr2 = ptr;
					char[] array2;
					char* ptr3;
					if ((array2 = array) == null || array2.Length == 0)
					{
						ptr3 = null;
					}
					else
					{
						ptr3 = &array2[0];
					}
					string.wstrcpy(ptr3, ptr2, length);
					array2 = null;
				}
			}
			return array;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000113E0 File Offset: 0x0000F5E0
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe char[] ToCharArray(int startIndex, int length)
		{
			if (startIndex < 0 || startIndex > this.Length || startIndex > this.Length - length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			char[] array = new char[length];
			if (length > 0)
			{
				fixed (char* ptr = &this.m_firstChar)
				{
					char* ptr2 = ptr;
					char[] array2;
					char* ptr3;
					if ((array2 = array) == null || array2.Length == 0)
					{
						ptr3 = null;
					}
					else
					{
						ptr3 = &array2[0];
					}
					string.wstrcpy(ptr3, ptr2 + startIndex, length);
					array2 = null;
				}
			}
			return array;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00011477 File Offset: 0x0000F677
		[__DynamicallyInvokable]
		public static bool IsNullOrEmpty(string value)
		{
			return value == null || value.Length == 0;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00011488 File Offset: 0x0000F688
		[__DynamicallyInvokable]
		public static bool IsNullOrWhiteSpace(string value)
		{
			if (value == null)
			{
				return true;
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (!char.IsWhiteSpace(value[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004CE RID: 1230
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int InternalMarvin32HashString(string s, int strLen, long additionalEntropy);

		// Token: 0x060004CF RID: 1231 RVA: 0x000114BC File Offset: 0x0000F6BC
		[SecuritySafeCritical]
		internal static bool UseRandomizedHashing()
		{
			return string.InternalUseRandomizedHashing();
		}

		// Token: 0x060004D0 RID: 1232
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern bool InternalUseRandomizedHashing();

		// Token: 0x060004D1 RID: 1233 RVA: 0x000114C4 File Offset: 0x0000F6C4
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public unsafe override int GetHashCode()
		{
			if (HashHelpers.s_UseRandomizedStringHashing)
			{
				return string.InternalMarvin32HashString(this, this.Length, 0L);
			}
			char* ptr = this;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			int num = 352654597;
			int num2 = num;
			int* ptr2 = (int*)ptr;
			int i;
			for (i = this.Length; i > 2; i -= 4)
			{
				num = ((num << 5) + num + (num >> 27)) ^ *ptr2;
				num2 = ((num2 << 5) + num2 + (num2 >> 27)) ^ ptr2[1];
				ptr2 += 2;
			}
			if (i > 0)
			{
				num = ((num << 5) + num + (num >> 27)) ^ *ptr2;
			}
			return num + num2 * 1566083941;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00011560 File Offset: 0x0000F760
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal unsafe int GetLegacyNonRandomizedHashCode()
		{
			char* ptr = this;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			int num = 352654597;
			int num2 = num;
			int* ptr2 = (int*)ptr;
			int i;
			for (i = this.Length; i > 2; i -= 4)
			{
				num = ((num << 5) + num + (num >> 27)) ^ *ptr2;
				num2 = ((num2 << 5) + num2 + (num2 >> 27)) ^ ptr2[1];
				ptr2 += 2;
			}
			if (i > 0)
			{
				num = ((num << 5) + num + (num >> 27)) ^ *ptr2;
			}
			return num + num2 * 1566083941;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060004D3 RID: 1235
		[__DynamicallyInvokable]
		public extern int Length
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000115E3 File Offset: 0x0000F7E3
		[__DynamicallyInvokable]
		public string[] Split(params char[] separator)
		{
			return this.SplitInternal(separator, int.MaxValue, StringSplitOptions.None);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000115F2 File Offset: 0x0000F7F2
		[__DynamicallyInvokable]
		public string[] Split(char[] separator, int count)
		{
			return this.SplitInternal(separator, count, StringSplitOptions.None);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000115FD File Offset: 0x0000F7FD
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public string[] Split(char[] separator, StringSplitOptions options)
		{
			return this.SplitInternal(separator, int.MaxValue, options);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001160C File Offset: 0x0000F80C
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public string[] Split(char[] separator, int count, StringSplitOptions options)
		{
			return this.SplitInternal(separator, count, options);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00011618 File Offset: 0x0000F818
		[ComVisible(false)]
		internal string[] SplitInternal(char[] separator, int count, StringSplitOptions options)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
			}
			if (options < StringSplitOptions.None || options > StringSplitOptions.RemoveEmptyEntries)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { options }));
			}
			bool flag = options == StringSplitOptions.RemoveEmptyEntries;
			if (count == 0 || (flag && this.Length == 0))
			{
				return new string[0];
			}
			int[] array = new int[this.Length];
			int num = this.MakeSeparatorList(separator, ref array);
			if (num == 0 || count == 1)
			{
				return new string[] { this };
			}
			if (flag)
			{
				return this.InternalSplitOmitEmptyEntries(array, null, num, count);
			}
			return this.InternalSplitKeepEmptyEntries(array, null, num, count);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000116C1 File Offset: 0x0000F8C1
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public string[] Split(string[] separator, StringSplitOptions options)
		{
			return this.Split(separator, int.MaxValue, options);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x000116D0 File Offset: 0x0000F8D0
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public string[] Split(string[] separator, int count, StringSplitOptions options)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
			}
			if (options < StringSplitOptions.None || options > StringSplitOptions.RemoveEmptyEntries)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)options }));
			}
			bool flag = options == StringSplitOptions.RemoveEmptyEntries;
			if (separator == null || separator.Length == 0)
			{
				return this.SplitInternal(null, count, options);
			}
			if (count == 0 || (flag && this.Length == 0))
			{
				return new string[0];
			}
			int[] array = new int[this.Length];
			int[] array2 = new int[this.Length];
			int num = this.MakeSeparatorList(separator, ref array, ref array2);
			if (num == 0 || count == 1)
			{
				return new string[] { this };
			}
			if (flag)
			{
				return this.InternalSplitOmitEmptyEntries(array, array2, num, count);
			}
			return this.InternalSplitKeepEmptyEntries(array, array2, num, count);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001179C File Offset: 0x0000F99C
		private string[] InternalSplitKeepEmptyEntries(int[] sepList, int[] lengthList, int numReplaces, int count)
		{
			int num = 0;
			int num2 = 0;
			count--;
			int num3 = ((numReplaces < count) ? numReplaces : count);
			string[] array = new string[num3 + 1];
			int num4 = 0;
			while (num4 < num3 && num < this.Length)
			{
				array[num2++] = this.Substring(num, sepList[num4] - num);
				num = sepList[num4] + ((lengthList == null) ? 1 : lengthList[num4]);
				num4++;
			}
			if (num < this.Length && num3 >= 0)
			{
				array[num2] = this.Substring(num);
			}
			else if (num2 == num3)
			{
				array[num2] = string.Empty;
			}
			return array;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001182C File Offset: 0x0000FA2C
		private string[] InternalSplitOmitEmptyEntries(int[] sepList, int[] lengthList, int numReplaces, int count)
		{
			int num = ((numReplaces < count) ? (numReplaces + 1) : count);
			string[] array = new string[num];
			int num2 = 0;
			int num3 = 0;
			int i = 0;
			while (i < numReplaces && num2 < this.Length)
			{
				if (sepList[i] - num2 > 0)
				{
					array[num3++] = this.Substring(num2, sepList[i] - num2);
				}
				num2 = sepList[i] + ((lengthList == null) ? 1 : lengthList[i]);
				if (num3 == count - 1)
				{
					while (i < numReplaces - 1)
					{
						if (num2 != sepList[++i])
						{
							break;
						}
						num2 += ((lengthList == null) ? 1 : lengthList[i]);
					}
					break;
				}
				i++;
			}
			if (num2 < this.Length)
			{
				array[num3++] = this.Substring(num2);
			}
			string[] array2 = array;
			if (num3 != num)
			{
				array2 = new string[num3];
				for (int j = 0; j < num3; j++)
				{
					array2[j] = array[j];
				}
			}
			return array2;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00011904 File Offset: 0x0000FB04
		[SecuritySafeCritical]
		private unsafe int MakeSeparatorList(char[] separator, ref int[] sepList)
		{
			int num = 0;
			if (separator == null || separator.Length == 0)
			{
				fixed (char* ptr = &this.m_firstChar)
				{
					char* ptr2 = ptr;
					int num2 = 0;
					while (num2 < this.Length && num < sepList.Length)
					{
						if (char.IsWhiteSpace(ptr2[num2]))
						{
							sepList[num++] = num2;
						}
						num2++;
					}
				}
			}
			else
			{
				int num3 = sepList.Length;
				int num4 = separator.Length;
				fixed (char* ptr3 = &this.m_firstChar)
				{
					char* ptr4 = ptr3;
					fixed (char[] array = separator)
					{
						char* ptr5;
						if (separator == null || array.Length == 0)
						{
							ptr5 = null;
						}
						else
						{
							ptr5 = &array[0];
						}
						int num5 = 0;
						while (num5 < this.Length && num < num3)
						{
							char* ptr6 = ptr5;
							int i = 0;
							while (i < num4)
							{
								if (ptr4[num5] == *ptr6)
								{
									sepList[num++] = num5;
									break;
								}
								i++;
								ptr6++;
							}
							num5++;
						}
						ptr3 = null;
					}
				}
			}
			return num;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000119E8 File Offset: 0x0000FBE8
		[SecuritySafeCritical]
		private unsafe int MakeSeparatorList(string[] separators, ref int[] sepList, ref int[] lengthList)
		{
			int num = 0;
			int num2 = sepList.Length;
			int num3 = separators.Length;
			fixed (char* ptr = &this.m_firstChar)
			{
				char* ptr2 = ptr;
				int num4 = 0;
				while (num4 < this.Length && num < num2)
				{
					foreach (string text in separators)
					{
						if (!string.IsNullOrEmpty(text))
						{
							int length = text.Length;
							if (ptr2[num4] == text[0] && length <= this.Length - num4 && (length == 1 || string.CompareOrdinal(this, num4, text, 0, length) == 0))
							{
								sepList[num] = num4;
								lengthList[num] = length;
								num++;
								num4 += length - 1;
								break;
							}
						}
					}
					num4++;
				}
			}
			return num;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00011AA5 File Offset: 0x0000FCA5
		[__DynamicallyInvokable]
		public string Substring(int startIndex)
		{
			return this.Substring(startIndex, this.Length - startIndex);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00011AB8 File Offset: 0x0000FCB8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string Substring(int startIndex, int length)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
			}
			if (startIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndexLargerThanLength"));
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
			}
			if (startIndex > this.Length - length)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_IndexLength"));
			}
			if (length == 0)
			{
				return string.Empty;
			}
			if (startIndex == 0 && length == this.Length)
			{
				return this;
			}
			return this.InternalSubString(startIndex, length);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00011B54 File Offset: 0x0000FD54
		[SecurityCritical]
		private unsafe string InternalSubString(int startIndex, int length)
		{
			string text = string.FastAllocateString(length);
			fixed (char* ptr = &text.m_firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &this.m_firstChar)
				{
					char* ptr4 = ptr3;
					string.wstrcpy(ptr2, ptr4 + startIndex, length);
				}
			}
			return text;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00011B93 File Offset: 0x0000FD93
		[__DynamicallyInvokable]
		public string Trim(params char[] trimChars)
		{
			if (trimChars == null || trimChars.Length == 0)
			{
				return this.TrimHelper(2);
			}
			return this.TrimHelper(trimChars, 2);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00011BAC File Offset: 0x0000FDAC
		[__DynamicallyInvokable]
		public string TrimStart(params char[] trimChars)
		{
			if (trimChars == null || trimChars.Length == 0)
			{
				return this.TrimHelper(0);
			}
			return this.TrimHelper(trimChars, 0);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00011BC5 File Offset: 0x0000FDC5
		[__DynamicallyInvokable]
		public string TrimEnd(params char[] trimChars)
		{
			if (trimChars == null || trimChars.Length == 0)
			{
				return this.TrimHelper(1);
			}
			return this.TrimHelper(trimChars, 1);
		}

		// Token: 0x060004E5 RID: 1253
		[SecurityCritical]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(char* value);

		// Token: 0x060004E6 RID: 1254
		[SecurityCritical]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(char* value, int startIndex, int length);

		// Token: 0x060004E7 RID: 1255
		[SecurityCritical]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(sbyte* value);

		// Token: 0x060004E8 RID: 1256
		[SecurityCritical]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(sbyte* value, int startIndex, int length);

		// Token: 0x060004E9 RID: 1257
		[SecurityCritical]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(sbyte* value, int startIndex, int length, Encoding enc);

		// Token: 0x060004EA RID: 1258 RVA: 0x00011BE0 File Offset: 0x0000FDE0
		[SecurityCritical]
		private unsafe static string CreateString(sbyte* value, int startIndex, int length, Encoding enc)
		{
			if (enc == null)
			{
				return new string(value, startIndex, length);
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
			}
			if (value + startIndex < value)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_PartialWCHAR"));
			}
			byte[] array = new byte[length];
			try
			{
				Buffer.Memcpy(array, 0, (byte*)value, startIndex, length);
			}
			catch (NullReferenceException)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_PartialWCHAR"));
			}
			return enc.GetString(array);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00011C88 File Offset: 0x0000FE88
		[SecurityCritical]
		internal unsafe static string CreateStringFromEncoding(byte* bytes, int byteLength, Encoding encoding)
		{
			int charCount = encoding.GetCharCount(bytes, byteLength, null);
			if (charCount == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(charCount);
			fixed (char* ptr = &text.m_firstChar)
			{
				char* ptr2 = ptr;
				int chars = encoding.GetChars(bytes, byteLength, ptr2, charCount, null);
			}
			return text;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00011CCC File Offset: 0x0000FECC
		[SecuritySafeCritical]
		internal unsafe int GetBytesFromEncoding(byte* pbNativeBuffer, int cbNativeBuffer, Encoding encoding)
		{
			fixed (char* ptr = &this.m_firstChar)
			{
				char* ptr2 = ptr;
				return encoding.GetBytes(ptr2, this.m_stringLength, pbNativeBuffer, cbNativeBuffer);
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00011CF4 File Offset: 0x0000FEF4
		[SecuritySafeCritical]
		internal unsafe int ConvertToAnsi(byte* pbNativeBuffer, int cbNativeBuffer, bool fBestFit, bool fThrowOnUnmappableChar)
		{
			uint num = (fBestFit ? 0U : 1024U);
			uint num2 = 0U;
			int num3;
			fixed (char* ptr = &this.m_firstChar)
			{
				char* ptr2 = ptr;
				num3 = Win32Native.WideCharToMultiByte(0U, num, ptr2, this.Length, pbNativeBuffer, cbNativeBuffer, IntPtr.Zero, fThrowOnUnmappableChar ? new IntPtr((void*)(&num2)) : IntPtr.Zero);
			}
			if (num2 != 0U)
			{
				throw new ArgumentException(Environment.GetResourceString("Interop_Marshal_Unmappable_Char"));
			}
			pbNativeBuffer[num3] = 0;
			return num3;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00011D61 File Offset: 0x0000FF61
		public bool IsNormalized()
		{
			return this.IsNormalized(NormalizationForm.FormC);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00011D6A File Offset: 0x0000FF6A
		[SecuritySafeCritical]
		public bool IsNormalized(NormalizationForm normalizationForm)
		{
			return (this.IsFastSort() && (normalizationForm == NormalizationForm.FormC || normalizationForm == NormalizationForm.FormKC || normalizationForm == NormalizationForm.FormD || normalizationForm == NormalizationForm.FormKD)) || Normalization.IsNormalized(this, normalizationForm);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00011D8D File Offset: 0x0000FF8D
		public string Normalize()
		{
			return this.Normalize(NormalizationForm.FormC);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00011D96 File Offset: 0x0000FF96
		[SecuritySafeCritical]
		public string Normalize(NormalizationForm normalizationForm)
		{
			if (this.IsAscii() && (normalizationForm == NormalizationForm.FormC || normalizationForm == NormalizationForm.FormKC || normalizationForm == NormalizationForm.FormD || normalizationForm == NormalizationForm.FormKD))
			{
				return this;
			}
			return Normalization.Normalize(this, normalizationForm);
		}

		// Token: 0x060004F2 RID: 1266
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string FastAllocateString(int length);

		// Token: 0x060004F3 RID: 1267 RVA: 0x00011DBC File Offset: 0x0000FFBC
		[SecuritySafeCritical]
		private unsafe static void FillStringChecked(string dest, int destPos, string src)
		{
			if (src.Length > dest.Length - destPos)
			{
				throw new IndexOutOfRangeException();
			}
			fixed (char* ptr = &dest.m_firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &src.m_firstChar)
				{
					char* ptr4 = ptr3;
					string.wstrcpy(ptr2 + destPos, ptr4, src.Length);
				}
			}
		}

		// Token: 0x060004F4 RID: 1268
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern String(char[] value, int startIndex, int length);

		// Token: 0x060004F5 RID: 1269
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern String(char[] value);

		// Token: 0x060004F6 RID: 1270 RVA: 0x00011E0B File Offset: 0x0001000B
		[SecurityCritical]
		internal unsafe static void wstrcpy(char* dmem, char* smem, int charCount)
		{
			Buffer.Memcpy((byte*)dmem, (byte*)smem, charCount * 2);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00011E18 File Offset: 0x00010018
		[SecuritySafeCritical]
		private unsafe string CtorCharArray(char[] value)
		{
			if (value != null && value.Length != 0)
			{
				string text = string.FastAllocateString(value.Length);
				fixed (string text2 = text)
				{
					char* ptr = text2;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (char[] array = value)
					{
						char* ptr2;
						if (value == null || array.Length == 0)
						{
							ptr2 = null;
						}
						else
						{
							ptr2 = &array[0];
						}
						string.wstrcpy(ptr, ptr2, value.Length);
						text2 = null;
					}
					return text;
				}
			}
			return string.Empty;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00011E78 File Offset: 0x00010078
		[SecuritySafeCritical]
		private unsafe string CtorCharArrayStartLength(char[] value, int startIndex, int length)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
			}
			if (startIndex > value.Length - length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (length > 0)
			{
				string text = string.FastAllocateString(length);
				fixed (string text2 = text)
				{
					char* ptr = text2;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (char[] array = value)
					{
						char* ptr2;
						if (value == null || array.Length == 0)
						{
							ptr2 = null;
						}
						else
						{
							ptr2 = &array[0];
						}
						string.wstrcpy(ptr, ptr2 + startIndex, length);
						text2 = null;
					}
					return text;
				}
			}
			return string.Empty;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00011F34 File Offset: 0x00010134
		[SecuritySafeCritical]
		private unsafe string CtorCharCount(char c, int count)
		{
			if (count > 0)
			{
				string text = string.FastAllocateString(count);
				if (c != '\0')
				{
					fixed (string text2 = text)
					{
						char* ptr = text2;
						if (ptr != null)
						{
							ptr += RuntimeHelpers.OffsetToStringData / 2;
						}
						char* ptr2 = ptr;
						while ((ptr2 & 3U) != 0U && count > 0)
						{
							*(ptr2++) = c;
							count--;
						}
						uint num = (uint)(((uint)c << 16) | c);
						if (count >= 4)
						{
							count -= 4;
							do
							{
								*(int*)ptr2 = (int)num;
								*(int*)(ptr2 + 2) = (int)num;
								ptr2 += 4;
								count -= 4;
							}
							while (count >= 0);
						}
						if ((count & 2) != 0)
						{
							*(int*)ptr2 = (int)num;
							ptr2 += 2;
						}
						if ((count & 1) != 0)
						{
							*ptr2 = c;
						}
					}
				}
				return text;
			}
			if (count == 0)
			{
				return string.Empty;
			}
			throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegNum", new object[] { "count" }));
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00011FEC File Offset: 0x000101EC
		[SecurityCritical]
		private unsafe static int wcslen(char* ptr)
		{
			char* ptr2 = ptr;
			while ((ptr2 & 3U) != 0U && *ptr2 != '\0')
			{
				ptr2++;
			}
			if (*ptr2 != '\0')
			{
				for (;;)
				{
					if ((*ptr2 & ptr2[1]) == '\0')
					{
						if (*ptr2 == '\0')
						{
							break;
						}
						if (ptr2[1] == '\0')
						{
							break;
						}
					}
					ptr2 += 2;
				}
			}
			while (*ptr2 != '\0')
			{
				ptr2++;
			}
			return (int)((long)(ptr2 - ptr));
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00012040 File Offset: 0x00010240
		[SecurityCritical]
		private unsafe string CtorCharPtr(char* ptr)
		{
			if (ptr == null)
			{
				return string.Empty;
			}
			if (ptr < 64000)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeStringPtrNotAtom"));
			}
			string text;
			try
			{
				int num = string.wcslen(ptr);
				if (num == 0)
				{
					text = string.Empty;
				}
				else
				{
					string text2 = string.FastAllocateString(num);
					try
					{
						fixed (string text3 = text2)
						{
							char* ptr2 = text3;
							if (ptr2 != null)
							{
								ptr2 += RuntimeHelpers.OffsetToStringData / 2;
							}
							string.wstrcpy(ptr2, ptr, num);
						}
					}
					finally
					{
						string text3 = null;
					}
					text = text2;
				}
			}
			catch (NullReferenceException)
			{
				throw new ArgumentOutOfRangeException("ptr", Environment.GetResourceString("ArgumentOutOfRange_PartialWCHAR"));
			}
			return text;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000120E4 File Offset: 0x000102E4
		[SecurityCritical]
		private unsafe string CtorCharPtrStartLength(char* ptr, int startIndex, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
			}
			char* ptr2 = ptr + startIndex;
			if (ptr2 < ptr)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_PartialWCHAR"));
			}
			if (length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(length);
			string text3;
			try
			{
				try
				{
					fixed (string text2 = text)
					{
						char* ptr3 = text2;
						if (ptr3 != null)
						{
							ptr3 += RuntimeHelpers.OffsetToStringData / 2;
						}
						string.wstrcpy(ptr3, ptr2, length);
					}
				}
				finally
				{
					string text2 = null;
				}
				text3 = text;
			}
			catch (NullReferenceException)
			{
				throw new ArgumentOutOfRangeException("ptr", Environment.GetResourceString("ArgumentOutOfRange_PartialWCHAR"));
			}
			return text3;
		}

		// Token: 0x060004FD RID: 1277
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern String(char c, int count);

		// Token: 0x060004FE RID: 1278 RVA: 0x000121AC File Offset: 0x000103AC
		[__DynamicallyInvokable]
		public static int Compare(string strA, string strB)
		{
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000121C0 File Offset: 0x000103C0
		[__DynamicallyInvokable]
		public static int Compare(string strA, string strB, bool ignoreCase)
		{
			if (ignoreCase)
			{
				return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
			}
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000121EC File Offset: 0x000103EC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static int Compare(string strA, string strB, StringComparison comparisonType)
		{
			if (comparisonType - StringComparison.CurrentCulture > 5)
			{
				throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
			}
			if (strA == strB)
			{
				return 0;
			}
			if (strA == null)
			{
				return -1;
			}
			if (strB == null)
			{
				return 1;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				if (strA.m_firstChar - strB.m_firstChar != '\0')
				{
					return (int)(strA.m_firstChar - strB.m_firstChar);
				}
				return string.CompareOrdinalHelper(strA, strB);
			case StringComparison.OrdinalIgnoreCase:
				if (strA.IsAscii() && strB.IsAscii())
				{
					return string.CompareOrdinalIgnoreCaseHelper(strA, strB);
				}
				return TextInfo.CompareOrdinalIgnoreCase(strA, strB);
			default:
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_StringComparison"));
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000122E7 File Offset: 0x000104E7
		[__DynamicallyInvokable]
		public static int Compare(string strA, string strB, CultureInfo culture, CompareOptions options)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.CompareInfo.Compare(strA, strB, options);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00012305 File Offset: 0x00010505
		public static int Compare(string strA, string strB, bool ignoreCase, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			if (ignoreCase)
			{
				return culture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
			}
			return culture.CompareInfo.Compare(strA, strB, CompareOptions.None);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00012338 File Offset: 0x00010538
		[__DynamicallyInvokable]
		public static int Compare(string strA, int indexA, string strB, int indexB, int length)
		{
			int num = length;
			int num2 = length;
			if (strA != null && strA.Length - indexA < num)
			{
				num = strA.Length - indexA;
			}
			if (strB != null && strB.Length - indexB < num2)
			{
				num2 = strB.Length - indexB;
			}
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.None);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00012390 File Offset: 0x00010590
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase)
		{
			int num = length;
			int num2 = length;
			if (strA != null && strA.Length - indexA < num)
			{
				num = strA.Length - indexA;
			}
			if (strB != null && strB.Length - indexB < num2)
			{
				num2 = strB.Length - indexB;
			}
			if (ignoreCase)
			{
				return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.IgnoreCase);
			}
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.None);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00012404 File Offset: 0x00010604
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			int num = length;
			int num2 = length;
			if (strA != null && strA.Length - indexA < num)
			{
				num = strA.Length - indexA;
			}
			if (strB != null && strB.Length - indexB < num2)
			{
				num2 = strB.Length - indexB;
			}
			if (ignoreCase)
			{
				return culture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.IgnoreCase);
			}
			return culture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.None);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00012480 File Offset: 0x00010680
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, CultureInfo culture, CompareOptions options)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			int num = length;
			int num2 = length;
			if (strA != null && strA.Length - indexA < num)
			{
				num = strA.Length - indexA;
			}
			if (strB != null && strB.Length - indexB < num2)
			{
				num2 = strB.Length - indexB;
			}
			return culture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, options);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000124E4 File Offset: 0x000106E4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, StringComparison comparisonType)
		{
			if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
			{
				throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
			}
			if (strA == null || strB == null)
			{
				if (strA == strB)
				{
					return 0;
				}
				if (strA != null)
				{
					return 1;
				}
				return -1;
			}
			else
			{
				if (length < 0)
				{
					throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
				}
				if (indexA < 0)
				{
					throw new ArgumentOutOfRangeException("indexA", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (indexB < 0)
				{
					throw new ArgumentOutOfRangeException("indexB", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (strA.Length - indexA < 0)
				{
					throw new ArgumentOutOfRangeException("indexA", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (strB.Length - indexB < 0)
				{
					throw new ArgumentOutOfRangeException("indexB", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (length == 0 || (strA == strB && indexA == indexB))
				{
					return 0;
				}
				int num = length;
				int num2 = length;
				if (strA != null && strA.Length - indexA < num)
				{
					num = strA.Length - indexA;
				}
				if (strB != null && strB.Length - indexB < num2)
				{
					num2 = strB.Length - indexB;
				}
				switch (comparisonType)
				{
				case StringComparison.CurrentCulture:
					return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.None);
				case StringComparison.CurrentCultureIgnoreCase:
					return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.IgnoreCase);
				case StringComparison.InvariantCulture:
					return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.None);
				case StringComparison.InvariantCultureIgnoreCase:
					return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.IgnoreCase);
				case StringComparison.Ordinal:
					return string.nativeCompareOrdinalEx(strA, indexA, strB, indexB, length);
				case StringComparison.OrdinalIgnoreCase:
					return TextInfo.CompareOrdinalIgnoreCaseEx(strA, indexA, strB, indexB, num, num2);
				default:
					throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"));
				}
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001269A File Offset: 0x0001089A
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is string))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeString"));
			}
			return string.Compare(this, (string)value, StringComparison.CurrentCulture);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000126C6 File Offset: 0x000108C6
		[__DynamicallyInvokable]
		public int CompareTo(string strB)
		{
			if (strB == null)
			{
				return 1;
			}
			return CultureInfo.CurrentCulture.CompareInfo.Compare(this, strB, CompareOptions.None);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000126DF File Offset: 0x000108DF
		[__DynamicallyInvokable]
		public static int CompareOrdinal(string strA, string strB)
		{
			if (strA == strB)
			{
				return 0;
			}
			if (strA == null)
			{
				return -1;
			}
			if (strB == null)
			{
				return 1;
			}
			if (strA.m_firstChar - strB.m_firstChar != '\0')
			{
				return (int)(strA.m_firstChar - strB.m_firstChar);
			}
			return string.CompareOrdinalHelper(strA, strB);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00012715 File Offset: 0x00010915
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static int CompareOrdinal(string strA, int indexA, string strB, int indexB, int length)
		{
			if (strA != null && strB != null)
			{
				return string.nativeCompareOrdinalEx(strA, indexA, strB, indexB, length);
			}
			if (strA == strB)
			{
				return 0;
			}
			if (strA != null)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00012735 File Offset: 0x00010935
		[__DynamicallyInvokable]
		public bool Contains(string value)
		{
			return this.IndexOf(value, StringComparison.Ordinal) >= 0;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00012745 File Offset: 0x00010945
		[__DynamicallyInvokable]
		public bool EndsWith(string value)
		{
			return this.EndsWith(value, StringComparison.CurrentCulture);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00012750 File Offset: 0x00010950
		[SecuritySafeCritical]
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public bool EndsWith(string value, StringComparison comparisonType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
			{
				throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
			}
			if (this == value)
			{
				return true;
			}
			if (value.Length == 0)
			{
				return true;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CultureInfo.InvariantCulture.CompareInfo.IsSuffix(this, value, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CultureInfo.InvariantCulture.CompareInfo.IsSuffix(this, value, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				return this.Length >= value.Length && string.nativeCompareOrdinalEx(this, this.Length - value.Length, value, 0, value.Length) == 0;
			case StringComparison.OrdinalIgnoreCase:
				return this.Length >= value.Length && TextInfo.CompareOrdinalIgnoreCaseEx(this, this.Length - value.Length, value, 0, value.Length, value.Length) == 0;
			default:
				throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00012880 File Offset: 0x00010A80
		public bool EndsWith(string value, bool ignoreCase, CultureInfo culture)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this == value)
			{
				return true;
			}
			CultureInfo cultureInfo;
			if (culture == null)
			{
				cultureInfo = CultureInfo.CurrentCulture;
			}
			else
			{
				cultureInfo = culture;
			}
			return cultureInfo.CompareInfo.IsSuffix(this, value, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000128C4 File Offset: 0x00010AC4
		internal bool EndsWith(char value)
		{
			int length = this.Length;
			return length != 0 && this[length - 1] == value;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000128EA File Offset: 0x00010AEA
		[__DynamicallyInvokable]
		public int IndexOf(char value)
		{
			return this.IndexOf(value, 0, this.Length);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x000128FA File Offset: 0x00010AFA
		[__DynamicallyInvokable]
		public int IndexOf(char value, int startIndex)
		{
			return this.IndexOf(value, startIndex, this.Length - startIndex);
		}

		// Token: 0x06000513 RID: 1299
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int IndexOf(char value, int startIndex, int count);

		// Token: 0x06000514 RID: 1300 RVA: 0x0001290C File Offset: 0x00010B0C
		[__DynamicallyInvokable]
		public int IndexOfAny(char[] anyOf)
		{
			return this.IndexOfAny(anyOf, 0, this.Length);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001291C File Offset: 0x00010B1C
		[__DynamicallyInvokable]
		public int IndexOfAny(char[] anyOf, int startIndex)
		{
			return this.IndexOfAny(anyOf, startIndex, this.Length - startIndex);
		}

		// Token: 0x06000516 RID: 1302
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int IndexOfAny(char[] anyOf, int startIndex, int count);

		// Token: 0x06000517 RID: 1303 RVA: 0x0001292E File Offset: 0x00010B2E
		[__DynamicallyInvokable]
		public int IndexOf(string value)
		{
			return this.IndexOf(value, StringComparison.CurrentCulture);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00012938 File Offset: 0x00010B38
		[__DynamicallyInvokable]
		public int IndexOf(string value, int startIndex)
		{
			return this.IndexOf(value, startIndex, StringComparison.CurrentCulture);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00012944 File Offset: 0x00010B44
		[__DynamicallyInvokable]
		public int IndexOf(string value, int startIndex, int count)
		{
			if (startIndex < 0 || startIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0 || count > this.Length - startIndex)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			return this.IndexOf(value, startIndex, count, StringComparison.CurrentCulture);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000129A1 File Offset: 0x00010BA1
		[__DynamicallyInvokable]
		public int IndexOf(string value, StringComparison comparisonType)
		{
			return this.IndexOf(value, 0, this.Length, comparisonType);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000129B2 File Offset: 0x00010BB2
		[__DynamicallyInvokable]
		public int IndexOf(string value, int startIndex, StringComparison comparisonType)
		{
			return this.IndexOf(value, startIndex, this.Length - startIndex, comparisonType);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000129C8 File Offset: 0x00010BC8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public int IndexOf(string value, int startIndex, int count, StringComparison comparisonType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0 || startIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0 || startIndex > this.Length - count)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.Ordinal);
			case StringComparison.OrdinalIgnoreCase:
				if (value.IsAscii() && this.IsAscii())
				{
					return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
				}
				return TextInfo.IndexOfStringOrdinalIgnoreCase(this, value, startIndex, count);
			default:
				throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00012AFD File Offset: 0x00010CFD
		[__DynamicallyInvokable]
		public int LastIndexOf(char value)
		{
			return this.LastIndexOf(value, this.Length - 1, this.Length);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00012B14 File Offset: 0x00010D14
		[__DynamicallyInvokable]
		public int LastIndexOf(char value, int startIndex)
		{
			return this.LastIndexOf(value, startIndex, startIndex + 1);
		}

		// Token: 0x0600051F RID: 1311
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int LastIndexOf(char value, int startIndex, int count);

		// Token: 0x06000520 RID: 1312 RVA: 0x00012B21 File Offset: 0x00010D21
		[__DynamicallyInvokable]
		public int LastIndexOfAny(char[] anyOf)
		{
			return this.LastIndexOfAny(anyOf, this.Length - 1, this.Length);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00012B38 File Offset: 0x00010D38
		[__DynamicallyInvokable]
		public int LastIndexOfAny(char[] anyOf, int startIndex)
		{
			return this.LastIndexOfAny(anyOf, startIndex, startIndex + 1);
		}

		// Token: 0x06000522 RID: 1314
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int LastIndexOfAny(char[] anyOf, int startIndex, int count);

		// Token: 0x06000523 RID: 1315 RVA: 0x00012B45 File Offset: 0x00010D45
		[__DynamicallyInvokable]
		public int LastIndexOf(string value)
		{
			return this.LastIndexOf(value, this.Length - 1, this.Length, StringComparison.CurrentCulture);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00012B5D File Offset: 0x00010D5D
		[__DynamicallyInvokable]
		public int LastIndexOf(string value, int startIndex)
		{
			return this.LastIndexOf(value, startIndex, startIndex + 1, StringComparison.CurrentCulture);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00012B6B File Offset: 0x00010D6B
		[__DynamicallyInvokable]
		public int LastIndexOf(string value, int startIndex, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			return this.LastIndexOf(value, startIndex, count, StringComparison.CurrentCulture);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00012B90 File Offset: 0x00010D90
		[__DynamicallyInvokable]
		public int LastIndexOf(string value, StringComparison comparisonType)
		{
			return this.LastIndexOf(value, this.Length - 1, this.Length, comparisonType);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00012BA8 File Offset: 0x00010DA8
		[__DynamicallyInvokable]
		public int LastIndexOf(string value, int startIndex, StringComparison comparisonType)
		{
			return this.LastIndexOf(value, startIndex, startIndex + 1, comparisonType);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00012BB8 File Offset: 0x00010DB8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public int LastIndexOf(string value, int startIndex, int count, StringComparison comparisonType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.Length == 0 && (startIndex == -1 || startIndex == 0))
			{
				if (value.Length != 0)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (startIndex < 0 || startIndex > this.Length)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (startIndex == this.Length)
				{
					startIndex--;
					if (count > 0)
					{
						count--;
					}
					if (value.Length == 0 && count >= 0 && startIndex - count + 1 >= 0)
					{
						return startIndex;
					}
				}
				if (count < 0 || startIndex - count + 1 < 0)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
				}
				switch (comparisonType)
				{
				case StringComparison.CurrentCulture:
					return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.None);
				case StringComparison.CurrentCultureIgnoreCase:
					return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
				case StringComparison.InvariantCulture:
					return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.None);
				case StringComparison.InvariantCultureIgnoreCase:
					return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
				case StringComparison.Ordinal:
					return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.Ordinal);
				case StringComparison.OrdinalIgnoreCase:
					if (value.IsAscii() && this.IsAscii())
					{
						return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
					}
					return TextInfo.LastIndexOfStringOrdinalIgnoreCase(this, value, startIndex, count);
				default:
					throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
				}
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00012D32 File Offset: 0x00010F32
		[__DynamicallyInvokable]
		public string PadLeft(int totalWidth)
		{
			return this.PadHelper(totalWidth, ' ', false);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00012D3E File Offset: 0x00010F3E
		[__DynamicallyInvokable]
		public string PadLeft(int totalWidth, char paddingChar)
		{
			return this.PadHelper(totalWidth, paddingChar, false);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00012D49 File Offset: 0x00010F49
		[__DynamicallyInvokable]
		public string PadRight(int totalWidth)
		{
			return this.PadHelper(totalWidth, ' ', true);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00012D55 File Offset: 0x00010F55
		[__DynamicallyInvokable]
		public string PadRight(int totalWidth, char paddingChar)
		{
			return this.PadHelper(totalWidth, paddingChar, true);
		}

		// Token: 0x0600052D RID: 1325
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string PadHelper(int totalWidth, char paddingChar, bool isRightPadded);

		// Token: 0x0600052E RID: 1326 RVA: 0x00012D60 File Offset: 0x00010F60
		[__DynamicallyInvokable]
		public bool StartsWith(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return this.StartsWith(value, StringComparison.CurrentCulture);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00012D78 File Offset: 0x00010F78
		[SecuritySafeCritical]
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public bool StartsWith(string value, StringComparison comparisonType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
			{
				throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
			}
			if (this == value)
			{
				return true;
			}
			if (value.Length == 0)
			{
				return true;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CultureInfo.InvariantCulture.CompareInfo.IsPrefix(this, value, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CultureInfo.InvariantCulture.CompareInfo.IsPrefix(this, value, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				return this.Length >= value.Length && string.nativeCompareOrdinalEx(this, 0, value, 0, value.Length) == 0;
			case StringComparison.OrdinalIgnoreCase:
				return this.Length >= value.Length && TextInfo.CompareOrdinalIgnoreCaseEx(this, 0, value, 0, value.Length, value.Length) == 0;
			default:
				throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00012E90 File Offset: 0x00011090
		public bool StartsWith(string value, bool ignoreCase, CultureInfo culture)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this == value)
			{
				return true;
			}
			CultureInfo cultureInfo;
			if (culture == null)
			{
				cultureInfo = CultureInfo.CurrentCulture;
			}
			else
			{
				cultureInfo = culture;
			}
			return cultureInfo.CompareInfo.IsPrefix(this, value, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00012ED2 File Offset: 0x000110D2
		[__DynamicallyInvokable]
		public string ToLower()
		{
			return this.ToLower(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00012EDF File Offset: 0x000110DF
		public string ToLower(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToLower(this);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00012EFB File Offset: 0x000110FB
		[__DynamicallyInvokable]
		public string ToLowerInvariant()
		{
			return this.ToLower(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00012F08 File Offset: 0x00011108
		[__DynamicallyInvokable]
		public string ToUpper()
		{
			return this.ToUpper(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00012F15 File Offset: 0x00011115
		public string ToUpper(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToUpper(this);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00012F31 File Offset: 0x00011131
		[__DynamicallyInvokable]
		public string ToUpperInvariant()
		{
			return this.ToUpper(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00012F3E File Offset: 0x0001113E
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00012F41 File Offset: 0x00011141
		public string ToString(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00012F44 File Offset: 0x00011144
		public object Clone()
		{
			return this;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00012F47 File Offset: 0x00011147
		private static bool IsBOMWhitespace(char c)
		{
			return false;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00012F4A File Offset: 0x0001114A
		[__DynamicallyInvokable]
		public string Trim()
		{
			return this.TrimHelper(2);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00012F54 File Offset: 0x00011154
		[SecuritySafeCritical]
		private string TrimHelper(int trimType)
		{
			int num = this.Length - 1;
			int num2 = 0;
			if (trimType != 1)
			{
				num2 = 0;
				while (num2 < this.Length && (char.IsWhiteSpace(this[num2]) || string.IsBOMWhitespace(this[num2])))
				{
					num2++;
				}
			}
			if (trimType != 0)
			{
				num = this.Length - 1;
				while (num >= num2 && (char.IsWhiteSpace(this[num]) || string.IsBOMWhitespace(this[num2])))
				{
					num--;
				}
			}
			return this.CreateTrimmedString(num2, num);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00012FD8 File Offset: 0x000111D8
		[SecuritySafeCritical]
		private string TrimHelper(char[] trimChars, int trimType)
		{
			int i = this.Length - 1;
			int j = 0;
			if (trimType != 1)
			{
				for (j = 0; j < this.Length; j++)
				{
					char c = this[j];
					int num = 0;
					while (num < trimChars.Length && trimChars[num] != c)
					{
						num++;
					}
					if (num == trimChars.Length)
					{
						break;
					}
				}
			}
			if (trimType != 0)
			{
				for (i = this.Length - 1; i >= j; i--)
				{
					char c2 = this[i];
					int num2 = 0;
					while (num2 < trimChars.Length && trimChars[num2] != c2)
					{
						num2++;
					}
					if (num2 == trimChars.Length)
					{
						break;
					}
				}
			}
			return this.CreateTrimmedString(j, i);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00013074 File Offset: 0x00011274
		[SecurityCritical]
		private string CreateTrimmedString(int start, int end)
		{
			int num = end - start + 1;
			if (num == this.Length)
			{
				return this;
			}
			if (num == 0)
			{
				return string.Empty;
			}
			return this.InternalSubString(start, num);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000130A4 File Offset: 0x000112A4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe string Insert(int startIndex, string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0 || startIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			int length = this.Length;
			int length2 = value.Length;
			int num = length + length2;
			if (num == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(num);
			fixed (char* ptr = &this.m_firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &value.m_firstChar)
				{
					char* ptr4 = ptr3;
					fixed (char* ptr5 = &text.m_firstChar)
					{
						char* ptr6 = ptr5;
						string.wstrcpy(ptr6, ptr2, startIndex);
						string.wstrcpy(ptr6 + startIndex, ptr4, length2);
						string.wstrcpy(ptr6 + startIndex + length2, ptr2 + startIndex, length - startIndex);
					}
				}
			}
			return text;
		}

		// Token: 0x06000540 RID: 1344
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string ReplaceInternal(char oldChar, char newChar);

		// Token: 0x06000541 RID: 1345 RVA: 0x00013161 File Offset: 0x00011361
		[__DynamicallyInvokable]
		public string Replace(char oldChar, char newChar)
		{
			return this.ReplaceInternal(oldChar, newChar);
		}

		// Token: 0x06000542 RID: 1346
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string ReplaceInternal(string oldValue, string newValue);

		// Token: 0x06000543 RID: 1347 RVA: 0x0001316C File Offset: 0x0001136C
		[__DynamicallyInvokable]
		public string Replace(string oldValue, string newValue)
		{
			if (oldValue == null)
			{
				throw new ArgumentNullException("oldValue");
			}
			return this.ReplaceInternal(oldValue, newValue);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00013194 File Offset: 0x00011394
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe string Remove(int startIndex, int count)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
			}
			if (count > this.Length - startIndex)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_IndexCount"));
			}
			int num = this.Length - count;
			if (num == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(num);
			fixed (char* ptr = &this.m_firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &text.m_firstChar)
				{
					char* ptr4 = ptr3;
					string.wstrcpy(ptr4, ptr2, startIndex);
					string.wstrcpy(ptr4 + startIndex, ptr2 + startIndex + count, num - startIndex);
				}
			}
			return text;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00013250 File Offset: 0x00011450
		[__DynamicallyInvokable]
		public string Remove(int startIndex)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
			}
			if (startIndex >= this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndexLessThanLength"));
			}
			return this.Substring(0, startIndex);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001329C File Offset: 0x0001149C
		[__DynamicallyInvokable]
		public static string Format(string format, object arg0)
		{
			return string.FormatHelper(null, format, new ParamsArray(arg0));
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000132AB File Offset: 0x000114AB
		[__DynamicallyInvokable]
		public static string Format(string format, object arg0, object arg1)
		{
			return string.FormatHelper(null, format, new ParamsArray(arg0, arg1));
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x000132BB File Offset: 0x000114BB
		[__DynamicallyInvokable]
		public static string Format(string format, object arg0, object arg1, object arg2)
		{
			return string.FormatHelper(null, format, new ParamsArray(arg0, arg1, arg2));
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x000132CC File Offset: 0x000114CC
		[__DynamicallyInvokable]
		public static string Format(string format, params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException((format == null) ? "format" : "args");
			}
			return string.FormatHelper(null, format, new ParamsArray(args));
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x000132F3 File Offset: 0x000114F3
		[__DynamicallyInvokable]
		public static string Format(IFormatProvider provider, string format, object arg0)
		{
			return string.FormatHelper(provider, format, new ParamsArray(arg0));
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00013302 File Offset: 0x00011502
		[__DynamicallyInvokable]
		public static string Format(IFormatProvider provider, string format, object arg0, object arg1)
		{
			return string.FormatHelper(provider, format, new ParamsArray(arg0, arg1));
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00013312 File Offset: 0x00011512
		[__DynamicallyInvokable]
		public static string Format(IFormatProvider provider, string format, object arg0, object arg1, object arg2)
		{
			return string.FormatHelper(provider, format, new ParamsArray(arg0, arg1, arg2));
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00013324 File Offset: 0x00011524
		[__DynamicallyInvokable]
		public static string Format(IFormatProvider provider, string format, params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException((format == null) ? "format" : "args");
			}
			return string.FormatHelper(provider, format, new ParamsArray(args));
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001334B File Offset: 0x0001154B
		private static string FormatHelper(IFormatProvider provider, string format, ParamsArray args)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			return StringBuilderCache.GetStringAndRelease(StringBuilderCache.Acquire(format.Length + args.Length * 8).AppendFormatHelper(provider, format, args));
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00013380 File Offset: 0x00011580
		[SecuritySafeCritical]
		public unsafe static string Copy(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int length = str.Length;
			string text = string.FastAllocateString(length);
			fixed (char* ptr = &text.m_firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &str.m_firstChar)
				{
					char* ptr4 = ptr3;
					string.wstrcpy(ptr2, ptr4, length);
				}
			}
			return text;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x000133D1 File Offset: 0x000115D1
		[__DynamicallyInvokable]
		public static string Concat(object arg0)
		{
			if (arg0 == null)
			{
				return string.Empty;
			}
			return arg0.ToString();
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x000133E2 File Offset: 0x000115E2
		[__DynamicallyInvokable]
		public static string Concat(object arg0, object arg1)
		{
			if (arg0 == null)
			{
				arg0 = string.Empty;
			}
			if (arg1 == null)
			{
				arg1 = string.Empty;
			}
			return arg0.ToString() + arg1.ToString();
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00013409 File Offset: 0x00011609
		[__DynamicallyInvokable]
		public static string Concat(object arg0, object arg1, object arg2)
		{
			if (arg0 == null)
			{
				arg0 = string.Empty;
			}
			if (arg1 == null)
			{
				arg1 = string.Empty;
			}
			if (arg2 == null)
			{
				arg2 = string.Empty;
			}
			return arg0.ToString() + arg1.ToString() + arg2.ToString();
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00013440 File Offset: 0x00011640
		[CLSCompliant(false)]
		public static string Concat(object arg0, object arg1, object arg2, object arg3, __arglist)
		{
			ArgIterator argIterator = new ArgIterator(__arglist);
			int num = argIterator.GetRemainingCount() + 4;
			object[] array = new object[num];
			array[0] = arg0;
			array[1] = arg1;
			array[2] = arg2;
			array[3] = arg3;
			for (int i = 4; i < num; i++)
			{
				array[i] = TypedReference.ToObject(argIterator.GetNextArg());
			}
			return string.Concat(array);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00013498 File Offset: 0x00011698
		[__DynamicallyInvokable]
		public static string Concat(params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			string[] array = new string[args.Length];
			int num = 0;
			for (int i = 0; i < args.Length; i++)
			{
				object obj = args[i];
				array[i] = ((obj == null) ? string.Empty : obj.ToString());
				if (array[i] == null)
				{
					array[i] = string.Empty;
				}
				num += array[i].Length;
				if (num < 0)
				{
					throw new OutOfMemoryException();
				}
			}
			return string.ConcatArray(array, num);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001350C File Offset: 0x0001170C
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public static string Concat<T>(IEnumerable<T> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			using (IEnumerator<T> enumerator = values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current != null)
					{
						T t = enumerator.Current;
						string text = t.ToString();
						if (text != null)
						{
							stringBuilder.Append(text);
						}
					}
				}
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00013590 File Offset: 0x00011790
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public static string Concat(IEnumerable<string> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			using (IEnumerator<string> enumerator = values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current != null)
					{
						stringBuilder.Append(enumerator.Current);
					}
				}
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000135FC File Offset: 0x000117FC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static string Concat(string str0, string str1)
		{
			if (string.IsNullOrEmpty(str0))
			{
				if (string.IsNullOrEmpty(str1))
				{
					return string.Empty;
				}
				return str1;
			}
			else
			{
				if (string.IsNullOrEmpty(str1))
				{
					return str0;
				}
				int length = str0.Length;
				string text = string.FastAllocateString(length + str1.Length);
				string.FillStringChecked(text, 0, str0);
				string.FillStringChecked(text, length, str1);
				return text;
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00013654 File Offset: 0x00011854
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static string Concat(string str0, string str1, string str2)
		{
			if (str0 == null && str1 == null && str2 == null)
			{
				return string.Empty;
			}
			if (str0 == null)
			{
				str0 = string.Empty;
			}
			if (str1 == null)
			{
				str1 = string.Empty;
			}
			if (str2 == null)
			{
				str2 = string.Empty;
			}
			int num = str0.Length + str1.Length + str2.Length;
			string text = string.FastAllocateString(num);
			string.FillStringChecked(text, 0, str0);
			string.FillStringChecked(text, str0.Length, str1);
			string.FillStringChecked(text, str0.Length + str1.Length, str2);
			return text;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000136D4 File Offset: 0x000118D4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static string Concat(string str0, string str1, string str2, string str3)
		{
			if (str0 == null && str1 == null && str2 == null && str3 == null)
			{
				return string.Empty;
			}
			if (str0 == null)
			{
				str0 = string.Empty;
			}
			if (str1 == null)
			{
				str1 = string.Empty;
			}
			if (str2 == null)
			{
				str2 = string.Empty;
			}
			if (str3 == null)
			{
				str3 = string.Empty;
			}
			int num = str0.Length + str1.Length + str2.Length + str3.Length;
			string text = string.FastAllocateString(num);
			string.FillStringChecked(text, 0, str0);
			string.FillStringChecked(text, str0.Length, str1);
			string.FillStringChecked(text, str0.Length + str1.Length, str2);
			string.FillStringChecked(text, str0.Length + str1.Length + str2.Length, str3);
			return text;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00013784 File Offset: 0x00011984
		[SecuritySafeCritical]
		private static string ConcatArray(string[] values, int totalLength)
		{
			string text = string.FastAllocateString(totalLength);
			int num = 0;
			for (int i = 0; i < values.Length; i++)
			{
				string.FillStringChecked(text, num, values[i]);
				num += values[i].Length;
			}
			return text;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000137C0 File Offset: 0x000119C0
		[__DynamicallyInvokable]
		public static string Concat(params string[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			int num = 0;
			string[] array = new string[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				string text = values[i];
				array[i] = ((text == null) ? string.Empty : text);
				num += array[i].Length;
				if (num < 0)
				{
					throw new OutOfMemoryException();
				}
			}
			return string.ConcatArray(array, num);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00013822 File Offset: 0x00011A22
		[SecuritySafeCritical]
		public static string Intern(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return Thread.GetDomain().GetOrInternString(str);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001383D File Offset: 0x00011A3D
		[SecuritySafeCritical]
		public static string IsInterned(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return Thread.GetDomain().IsStringInterned(str);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00013858 File Offset: 0x00011A58
		public TypeCode GetTypeCode()
		{
			return TypeCode.String;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001385C File Offset: 0x00011A5C
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this, provider);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00013865 File Offset: 0x00011A65
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this, provider);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001386E File Offset: 0x00011A6E
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this, provider);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00013877 File Offset: 0x00011A77
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this, provider);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00013880 File Offset: 0x00011A80
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this, provider);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00013889 File Offset: 0x00011A89
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this, provider);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00013892 File Offset: 0x00011A92
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this, provider);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001389B File Offset: 0x00011A9B
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this, provider);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000138A4 File Offset: 0x00011AA4
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this, provider);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x000138AD File Offset: 0x00011AAD
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this, provider);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x000138B6 File Offset: 0x00011AB6
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this, provider);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000138BF File Offset: 0x00011ABF
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this, provider);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000138C8 File Offset: 0x00011AC8
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this, provider);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000138D1 File Offset: 0x00011AD1
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(this, provider);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000138DA File Offset: 0x00011ADA
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x0600056E RID: 1390
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsFastSort();

		// Token: 0x0600056F RID: 1391
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsAscii();

		// Token: 0x06000570 RID: 1392
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetTrailByte(byte data);

		// Token: 0x06000571 RID: 1393
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool TryGetTrailByte(out byte data);

		// Token: 0x06000572 RID: 1394 RVA: 0x000138E4 File Offset: 0x00011AE4
		public CharEnumerator GetEnumerator()
		{
			return new CharEnumerator(this);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000138EC File Offset: 0x00011AEC
		[__DynamicallyInvokable]
		IEnumerator<char> IEnumerable<char>.GetEnumerator()
		{
			return new CharEnumerator(this);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x000138F4 File Offset: 0x00011AF4
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new CharEnumerator(this);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x000138FC File Offset: 0x00011AFC
		[SecurityCritical]
		internal unsafe static void InternalCopy(string src, IntPtr dest, int len)
		{
			if (len == 0)
			{
				return;
			}
			fixed (char* ptr = &src.m_firstChar)
			{
				char* ptr2 = ptr;
				byte* ptr3 = (byte*)ptr2;
				byte* ptr4 = (byte*)(void*)dest;
				Buffer.Memcpy(ptr4, ptr3, len);
			}
		}

		// Token: 0x04000283 RID: 643
		[NonSerialized]
		private int m_stringLength;

		// Token: 0x04000284 RID: 644
		[NonSerialized]
		private char m_firstChar;

		// Token: 0x04000285 RID: 645
		private const int TrimHead = 0;

		// Token: 0x04000286 RID: 646
		private const int TrimTail = 1;

		// Token: 0x04000287 RID: 647
		private const int TrimBoth = 2;

		// Token: 0x04000288 RID: 648
		[__DynamicallyInvokable]
		public static readonly string Empty;

		// Token: 0x04000289 RID: 649
		private const int charPtrAlignConst = 1;

		// Token: 0x0400028A RID: 650
		private const int alignConst = 3;
	}
}
