using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_GMCHAT_NOTI_USER_PENALTY_REQ : GameClientPacket
{
	private long long_0;

	private int int_0;

	private byte byte_0;

	public override void Read()
	{
		int_0 = ReadD();
		byte_0 = ReadC();
		long_0 = ReadQ();
	}

	public override void Run()
	{
		try
		{
			if (Client.Player != null)
			{
				Account account = AccountManager.GetAccount(long_0, 31);
				if (account != null)
				{
					Client.SendPacket(new PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK(0u, account));
				}
				else
				{
					Client.SendPacket(new PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK(2147483648u, account));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(GetType().Name + ": " + ex.Message, LoggerType.Error, ex);
		}
	}
}
