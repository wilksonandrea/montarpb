using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_LOBBY_CHATTING_ACK : GameServerPacket
	{
		private readonly string string_0;

		private readonly string string_1;

		private readonly int int_0;

		private readonly int int_1;

		private readonly bool bool_0;

		public PROTOCOL_LOBBY_CHATTING_ACK(Account account_0, string string_2, bool bool_1 = false)
		{
			if (bool_1)
			{
				this.bool_0 = true;
			}
			else
			{
				this.int_1 = account_0.NickColor;
				this.bool_0 = account_0.UseChatGM();
			}
			this.string_0 = account_0.Nickname;
			this.int_0 = account_0.GetSessionId();
			this.string_1 = string_2;
		}

		public PROTOCOL_LOBBY_CHATTING_ACK(string string_2, int int_2, int int_3, bool bool_1, string string_3)
		{
			this.string_0 = string_2;
			this.int_0 = int_2;
			this.int_1 = int_3;
			this.bool_0 = bool_1;
			this.string_1 = string_3;
		}

		public override void Write()
		{
			base.WriteH(2571);
			base.WriteD(this.int_0);
			base.WriteC((byte)(this.string_0.Length + 1));
			base.WriteN(this.string_0, this.string_0.Length + 2, "UTF-16LE");
			base.WriteC((byte)this.int_1);
			base.WriteC((byte)this.bool_0);
			base.WriteH((ushort)(this.string_1.Length + 1));
			base.WriteN(this.string_1, this.string_1.Length + 2, "UTF-16LE");
		}
	}
}