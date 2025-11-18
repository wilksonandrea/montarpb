using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ : GameClientPacket
{
	private int int_0;

	private int int_1;

	private uint uint_0;

	public override void Read()
	{
		int_0 = ReadH();
		int_1 = ReadH();
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
			if (player.Match != null && player.MatchSlot == player.Match.Leader && player.Match.State == MatchState.Ready)
			{
				int ıd = int_1 - int_1 / 10 * 10;
				MatchModel match = ChannelsXML.GetChannel(int_1, ıd).GetMatch(int_0);
				if (match != null)
				{
					Account leader = match.GetLeader();
					if (leader != null && leader.Connection != null && leader.IsOnline)
					{
						leader.SendPacket(new PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK(player.Match, player));
					}
					else
					{
						uint_0 = 2147483648u;
					}
				}
				else
				{
					uint_0 = 2147483648u;
				}
			}
			else
			{
				uint_0 = 2147483648u;
			}
			Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_PROPOSE_ACK(uint_0));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
