using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_GM_LOG_LOBBY_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_GM_LOG_LOBBY_REQ()
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
				if (player != null && player.IsGM())
				{
					long playerId = player.GetChannel().GetPlayer(this.int_0).PlayerId;
					if (playerId > 0L)
					{
						this.Client.SendPacket(new PROTOCOL_GM_LOG_LOBBY_ACK(0, playerId));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_GM_LOG_LOBBY_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}