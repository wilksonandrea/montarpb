using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_ROOM_GET_SLOTINFO_ACK : GameServerPacket
	{
		private readonly RoomModel roomModel_0;

		public PROTOCOL_ROOM_GET_SLOTINFO_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
			if (roomModel_1 != null && roomModel_1.GetLeader() == null)
			{
				roomModel_1.SetNewLeader(-1, SlotState.EMPTY, roomModel_1.LeaderSlot, false);
			}
		}

		private byte[] method_0(RoomModel roomModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				SlotModel[] slots = roomModel_1.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel = slots[i];
					syncServerPacket.WriteH(35);
					syncServerPacket.WriteC((byte)slotModel.State);
					Account playerBySlot = roomModel_1.GetPlayerBySlot(slotModel);
					if (playerBySlot == null)
					{
						syncServerPacket.WriteB(new byte[10]);
						syncServerPacket.WriteD((uint)-1);
						syncServerPacket.WriteB(new byte[21]);
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
						syncServerPacket.WriteC((byte)slotModel.ViewType);
						syncServerPacket.WriteC((byte)this.NATIONS);
						syncServerPacket.WriteC(0);
						syncServerPacket.WriteD(playerBySlot.Equipment.NameCardId);
						syncServerPacket.WriteC((byte)playerBySlot.Bonus.NickBorderColor);
						syncServerPacket.WriteC((byte)playerBySlot.AuthLevel());
						syncServerPacket.WriteC((byte)(clan.Name.Length * 2));
						syncServerPacket.WriteU(clan.Name, clan.Name.Length * 2);
					}
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
				SlotModel[] slots = roomModel_1.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel = slots[i];
					syncServerPacket.WriteC((byte)roomModel_1.ValidateTeam(slotModel.Team, slotModel.CostumeTeam));
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		public override void Write()
		{
			base.WriteH(3595);
			base.WriteC((byte)this.roomModel_0.LeaderSlot);
			base.WriteB(this.method_0(this.roomModel_0));
			base.WriteB(this.method_1(this.roomModel_0));
		}
	}
}