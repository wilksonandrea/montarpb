using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001CF RID: 463
	public class PROTOCOL_ROOM_JOIN_REQ : GameClientPacket
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x000057CD File Offset: 0x000039CD
		public override void Read()
		{
			this.int_0 = base.ReadD();
			this.string_0 = base.ReadS(4);
			this.int_1 = (int)base.ReadC();
			base.ReadC();
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00025F0C File Offset: 0x0002410C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ChannelModel channelModel;
					if (player.Nickname.Length > 0 && player.Room == null && player.Match == null && player.GetChannel(out channelModel))
					{
						RoomModel room = channelModel.GetRoom(this.int_0);
						if (room != null && room.GetLeader() != null)
						{
							if (room.RoomType == RoomCondition.Tutorial)
							{
								this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487868U, null));
							}
							else if (room.Password.Length > 0 && this.string_0 != room.Password && player.Rank != 53 && !player.IsGM() && this.int_1 != 1)
							{
								this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487749U, null));
							}
							else if (room.Limit == 1 && room.State >= RoomState.COUNTDOWN && !player.IsGM())
							{
								this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487763U, null));
							}
							else if (room.KickedPlayersVote.Contains(player.PlayerId) && !player.IsGM())
							{
								this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487756U, null));
							}
							else if (room.KickedPlayersHost.ContainsKey(player.PlayerId) && ComDiv.GetDuration(room.KickedPlayersHost[player.PlayerId]) < (double)ConfigLoader.IntervalEnterRoomAfterKickSeconds)
							{
								this.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(Translation.GetLabel("KickByHostMessage", new object[]
								{
									ConfigLoader.IntervalEnterRoomAfterKickSeconds,
									(int)ComDiv.GetDuration(room.KickedPlayersHost[player.PlayerId])
								})));
							}
							else if (room.AddPlayer(player) >= 0)
							{
								player.ResetPages();
								using (PROTOCOL_ROOM_GET_SLOTONEINFO_ACK protocol_ROOM_GET_SLOTONEINFO_ACK = new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(player))
								{
									room.SendPacketToPlayers(protocol_ROOM_GET_SLOTONEINFO_ACK, player.PlayerId);
								}
								if (room.Competitive)
								{
									AllUtils.SendCompetitiveInfo(player);
								}
								this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0U, player));
							}
							else
							{
								this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487747U, null));
							}
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487748U, null));
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487748U, null));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_LOBBY_JOIN_ROOM_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_ROOM_JOIN_REQ()
		{
		}

		// Token: 0x0400037B RID: 891
		private int int_0;

		// Token: 0x0400037C RID: 892
		private int int_1;

		// Token: 0x0400037D RID: 893
		private string string_0;
	}
}
