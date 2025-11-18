using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000180 RID: 384
	public class PROTOCOL_CLAN_WAR_JOIN_ROOM_REQ : GameClientPacket
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x000052F7 File Offset: 0x000034F7
		public override void Read()
		{
			this.int_0 = base.ReadD();
			this.teamEnum_0 = (TeamEnum)base.ReadH();
			this.int_1 = (int)base.ReadH();
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001F2B8 File Offset: 0x0001D4B8
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.ClanId != 0 && player.Match != null)
				{
					ChannelModel channelModel;
					if (player != null && player.Nickname.Length > 0 && player.Room == null && player.GetChannel(out channelModel))
					{
						RoomModel room = channelModel.GetRoom(this.int_0);
						if (room != null && room.GetLeader() != null)
						{
							if (room.Password.Length > 0 && !player.IsGM())
							{
								this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487749U, null));
							}
							else if (room.Limit == 1 && room.State >= RoomState.COUNTDOWN)
							{
								this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487763U, null));
							}
							else if (room.KickedPlayersVote.Contains(player.PlayerId))
							{
								this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487756U, null));
							}
							else if (room.AddPlayer(player, this.teamEnum_0) >= 0)
							{
								using (PROTOCOL_ROOM_GET_SLOTONEINFO_ACK protocol_ROOM_GET_SLOTONEINFO_ACK = new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(player))
								{
									room.SendPacketToPlayers(protocol_ROOM_GET_SLOTONEINFO_ACK, player.PlayerId);
								}
								room.UpdateSlotsInfo();
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
				CLogger.Print("PROTOCOL_CLAN_WAR_JOIN_ROOM_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CLAN_WAR_JOIN_ROOM_REQ()
		{
		}

		// Token: 0x040002D1 RID: 721
		private int int_0;

		// Token: 0x040002D2 RID: 722
		private int int_1;

		// Token: 0x040002D3 RID: 723
		private TeamEnum teamEnum_0;
	}
}
