using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001C4 RID: 452
	public class PROTOCOL_ROOM_CHANGE_PASSWD_REQ : GameClientPacket
	{
		// Token: 0x060004C6 RID: 1222 RVA: 0x00005767 File Offset: 0x00003967
		public override void Read()
		{
			this.string_0 = base.ReadS(4);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00024CB8 File Offset: 0x00022EB8
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && room.LeaderSlot == player.SlotId && room.Password != this.string_0)
					{
						room.Password = this.string_0;
						using (PROTOCOL_ROOM_CHANGE_PASSWD_ACK protocol_ROOM_CHANGE_PASSWD_ACK = new PROTOCOL_ROOM_CHANGE_PASSWD_ACK(this.string_0))
						{
							room.SendPacketToPlayers(protocol_ROOM_CHANGE_PASSWD_ACK);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_ROOM_CHANGE_PASSWD_REQ()
		{
		}

		// Token: 0x04000340 RID: 832
		private string string_0;
	}
}
