using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_PAGE_CHATTING_ACK : GameServerPacket
	{
		private readonly string string_0;

		private readonly string string_1;

		private readonly int int_0;

		private readonly int int_1;

		private readonly int int_2;

		private readonly bool bool_0;

		public PROTOCOL_CS_PAGE_CHATTING_ACK(Account account_0, string string_2)
		{
			this.string_0 = account_0.Nickname;
			this.string_1 = string_2;
			this.bool_0 = account_0.UseChatGM();
			this.int_2 = account_0.NickColor;
		}

		public PROTOCOL_CS_PAGE_CHATTING_ACK(int int_3, int int_4)
		{
			this.int_0 = int_3;
			this.int_1 = int_4;
		}

		public override void Write()
		{
			base.WriteH(887);
			base.WriteC((byte)this.int_0);
			if (this.int_0 != 0)
			{
				base.WriteD(this.int_1);
				return;
			}
			base.WriteC((byte)(this.string_0.Length + 1));
			base.WriteN(this.string_0, this.string_0.Length + 2, "UTF-16LE");
			base.WriteC((byte)this.bool_0);
			base.WriteC((byte)this.int_2);
			base.WriteC((byte)(this.string_1.Length + 1));
			base.WriteN(this.string_1, this.string_1.Length + 2, "UTF-16LE");
		}
	}
}