using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth;
using Server.Auth.Network;
using Server.Auth.Network.ServerPacket;
using System;

namespace Server.Auth.Network.ClientPacket
{
	public class PROTOCOL_MATCH_CLAN_SEASON_REQ : AuthClientPacket
	{
		public PROTOCOL_MATCH_CLAN_SEASON_REQ()
		{
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_MATCH_CLAN_SEASON_ACK(0));
				this.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK());
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_MATCH_CLAN_SEASON_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}