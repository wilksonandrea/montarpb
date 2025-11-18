using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_RESULT_REQ : GameClientPacket
{
	private int int_0;

	public override void Read()
	{
		int_0 = ReadD();
	}

	public override void Run()
	{
		try
		{
			Client.SendPacket(new PROTOCOL_CLAN_WAR_RESULT_ACK());
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CLAN_WAR_RESULT_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
