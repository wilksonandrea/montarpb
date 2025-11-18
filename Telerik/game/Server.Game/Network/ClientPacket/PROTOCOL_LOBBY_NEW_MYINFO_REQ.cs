using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_LOBBY_NEW_MYINFO_REQ : GameClientPacket
	{
		public PROTOCOL_LOBBY_NEW_MYINFO_REQ()
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
					this.Client.SendPacket(new PROTOCOL_LOBBY_NEW_MYINFO_ACK(player));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_LOBBY_NEW_MYINFO_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}