using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_BASE_GAMEGUARD_REQ : AuthClientPacket
{
	private byte[] byte_0;

	public override void Read()
	{
		ReadB(48);
		byte_0 = ReadB(3);
	}

	public override void Run()
	{
		try
		{
			Client.SendPacket(new PROTOCOL_BASE_GAMEGUARD_ACK(0, byte_0));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BASE_GAMEGUARD_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
