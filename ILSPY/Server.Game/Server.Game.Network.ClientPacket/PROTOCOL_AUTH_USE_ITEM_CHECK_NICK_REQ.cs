using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SQL;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_REQ : GameClientPacket
{
	private string string_0;

	public override void Read()
	{
		string_0 = ReadU(66);
	}

	public override void Run()
	{
		try
		{
			Client.SendPacket(new PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_ACK(DaoManagerSQL.IsPlayerNameExist(string_0) ? 2147483923u : 0u));
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
