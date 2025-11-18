using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200012C RID: 300
	public class PROTOCOL_AUTH_RECV_WHISPER_REQ : GameClientPacket
	{
		// Token: 0x060002DA RID: 730 RVA: 0x00004D70 File Offset: 0x00002F70
		public override void Read()
		{
			this.string_0 = base.ReadU(66);
			this.string_1 = base.ReadU((int)(base.ReadH() * 2));
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000158A4 File Offset: 0x00013AA4
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && !(player.Nickname == this.string_0))
				{
					Account account = AccountManager.GetAccount(this.string_0, 1, 0);
					if (account != null && !(account.Nickname != this.string_0) && account.IsOnline)
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

		// Token: 0x060002DC RID: 732 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_AUTH_RECV_WHISPER_REQ()
		{
		}

		// Token: 0x0400021A RID: 538
		private string string_0;

		// Token: 0x0400021B RID: 539
		private string string_1;
	}
}
