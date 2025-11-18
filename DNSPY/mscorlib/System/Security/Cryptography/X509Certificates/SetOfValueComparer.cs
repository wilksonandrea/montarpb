using System;
using System.Collections.Generic;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002D2 RID: 722
	internal sealed class SetOfValueComparer : IComparer<ReadOnlyMemory<byte>>
	{
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06002575 RID: 9589 RVA: 0x00088AE7 File Offset: 0x00086CE7
		internal static SetOfValueComparer Instance
		{
			get
			{
				return SetOfValueComparer._instance;
			}
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x00088AEE File Offset: 0x00086CEE
		public int Compare(ReadOnlyMemory<byte> x, ReadOnlyMemory<byte> y)
		{
			return SetOfValueComparer.Compare(x.Span, y.Span);
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x00088B04 File Offset: 0x00086D04
		internal static int Compare(ReadOnlySpan<byte> x, ReadOnlySpan<byte> y)
		{
			int num = Math.Min(x.Length, y.Length);
			for (int i = 0; i < num; i++)
			{
				int num2 = (int)x[i];
				byte b = y[i];
				int num3 = num2 - (int)b;
				if (num3 != 0)
				{
					return num3;
				}
			}
			return x.Length - y.Length;
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x00088B5F File Offset: 0x00086D5F
		public SetOfValueComparer()
		{
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x00088B67 File Offset: 0x00086D67
		// Note: this type is marked as 'beforefieldinit'.
		static SetOfValueComparer()
		{
		}

		// Token: 0x04000E23 RID: 3619
		private static SetOfValueComparer _instance = new SetOfValueComparer();
	}
}
