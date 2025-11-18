using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200026A RID: 618
	public struct HashAlgorithmName : IEquatable<HashAlgorithmName>
	{
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060021EC RID: 8684 RVA: 0x00078000 File Offset: 0x00076200
		public static HashAlgorithmName MD5
		{
			get
			{
				return new HashAlgorithmName("MD5");
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x0007800C File Offset: 0x0007620C
		public static HashAlgorithmName SHA1
		{
			get
			{
				return new HashAlgorithmName("SHA1");
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060021EE RID: 8686 RVA: 0x00078018 File Offset: 0x00076218
		public static HashAlgorithmName SHA256
		{
			get
			{
				return new HashAlgorithmName("SHA256");
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060021EF RID: 8687 RVA: 0x00078024 File Offset: 0x00076224
		public static HashAlgorithmName SHA384
		{
			get
			{
				return new HashAlgorithmName("SHA384");
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x00078030 File Offset: 0x00076230
		public static HashAlgorithmName SHA512
		{
			get
			{
				return new HashAlgorithmName("SHA512");
			}
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x0007803C File Offset: 0x0007623C
		public HashAlgorithmName(string name)
		{
			this._name = name;
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060021F2 RID: 8690 RVA: 0x00078045 File Offset: 0x00076245
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x0007804D File Offset: 0x0007624D
		public override string ToString()
		{
			return this._name ?? string.Empty;
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x0007805E File Offset: 0x0007625E
		public override bool Equals(object obj)
		{
			return obj is HashAlgorithmName && this.Equals((HashAlgorithmName)obj);
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x00078076 File Offset: 0x00076276
		public bool Equals(HashAlgorithmName other)
		{
			return this._name == other._name;
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x00078089 File Offset: 0x00076289
		public override int GetHashCode()
		{
			if (this._name != null)
			{
				return this._name.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x000780A0 File Offset: 0x000762A0
		public static bool operator ==(HashAlgorithmName left, HashAlgorithmName right)
		{
			return left.Equals(right);
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x000780AA File Offset: 0x000762AA
		public static bool operator !=(HashAlgorithmName left, HashAlgorithmName right)
		{
			return !(left == right);
		}

		// Token: 0x04000C59 RID: 3161
		private readonly string _name;
	}
}
