using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000D9 RID: 217
	public class PROTOCOL_LOBBY_CHATTING_ACK : GameServerPacket
	{
		// Token: 0x06000216 RID: 534 RVA: 0x00010BE0 File Offset: 0x0000EDE0
		public PROTOCOL_LOBBY_CHATTING_ACK(Account account_0, string string_2, bool bool_1 = false)
		{
			if (!bool_1)
			{
				this.int_1 = account_0.NickColor;
				this.bool_0 = account_0.UseChatGM();
			}
			else
			{
				this.bool_0 = true;
			}
			this.string_0 = account_0.Nickname;
			this.int_0 = account_0.GetSessionId();
			this.string_1 = string_2;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00004367 File Offset: 0x00002567
		public PROTOCOL_LOBBY_CHATTING_ACK(string string_2, int int_2, int int_3, bool bool_1, string string_3)
		{
			this.string_0 = string_2;
			this.int_0 = int_2;
			this.int_1 = int_3;
			this.bool_0 = bool_1;
			this.string_1 = string_3;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00010C38 File Offset: 0x0000EE38
		public override void Write()
		{
			base.WriteH(2571);
			base.WriteD(this.int_0);
			base.WriteC((byte)(this.string_0.Length + 1));
			base.WriteN(this.string_0, this.string_0.Length + 2, "UTF-16LE");
			base.WriteC((byte)this.int_1);
			base.WriteC((this.bool_0 > false) ? 1 : 0);
			base.WriteH((ushort)(this.string_1.Length + 1));
			base.WriteN(this.string_1, this.string_1.Length + 2, "UTF-16LE");
		}

		// Token: 0x04000184 RID: 388
		private readonly string string_0;

		// Token: 0x04000185 RID: 389
		private readonly string string_1;

		// Token: 0x04000186 RID: 390
		private readonly int int_0;

		// Token: 0x04000187 RID: 391
		private readonly int int_1;

		// Token: 0x04000188 RID: 392
		private readonly bool bool_0;
	}
}
