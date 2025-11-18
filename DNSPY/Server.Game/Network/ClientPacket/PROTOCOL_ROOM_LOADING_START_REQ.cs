using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001D0 RID: 464
	public class PROTOCOL_ROOM_LOADING_START_REQ : GameClientPacket
	{
		// Token: 0x060004EC RID: 1260 RVA: 0x000057FB File Offset: 0x000039FB
		public override void Read()
		{
			this.string_0 = base.ReadS((int)base.ReadC());
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000261D4 File Offset: 0x000243D4
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					SlotModel slotModel;
					if (room != null && room.IsPreparing() && room.GetSlot(player.SlotId, out slotModel) && slotModel.State == SlotState.LOAD)
					{
						slotModel.PreLoadDate = DateTimeUtil.Now();
						room.StartCounter(0, player, slotModel);
						room.ChangeSlotState(slotModel, SlotState.RENDEZVOUS, true);
						room.MapName = this.string_0;
						if (slotModel.Id == room.LeaderSlot)
						{
							room.UdpServer = SynchronizeXML.GetServer(ConfigLoader.DEFAULT_PORT[2]);
							room.State = RoomState.RENDEZVOUS;
							room.UpdateRoomInfo();
						}
					}
					this.Client.SendPacket(new PROTOCOL_ROOM_LOADING_START_ACK(0U));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_ROOM_LOADING_START_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_ROOM_LOADING_START_REQ()
		{
		}

		// Token: 0x0400037E RID: 894
		private string string_0;
	}
}
