using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200018C RID: 396
	public class PROTOCOL_CS_CHATTING_REQ : GameClientPacket
	{
		// Token: 0x0600040A RID: 1034 RVA: 0x000053B8 File Offset: 0x000035B8
		public override void Read()
		{
			this.chattingType_0 = (ChattingType)base.ReadH();
			this.string_0 = base.ReadU((int)(base.ReadH() * 2));
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001FED8 File Offset: 0x0001E0D8
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					int length = this.string_0.Length;
					int num = -1;
					bool flag = true;
					bool flag2 = true;
					if (length <= 60 && this.chattingType_0 == ChattingType.Clan)
					{
						using (PROTOCOL_CS_CHATTING_ACK protocol_CS_CHATTING_ACK = new PROTOCOL_CS_CHATTING_ACK(this.string_0, player))
						{
							ClanManager.SendPacket(protocol_CS_CHATTING_ACK, player.ClanId, (long)num, flag2, flag);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_CHATTING_REQ()
		{
		}

		// Token: 0x040002E4 RID: 740
		private ChattingType chattingType_0;

		// Token: 0x040002E5 RID: 741
		private string string_0;
	}
}
