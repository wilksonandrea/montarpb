using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ : GameClientPacket
{
	private readonly List<int> list_0 = new List<int>();

	public override void Read()
	{
		int num = ReadC();
		for (int i = 0; i < num; i++)
		{
			list_0.Add(ReadD());
		}
	}

	public override void Run()
	{
		try
		{
			if (ComDiv.UpdateDB("player_messages", "object_id", list_0.ToArray(), "owner_id", Client.PlayerId, new string[2] { "expire_date", "state" }, long.Parse(DateTimeUtil.Now().AddDays(7.0).ToString("yyMMddHHmm")), 0))
			{
				Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK(list_0));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
