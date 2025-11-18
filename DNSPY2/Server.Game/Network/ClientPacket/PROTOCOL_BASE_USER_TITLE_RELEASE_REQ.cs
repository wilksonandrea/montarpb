using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000160 RID: 352
	public class PROTOCOL_BASE_USER_TITLE_RELEASE_REQ : GameClientPacket
	{
		// Token: 0x06000380 RID: 896 RVA: 0x00005125 File Offset: 0x00003325
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
			this.int_1 = (int)base.ReadC();
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001B0C8 File Offset: 0x000192C8
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && this.int_0 < 3 && player.Title != null)
				{
					PlayerTitles title = player.Title;
					int equip = title.GetEquip(this.int_0);
					if (this.int_0 < 3 && this.int_1 < 45 && equip == this.int_1 && DaoManagerSQL.UpdateEquipedPlayerTitle(title.OwnerId, this.int_0, 0))
					{
						title.SetEquip(this.int_0, 0);
						if (TitleAwardXML.Contains(equip, player.Equipment.BeretItem) && ComDiv.UpdateDB("player_equipments", "beret_item_part", 0, "owner_id", player.PlayerId))
						{
							player.Equipment.BeretItem = 0;
							RoomModel room = player.Room;
							if (room != null)
							{
								AllUtils.UpdateSlotEquips(player, room);
							}
						}
						this.Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_RELEASE_ACK(0U, this.int_0, this.int_1));
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_RELEASE_ACK(2147483648U, -1, -1));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_USER_TITLE_RELEASE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_USER_TITLE_RELEASE_REQ()
		{
		}

		// Token: 0x04000282 RID: 642
		private int int_0;

		// Token: 0x04000283 RID: 643
		private int int_1;
	}
}
