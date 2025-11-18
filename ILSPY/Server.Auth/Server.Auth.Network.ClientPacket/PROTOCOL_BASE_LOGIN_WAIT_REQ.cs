using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_BASE_LOGIN_WAIT_REQ : AuthClientPacket
{
	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			Client.SendPacket(new PROTOCOL_BASE_LOGIN_WAIT_ACK(0));
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
