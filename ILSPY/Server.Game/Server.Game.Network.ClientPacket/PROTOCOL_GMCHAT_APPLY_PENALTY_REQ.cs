using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_GMCHAT_APPLY_PENALTY_REQ : GameClientPacket
{
	private long long_0;

	private string string_0;

	private string string_1;

	private int int_0;

	private byte byte_0;

	public override void Read()
	{
		string_0 = ReadU(ReadC() * 2);
		string_1 = ReadU(ReadC() * 2);
		byte_0 = ReadC();
		int_0 = ReadD();
		ReadC();
		long_0 = ReadQ();
	}

	public override void Run()
	{
		try
		{
			if (Client.Player != null)
			{
				if (AccountManager.GetAccount(long_0, 31) != null)
				{
					Client.SendPacket(new PROTOCOL_GMCHAT_APPLY_PENALTY_ACK(0u));
				}
				else
				{
					Client.SendPacket(new PROTOCOL_GMCHAT_APPLY_PENALTY_ACK(2147483648u));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_GMCHAT_APPLY_PENALTY_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
