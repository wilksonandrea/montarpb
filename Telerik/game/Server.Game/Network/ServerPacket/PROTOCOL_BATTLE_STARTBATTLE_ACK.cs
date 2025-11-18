using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_STARTBATTLE_ACK : GameServerPacket
	{
		private readonly RoomModel roomModel_0;

		private readonly SlotModel slotModel_0;

		private readonly bool bool_0;

		private readonly List<int> list_0;

		public PROTOCOL_BATTLE_STARTBATTLE_ACK(SlotModel slotModel_1, Account account_0, List<int> list_1, bool bool_1)
		{
			this.slotModel_0 = slotModel_1;
			this.roomModel_0 = account_0.Room;
			if (this.roomModel_0 != null)
			{
				this.bool_0 = bool_1;
				this.list_0 = list_1;
				if (!this.roomModel_0.IsBotMode() && this.roomModel_0.RoomType != RoomCondition.Tutorial)
				{
					AllUtils.CompleteMission(this.roomModel_0, account_0, slotModel_1, (bool_1 ? MissionType.STAGE_ENTER : MissionType.STAGE_INTERCEPT), 0);
				}
			}
		}

		public PROTOCOL_BATTLE_STARTBATTLE_ACK()
		{
		}

		private byte[] method_0(RoomModel roomModel_1, List<int> list_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				if (!roomModel_1.IsDinoMode(""))
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

		public override void Write()
		{
			object obj;
			object fRDino;
			object cTDino;
			object obj1;
			base.WriteH(5132);
			base.WriteH(0);
			base.WriteD(0);
			base.WriteC(0);
			base.WriteB(this.method_0(this.roomModel_0, this.list_0));
			base.WriteC((byte)this.roomModel_0.Rounds);
			base.WriteD(AllUtils.GetSlotsFlag(this.roomModel_0, true, false));
			if (this.roomModel_0.ThisModeHaveRounds() || this.roomModel_0.IsDinoMode("") || this.roomModel_0.RoomType == RoomCondition.FreeForAll)
			{
				obj = 2;
			}
			else
			{
				obj = null;
			}
			base.WriteC((byte)obj);
			if (this.roomModel_0.ThisModeHaveRounds() || this.roomModel_0.IsDinoMode("") || this.roomModel_0.RoomType == RoomCondition.FreeForAll)
			{
				if (this.roomModel_0.IsDinoMode("DE"))
				{
					fRDino = this.roomModel_0.FRDino;
				}
				else
				{
					fRDino = (this.roomModel_0.IsDinoMode("CC") ? this.roomModel_0.FRKills : this.roomModel_0.FRRounds);
				}
				base.WriteH((ushort)fRDino);
				if (this.roomModel_0.IsDinoMode("DE"))
				{
					cTDino = this.roomModel_0.CTDino;
				}
				else
				{
					cTDino = (this.roomModel_0.IsDinoMode("CC") ? this.roomModel_0.CTKills : this.roomModel_0.CTRounds);
				}
				base.WriteH((ushort)cTDino);
			}
			if (this.roomModel_0.ThisModeHaveRounds() || this.roomModel_0.IsDinoMode("") || this.roomModel_0.RoomType == RoomCondition.FreeForAll)
			{
				obj1 = 2;
			}
			else
			{
				obj1 = null;
			}
			base.WriteC((byte)obj1);
			if (this.roomModel_0.ThisModeHaveRounds() || this.roomModel_0.IsDinoMode("") || this.roomModel_0.RoomType == RoomCondition.FreeForAll)
			{
				base.WriteH(0);
				base.WriteH(0);
			}
			base.WriteD(AllUtils.GetSlotsFlag(this.roomModel_0, false, false));
			base.WriteC((byte)(!this.bool_0));
			base.WriteC((byte)this.slotModel_0.Id);
		}
	}
}