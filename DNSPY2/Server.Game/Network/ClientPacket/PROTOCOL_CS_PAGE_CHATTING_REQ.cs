using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001A5 RID: 421
	public class PROTOCOL_CS_PAGE_CHATTING_REQ : GameClientPacket
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x00005584 File Offset: 0x00003784
		public override void Read()
		{
			this.chattingType_0 = (ChattingType)base.ReadH();
			this.string_0 = base.ReadU((int)(base.ReadH() * 2));
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000220C0 File Offset: 0x000202C0
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (this.chattingType_0 == ChattingType.ClanMemberPage)
					{
						using (PROTOCOL_CS_PAGE_CHATTING_ACK protocol_CS_PAGE_CHATTING_ACK = new PROTOCOL_CS_PAGE_CHATTING_ACK(player, this.string_0))
						{
							ClanManager.SendPacket(protocol_CS_PAGE_CHATTING_ACK, player.ClanId, -1L, true, true);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CS_PAGE_CHATTING_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_PAGE_CHATTING_REQ()
		{
		}

		// Token: 0x0400030C RID: 780
		private ChattingType chattingType_0;

		// Token: 0x0400030D RID: 781
		private string string_0;
	}
}
