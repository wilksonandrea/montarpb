using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CHATTING_REQ : GameClientPacket
{
	private ChattingType chattingType_0;

	private string string_0;

	public override void Read()
	{
		chattingType_0 = (ChattingType)ReadH();
		string_0 = ReadU(ReadH() * 2);
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			int length = string_0.Length;
			int num = -1;
			bool ısOnline = true;
			bool useCache = true;
			if (length > 60 || chattingType_0 != ChattingType.Clan)
			{
				return;
			}
			using PROTOCOL_CS_CHATTING_ACK packet = new PROTOCOL_CS_CHATTING_ACK(string_0, player);
			ClanManager.SendPacket(packet, player.ClanId, num, useCache, ısOnline);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
