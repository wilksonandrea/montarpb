using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_LEAVE_TEAM_REQ : GameClientPacket
{
	private uint uint_0;

	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player != null)
			{
				MatchModel match = player.Match;
				if (match == null || !match.RemovePlayer(player))
				{
					uint_0 = 2147483648u;
				}
				Client.SendPacket(new PROTOCOL_CLAN_WAR_LEAVE_TEAM_ACK(uint_0));
				if (uint_0 == 0)
				{
					player.Status.UpdateClanMatch(byte.MaxValue);
					AllUtils.SyncPlayerToClanMembers(player);
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
