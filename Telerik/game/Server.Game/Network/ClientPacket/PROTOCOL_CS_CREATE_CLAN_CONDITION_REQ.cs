using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CS_CREATE_CLAN_CONDITION_REQ : GameClientPacket
	{
		public PROTOCOL_CS_CREATE_CLAN_CONDITION_REQ()
		{
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_CS_CREATE_CLAN_CONDITION_ACK());
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CS_CREATE_CLAN_CONDITION_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}