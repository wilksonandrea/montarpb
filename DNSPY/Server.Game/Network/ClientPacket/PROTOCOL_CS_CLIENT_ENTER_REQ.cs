using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000194 RID: 404
	public class PROTOCOL_CS_CLIENT_ENTER_REQ : GameClientPacket
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000204C8 File Offset: 0x0001E6C8
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
						room.ChangeSlotState(player.SlotId, SlotState.CLAN, false);
						room.StopCountDown(player.SlotId);
						room.UpdateSlotsInfo();
					}
					int num = 0;
					ClanModel clan = ClanManager.GetClan(player.ClanId);
					if (player.ClanId == 0 && player.Nickname.Length > 0)
					{
						num = DaoManagerSQL.GetRequestClanId(player.PlayerId);
					}
					this.Client.SendPacket(new PROTOCOL_CS_CLIENT_ENTER_ACK((num > 0) ? num : clan.Id, player.ClanAccess));
					if (clan.Id > 0 && num == 0)
					{
						this.Client.SendPacket(new PROTOCOL_CS_DETAIL_INFO_ACK(0, clan));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CS_CLIENT_ENTER_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_CLIENT_ENTER_REQ()
		{
		}
	}
}
