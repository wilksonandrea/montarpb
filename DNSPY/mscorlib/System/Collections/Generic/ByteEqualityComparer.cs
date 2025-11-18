using System;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x020004C4 RID: 1220
	[Serializable]
	internal class ByteEqualityComparer : EqualityComparer<byte>
	{
		// Token: 0x06003A92 RID: 14994 RVA: 0x000DF26E File Offset: 0x000DD46E
		public override bool Equals(byte x, byte y)
		{
			return x == y;
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x000DF274 File Offset: 0x000DD474
		public override int GetHashCode(byte b)
		{
			return b.GetHashCode();
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x000DF280 File Offset: 0x000DD480
		[SecuritySafeCritical]
		internal unsafe override int IndexOf(byte[] array, byte value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			if (count > array.Length - startIndex)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (count == 0)
			{
				return -1;
			}
			byte* ptr;
			if (array == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return Buffer.IndexOfByte(ptr, value, startIndex, count);
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x000DF310 File Offset: 0x000DD510
		internal override int LastIndexOf(byte[] array, byte value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			for (int i = startIndex; i >= num; i--)
			{
				if (array[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x000DF33C File Offset: 0x000DD53C
		public override bool Equals(object obj)
		{
			ByteEqualityComparer byteEqualityComparer = obj as ByteEqualityComparer;
			return byteEqualityComparer != null;
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x000DF354 File Offset: 0x000DD554
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x000DF366 File Offset: 0x000DD566
		public ByteEqualityComparer()
		{
		}
	}
}
