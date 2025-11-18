using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_SHOP_REPAIR_REQ : GameClientPacket
	{
		private int int_0;

		private List<long> list_0 = new List<long>();

		public PROTOCOL_SHOP_REPAIR_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadC();
			for (int i = 0; i < this.int_0; i++)
			{
				uint uInt32 = base.ReadUD();
				this.list_0.Add((long)uInt32);
			}
		}

		public override void Run()
		{
			int ınt32;
			int ınt321;
			uint uInt32;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					List<ItemsModel> ıtemsModels = AllUtils.RepairableItems(player, this.list_0, out ınt32, out ınt321, out uInt32);
					if (ıtemsModels.Count > 0)
					{
						player.Gold -= ınt32;
						player.Cash -= ınt321;
						if (ComDiv.UpdateDB("accounts", "player_id", player.PlayerId, new string[] { "gold", "cash" }, new object[] { player.Gold, player.Cash }))
						{
							this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(2, player, ıtemsModels));
						}
						this.Client.SendPacket(new PROTOCOL_SHOP_REPAIR_ACK(uInt32, ıtemsModels, player));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_SHOP_REPAIR_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}