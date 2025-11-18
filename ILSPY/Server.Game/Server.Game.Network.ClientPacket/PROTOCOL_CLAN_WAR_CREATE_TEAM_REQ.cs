using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_CREATE_TEAM_REQ : GameClientPacket
{
	private int int_0;

	private List<int> list_0 = new List<int>();

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
			ChannelModel channel = player.GetChannel();
			if (channel != null && channel.Type == ChannelType.Clan && player.Room == null)
			{
				if (player.Match != null)
				{
					Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(2147487879u));
					return;
				}
				if (player.ClanId == 0)
				{
					Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(2147487835u));
					return;
				}
				int num = -1;
				int num2 = -1;
				lock (channel.Matches)
				{
					for (int i = 0; i < 250; i++)
					{
						if (channel.GetMatch(i) == null)
						{
							num = i;
							break;
						}
					}
					for (int j = 0; j < channel.Matches.Count; j++)
					{
						MatchModel matchModel = channel.Matches[j];
						if (matchModel.Clan.Id == player.ClanId)
						{
							list_0.Add(matchModel.FriendId);
						}
					}
				}
				for (int k = 0; k < 25; k++)
				{
					if (!list_0.Contains(k))
					{
						num2 = k;
						break;
					}
				}
				if (num == -1)
				{
					Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(2147487880u));
					return;
				}
				if (num2 == -1)
				{
					Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(2147487881u));
					return;
				}
				MatchModel matchModel2 = new MatchModel(ClanManager.GetClan(player.ClanId))
				{
					MatchId = num,
					FriendId = num2,
					Training = int_0,
					ChannelId = player.ChannelId,
					ServerId = player.ServerId
				};
				matchModel2.AddPlayer(player);
				channel.AddMatch(matchModel2);
				Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(0u, matchModel2));
				Client.SendPacket(new PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(matchModel2));
			}
			else
			{
				Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(2147483648u));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
