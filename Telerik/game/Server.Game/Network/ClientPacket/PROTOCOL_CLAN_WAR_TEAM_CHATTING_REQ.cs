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
	public class PROTOCOL_CLAN_WAR_TEAM_CHATTING_REQ : GameClientPacket
	{
		private ChattingType chattingType_0;

		private string string_0;

		public PROTOCOL_CLAN_WAR_TEAM_CHATTING_REQ()
		{
		}

		public override void Read()
		{
			this.chattingType_0 = (ChattingType)base.ReadH();
			this.string_0 = base.ReadS(base.ReadH());
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.Match != null)
				{
					if (this.chattingType_0 == ChattingType.Match)
					{
						MatchModel match = player.Match;
						using (PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK pROTOCOLCLANWARTEAMCHATTINGACK = new PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK(player.Nickname, this.string_0))
						{
							match.SendPacketToPlayers(pROTOCOLCLANWARTEAMCHATTINGACK);
						}
						return;
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CLAN_WAR_TEAM_CHATTING_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}