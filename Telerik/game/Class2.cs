using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

internal class Class2 : GameClientPacket
{
	private uint uint_0;

	private List<SlotModel> list_0 = new List<SlotModel>();

	public Class2()
	{
	}

	public override void Server.Game.Network.GameClientPacket.Read()
	{
	}

	public override void Server.Game.Network.GameClientPacket.Run()
	{
		uint ıd;
		try
		{
			Account player = this.Client.Player;
			if (player != null)
			{
				RoomModel room = player.Room;
				if (room == null || room.LeaderSlot != player.SlotId || room.State != RoomState.READY)
				{
					this.uint_0 = -2147483648;
				}
				else
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
						this.uint_0 = -2147483648;
					}
					else
					{
						int ınt32 = (new Random()).Next(this.list_0.Count);
						SlotModel ıtem = this.list_0[ınt32];
						if (room.GetPlayerBySlot(ıtem) != null)
						{
							ıd = (uint)ıtem.Id;
						}
						else
						{
							ıd = -2147483648;
						}
						this.uint_0 = ıd;
						this.list_0 = null;
					}
				}
				this.Client.SendPacket(new PROTOCOL_ROOM_CHECK_MAIN_ACK(this.uint_0));
			}
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(string.Concat("PROTOCOL_ROOM_CHECK_MAIN_REQ: ", exception.Message), LoggerType.Error, exception);
		}
	}
}