using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200027E RID: 638
	public sealed class RSASignaturePadding : IEquatable<RSASignaturePadding>
	{
		// Token: 0x060022A9 RID: 8873 RVA: 0x0007CAA3 File Offset: 0x0007ACA3
		private RSASignaturePadding(RSASignaturePaddingMode mode)
		{
			this._mode = mode;
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060022AA RID: 8874 RVA: 0x0007CAB2 File Offset: 0x0007ACB2
		public static RSASignaturePadding Pkcs1
		{
			get
			{
				return RSASignaturePadding.s_pkcs1;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x0007CAB9 File Offset: 0x0007ACB9
		public static RSASignaturePadding Pss
		{
			get
			{
				return RSASignaturePadding.s_pss;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060022AC RID: 8876 RVA: 0x0007CAC0 File Offset: 0x0007ACC0
		public RSASignaturePaddingMode Mode
		{
			get
			{
				return this._mode;
			}
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x0007CAC8 File Offset: 0x0007ACC8
		public override int GetHashCode()
		{
			return this._mode.GetHashCode();
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x0007CAE9 File Offset: 0x0007ACE9
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RSASignaturePadding);
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x0007CAF7 File Offset: 0x0007ACF7
		public bool Equals(RSASignaturePadding other)
		{
			return other != null && this._mode == other._mode;
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x0007CB12 File Offset: 0x0007AD12
		public static bool operator ==(RSASignaturePadding left, RSASignaturePadding right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x0007CB23 File Offset: 0x0007AD23
		public static bool operator !=(RSASignaturePadding left, RSASignaturePadding right)
		{
			return !(left == right);
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x0007CB30 File Offset: 0x0007AD30
		public override string ToString()
		{
			return this._mode.ToString();
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x0007CB51 File Offset: 0x0007AD51
		// Note: this type is marked as 'beforefieldinit'.
		static RSASignaturePadding()
		{
		}

		// Token: 0x04000C96 RID: 3222
		private static readonly RSASignaturePadding s_pkcs1 = new RSASignaturePadding(RSASignaturePaddingMode.Pkcs1);

		// Token: 0x04000C97 RID: 3223
		private static readonly RSASignaturePadding s_pss = new RSASignaturePadding(RSASignaturePaddingMode.Pss);

		// Token: 0x04000C98 RID: 3224
		private readonly RSASignaturePaddingMode _mode;
	}
}
