using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_GAMEGUARD_REQ : GameClientPacket
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
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
