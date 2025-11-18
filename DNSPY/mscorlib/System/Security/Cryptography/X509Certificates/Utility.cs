using System;
using System.Text;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002D9 RID: 729
	internal static class Utility
	{
		// Token: 0x060025A9 RID: 9641 RVA: 0x00089304 File Offset: 0x00087504
		public static Span<T> GetSpanForArray<T>(T[] array, int offset)
		{
			return Utility.GetSpanForArray<T>(array, offset, array.Length - offset);
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x00089312 File Offset: 0x00087512
		public static Span<T> GetSpanForArray<T>(T[] array, int offset, int count)
		{
			return new Span<T>(array, offset, count);
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x0008931C File Offset: 0x0008751C
		public static int EncodingGetByteCount(Encoding encoding, ReadOnlySpan<char> input)
		{
			if (input.IsNull)
			{
				return encoding.GetByteCount(new char[0]);
			}
			ArraySegment<char> arraySegment = input.DangerousGetArraySegment();
			return encoding.GetByteCount(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x00089364 File Offset: 0x00087564
		public static int EncodingGetBytes(Encoding encoding, char[] input, Span<byte> destination)
		{
			ArraySegment<byte> arraySegment = destination.DangerousGetArraySegment();
			return encoding.GetBytes(input, 0, input.Length, arraySegment.Array, arraySegment.Offset);
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x00089394 File Offset: 0x00087594
		public static int EncodingGetBytes(Encoding encoding, ReadOnlySpan<char> input, Span<byte> destination)
		{
			ArraySegment<byte> arraySegment = destination.DangerousGetArraySegment();
			ArraySegment<char> arraySegment2 = input.DangerousGetArraySegment();
			return encoding.GetBytes(arraySegment2.Array, arraySegment2.Offset, arraySegment2.Count, arraySegment.Array, arraySegment.Offset);
		}
	}
}
