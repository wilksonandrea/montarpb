using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200014A RID: 330
	public class PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_REQ : GameClientPacket
	{
		// Token: 0x0600033C RID: 828 RVA: 0x0000500E File Offset: 0x0000320E
		public override void Read()
		{
			this.viewerType_0 = (ViewerType)base.ReadC();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00019488 File Offset: 0x00017688
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && player.IsGM())
					{
						SlotModel slot = room.GetSlot(player.SlotId);
						if (slot != null)
						{
							slot.ViewType = this.viewerType_0;
							if (slot.ViewType == ViewerType.SpecGM)
							{
								slot.SpecGM = true;
							}
							this.Client.SendPacket(new PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_ACK(slot.Id));
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(base.GetType().Name + "; " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_REQ()
		{
		}

		// Token: 0x04000255 RID: 597
		private ViewerType viewerType_0;
	}
}
