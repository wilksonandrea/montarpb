using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SQL;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_MESSENGER_NOTE_DELETE_REQ : GameClientPacket
{
	private uint uint_0;

	private List<object> list_0 = new List<object>();

	public override void Read()
	{
		int num = ReadC();
		for (int i = 0; i < num; i++)
		{
			long num2 = ReadUD();
			list_0.Add(num2);
		}
	}

	public override void Run()
	{
		try
		{
			if (!DaoManagerSQL.DeleteMessages(list_0, Client.PlayerId))
			{
				uint_0 = 2147483648u;
			}
			Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_DELETE_ACK(uint_0, list_0));
			list_0 = null;
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_MESSENGER_NOTE_DELETE_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
