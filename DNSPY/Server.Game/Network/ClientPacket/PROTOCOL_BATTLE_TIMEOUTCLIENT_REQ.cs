using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000174 RID: 372
	public class PROTOCOL_BATTLE_TIMEOUTCLIENT_REQ : GameClientPacket
	{
		// Token: 0x060003BE RID: 958 RVA: 0x0000524F File Offset: 0x0000344F
		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001D810 File Offset: 0x0001BA10
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					SlotModel slotModel;
					if (room != null && room.GetSlot(this.int_0, out slotModel) && player.SlotId == slotModel.Id)
					{
						player.SendPacket(new PROTOCOL_BATTLE_TIMEOUTCLIENT_ACK());
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BATTLE_TIMEOUTCLIENT_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BATTLE_TIMEOUTCLIENT_REQ()
		{
		}

		// Token: 0x040002A7 RID: 679
		private int int_0;
	}
}
