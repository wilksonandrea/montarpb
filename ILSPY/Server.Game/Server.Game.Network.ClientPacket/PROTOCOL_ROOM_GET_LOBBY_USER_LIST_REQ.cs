using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ : GameClientPacket
{
	private int int_0;

	private uint uint_0;

	private int int_1;

	public override void Read()
	{
		int_0 = ReadD();
		int_1 = ReadD();
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
			RoomModel room = player.Room;
			if (room != null && int_0 > 0 && int_0 <= 8)
			{
				ChannelModel channel = player.GetChannel();
				if (channel != null)
				{
					using PROTOCOL_SERVER_MESSAGE_INVITED_ACK pROTOCOL_SERVER_MESSAGE_INVITED_ACK = new PROTOCOL_SERVER_MESSAGE_INVITED_ACK(player, room);
					byte[] completeBytes = pROTOCOL_SERVER_MESSAGE_INVITED_ACK.GetCompleteBytes("PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ");
					for (int i = 0; i < int_0; i++)
					{
						AccountManager.GetAccount(channel.GetPlayer(int_1).PlayerId, noUseDB: true)?.SendCompletePacket(completeBytes, pROTOCOL_SERVER_MESSAGE_INVITED_ACK.GetType().Name);
					}
				}
				else
				{
					uint_0 = 2147483648u;
				}
			}
			Client.SendPacket(new PROTOCOL_ROOM_GET_LOBBY_USER_LIST_ACK(uint_0));
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
