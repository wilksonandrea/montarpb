using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Auth.Data.Models;
using Server.Auth.Data.Utils;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket
{
	// Token: 0x0200003E RID: 62
	public class PROTOCOL_BASE_GET_INVEN_INFO_REQ : AuthClientPacket
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x00002A1B File Offset: 0x00000C1B
		public override void Read()
		{
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007464 File Offset: 0x00005664
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					List<ItemsModel> list = AllUtils.LimitationIndex(player, player.Inventory.Items);
					if (list.Count > 0)
					{
						int num = TemplatePackXML.Basics.Count;
						if (TemplatePackXML.GetPCCafe(player.CafePC) != null)
						{
							num += TemplatePackXML.GetPCCafeRewards(player.CafePC).Count;
						}
						this.Client.SendPacket(new PROTOCOL_BASE_GET_INVEN_INFO_ACK(0U, list, num));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_GET_INVEN_INFO_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00002A1D File Offset: 0x00000C1D
		public PROTOCOL_BASE_GET_INVEN_INFO_REQ()
		{
		}
	}
}
