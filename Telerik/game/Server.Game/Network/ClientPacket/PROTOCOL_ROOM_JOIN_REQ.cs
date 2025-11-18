using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_ROOM_JOIN_REQ : GameClientPacket
	{
		private int int_0;

		private int int_1;

		private string string_0;

		public PROTOCOL_ROOM_JOIN_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadD();
			this.string_0 = base.ReadS(4);
			this.int_1 = base.ReadC();
			base.ReadC();
		}

		public override void Run()
		{
			ChannelModel channelModel;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.Nickname.Length <= 0 || player.Room != null || player.Match != null || !player.GetChannel(out channelModel))
					{
						this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(-2147479548, null));
					}
					else
					{
						RoomModel room = channelModel.GetRoom(this.int_0);
						if (room == null || room.GetLeader() == null)
						{
							this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(-2147479548, null));
						}
						else if (room.RoomType == RoomCondition.Tutorial)
						{
							this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(-2147479428, null));
						}
						else if (room.Password.Length > 0 && this.string_0 != room.Password && player.Rank != 53 && !player.IsGM() && this.int_1 != 1)
						{
							this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(-2147479547, null));
						}
						else if (room.Limit == 1 && room.State >= RoomState.COUNTDOWN && !player.IsGM())
						{
							this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(-2147479533, null));
						}
						else if (room.KickedPlayersVote.Contains(player.PlayerId) && !player.IsGM())
						{
							this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(-2147479540, null));
						}
						else if (room.KickedPlayersHost.ContainsKey(player.PlayerId) && ComDiv.GetDuration(room.KickedPlayersHost[player.PlayerId]) < (double)ConfigLoader.IntervalEnterRoomAfterKickSeconds)
						{
							this.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(Translation.GetLabel("KickByHostMessage", new object[] { ConfigLoader.IntervalEnterRoomAfterKickSeconds, (int)ComDiv.GetDuration(room.KickedPlayersHost[player.PlayerId]) })));
						}
						else if (room.AddPlayer(player) < 0)
						{
							this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(-2147479549, null));
						}
						else
						{
							player.ResetPages();
							using (PROTOCOL_ROOM_GET_SLOTONEINFO_ACK pROTOCOLROOMGETSLOTONEINFOACK = new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(player))
							{
								room.SendPacketToPlayers(pROTOCOLROOMGETSLOTONEINFOACK, player.PlayerId);
							}
							if (room.Competitive)
							{
								AllUtils.SendCompetitiveInfo(player);
							}
							this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0, player));
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_LOBBY_JOIN_ROOM_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}