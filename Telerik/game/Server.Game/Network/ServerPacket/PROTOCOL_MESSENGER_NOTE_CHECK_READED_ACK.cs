using Plugin.Core.Network;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK : GameServerPacket
	{
		private readonly List<int> list_0;

		public PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK(List<int> list_1)
		{
			this.list_0 = list_1;
		}

		public override void Write()
		{
			base.WriteH(1927);
			base.WriteC((byte)this.list_0.Count);
			for (int i = 0; i < this.list_0.Count; i++)
			{
				base.WriteD(this.list_0[i]);
			}
		}
	}
}