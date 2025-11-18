using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_LOBBY_ENTER_REQ : GameClientPacket
	{
		public PROTOCOL_LOBBY_ENTER_REQ()
		{
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					player.LastLobbyEnter = DateTimeUtil.Now();
					if (player.ChannelId >= 0)
					{
						ChannelModel channel = player.GetChannel();
						if (channel != null)
						{
							channel.AddPlayer(player.Session);
						}
					}
					RoomModel room = player.Room;
					if (room != null)
					{
						if (player.SlotId < 0 || room.State < RoomState.LOADING || room.Slots[player.SlotId].State < SlotState.LOAD)
						{
							room.RemovePlayer(player, false, 0);
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_LOBBY_ENTER_ACK(0));
							player.LastLobbyEnter = DateTimeUtil.Now();
							return;
						}
					}
					AllUtils.SyncPlayerToFriends(player, false);
					AllUtils.SyncPlayerToClanMembers(player);
					AllUtils.GetXmasReward(player);
					this.Client.SendPacket(new PROTOCOL_LOBBY_ENTER_ACK(0));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_LOBBY_ENTER_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}