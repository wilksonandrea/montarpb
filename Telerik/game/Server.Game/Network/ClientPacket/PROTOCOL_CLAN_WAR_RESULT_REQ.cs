using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CLAN_WAR_RESULT_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_CLAN_WAR_RESULT_REQ()
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
				this.Client.SendPacket(new PROTOCOL_CLAN_WAR_RESULT_ACK());
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CLAN_WAR_RESULT_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}