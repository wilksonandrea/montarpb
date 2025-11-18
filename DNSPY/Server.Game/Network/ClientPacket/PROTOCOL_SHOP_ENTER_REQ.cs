using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001D8 RID: 472
	public class PROTOCOL_SHOP_ENTER_REQ : GameClientPacket
	{
		// Token: 0x06000506 RID: 1286 RVA: 0x00005858 File Offset: 0x00003A58
		public override void Read()
		{
			base.ReadS(32);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00026C50 File Offset: 0x00024E50
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null)
					{
						room.ChangeSlotState(player.SlotId, SlotState.SHOP, false);
						room.StopCountDown(player.SlotId);
						room.UpdateSlotsInfo();
					}
					this.Client.SendPacket(new PROTOCOL_SHOP_ENTER_ACK());
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_SHOP_ENTER_REQ()
		{
		}
	}
}
