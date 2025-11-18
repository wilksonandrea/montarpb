using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_PAGE_CHATTING_REQ : GameClientPacket
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
			if (player != null && chattingType_0 == ChattingType.ClanMemberPage)
			{
				using (PROTOCOL_CS_PAGE_CHATTING_ACK packet = new PROTOCOL_CS_PAGE_CHATTING_ACK(player, string_0))
				{
					ClanManager.SendPacket(packet, player.ClanId, -1L, UseCache: true, IsOnline: true);
					return;
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CS_PAGE_CHATTING_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
