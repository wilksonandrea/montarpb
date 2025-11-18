using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ : AuthClientPacket
{
	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			Client.SendPacket(new PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK());
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
