using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CREATE_CLAN_REQ : GameClientPacket
{
	private uint uint_0;

	private string string_0;

	private string string_1;

	public override void Read()
	{
		ReadD();
		string_0 = ReadU(34);
		string_1 = ReadU(510);
		ReadD();
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
			ClanModel clanModel = new ClanModel
			{
				Name = string_0,
				Info = string_1,
				Logo = 0u,
				OwnerId = player.PlayerId,
				CreationDate = uint.Parse(DateTimeUtil.Now("yyyyMMdd"))
			};
			if (player.ClanId <= 0 && DaoManagerSQL.GetRequestClanId(player.PlayerId) <= 0)
			{
				if (0 <= player.Gold - ConfigLoader.MinCreateGold && ConfigLoader.MinCreateRank <= player.Rank)
				{
					if (ClanManager.IsClanNameExist(clanModel.Name))
					{
						uint_0 = 2147487834u;
						return;
					}
					if (ClanManager.Clans.Count > ConfigLoader.MaxActiveClans)
					{
						uint_0 = 2147487829u;
					}
					else if (DaoManagerSQL.CreateClan(out clanModel.Id, clanModel.Name, clanModel.OwnerId, clanModel.Info, clanModel.CreationDate) && DaoManagerSQL.UpdateAccountGold(player.PlayerId, player.Gold - ConfigLoader.MinCreateGold))
					{
						clanModel.BestPlayers.SetDefault();
						player.ClanDate = clanModel.CreationDate;
						if (ComDiv.UpdateDB("accounts", "player_id", player.PlayerId, new string[3] { "clan_access", "clan_date", "clan_id" }, 1, (long)clanModel.CreationDate, clanModel.Id))
						{
							if (clanModel.Id > 0)
							{
								player.ClanId = clanModel.Id;
								player.ClanAccess = 1;
								ClanManager.AddClan(clanModel);
								SendClanInfo.Load(clanModel, 0);
								player.Gold -= ConfigLoader.MinCreateGold;
							}
							else
							{
								uint_0 = 2147487819u;
							}
						}
						else
						{
							uint_0 = 2147487816u;
						}
					}
					else
					{
						uint_0 = 2147487816u;
					}
				}
				else
				{
					uint_0 = 2147487818u;
				}
			}
			else
			{
				uint_0 = 2147487836u;
			}
			Client.SendPacket(new PROTOCOL_CS_DETAIL_INFO_ACK((int)uint_0, clanModel));
			Client.SendPacket(new PROTOCOL_CS_CREATE_CLAN_ACK(uint_0, clanModel, player));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CS_CREATE_CLAN_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
