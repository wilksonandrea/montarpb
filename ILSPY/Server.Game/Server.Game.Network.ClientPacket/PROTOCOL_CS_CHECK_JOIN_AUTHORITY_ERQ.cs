using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ : GameClientPacket
{
	private int int_0;

	private uint uint_0;

	public override void Read()
	{
		int_0 = ReadD();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player != null)
			{
				ClanModel clan = ClanManager.GetClan(int_0);
				if (clan.Id == 0)
				{
					uint_0 = 2147483648u;
				}
				else if (clan.RankLimit > player.Rank)
				{
					uint_0 = 2147487867u;
				}
				Client.SendPacket(new PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ACK(uint_0));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
