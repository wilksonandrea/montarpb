using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		public override void Run()
		{
			Account player = this.Client.Player;
			if (player == null)
			{
				return;
			}
			RoomModel room = player.Room;
			if (room != null && !room.RoundTime.IsTimer() && room.State == RoomState.BATTLE && room.ActiveC4)
			{
				SlotModel slot = room.GetSlot(this.int_0);
				if (slot != null)
				{
					if (slot.State != SlotState.BATTLE)
					{
						return;
					}
					RoomBombC4.UninstallBomb(room, slot);
					return;
				}
				return;
			}
		}
	}
}