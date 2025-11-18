using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_DETAIL_INFO_REQ : GameClientPacket
{
	private int int_0;

	private int int_1;

	public override void Read()
	{
		int_0 = ReadD();
		int_1 = ReadC();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player != null)
			{
				player.FindClanId = int_0;
				ClanModel clan = ClanManager.GetClan(player.FindClanId);
				if (clan.Id > 0)
				{
					Client.SendPacket(new PROTOCOL_CS_DETAIL_INFO_ACK(int_1, clan));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CS_DETAIL_INFO_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
