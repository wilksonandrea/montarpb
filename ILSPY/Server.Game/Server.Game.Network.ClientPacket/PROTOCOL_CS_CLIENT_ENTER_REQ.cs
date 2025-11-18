using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CLIENT_ENTER_REQ : GameClientPacket
{
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
				RoomModel room = player.Room;
				if (room != null)
				{
					room.ChangeSlotState(player.SlotId, SlotState.CLAN, SendInfo: false);
					room.StopCountDown(player.SlotId);
					room.UpdateSlotsInfo();
				}
				int num = 0;
				ClanModel clan = ClanManager.GetClan(player.ClanId);
				if (player.ClanId == 0 && player.Nickname.Length > 0)
				{
					num = DaoManagerSQL.GetRequestClanId(player.PlayerId);
				}
				Client.SendPacket(new PROTOCOL_CS_CLIENT_ENTER_ACK((num > 0) ? num : clan.Id, player.ClanAccess));
				if (clan.Id > 0 && num == 0)
				{
					Client.SendPacket(new PROTOCOL_CS_DETAIL_INFO_ACK(0, clan));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CS_CLIENT_ENTER_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
