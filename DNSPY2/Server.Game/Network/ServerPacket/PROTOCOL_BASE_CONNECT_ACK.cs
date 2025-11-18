using System;
using System.Collections.Generic;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.XML;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200004C RID: 76
	public class PROTOCOL_BASE_CONNECT_ACK : GameServerPacket
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x0000B30C File Offset: 0x0000950C
		public PROTOCOL_BASE_CONNECT_ACK(GameClient gameClient_0)
		{
			this.int_0 = gameClient_0.ServerId;
			this.int_1 = gameClient_0.SessionId;
			this.ushort_0 = gameClient_0.SessionSeed;
			this.list_0 = Bitwise.GenerateRSAKeyPair(this.int_1, this.SECURITY_KEY, this.SEED_LENGTH);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000B360 File Offset: 0x00009560
		public override void Write()
		{
			base.WriteH(2306);
			base.WriteH(0);
			base.WriteC((byte)ChannelsXML.GetChannels(this.int_0).Count);
			foreach (ChannelModel channelModel in ChannelsXML.GetChannels(this.int_0))
			{
				base.WriteC((byte)channelModel.Type);
			}
			base.WriteH((ushort)(this.list_0[0].Length + this.list_0[1].Length + 2));
			base.WriteH((ushort)this.list_0[0].Length);
			base.WriteB(this.list_0[0]);
			base.WriteB(this.list_0[1]);
			base.WriteC(3);
			base.WriteH(80);
			base.WriteH(this.ushort_0);
			base.WriteD(this.int_1);
		}

		// Token: 0x04000098 RID: 152
		private readonly int int_0;

		// Token: 0x04000099 RID: 153
		private readonly int int_1;

		// Token: 0x0400009A RID: 154
		private readonly ushort ushort_0;

		// Token: 0x0400009B RID: 155
		private readonly List<byte[]> list_0;
	}
}
