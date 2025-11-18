using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_BASE_LOGOUT_REQ : AuthClientPacket
{
	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			Client.SendPacket(new PROTOCOL_BASE_LOGOUT_ACK());
			Client.Close(5000, DestroyConnection: true);
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BASE_LOGOUT_REQ: " + ex.Message, LoggerType.Error, ex);
			Client.Close(0, DestroyConnection: true);
		}
	}
}
