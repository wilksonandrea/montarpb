using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BATTLE_SENDPING_REQ : GameClientPacket
	{
		private byte[] byte_0;

		public PROTOCOL_BATTLE_SENDPING_REQ()
		{
		}

		public override void Read()
		{
			this.byte_0 = base.ReadB(16);
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
					if (room != null && room.GetSlot(player.SlotId, out slotModel))
					{
						int ınt32 = 0;
						if (slotModel != null && slotModel.State >= SlotState.BATTLE_READY)
						{
							if (room.State == RoomState.BATTLE)
							{
								room.Ping = this.byte_0[room.LeaderSlot];
							}
							using (PROTOCOL_BATTLE_SENDPING_ACK pROTOCOLBATTLESENDPINGACK = new PROTOCOL_BATTLE_SENDPING_ACK(this.byte_0))
							{
								List<Account> allPlayers = room.GetAllPlayers(SlotState.READY, 1);
								if (allPlayers.Count != 0)
								{
									byte[] completeBytes = pROTOCOLBATTLESENDPINGACK.GetCompleteBytes(base.GetType().Name);
									foreach (Account allPlayer in allPlayers)
									{
										SlotModel slot = room.GetSlot(allPlayer.SlotId);
										if (slot == null || slot.State < SlotState.BATTLE_READY)
										{
											ınt32++;
										}
										else
										{
											allPlayer.SendCompletePacket(completeBytes, pROTOCOLBATTLESENDPINGACK.GetType().Name);
										}
									}
								}
								else
								{
									return;
								}
							}
							if (ınt32 == 0)
							{
								room.SpawnReadyPlayers();
							}
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BATTLE_SENDPING_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}