using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK : GameServerPacket
	{
		private readonly RoomModel roomModel_0;

		private readonly List<int> list_0;

		public PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(RoomModel roomModel_1, List<int> list_1)
		{
			this.roomModel_0 = roomModel_1;
			this.list_0 = list_1;
		}

		private byte[] method_0(RoomModel roomModel_1, List<int> list_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				if (roomModel_1.IsBotMode())
				{
					syncServerPacket.WriteB(Bitwise.HexStringToByteArray("FF FF FF FF FF FF FF FF FF FF"));
				}
				else if (!roomModel_1.IsDinoMode(""))
				{
					syncServerPacket.WriteB(new byte[10]);
				}
				else
				{
					int ınt32 = (list_1.Count == 1 || roomModel_1.IsDinoMode("CC") ? 255 : roomModel_1.TRex);
					syncServerPacket.WriteC((byte)ınt32);
					syncServerPacket.WriteC(10);
					for (int i = 0; i < list_1.Count; i++)
					{
						int ıtem = list_1[i];
						if (ıtem != roomModel_1.TRex && roomModel_1.IsDinoMode("DE") || roomModel_1.IsDinoMode("CC"))
						{
							syncServerPacket.WriteC((byte)ıtem);
						}
					}
					int count = 8 - list_1.Count - (ınt32 == 255);
					for (int j = 0; j < count; j++)
					{
						syncServerPacket.WriteC(255);
					}
					syncServerPacket.WriteC(255);
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
					PlayerEquipment equipment = slotModel.Equipment;
					if (equipment != null)
					{
						if (slotModel.Team == TeamEnum.FR_TEAM)
						{
							syncServerPacket.WriteD((!roomModel_1.SwapRound ? equipment.CharaRedId : equipment.CharaBlueId));
						}
						else if (slotModel.Team == TeamEnum.CT_TEAM)
						{
							syncServerPacket.WriteD((!roomModel_1.SwapRound ? equipment.CharaBlueId : equipment.CharaRedId));
						}
					}
					else if (slotModel.Team == TeamEnum.FR_TEAM)
					{
						syncServerPacket.WriteD((!roomModel_1.SwapRound ? 601001 : 602002));
					}
					else if (slotModel.Team == TeamEnum.CT_TEAM)
					{
						syncServerPacket.WriteD((!roomModel_1.SwapRound ? 602002 : 601001));
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		public override void Write()
		{
			object obj;
			base.WriteH(5151);
			base.WriteD(AllUtils.GetSlotsFlag(this.roomModel_0, false, false));
			base.WriteB(this.method_0(this.roomModel_0, this.list_0));
			if (this.roomModel_0.SwapRound)
			{
				obj = 3;
			}
			else
			{
				obj = null;
			}
			base.WriteC((byte)obj);
			if (this.roomModel_0.SwapRound)
			{
				base.WriteB(this.method_1(this.roomModel_0));
			}
		}
	}
}