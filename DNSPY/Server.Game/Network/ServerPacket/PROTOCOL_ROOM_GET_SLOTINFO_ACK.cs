using System;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000F7 RID: 247
	public class PROTOCOL_ROOM_GET_SLOTINFO_ACK : GameServerPacket
	{
		// Token: 0x06000258 RID: 600 RVA: 0x00004766 File Offset: 0x00002966
		public PROTOCOL_ROOM_GET_SLOTINFO_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
			if (roomModel_1 != null && roomModel_1.GetLeader() == null)
			{
				roomModel_1.SetNewLeader(-1, SlotState.EMPTY, roomModel_1.LeaderSlot, false);
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00012AE0 File Offset: 0x00010CE0
		public override void Write()
		{
			base.WriteH(3595);
			base.WriteC((byte)this.roomModel_0.LeaderSlot);
			base.WriteB(this.method_0(this.roomModel_0));
			base.WriteB(this.method_1(this.roomModel_0));
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00012B30 File Offset: 0x00010D30
		private byte[] method_0(RoomModel roomModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				foreach (SlotModel slotModel in roomModel_1.Slots)
				{
					syncServerPacket.WriteH(35);
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
						syncServerPacket.WriteC((byte)slotModel.ViewType);
						syncServerPacket.WriteC((byte)this.NATIONS);
						syncServerPacket.WriteC(0);
						syncServerPacket.WriteD(playerBySlot.Equipment.NameCardId);
						syncServerPacket.WriteC((byte)playerBySlot.Bonus.NickBorderColor);
						syncServerPacket.WriteC((byte)playerBySlot.AuthLevel());
						syncServerPacket.WriteC((byte)(clan.Name.Length * 2));
						syncServerPacket.WriteU(clan.Name, clan.Name.Length * 2);
					}
					else
					{
						syncServerPacket.WriteB(new byte[10]);
						syncServerPacket.WriteD(uint.MaxValue);
						syncServerPacket.WriteB(new byte[21]);
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00012CE8 File Offset: 0x00010EE8
		private byte[] method_1(RoomModel roomModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				foreach (SlotModel slotModel in roomModel_1.Slots)
				{
					syncServerPacket.WriteC((byte)roomModel_1.ValidateTeam(slotModel.Team, slotModel.CostumeTeam));
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x040001CC RID: 460
		private readonly RoomModel roomModel_0;
	}
}
