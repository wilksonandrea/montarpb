using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001BB RID: 443
	public class PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ : GameClientPacket
	{
		// Token: 0x060004A9 RID: 1193 RVA: 0x00024178 File Offset: 0x00022378
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
			for (int i = 0; i < 3; i++)
			{
				QuickstartModel quickstartModel = new QuickstartModel
				{
					MapId = (int)base.ReadC(),
					Rule = (int)base.ReadC(),
					StageOptions = (int)base.ReadC(),
					Type = (int)base.ReadC()
				};
				this.list_1.Add(quickstartModel);
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000241E0 File Offset: 0x000223E0
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					player.Quickstart.Quickjoins[this.int_0] = this.list_1[this.int_0];
					ComDiv.UpdateDB("player_quickstarts", "owner_id", player.PlayerId, new string[]
					{
						string.Format("list{0}_map_id", this.int_0),
						string.Format("list{0}_map_rule", this.int_0),
						string.Format("list{0}_map_stage", this.int_0),
						string.Format("list{0}_map_type", this.int_0)
					}, new object[]
					{
						this.list_1[this.int_0].MapId,
						this.list_1[this.int_0].Rule,
						this.list_1[this.int_0].StageOptions,
						this.list_1[this.int_0].Type
					});
					ChannelModel channelModel;
					if (player.Nickname.Length > 0 && player.Room == null && player.Match == null && player.GetChannel(out channelModel))
					{
						List<RoomModel> rooms = channelModel.Rooms;
						lock (rooms)
						{
							foreach (RoomModel roomModel in channelModel.Rooms)
							{
								if (roomModel.RoomType != RoomCondition.Tutorial)
								{
									this.quickstartModel_0 = this.list_1[this.int_0];
									if (this.quickstartModel_0.MapId == (int)roomModel.MapId && this.quickstartModel_0.Rule == (int)roomModel.Rule && this.quickstartModel_0.StageOptions == (int)roomModel.Stage && this.quickstartModel_0.Type == (int)roomModel.RoomType && roomModel.Password.Length == 0 && roomModel.Limit == 0 && (!roomModel.KickedPlayersVote.Contains(player.PlayerId) || player.IsGM()))
									{
										foreach (SlotModel slotModel in roomModel.Slots)
										{
											if (slotModel.PlayerId == 0L && slotModel.State == SlotState.EMPTY)
											{
												this.list_0.Add(roomModel);
												break;
											}
										}
									}
								}
							}
						}
					}
					if (this.list_0.Count == 0)
					{
						this.Client.SendPacket(new PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK(2147483648U, this.list_1, null, null));
					}
					else
					{
						RoomModel roomModel2 = this.list_0[new Random().Next(this.list_0.Count)];
						if (roomModel2 != null && roomModel2.GetLeader() != null && roomModel2.AddPlayer(player) >= 0)
						{
							player.ResetPages();
							using (PROTOCOL_ROOM_GET_SLOTONEINFO_ACK protocol_ROOM_GET_SLOTONEINFO_ACK = new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(player))
							{
								roomModel2.SendPacketToPlayers(protocol_ROOM_GET_SLOTONEINFO_ACK, player.PlayerId);
							}
							roomModel2.UpdateSlotsInfo();
							this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0U, player));
							this.Client.SendPacket(new PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK(0U, this.list_1, roomModel2, this.quickstartModel_0));
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK(2147483648U, null, null, null));
						}
					}
					this.list_0 = null;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000056D0 File Offset: 0x000038D0
		public PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ()
		{
		}

		// Token: 0x0400032E RID: 814
		private int int_0;

		// Token: 0x0400032F RID: 815
		private List<RoomModel> list_0 = new List<RoomModel>();

		// Token: 0x04000330 RID: 816
		private List<QuickstartModel> list_1 = new List<QuickstartModel>();

		// Token: 0x04000331 RID: 817
		private QuickstartModel quickstartModel_0;
	}
}
