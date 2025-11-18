using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001DB RID: 475
	public class PROTOCOL_SHOP_REPAIR_REQ : GameClientPacket
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x00027044 File Offset: 0x00025244
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
			for (int i = 0; i < this.int_0; i++)
			{
				uint num = base.ReadUD();
				this.list_0.Add((long)((ulong)num));
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00027084 File Offset: 0x00025284
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					int num;
					int num2;
					uint num3;
					List<ItemsModel> list = AllUtils.RepairableItems(player, this.list_0, out num, out num2, out num3);
					if (list.Count > 0)
					{
						player.Gold -= num;
						player.Cash -= num2;
						if (ComDiv.UpdateDB("accounts", "player_id", player.PlayerId, new string[] { "gold", "cash" }, new object[] { player.Gold, player.Cash }))
						{
							this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(2, player, list));
						}
						this.Client.SendPacket(new PROTOCOL_SHOP_REPAIR_ACK(num3, list, player));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_SHOP_REPAIR_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00005873 File Offset: 0x00003A73
		public PROTOCOL_SHOP_REPAIR_REQ()
		{
		}

		// Token: 0x04000385 RID: 901
		private int int_0;

		// Token: 0x04000386 RID: 902
		private List<long> list_0 = new List<long>();
	}
}
