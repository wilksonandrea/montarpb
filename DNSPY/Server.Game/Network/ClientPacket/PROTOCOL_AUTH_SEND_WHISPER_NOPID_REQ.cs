using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000129 RID: 297
	public class PROTOCOL_AUTH_SEND_WHISPER_NOPID_REQ : GameClientPacket
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x00004D26 File Offset: 0x00002F26
		public override void Read()
		{
			this.string_0 = base.ReadU(66);
			this.string_1 = base.ReadU((int)(base.ReadH() * 2));
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00015228 File Offset: 0x00013428
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && !(player.Nickname == this.string_0))
				{
					Account account = AccountManager.GetAccount(this.string_0, 1, 286);
					if (account != null && account.IsOnline)
					{
						account.SendPacket(new PROTOCOL_AUTH_RECV_WHISPER_ACK(player.Nickname, this.string_1, player.UseChatGM()), true);
					}
					else
					{
						Console.WriteLine("null");
						this.Client.SendPacket(new PROTOCOL_AUTH_SEND_WHISPER_ACK(this.string_0, this.string_1, 2147483648U));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_AUTH_SEND_WHISPER_NOPID_REQ()
		{
		}

		// Token: 0x04000213 RID: 531
		private long long_0;

		// Token: 0x04000214 RID: 532
		private string string_0;

		// Token: 0x04000215 RID: 533
		private string string_1;
	}
}
