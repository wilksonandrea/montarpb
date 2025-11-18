using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ : GameClientPacket
	{
		private ushort ushort_0;

		private ushort ushort_1;

		private List<ushort> list_0 = new List<ushort>();

		private List<ushort> list_1 = new List<ushort>();

		public PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ()
		{
		}

		public override void Read()
		{
			this.ushort_0 = base.ReadUH();
			this.ushort_1 = base.ReadUH();
			for (int i = 0; i < 18; i++)
			{
				this.list_0.Add(base.ReadUH());
			}
			for (int j = 0; j < 18; j++)
			{
				this.list_1.Add(base.ReadUH());
			}
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && !room.RoundTime.IsTimer() && room.State == RoomState.BATTLE)
					{
						SlotModel slot = room.GetSlot(player.SlotId);
						if (slot != null)
						{
							if (slot.State != SlotState.BATTLE)
							{
								return;
							}
							room.Bar1 = this.ushort_0;
							room.Bar2 = this.ushort_1;
							for (int i = 0; i < 18; i++)
							{
								SlotModel slots = room.Slots[i];
								if (slots.PlayerId > 0L && slots.State == SlotState.BATTLE)
								{
									slots.DamageBar1 = this.list_0[i];
									slots.DamageBar2 = this.list_1[i];
								}
							}
							using (PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK pROTOCOLBATTLEMISSIONDEFENCEINFOACK = new PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK(room))
							{
								room.SendPacketToPlayers(pROTOCOLBATTLEMISSIONDEFENCEINFOACK, SlotState.BATTLE, 0);
							}
							if (this.ushort_0 == 0 && this.ushort_1 == 0)
							{
								RoomSadeSync.EndRound(room, (!room.SwapRound ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM));
								goto Label1;
							}
							else
							{
								goto Label1;
							}
						}
						return;
					}
				Label1:
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}