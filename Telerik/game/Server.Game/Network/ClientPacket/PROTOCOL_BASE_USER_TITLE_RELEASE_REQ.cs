using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BASE_USER_TITLE_RELEASE_REQ : GameClientPacket
	{
		private int int_0;

		private int int_1;

		public PROTOCOL_BASE_USER_TITLE_RELEASE_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadC();
			this.int_1 = base.ReadC();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && this.int_0 < 3 && player.Title != null)
				{
					PlayerTitles title = player.Title;
					int equip = title.GetEquip(this.int_0);
					if (this.int_0 >= 3 || this.int_1 >= 45 || equip != this.int_1 || !DaoManagerSQL.UpdateEquipedPlayerTitle(title.OwnerId, this.int_0, 0))
					{
						this.Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_RELEASE_ACK(-2147483648, -1, -1));
					}
					else
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
						this.Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_RELEASE_ACK(0, this.int_0, this.int_1));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BASE_USER_TITLE_RELEASE_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}