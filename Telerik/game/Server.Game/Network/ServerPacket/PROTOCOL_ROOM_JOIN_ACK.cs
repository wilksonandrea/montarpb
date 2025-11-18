using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_ROOM_JOIN_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		private readonly RoomModel roomModel_0;

		private readonly int int_0;

		public PROTOCOL_ROOM_JOIN_ACK(uint uint_1, Account account_0)
		{
			this.uint_0 = uint_1;
			if (account_0 != null)
			{
				this.int_0 = account_0.SlotId;
				this.roomModel_0 = account_0.Room;
			}
		}

		private byte[] method_0(RoomModel roomModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)((int)roomModel_1.Slots.Length));
				SlotModel[] slots = roomModel_1.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					syncServerPacket.WriteC((byte)slots[i].Team);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private byte[] method_1(RoomModel roomModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)((int)roomModel_1.Slots.Length));
				SlotModel[] slots = roomModel_1.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel = slots[i];
					syncServerPacket.WriteC((byte)slotModel.State);
					Account playerBySlot = roomModel_1.GetPlayerBySlot(slotModel);
					if (playerBySlot == null)
					{
						syncServerPacket.WriteB(new byte[10]);
						syncServerPacket.WriteD((uint)-1);
						syncServerPacket.WriteB(new byte[54]);
						syncServerPacket.WriteC((byte)slotModel.Id);
						syncServerPacket.WriteB(new byte[68]);
						syncServerPacket.WriteC(0);
						syncServerPacket.WriteC(255);
						syncServerPacket.WriteC(255);
					}
					else
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
						syncServerPacket.WriteC(255);
						syncServerPacket.WriteC(255);
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		public override void Write()
		{
			base.WriteH(3586);
			base.WriteH(0);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0)
			{
				lock (this.roomModel_0.Slots)
				{
					base.WriteB(this.method_0(this.roomModel_0));
					base.WriteB(this.method_1(this.roomModel_0));
					base.WriteC(this.roomModel_0.AiType);
					base.WriteC((this.roomModel_0.IsStartingMatch() ? this.roomModel_0.IngameAiLevel : this.roomModel_0.AiLevel));
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
	}
}