using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;

namespace System.Resources
{
	// Token: 0x0200038A RID: 906
	internal sealed class FastResourceComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string>
	{
		// Token: 0x06002CDF RID: 11487 RVA: 0x000A8FE8 File Offset: 0x000A71E8
		public int GetHashCode(object key)
		{
			string text = (string)key;
			return FastResourceComparer.HashFunction(text);
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x000A9002 File Offset: 0x000A7202
		public int GetHashCode(string key)
		{
			return FastResourceComparer.HashFunction(key);
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x000A900C File Offset: 0x000A720C
		internal static int HashFunction(string key)
		{
			uint num = 5381U;
			for (int i = 0; i < key.Length; i++)
			{
				num = ((num << 5) + num) ^ (uint)key[i];
			}
			return (int)num;
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x000A9040 File Offset: 0x000A7240
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			string text = (string)a;
			string text2 = (string)b;
			return string.CompareOrdinal(text, text2);
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x000A9068 File Offset: 0x000A7268
		public int Compare(string a, string b)
		{
			return string.CompareOrdinal(a, b);
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x000A9071 File Offset: 0x000A7271
		public bool Equals(string a, string b)
		{
			return string.Equals(a, b);
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x000A907C File Offset: 0x000A727C
		public bool Equals(object a, object b)
		{
			if (a == b)
			{
				return true;
			}
			string text = (string)a;
			string text2 = (string)b;
			return string.Equals(text, text2);
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x000A90A4 File Offset: 0x000A72A4
		[SecurityCritical]
		public unsafe static int CompareOrdinal(string a, byte[] bytes, int bCharLength)
		{
			int num = 0;
			int num2 = 0;
			int num3 = a.Length;
			if (num3 > bCharLength)
			{
				num3 = bCharLength;
			}
			if (bCharLength == 0)
			{
				if (a.Length != 0)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				fixed (byte[] array = bytes)
				{
					byte* ptr;
					if (bytes == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					byte* ptr2 = ptr;
					while (num < num3 && num2 == 0)
					{
						int num4 = (int)(*ptr2) | ((int)ptr2[1] << 8);
						num2 = (int)a[num++] - num4;
						ptr2 += 2;
					}
				}
				if (num2 != 0)
				{
					return num2;
				}
				return a.Length - bCharLength;
			}
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x000A912A File Offset: 0x000A732A
		[SecurityCritical]
		public static int CompareOrdinal(byte[] bytes, int aCharLength, string b)
		{
			return -FastResourceComparer.CompareOrdinal(b, bytes, aCharLength);
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x000A9138 File Offset: 0x000A7338
		[SecurityCritical]
		internal unsafe static int CompareOrdinal(byte* a, int byteLen, string b)
		{
			int num = 0;
			int num2 = 0;
			int num3 = byteLen >> 1;
			if (num3 > b.Length)
			{
				num3 = b.Length;
			}
			while (num2 < num3 && num == 0)
			{
				char c = (char)((int)(*(a++)) | ((int)(*(a++)) << 8));
				num = (int)(c - b[num2++]);
			}
			if (num != 0)
			{
				return num;
			}
			return byteLen - b.Length * 2;
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x000A9196 File Offset: 0x000A7396
		public FastResourceComparer()
		{
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x000A919E File Offset: 0x000A739E
		// Note: this type is marked as 'beforefieldinit'.
		static FastResourceComparer()
		{
		}

		// Token: 0x0400122A RID: 4650
		internal static readonly FastResourceComparer Default = new FastResourceComparer();
	}
}
