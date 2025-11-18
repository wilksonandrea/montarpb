using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_REQ : GameClientPacket
{
	private int int_0;

	public override void Read()
	{
		int_0 = ReadC();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			MatchModel match = player.Match;
			if (match != null && player.MatchSlot == match.Leader)
			{
				match.Training = int_0;
				using PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK packet = new PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK(0u, int_0);
				match.SendPacketToPlayers(packet);
				return;
			}
			Client.SendPacket(new PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK(2147483648u));
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
