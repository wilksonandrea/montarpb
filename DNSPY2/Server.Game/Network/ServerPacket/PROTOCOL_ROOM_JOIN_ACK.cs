using System;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000FE RID: 254
	public class PROTOCOL_ROOM_JOIN_ACK : GameServerPacket
	{
		// Token: 0x0600026C RID: 620 RVA: 0x00004892 File Offset: 0x00002A92
		public PROTOCOL_ROOM_JOIN_ACK(uint uint_1, Account account_0)
		{
			this.uint_0 = uint_1;
			if (account_0 != null)
			{
				this.int_0 = account_0.SlotId;
				this.roomModel_0 = account_0.Room;
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000134B4 File Offset: 0x000116B4
		public override void Write()
		{
			base.WriteH(3586);
			base.WriteH(0);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				SlotModel[] slots = this.roomModel_0.Slots;
				lock (slots)
				{
					base.WriteB(this.method_0(this.roomModel_0));
					base.WriteB(this.method_1(this.roomModel_0));
					base.WriteC(this.roomModel_0.AiType);
					base.WriteC(this.roomModel_0.IsStartingMatch() ? this.roomModel_0.IngameAiLevel : this.roomModel_0.AiLevel);
					base.WriteC(this.roomModel_0.AiCount);
					base.WriteC((byte)this.roomModel_0.GetAllPlayers().Count);
					base.WriteC((byte)this.roomModel_0.LeaderSlot);
					base.WriteC((byte)this.roomModel_0.CountdownTime.GetTimeLeft());
					base.WriteC((byte)this.roomModel_0.Password.Length);
					base.WriteS(this.roomModel_0.Password, this.roomModel_0.Password.Length);
					base.WriteB(new byte[17]);
					base.WriteU(this.roomModel_0.LeaderName, 66);
					base.WriteD(this.roomModel_0.KillTime);
					base.WriteC(this.roomModel_0.Limit);
					base.WriteC(this.roomModel_0.WatchRuleFlag);
					base.WriteH((ushort)this.roomModel_0.BalanceType);
					base.WriteB(this.roomModel_0.RandomMaps);
					base.WriteC(this.roomModel_0.CountdownIG);
					base.WriteB(this.roomModel_0.LeaderAddr);
					base.WriteC(this.roomModel_0.KillCam);
					base.WriteH(0);
					base.WriteD(this.roomModel_0.RoomId);
					base.WriteU(this.roomModel_0.Name, 46);
					base.WriteC((byte)this.roomModel_0.MapId);
					base.WriteC((byte)this.roomModel_0.Rule);
					base.WriteC((byte)this.roomModel_0.Stage);
					base.WriteC((byte)this.roomModel_0.RoomType);
					base.WriteC((byte)this.roomModel_0.State);
					base.WriteC((byte)this.roomModel_0.GetCountPlayers());
					base.WriteC((byte)this.roomModel_0.GetSlotCount());
					base.WriteC((byte)this.roomModel_0.Ping);
					base.WriteH((ushort)this.roomModel_0.WeaponsFlag);
					base.WriteD(this.roomModel_0.GetFlag());
					base.WriteH(0);
					base.WriteB(new byte[4]);
					base.WriteC(0);
					base.WriteC((byte)this.int_0);
				}
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000E108 File Offset: 0x0000C308
		private byte[] method_0(RoomModel roomModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)roomModel_1.Slots.Length);
				foreach (SlotModel slotModel in roomModel_1.Slots)
				{
					syncServerPacket.WriteC((byte)slotModel.Team);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000137C0 File Offset: 0x000119C0
		private byte[] method_1(RoomModel roomModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)roomModel_1.Slots.Length);
				foreach (SlotModel slotModel in roomModel_1.Slots)
				{
					syncServerPacket.WriteC((byte)slotModel.State);
					Account playerBySlot = roomModel_1.GetPlayerBySlot(slotModel);
					if (playerBySlot != null)
					{
						ClanModel clan = ClanManager.GetClan(playerBySlot.ClanId);
						syncServerPacket.WriteC((byte)playerBySlot.GetRank());
						syncServerPacket.WriteD(clan.Id);
						syncServerPacket.WriteD(playerBySlot.ClanAccess);
						syncServerPacket.WriteC((byte)clan.Rank);
						syncServerPacket.WriteD(clan.Logo);
						syncServerPacket.WriteC((byte)playerBySlot.CafePC);
						syncServerPacket.WriteC((byte)playerBySlot.Country);
						syncServerPacket.WriteQ((long)playerBySlot.Effects);
						syncServerPacket.WriteC((byte)clan.Effect);
						syncServerPacket.WriteC(0);
						syncServerPacket.WriteC((byte)this.NATIONS);
						syncServerPacket.WriteC(0);
						syncServerPacket.WriteD(playerBySlot.Equipment.NameCardId);
						syncServerPacket.WriteC((byte)playerBySlot.Bonus.NickBorderColor);
						syncServerPacket.WriteC((byte)playerBySlot.AuthLevel());
						syncServerPacket.WriteU(clan.Name, 34);
						syncServerPacket.WriteC((byte)playerBySlot.SlotId);
						syncServerPacket.WriteU(playerBySlot.Nickname, 66);
						syncServerPacket.WriteC((byte)playerBySlot.NickColor);
						syncServerPacket.WriteC((byte)playerBySlot.Bonus.MuzzleColor);
						syncServerPacket.WriteC(0);
						syncServerPacket.WriteC(byte.MaxValue);
						syncServerPacket.WriteC(byte.MaxValue);
					}
					else
					{
						syncServerPacket.WriteB(new byte[10]);
						syncServerPacket.WriteD(uint.MaxValue);
						syncServerPacket.WriteB(new byte[54]);
						syncServerPacket.WriteC((byte)slotModel.Id);
						syncServerPacket.WriteB(new byte[68]);
						syncServerPacket.WriteC(0);
						syncServerPacket.WriteC(byte.MaxValue);
						syncServerPacket.WriteC(byte.MaxValue);
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x040001D8 RID: 472
		private readonly uint uint_0;

		// Token: 0x040001D9 RID: 473
		private readonly RoomModel roomModel_0;

		// Token: 0x040001DA RID: 474
		private readonly int int_0;
	}
}
