using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ : GameClientPacket
	{
		public PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ()
		{
		}

		public override void Read()
		{
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
						this.Client.SendPacket(new PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK(channel));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}