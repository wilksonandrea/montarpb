using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BASE_URL_LIST_REQ : GameClientPacket
	{
		public PROTOCOL_BASE_URL_LIST_REQ()
		{
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			try
			{
				ServerConfig config = GameXender.Client.Config;
				if (config != null & config.OfficialBannerEnabled)
				{
					this.Client.SendPacket(new PROTOCOL_BASE_URL_LIST_ACK(config));
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