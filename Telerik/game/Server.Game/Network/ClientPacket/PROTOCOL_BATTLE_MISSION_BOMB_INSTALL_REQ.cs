using Plugin.Core;
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
	public class PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ : GameClientPacket
	{
		private int int_0;

		private float float_0;

		private float float_1;

		private float float_2;

		private byte byte_0;

		private int int_1;

		public PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadD();
			this.byte_0 = base.ReadC();
			this.int_1 = base.ReadD();
			this.float_0 = base.ReadT();
			this.float_1 = base.ReadT();
			this.float_2 = base.ReadT();
		}

		public override void Run()
		{
			object obj;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && !room.RoundTime.IsTimer() && room.State == RoomState.BATTLE && !room.ActiveC4)
					{
						SlotModel slot = room.GetSlot(this.int_0);
						if (slot != null)
						{
							if (slot.State != SlotState.BATTLE)
							{
								return;
							}
							RoomModel roomModel = room;
							SlotModel slotModel = slot;
							byte byte0 = this.byte_0;
							if (this.int_1 == 0)
							{
								obj = 42;
							}
							else
							{
								obj = null;
							}
							RoomBombC4.InstallBomb(roomModel, slotModel, byte0, (ushort)obj, this.float_0, this.float_1, this.float_2);
							goto Label1;
						}
						return;
					}
				Label1:
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}