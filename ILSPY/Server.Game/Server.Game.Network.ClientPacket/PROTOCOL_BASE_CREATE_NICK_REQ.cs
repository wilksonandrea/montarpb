using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Filters;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_CREATE_NICK_REQ : GameClientPacket
{
	private string string_0;

	public override void Read()
	{
		string_0 = ReadU(ReadC() * 2);
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
			if (player.Nickname.Length == 0 && !string.IsNullOrEmpty(string_0) && string_0.Length >= ConfigLoader.MinNickSize && string_0.Length <= ConfigLoader.MaxNickSize)
			{
				foreach (string filter in NickFilter.Filters)
				{
					if (string_0.Contains(filter))
					{
						Client.SendPacket(new PROTOCOL_BASE_CREATE_NICK_ACK(2147487763u, null));
						break;
					}
				}
				if (!DaoManagerSQL.IsPlayerNameExist(string_0))
				{
					if (AccountManager.UpdatePlayerName(string_0, player.PlayerId))
					{
						DaoManagerSQL.CreatePlayerNickHistory(player.PlayerId, player.Nickname, string_0, "First nick choosed");
						player.Nickname = string_0;
						List<ItemsModel> basics = TemplatePackXML.Basics;
						if (basics.Count > 0)
						{
							foreach (ItemsModel item in basics)
							{
								if (ComDiv.GetIdStatics(item.Id, 1) == 6 && player.Character.GetCharacter(item.Id) == null)
								{
									AllUtils.CreateCharacter(player, item);
								}
							}
						}
						List<ItemsModel> awards = TemplatePackXML.Awards;
						if (awards.Count > 0)
						{
							foreach (ItemsModel item2 in awards)
							{
								if (ComDiv.GetIdStatics(item2.Id, 1) == 6 && player.Character.GetCharacter(item2.Id) == null)
								{
									AllUtils.CreateCharacter(player, item2);
								}
								else
								{
									Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, item2));
								}
							}
						}
						Client.SendPacket(new PROTOCOL_BASE_CREATE_NICK_ACK(0u, player));
						Client.SendPacket(new PROTOCOL_SHOP_PLUS_POINT_ACK(player.Gold, player.Gold, 4));
						Client.SendPacket(new PROTOCOL_BASE_QUEST_GET_INFO_ACK(player));
						Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_ACK(player.Friend.Friends));
					}
					else
					{
						Client.SendPacket(new PROTOCOL_BASE_CREATE_NICK_ACK(2147487763u, null));
					}
				}
				else
				{
					Client.SendPacket(new PROTOCOL_BASE_CREATE_NICK_ACK(2147483923u, null));
				}
			}
			else
			{
				Client.SendPacket(new PROTOCOL_BASE_CREATE_NICK_ACK(2147483923u, null));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_LOBBY_CREATE_NICK_NAME_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
