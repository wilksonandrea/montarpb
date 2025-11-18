using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ : GameClientPacket
	{
		private int int_0;

		private List<RoomModel> list_0 = new List<RoomModel>();

		private List<QuickstartModel> list_1 = new List<QuickstartModel>();

		private QuickstartModel quickstartModel_0;

		public PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadC();
			for (int i = 0; i < 3; i++)
			{
				QuickstartModel quickstartModel = new QuickstartModel()
				{
					MapId = base.ReadC(),
					Rule = base.ReadC(),
					StageOptions = base.ReadC(),
					Type = base.ReadC()
				};
				this.list_1.Add(quickstartModel);
			}
		}

		public override void Run()
		{
			ChannelModel channelModel;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					player.Quickstart.Quickjoins[this.int_0] = this.list_1[this.int_0];
					ComDiv.UpdateDB("player_quickstarts", "owner_id", player.PlayerId, new string[] { string.Format("list{0}_map_id", this.int_0), string.Format("list{0}_map_rule", this.int_0), string.Format("list{0}_map_stage", this.int_0), string.Format("list{0}_map_type", this.int_0) }, new object[] { this.list_1[this.int_0].MapId, this.list_1[this.int_0].Rule, this.list_1[this.int_0].StageOptions, this.list_1[this.int_0].Type });
					if (player.Nickname.Length > 0 && player.Room == null && player.Match == null && player.GetChannel(out channelModel))
					{
						lock (channelModel.Rooms)
						{
						Label0:
							foreach (RoomModel room in channelModel.Rooms)
							{
								if (room.RoomType == RoomCondition.Tutorial)
								{
									continue;
								}
								this.quickstartModel_0 = this.list_1[this.int_0];
								if (this.quickstartModel_0.MapId != (int)room.MapId || this.quickstartModel_0.Rule != (int)room.Rule || this.quickstartModel_0.StageOptions != (int)room.Stage || this.quickstartModel_0.Type != (int)room.RoomType || room.Password.Length != 0 || room.Limit != 0 || room.KickedPlayersVote.Contains(player.PlayerId) && !player.IsGM())
								{
									continue;
								}
								SlotModel[] slots = room.Slots;
								int ınt32 = 0;
								while (ınt32 < (int)slots.Length)
								{
									SlotModel slotModel = slots[ınt32];
									if (slotModel.PlayerId != 0 || slotModel.State != SlotState.EMPTY)
									{
										ınt32++;
									}
									else
									{
										this.list_0.Add(room);
										goto Label0;
									}
								}
							}
						}
					}
					if (this.list_0.Count != 0)
					{
						RoomModel ıtem = this.list_0[(new Random()).Next(this.list_0.Count)];
						if (ıtem == null || ıtem.GetLeader() == null || ıtem.AddPlayer(player) < 0)
						{
							this.Client.SendPacket(new PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK(-2147483648, null, null, null));
						}
						else
						{
							player.ResetPages();
							using (PROTOCOL_ROOM_GET_SLOTONEINFO_ACK pROTOCOLROOMGETSLOTONEINFOACK = new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(player))
							{
								ıtem.SendPacketToPlayers(pROTOCOLROOMGETSLOTONEINFOACK, player.PlayerId);
							}
							ıtem.UpdateSlotsInfo();
							this.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0, player));
							this.Client.SendPacket(new PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK(0, this.list_1, ıtem, this.quickstartModel_0));
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK(-2147483648, this.list_1, null, null));
					}
					this.list_0 = null;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}