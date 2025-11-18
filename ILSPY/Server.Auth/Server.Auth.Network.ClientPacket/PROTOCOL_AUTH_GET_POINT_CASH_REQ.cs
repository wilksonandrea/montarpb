using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth.Data.Models;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_AUTH_GET_POINT_CASH_REQ : AuthClientPacket
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
				Client.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0u, player));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_AUTH_GET_POINT_CASH_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
