using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CHECK_MARK_REQ : GameClientPacket
{
	private uint uint_0;

	private uint uint_1;

	public override void Read()
	{
		uint_0 = ReadUD();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null || ClanManager.GetClan(player.ClanId).Logo == uint_0 || ClanManager.IsClanLogoExist(uint_0))
			{
				uint_1 = 2147483648u;
			}
			Client.SendPacket(new PROTOCOL_CS_CHECK_MARK_ACK(uint_1));
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
