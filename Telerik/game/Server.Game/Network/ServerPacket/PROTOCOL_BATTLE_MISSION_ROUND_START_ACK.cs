using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_MISSION_ROUND_START_ACK : GameServerPacket
	{
		private readonly RoomModel roomModel_0;

		public PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
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
			base.WriteH(5153);
			base.WriteC((byte)this.roomModel_0.Rounds);
			base.WriteD(this.roomModel_0.GetInBattleTimeLeft());
			base.WriteD(AllUtils.GetSlotsFlag(this.roomModel_0, true, false));
			if (this.roomModel_0.SwapRound)
			{
				obj = 3;
			}
			else
			{
				obj = null;
			}
			base.WriteC((byte)obj);
			base.WriteH((ushort)this.roomModel_0.FRRounds);
			base.WriteH((ushort)this.roomModel_0.CTRounds);
			if (this.roomModel_0.SwapRound)
			{
				base.WriteB(this.method_0(this.roomModel_0));
			}
		}
	}
}