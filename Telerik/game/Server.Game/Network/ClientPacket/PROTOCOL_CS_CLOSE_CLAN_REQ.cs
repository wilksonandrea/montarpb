using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CS_CLOSE_CLAN_REQ : GameClientPacket
	{
		private uint uint_0;

		public PROTOCOL_CS_CLOSE_CLAN_REQ()
		{
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ClanModel clan = ClanManager.GetClan(player.ClanId);
					if (clan.Id > 0 && clan.OwnerId == this.Client.PlayerId && ComDiv.DeleteDB("system_clan", "id", clan.Id))
					{
						if (ComDiv.UpdateDB("accounts", "player_id", player.PlayerId, new string[] { "clan_id", "clan_access" }, new object[] { 0, 0 }))
						{
							if (!ComDiv.UpdateDB("player_stat_clans", "owner_id", player.PlayerId, new string[] { "clan_matches", "clan_match_wins" }, new object[] { 0, 0 }) || !ClanManager.RemoveClan(clan))
							{
								goto Label1;
							}
							player.ClanId = 0;
							player.ClanAccess = 0;
							SendClanInfo.Load(clan, 1);
							goto Label0;
						}
					}
				Label1:
					this.uint_0 = -2147479446;
				Label0:
					this.Client.SendPacket(new PROTOCOL_CS_CLOSE_CLAN_ACK(this.uint_0));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CS_CLOSE_CLAN_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}