using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CLAN_WAR_JOIN_ROOM_REQ : GameClientPacket
	{
		private int int_0;

		private int int_1;

		private TeamEnum teamEnum_0;

		public PROTOCOL_CLAN_WAR_JOIN_ROOM_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadD();
			this.teamEnum_0 = (TeamEnum)base.ReadH();
			this.int_1 = base.ReadH();
		}

		public override void Run()
		{
			ChannelModel channelModel;
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.ClanId != 0 && player.Match != null)
				{
					if (player == null || player.Nickname.Length <= 0 || player.Room != null || !player.GetChannel(out channelModel))
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
						else if (room.Password.Length > 0 && !player.IsGM())
						{
							this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(-2147479547, null));
						}
						else if (room.Limit == 1 && room.State >= RoomState.COUNTDOWN)
						{
							this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(-2147479533, null));
						}
						else if (room.KickedPlayersVote.Contains(player.PlayerId))
						{
							this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(-2147479540, null));
						}
						else if (room.AddPlayer(player, this.teamEnum_0) < 0)
						{
							this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(-2147479549, null));
						}
						else
						{
							using (PROTOCOL_ROOM_GET_SLOTONEINFO_ACK pROTOCOLROOMGETSLOTONEINFOACK = new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(player))
							{
								room.SendPacketToPlayers(pROTOCOLROOMGETSLOTONEINFOACK, player.PlayerId);
							}
							room.UpdateSlotsInfo();
							this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0, player));
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CLAN_WAR_JOIN_ROOM_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}