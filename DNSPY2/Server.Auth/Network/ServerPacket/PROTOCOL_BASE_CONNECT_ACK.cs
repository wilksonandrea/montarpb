using System;
using System.Collections.Generic;
using Plugin.Core.Utility;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000018 RID: 24
	public class PROTOCOL_BASE_CONNECT_ACK : AuthServerPacket
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00002678 File Offset: 0x00000878
		public PROTOCOL_BASE_CONNECT_ACK(AuthClient authClient_0)
		{
			this.int_0 = authClient_0.SessionId;
			this.ushort_0 = authClient_0.SessionSeed;
			this.list_0 = Bitwise.GenerateRSAKeyPair(this.int_0, this.SECURITY_KEY, this.SEED_LENGTH);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000047B4 File Offset: 0x000029B4
		public override void Write()
		{
			base.WriteH(2306);
			base.WriteH(0);
			base.WriteC(11);
			base.WriteB(Bitwise.HexStringToByteArray("00 00 00 00 00 00 00 00 00 00 00"));
			base.WriteH((ushort)(this.list_0[0].Length + this.list_0[1].Length + 2));
			base.WriteH((ushort)this.list_0[0].Length);
			base.WriteB(this.list_0[0]);
			base.WriteB(this.list_0[1]);
			base.WriteC(3);
			base.WriteH(80);
			base.WriteH(this.ushort_0);
			base.WriteD(this.int_0);
		}

		// Token: 0x0400002F RID: 47
		private readonly int int_0;

		// Token: 0x04000030 RID: 48
		private readonly ushort ushort_0;

		// Token: 0x04000031 RID: 49
		private readonly List<byte[]> list_0;
	}
}
