using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CLOSE_CLAN_REQ : GameClientPacket
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
				ClanModel clan = ClanManager.GetClan(player.ClanId);
				if (clan.Id > 0 && clan.OwnerId == Client.PlayerId && ComDiv.DeleteDB("system_clan", "id", clan.Id) && ComDiv.UpdateDB("accounts", "player_id", player.PlayerId, new string[2] { "clan_id", "clan_access" }, 0, 0) && ComDiv.UpdateDB("player_stat_clans", "owner_id", player.PlayerId, new string[2] { "clan_matches", "clan_match_wins" }, 0, 0) && ClanManager.RemoveClan(clan))
				{
					player.ClanId = 0;
					player.ClanAccess = 0;
					SendClanInfo.Load(clan, 1);
				}
				else
				{
					uint_0 = 2147487850u;
				}
				Client.SendPacket(new PROTOCOL_CS_CLOSE_CLAN_ACK(uint_0));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CS_CLOSE_CLAN_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
