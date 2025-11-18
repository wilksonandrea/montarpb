using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network;
using System;
using System.Reflection;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_REQ : GameClientPacket
	{
		public PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_REQ()
		{
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			try
			{
				CLogger.Print(string.Concat(base.GetType().Name, " CALLLED!"), LoggerType.Warning, null);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_MATCH_CLAN_SEASON_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}