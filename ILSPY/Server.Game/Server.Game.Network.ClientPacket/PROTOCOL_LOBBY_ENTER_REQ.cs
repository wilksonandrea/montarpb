using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_LOBBY_ENTER_REQ : GameClientPacket
{
	public override void Read()
	{
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
			player.LastLobbyEnter = DateTimeUtil.Now();
			if (player.ChannelId >= 0)
			{
				player.GetChannel()?.AddPlayer(player.Session);
			}
			RoomModel room = player.Room;
			if (room != null)
			{
				if (player.SlotId >= 0 && room.State >= RoomState.LOADING && room.Slots[player.SlotId].State >= SlotState.LOAD)
				{
					Client.SendPacket(new PROTOCOL_LOBBY_ENTER_ACK(0u));
					player.LastLobbyEnter = DateTimeUtil.Now();
					return;
				}
				room.RemovePlayer(player, WarnAllPlayers: false);
			}
			AllUtils.SyncPlayerToFriends(player, all: false);
			AllUtils.SyncPlayerToClanMembers(player);
			AllUtils.GetXmasReward(player);
			Client.SendPacket(new PROTOCOL_LOBBY_ENTER_ACK(0u));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_LOBBY_ENTER_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
