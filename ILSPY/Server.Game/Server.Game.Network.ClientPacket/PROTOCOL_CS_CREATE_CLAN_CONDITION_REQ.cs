using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CREATE_CLAN_CONDITION_REQ : GameClientPacket
{
	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			Client.SendPacket(new PROTOCOL_CS_CREATE_CLAN_CONDITION_ACK());
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CS_CREATE_CLAN_CONDITION_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
