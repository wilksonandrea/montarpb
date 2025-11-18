using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_REPLACE_NOTICE_REQ : GameClientPacket
{
	private string string_0;

	private EventErrorEnum eventErrorEnum_0;

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
			ClanModel clan = ClanManager.GetClan(player.ClanId);
			if (clan.Id > 0 && clan.News != string_0 && (clan.OwnerId == Client.PlayerId || (player.ClanAccess >= 1 && player.ClanAccess <= 2)))
			{
				if (ComDiv.UpdateDB("system_clan", "news", string_0, "id", clan.Id))
				{
					clan.News = string_0;
				}
				else
				{
					eventErrorEnum_0 = EventErrorEnum.CLAN_REPLACE_NOTICE_ERROR;
				}
			}
			else
			{
				eventErrorEnum_0 = EventErrorEnum.CLAN_FAILED_CHANGE_OPTION;
			}
			Client.SendPacket(new PROTOCOL_CS_REPLACE_NOTICE_ACK(eventErrorEnum_0));
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
