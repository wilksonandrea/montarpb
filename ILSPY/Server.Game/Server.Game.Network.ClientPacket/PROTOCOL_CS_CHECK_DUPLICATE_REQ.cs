using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CHECK_DUPLICATE_REQ : GameClientPacket
{
	private string string_0;

	public override void Read()
	{
		string_0 = ReadU(ReadC() * 2);
	}

	public override void Run()
	{
		try
		{
			Client.SendPacket(new PROTOCOL_CS_CHECK_DUPLICATE_ACK(ClanManager.IsClanNameExist(string_0) ? 2147483648u : 0u));
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
