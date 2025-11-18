using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ChannelModel channel = player.GetChannel();
					if (channel != null)
					{
						RoomModel room = channel.GetRoom(this.int_0);
						if (room != null && room.GetLeader() != null)
						{
							this.Client.SendPacket(new PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK(room));
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}