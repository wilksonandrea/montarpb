using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000141 RID: 321
	public class PROTOCOL_BASE_GET_ROOM_USER_DETAIL_INFO_REQ : GameClientPacket
	{
		// Token: 0x06000321 RID: 801 RVA: 0x00004F92 File Offset: 0x00003192
		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00018EC8 File Offset: 0x000170C8
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					SlotModel slotModel;
					if (room != null && room.GetSlot(this.int_0, out slotModel))
					{
						Account playerBySlot = room.GetPlayerBySlot(slotModel);
						if (playerBySlot != null)
						{
							if (player.Nickname != playerBySlot.Nickname)
							{
								player.FindPlayer = playerBySlot.Nickname;
							}
							int num = int.MaxValue;
							TeamEnum teamEnum = room.ValidateTeam(slotModel.Team, slotModel.CostumeTeam);
							if (teamEnum == TeamEnum.FR_TEAM)
							{
								num = playerBySlot.Equipment.CharaRedId;
							}
							else if (teamEnum == TeamEnum.CT_TEAM)
							{
								num = playerBySlot.Equipment.CharaBlueId;
							}
							this.Client.SendPacket(new PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(0U, playerBySlot, num));
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_GET_ROOM_USER_DETAIL_INFO_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_GET_ROOM_USER_DETAIL_INFO_REQ()
		{
		}

		// Token: 0x04000242 RID: 578
		private int int_0;
	}
}
