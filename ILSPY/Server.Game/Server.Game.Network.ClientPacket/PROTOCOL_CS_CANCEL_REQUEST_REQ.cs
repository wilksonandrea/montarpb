using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SQL;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CANCEL_REQUEST_REQ : GameClientPacket
{
	private uint uint_0;

	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			if (Client.Player == null || !DaoManagerSQL.DeleteClanInviteDB(Client.PlayerId))
			{
				uint_0 = 2147487835u;
			}
			Client.SendPacket(new PROTOCOL_CS_CANCEL_REQUEST_ACK(uint_0));
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
