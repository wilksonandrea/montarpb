using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

internal class Class3 : GameClientPacket
{
	private List<SlotModel> list_0 = new List<SlotModel>();

	public Class3()
	{
	}

	public override void Server.Game.Network.GameClientPacket.Read()
	{
	}

	public override void Server.Game.Network.GameClientPacket.Run()
	{
		try
		{
			Account player = this.Client.Player;
			if (player != null)
			{
				RoomModel room = player.Room;
				if (room != null && room.LeaderSlot == player.SlotId && room.State == RoomState.READY)
				{
					lock (room.Slots)
					{
						for (int i = 0; i < 18; i++)
						{
							SlotModel slots = room.Slots[i];
							if (slots.PlayerId > 0L && i != room.LeaderSlot)
							{
								this.list_0.Add(slots);
							}
						}
					}
					if (this.list_0.Count <= 0)
					{
						this.Client.SendPacket(new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK((uint)-2147483648));
					}
					else
					{
						SlotModel ıtem = this.list_0[(new Random()).Next(this.list_0.Count)];
						if (room.GetPlayerBySlot(ıtem) == null)
						{
							this.Client.SendPacket(new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK((uint)-2147483648));
						}
						else
						{
							room.SetNewLeader(ıtem.Id, SlotState.EMPTY, room.LeaderSlot, false);
							using (PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK pROTOCOLROOMREQUESTMAINCHANGEACK = new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK(ıtem.Id))
							{
								room.SendPacketToPlayers(pROTOCOLROOMREQUESTMAINCHANGEACK);
							}
							room.UpdateSlotsInfo();
						}
						this.list_0 = null;
					}
				}
			}
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(string.Concat("ROOM_RANDOM_HOST2_REC: ", exception.Message), LoggerType.Error, exception);
		}
	}
}