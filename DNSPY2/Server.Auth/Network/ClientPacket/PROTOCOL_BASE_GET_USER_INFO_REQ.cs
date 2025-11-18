using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth.Data.Models;
using Server.Auth.Data.Utils;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket
{
	// Token: 0x02000042 RID: 66
	public class PROTOCOL_BASE_GET_USER_INFO_REQ : AuthClientPacket
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00002A1B File Offset: 0x00000C1B
		public override void Read()
		{
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00007A70 File Offset: 0x00005C70
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.Inventory.Items.Count == 0)
					{
						AllUtils.ValidateAuthLevel(player);
						AllUtils.LoadPlayerInventory(player);
						AllUtils.LoadPlayerMissions(player);
						AllUtils.ValidatePlayerInventoryStatus(player);
						AllUtils.DiscountPlayerItems(player);
						AllUtils.CheckGameEvents(player);
						this.Client.SendPacket(new PROTOCOL_BASE_GET_USER_INFO_ACK(player));
						this.Client.SendPacket(new PROTOCOL_BASE_GET_CHARA_INFO_ACK(player));
						AllUtils.ProcessBattlepass(player);
						this.Client.SendPacket(new PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE());
						this.Client.SendPacket(new PROTOCOL_SEASON_CHALLENGE_INFO_ACK(player));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_GET_USER_INFO_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00002A1D File Offset: 0x00000C1D
		public PROTOCOL_BASE_GET_USER_INFO_REQ()
		{
		}
	}
}
