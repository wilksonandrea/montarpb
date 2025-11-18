using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK : GameServerPacket
	{
		private readonly RoomModel roomModel_0;

		public PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
		}

		public int GetSlotKill()
		{
			int[] allKills = new int[18];
			for (int i = 0; i < (int)allKills.Length; i++)
			{
				allKills[i] = this.roomModel_0.Slots[i].AllKills;
			}
			int ınt32 = 0;
			for (int j = 0; j < (int)allKills.Length; j++)
			{
				if (allKills[j] > allKills[ınt32])
				{
					ınt32 = j;
				}
			}
			return ınt32;
		}

		private byte[] method_0(RoomModel roomModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				if (roomModel_1.IsDinoMode("DE"))
				{
					syncServerPacket.WriteD(roomModel_1.FRDino);
					syncServerPacket.WriteD(roomModel_1.CTDino);
				}
				else if (roomModel_1.RoomType == RoomCondition.DeathMatch && !roomModel_1.IsBotMode())
				{
					syncServerPacket.WriteD(roomModel_1.FRKills);
					syncServerPacket.WriteD(roomModel_1.CTKills);
				}
				else if (roomModel_1.RoomType == RoomCondition.FreeForAll)
				{
					syncServerPacket.WriteD(this.GetSlotKill());
					syncServerPacket.WriteD(0);
				}
				else if (!roomModel_1.IsBotMode())
				{
					syncServerPacket.WriteD(roomModel_1.FRRounds);
					syncServerPacket.WriteD(roomModel_1.CTRounds);
				}
				else
				{
					syncServerPacket.WriteD((int)roomModel_1.IngameAiLevel);
					syncServerPacket.WriteD(0);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private int method_1()
		{
			if (this.roomModel_0.IsBotMode())
			{
				return 3;
			}
			if (this.roomModel_0.RoomType == RoomCondition.DeathMatch && !this.roomModel_0.IsBotMode())
			{
				return 1;
			}
			if (this.roomModel_0.IsDinoMode(""))
			{
				return 4;
			}
			if (this.roomModel_0.RoomType == RoomCondition.FreeForAll)
			{
				return 5;
			}
			return 2;
		}

		public override void Write()
		{
			base.WriteH(5263);
			base.WriteD(this.method_1());
			base.WriteD(this.roomModel_0.GetTimeByMask() * 60 - this.roomModel_0.GetInBattleTime());
			base.WriteB(this.method_0(this.roomModel_0));
		}
	}
}