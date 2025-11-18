using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_EVENT_PORTAL_REQ : GameClientPacket
{
	private string string_0;

	public override void Read()
	{
		string_0 = ReadS(32);
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player != null)
			{
				if (!player.LoadedShop)
				{
					player.LoadedShop = true;
				}
				if (Bitwise.ReadFile(Environment.CurrentDirectory + "/Data/Raws/EventPortal.dat") == string_0)
				{
					Client.SendPacket(new PROTOCOL_BASE_EVENT_PORTAL_ACK(bool_1: false));
				}
				else
				{
					Client.SendPacket(new PROTOCOL_BASE_EVENT_PORTAL_ACK(bool_1: true));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BASE_EVENT_PORTAL_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
