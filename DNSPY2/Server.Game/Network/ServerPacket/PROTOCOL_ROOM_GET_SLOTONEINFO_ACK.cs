using System;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000F8 RID: 248
	public class PROTOCOL_ROOM_GET_SLOTONEINFO_ACK : GameServerPacket
	{
		// Token: 0x0600025C RID: 604 RVA: 0x0000478F File Offset: 0x0000298F
		public PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(Account account_1)
		{
			this.account_0 = account_1;
			if (account_1 != null)
			{
				this.roomModel_0 = account_1.Room;
				this.clanModel_0 = ClanManager.GetClan(account_1.ClanId);
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000047BE File Offset: 0x000029BE
		public PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(Account account_1, ClanModel clanModel_1)
		{
			this.account_0 = account_1;
			if (account_1 != null)
			{
				this.roomModel_0 = account_1.Room;
			}
			this.clanModel_0 = clanModel_1;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00012D54 File Offset: 0x00010F54
		public override void Write()
		{
			base.WriteH(3588);
			base.WriteH(0);
			base.WriteC((byte)this.roomModel_0.GetSlot(this.account_0.SlotId).Team);
			base.WriteC((byte)this.roomModel_0.GetSlot(this.account_0.SlotId).State);
			base.WriteC((byte)this.account_0.GetRank());
			base.WriteD(this.clanModel_0.Id);
			base.WriteD(this.account_0.ClanAccess);
			base.WriteC((byte)this.clanModel_0.Rank);
			base.WriteD(this.clanModel_0.Logo);
			base.WriteC((byte)this.account_0.CafePC);
			base.WriteC((byte)this.account_0.Country);
			base.WriteQ((long)this.account_0.Effects);
			base.WriteC((byte)this.clanModel_0.Effect);
			base.WriteC((byte)this.roomModel_0.GetSlot(this.account_0.SlotId).ViewType);
			base.WriteC((byte)this.NATIONS);
			base.WriteC(0);
			base.WriteD(this.account_0.Equipment.NameCardId);
			base.WriteC((byte)this.account_0.Bonus.NickBorderColor);
			base.WriteC((byte)this.account_0.AuthLevel());
			base.WriteU(this.clanModel_0.Name, 34);
			base.WriteC((byte)this.account_0.SlotId);
			base.WriteU(this.account_0.Nickname, 66);
			base.WriteC((byte)this.account_0.NickColor);
			base.WriteC((byte)this.account_0.Bonus.MuzzleColor);
			base.WriteC(0);
			base.WriteC(byte.MaxValue);
			base.WriteC(byte.MaxValue);
		}

		// Token: 0x040001CD RID: 461
		private readonly Account account_0;

		// Token: 0x040001CE RID: 462
		private readonly RoomModel roomModel_0;

		// Token: 0x040001CF RID: 463
		private readonly ClanModel clanModel_0;
	}
}
