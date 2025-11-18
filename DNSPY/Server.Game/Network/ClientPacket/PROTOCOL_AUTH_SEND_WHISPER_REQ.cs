using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200012D RID: 301
	public class PROTOCOL_AUTH_SEND_WHISPER_REQ : GameClientPacket
	{
		// Token: 0x060002DD RID: 733 RVA: 0x00004D94 File Offset: 0x00002F94
		public override void Read()
		{
			this.long_0 = base.ReadQ();
			this.string_0 = base.ReadU(66);
			this.string_1 = base.ReadU((int)(base.ReadH() * 2));
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00015968 File Offset: 0x00013B68
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && !(player.Nickname == this.string_0))
				{
					Account account = AccountManager.GetAccount(this.long_0, 31);
					if (account != null && account.IsOnline)
					{
						account.SendPacket(new PROTOCOL_AUTH_RECV_WHISPER_ACK(player.Nickname, this.string_1, player.UseChatGM()), false);
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_SEND_WHISPER_ACK(this.string_0, this.string_1, 2147483648U));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_AUTH_SEND_WHISPER_REQ()
		{
		}

		// Token: 0x0400021C RID: 540
		private long long_0;

		// Token: 0x0400021D RID: 541
		private string string_0;

		// Token: 0x0400021E RID: 542
		private string string_1;
	}
}
