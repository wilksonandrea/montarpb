using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_URL_LIST_REQ : GameClientPacket
{
	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			ServerConfig config = GameXender.Client.Config;
			if (config?.OfficialBannerEnabled ?? false)
			{
				Client.SendPacket(new PROTOCOL_BASE_URL_LIST_ACK(config));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
