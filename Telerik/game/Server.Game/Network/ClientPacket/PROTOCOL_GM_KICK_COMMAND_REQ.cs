using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;
using System.Reflection;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_GM_KICK_COMMAND_REQ : GameClientPacket
	{
		private byte byte_0;

		public PROTOCOL_GM_KICK_COMMAND_REQ()
		{
		}

		public override void Read()
		{
			this.byte_0 = base.ReadC();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null)
					{
						Account playerBySlot = room.GetPlayerBySlot((int)this.byte_0);
						if (playerBySlot != null && !playerBySlot.IsGM())
						{
							room.RemovePlayer(playerBySlot, true, 0);
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat(base.GetType().Name, ": ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}