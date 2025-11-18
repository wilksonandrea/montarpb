using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000072 RID: 114
	public class ShopData
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00004BA6 File Offset: 0x00002DA6
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x00004BAE File Offset: 0x00002DAE
		public byte[] Buffer
		{
			[CompilerGenerated]
			get
			{
				return this.byte_0;
			}
			[CompilerGenerated]
			set
			{
				this.byte_0 = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00004BB7 File Offset: 0x00002DB7
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x00004BBF File Offset: 0x00002DBF
		public int ItemsCount
		{
			[CompilerGenerated]
			get
			{
				return this.int_0;
			}
			[CompilerGenerated]
			set
			{
				this.int_0 = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00004BC8 File Offset: 0x00002DC8
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x00004BD0 File Offset: 0x00002DD0
		public int Offset
		{
			[CompilerGenerated]
			get
			{
				return this.int_1;
			}
			[CompilerGenerated]
			set
			{
				this.int_1 = value;
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00002116 File Offset: 0x00000316
		public ShopData()
		{
		}

		// Token: 0x040001EA RID: 490
		[CompilerGenerated]
		private byte[] byte_0;

		// Token: 0x040001EB RID: 491
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040001EC RID: 492
		[CompilerGenerated]
		private int int_1;
	}
}
