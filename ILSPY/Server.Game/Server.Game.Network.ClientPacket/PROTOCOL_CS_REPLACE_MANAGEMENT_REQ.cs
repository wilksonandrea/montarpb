using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_REPLACE_MANAGEMENT_REQ : GameClientPacket
{
	private int int_0;

	private int int_1;

	private int int_2;

	private int int_3;

	private JoinClanType joinClanType_0;

	private uint uint_0;

	public override void Read()
	{
		int_3 = ReadC();
		int_0 = ReadC();
		int_2 = ReadC();
		int_1 = ReadC();
		joinClanType_0 = (JoinClanType)ReadC();
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
			ClanModel clan = ClanManager.GetClan(player.ClanId);
			if (clan.Id > 0 && clan.OwnerId == player.PlayerId)
			{
				if (clan.Authority != int_3)
				{
					clan.Authority = int_3;
				}
				if (clan.RankLimit != int_0)
				{
					clan.RankLimit = int_0;
				}
				if (clan.MinAgeLimit != int_1)
				{
					clan.MinAgeLimit = int_1;
				}
				if (clan.MaxAgeLimit != int_2)
				{
					clan.MaxAgeLimit = int_2;
				}
				if (clan.JoinType != joinClanType_0)
				{
					clan.JoinType = joinClanType_0;
				}
				DaoManagerSQL.UpdateClanInfo(clan.Id, clan.Authority, clan.RankLimit, clan.MinAgeLimit, clan.MaxAgeLimit, (int)clan.JoinType);
			}
			else
			{
				uint_0 = 2147483648u;
			}
			Client.SendPacket(new PROTOCOL_CS_REPLACE_MANAGEMENT_ACK(uint_0));
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
