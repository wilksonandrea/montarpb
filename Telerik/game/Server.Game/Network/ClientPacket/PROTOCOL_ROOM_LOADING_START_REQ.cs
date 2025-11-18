using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_ROOM_LOADING_START_REQ : GameClientPacket
	{
		private string string_0;

		public PROTOCOL_ROOM_LOADING_START_REQ()
		{
		}

		public override void Read()
		{
			this.string_0 = base.ReadS((int)base.ReadC());
		}

		public override void Run()
		{
			SlotModel slotModel;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
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
					this.Client.SendPacket(new PROTOCOL_ROOM_LOADING_START_ACK(0));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_ROOM_LOADING_START_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}