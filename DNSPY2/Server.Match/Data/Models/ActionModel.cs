using System;
using System.Runtime.CompilerServices;
using Server.Match.Data.Enums;

namespace Server.Match.Data.Models
{
	// Token: 0x02000032 RID: 50
	public class ActionModel
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00002422 File Offset: 0x00000622
		// (set) Token: 0x060000DD RID: 221 RVA: 0x0000242A File Offset: 0x0000062A
		public ushort Slot
		{
			[CompilerGenerated]
			get
			{
				return this.ushort_0;
			}
			[CompilerGenerated]
			set
			{
				this.ushort_0 = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00002433 File Offset: 0x00000633
		// (set) Token: 0x060000DF RID: 223 RVA: 0x0000243B File Offset: 0x0000063B
		public ushort Length
		{
			[CompilerGenerated]
			get
			{
				return this.ushort_1;
			}
			[CompilerGenerated]
			set
			{
				this.ushort_1 = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00002444 File Offset: 0x00000644
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x0000244C File Offset: 0x0000064C
		public UdpGameEvent Flag
		{
			[CompilerGenerated]
			get
			{
				return this.udpGameEvent_0;
			}
			[CompilerGenerated]
			set
			{
				this.udpGameEvent_0 = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00002455 File Offset: 0x00000655
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x0000245D File Offset: 0x0000065D
		public UdpSubHead SubHead
		{
			[CompilerGenerated]
			get
			{
				return this.udpSubHead_0;
			}
			[CompilerGenerated]
			set
			{
				this.udpSubHead_0 = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00002466 File Offset: 0x00000666
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000246E File Offset: 0x0000066E
		public byte[] Data
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

		// Token: 0x060000E6 RID: 230 RVA: 0x000020A2 File Offset: 0x000002A2
		public ActionModel()
		{
		}

		// Token: 0x0400001D RID: 29
		[CompilerGenerated]
		private ushort ushort_0;

		// Token: 0x0400001E RID: 30
		[CompilerGenerated]
		private ushort ushort_1;

		// Token: 0x0400001F RID: 31
		[CompilerGenerated]
		private UdpGameEvent udpGameEvent_0;

		// Token: 0x04000020 RID: 32
		[CompilerGenerated]
		private UdpSubHead udpSubHead_0;

		// Token: 0x04000021 RID: 33
		[CompilerGenerated]
		private byte[] byte_0;
	}
}
