using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_MATCH_SERVER_IDX_REQ : AuthClientPacket
{
	private short short_0;

	public override void Read()
	{
		short_0 = ReadH();
		ReadC();
	}

	public override void Run()
	{
		try
		{
			Client.SendPacket(new PROTOCOL_MATCH_SERVER_IDX_ACK(short_0));
			Client.Close(0, DestroyConnection: false);
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_MATCH_SERVER_IDX_REQ: " + ex.Message, LoggerType.Warning);
		}
	}
}
