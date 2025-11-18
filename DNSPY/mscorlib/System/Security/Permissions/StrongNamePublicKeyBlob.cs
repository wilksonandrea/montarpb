using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x0200030B RID: 779
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNamePublicKeyBlob
	{
		// Token: 0x0600276E RID: 10094 RVA: 0x0008F35A File Offset: 0x0008D55A
		internal StrongNamePublicKeyBlob()
		{
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x0008F362 File Offset: 0x0008D562
		public StrongNamePublicKeyBlob(byte[] publicKey)
		{
			if (publicKey == null)
			{
				throw new ArgumentNullException("PublicKey");
			}
			this.PublicKey = new byte[publicKey.Length];
			Array.Copy(publicKey, 0, this.PublicKey, 0, publicKey.Length);
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x0008F397 File Offset: 0x0008D597
		internal StrongNamePublicKeyBlob(string publicKey)
		{
			this.PublicKey = Hex.DecodeHexString(publicKey);
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x0008F3AC File Offset: 0x0008D5AC
		private static bool CompareArrays(byte[] first, byte[] second)
		{
			if (first.Length != second.Length)
			{
				return false;
			}
			int num = first.Length;
			for (int i = 0; i < num; i++)
			{
				if (first[i] != second[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x0008F3DE File Offset: 0x0008D5DE
		internal bool Equals(StrongNamePublicKeyBlob blob)
		{
			return blob != null && StrongNamePublicKeyBlob.CompareArrays(this.PublicKey, blob.PublicKey);
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x0008F3F6 File Offset: 0x0008D5F6
		public override bool Equals(object obj)
		{
			return obj != null && obj is StrongNamePublicKeyBlob && this.Equals((StrongNamePublicKeyBlob)obj);
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x0008F414 File Offset: 0x0008D614
		private static int GetByteArrayHashCode(byte[] baData)
		{
			if (baData == null)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < baData.Length; i++)
			{
				num = (num << 8) ^ (int)baData[i] ^ (num >> 24);
			}
			return num;
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x0008F444 File Offset: 0x0008D644
		public override int GetHashCode()
		{
			return StrongNamePublicKeyBlob.GetByteArrayHashCode(this.PublicKey);
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x0008F451 File Offset: 0x0008D651
		public override string ToString()
		{
			return Hex.EncodeHexString(this.PublicKey);
		}

		// Token: 0x04000F4A RID: 3914
		internal byte[] PublicKey;
	}
}
