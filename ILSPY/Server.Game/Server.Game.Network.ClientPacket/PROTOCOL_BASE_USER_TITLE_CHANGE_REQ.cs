using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_USER_TITLE_CHANGE_REQ : GameClientPacket
{
	private int int_0;

	private uint uint_0;

	public override void Read()
	{
		int_0 = ReadC();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null || int_0 >= 45)
			{
				return;
			}
			if (player.Title.OwnerId == 0L)
			{
				DaoManagerSQL.CreatePlayerTitlesDB(player.PlayerId);
				player.Title = new PlayerTitles
				{
					OwnerId = player.PlayerId
				};
			}
			TitleModel title = TitleSystemXML.GetTitle(int_0);
			if (title != null)
			{
				TitleSystemXML.Get2Titles(title.Req1, title.Req2, out var title2, out var title3, ReturnNull: false);
				if ((title.Req1 == 0 || title2 != null) && (title.Req2 == 0 || title3 != null) && player.Rank >= title.Rank && player.Ribbon >= title.Ribbon && player.Medal >= title.Medal && player.MasterMedal >= title.MasterMedal && player.Ensign >= title.Ensign && !player.Title.Contains(title.Flag) && player.Title.Contains(title2.Flag) && player.Title.Contains(title3.Flag))
				{
					player.Ribbon -= title.Ribbon;
					player.Medal -= title.Medal;
					player.MasterMedal -= title.MasterMedal;
					player.Ensign -= title.Ensign;
					long flags = player.Title.Add(title.Flag);
					DaoManagerSQL.UpdatePlayerTitlesFlags(player.PlayerId, flags);
					List<ItemsModel> awards = TitleAwardXML.GetAwards(int_0);
					if (awards.Count > 0)
					{
						Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, awards));
					}
					Client.SendPacket(new PROTOCOL_BASE_MEDAL_GET_INFO_ACK(player));
					DaoManagerSQL.UpdatePlayerTitleRequi(player.PlayerId, player.Medal, player.Ensign, player.MasterMedal, player.Ribbon);
					if (player.Title.Slots < title.Slot)
					{
						player.Title.Slots = title.Slot;
						ComDiv.UpdateDB("player_titles", "slots", player.Title.Slots, "owner_id", player.PlayerId);
					}
				}
				else
				{
					uint_0 = 2147487875u;
				}
			}
			else
			{
				uint_0 = 2147487875u;
			}
			Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_CHANGE_ACK(uint_0, player.Title.Slots));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BASE_USER_TITLE_CHANGE_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
