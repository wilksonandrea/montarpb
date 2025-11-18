using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_TEAM_CHATTING_REQ : GameClientPacket
{
	private ChattingType chattingType_0;

	private string string_0;

	public override void Read()
	{
		chattingType_0 = (ChattingType)ReadH();
		string_0 = ReadS(ReadH());
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player != null && player.Match != null && chattingType_0 == ChattingType.Match)
			{
				MatchModel match = player.Match;
				using PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK packet = new PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK(player.Nickname, string_0);
				match.SendPacketToPlayers(packet);
				return;
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CLAN_WAR_TEAM_CHATTING_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
