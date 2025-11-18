using System;
using System.Runtime.CompilerServices;
using Plugin.Core.Utility;

namespace Plugin.Core.Models
{
	// Token: 0x02000083 RID: 131
	public class AccountStatus
	{
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x000053C0 File Offset: 0x000035C0
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x000053C8 File Offset: 0x000035C8
		public long PlayerId
		{
			[CompilerGenerated]
			get
			{
				return this.long_0;
			}
			[CompilerGenerated]
			set
			{
				this.long_0 = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x000053D1 File Offset: 0x000035D1
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x000053D9 File Offset: 0x000035D9
		public byte ChannelId
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

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x000053E2 File Offset: 0x000035E2
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x000053EA File Offset: 0x000035EA
		public byte RoomId
		{
			[CompilerGenerated]
			get
			{
				return this.byte_1;
			}
			[CompilerGenerated]
			set
			{
				this.byte_1 = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x000053F3 File Offset: 0x000035F3
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x000053FB File Offset: 0x000035FB
		public byte ClanMatchId
		{
			[CompilerGenerated]
			get
			{
				return this.byte_2;
			}
			[CompilerGenerated]
			set
			{
				this.byte_2 = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00005404 File Offset: 0x00003604
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x0000540C File Offset: 0x0000360C
		public byte ServerId
		{
			[CompilerGenerated]
			get
			{
				return this.byte_3;
			}
			[CompilerGenerated]
			set
			{
				this.byte_3 = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00005415 File Offset: 0x00003615
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x0000541D File Offset: 0x0000361D
		public byte[] Buffer
		{
			[CompilerGenerated]
			get
			{
				return this.byte_4;
			}
			[CompilerGenerated]
			set
			{
				this.byte_4 = value;
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00005426 File Offset: 0x00003626
		public AccountStatus()
		{
			this.Buffer = new byte[4];
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001B830 File Offset: 0x00019A30
		public void ResetData(long PlayerId)
		{
			if (PlayerId == 0L)
			{
				return;
			}
			byte channelId = this.ChannelId;
			int roomId = (int)this.RoomId;
			int clanMatchId = (int)this.ClanMatchId;
			int serverId = (int)this.ServerId;
			this.SetData(uint.MaxValue, PlayerId);
			if (channelId != this.ChannelId || roomId != (int)this.RoomId || clanMatchId != (int)this.ClanMatchId || serverId != (int)this.ServerId)
			{
				ComDiv.UpdateDB("accounts", "status", 4294967295L, "player_id", PlayerId);
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0000543A File Offset: 0x0000363A
		public void SetData(uint Data, long PlayerId)
		{
			this.SetData(BitConverter.GetBytes(Data), PlayerId);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00005449 File Offset: 0x00003649
		public void SetData(byte[] Buffer, long PlayerId)
		{
			this.PlayerId = PlayerId;
			this.Buffer = Buffer;
			this.ChannelId = Buffer[0];
			this.RoomId = Buffer[1];
			this.ServerId = Buffer[2];
			this.ClanMatchId = Buffer[3];
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0000547D File Offset: 0x0000367D
		public void UpdateChannel(byte ChannelId)
		{
			this.ChannelId = ChannelId;
			this.Buffer[0] = ChannelId;
			this.method_0();
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00005495 File Offset: 0x00003695
		public void UpdateRoom(byte RoomId)
		{
			this.RoomId = RoomId;
			this.Buffer[1] = RoomId;
			this.method_0();
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000054AD File Offset: 0x000036AD
		public void UpdateServer(byte ServerId)
		{
			this.ServerId = ServerId;
			this.Buffer[2] = ServerId;
			this.method_0();
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x000054C5 File Offset: 0x000036C5
		public void UpdateClanMatch(byte ClanMatchId)
		{
			this.ClanMatchId = ClanMatchId;
			this.Buffer[3] = ClanMatchId;
			this.method_0();
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001B8B0 File Offset: 0x00019AB0
		private void method_0()
		{
			uint num = BitConverter.ToUInt32(this.Buffer, 0);
			ComDiv.UpdateDB("accounts", "status", (long)((ulong)num), "player_id", this.PlayerId);
		}

		// Token: 0x0400025A RID: 602
		[CompilerGenerated]
		private long long_0;

		// Token: 0x0400025B RID: 603
		[CompilerGenerated]
		private byte byte_0;

		// Token: 0x0400025C RID: 604
		[CompilerGenerated]
		private byte byte_1;

		// Token: 0x0400025D RID: 605
		[CompilerGenerated]
		private byte byte_2;

		// Token: 0x0400025E RID: 606
		[CompilerGenerated]
		private byte byte_3;

		// Token: 0x0400025F RID: 607
		[CompilerGenerated]
		private byte[] byte_4;
	}
}
