using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001B6 RID: 438
	public class PROTOCOL_LOBBY_ENTER_REQ : GameClientPacket
	{
		// Token: 0x06000496 RID: 1174 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00023B44 File Offset: 0x00021D44
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
						if (player.SlotId >= 0 && room.State >= RoomState.LOADING && room.Slots[player.SlotId].State >= SlotState.LOAD)
						{
							this.Client.SendPacket(new PROTOCOL_LOBBY_ENTER_ACK(0U));
							player.LastLobbyEnter = DateTimeUtil.Now();
							return;
						}
						room.RemovePlayer(player, false, 0);
					}
					AllUtils.SyncPlayerToFriends(player, false);
					AllUtils.SyncPlayerToClanMembers(player);
					AllUtils.GetXmasReward(player);
					this.Client.SendPacket(new PROTOCOL_LOBBY_ENTER_ACK(0U));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_LOBBY_ENTER_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_LOBBY_ENTER_REQ()
		{
		}
	}
}
