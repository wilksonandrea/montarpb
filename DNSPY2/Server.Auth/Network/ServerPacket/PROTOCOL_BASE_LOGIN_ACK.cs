using System;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Auth.Data.Models;
using Server.Auth.Data.Utils;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000022 RID: 34
	public class PROTOCOL_BASE_LOGIN_ACK : AuthServerPacket
	{
		// Token: 0x06000080 RID: 128 RVA: 0x000027AC File Offset: 0x000009AC
		public PROTOCOL_BASE_LOGIN_ACK(EventErrorEnum eventErrorEnum_1, Account account_1, uint uint_1)
		{
			this.eventErrorEnum_0 = eventErrorEnum_1;
			this.account_0 = account_1;
			this.uint_0 = uint_1;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000678C File Offset: 0x0000498C
		public override void Write()
		{
			base.WriteH(1283);
			base.WriteH(0);
			base.WriteD(this.uint_0);
			base.WriteB(new byte[12]);
			base.WriteD(AllUtils.GetFeatures());
			base.WriteH(1402);
			base.WriteB(new byte[16]);
			base.WriteB(this.method_0(this.eventErrorEnum_0, this.account_0));
			base.WriteD((uint)this.eventErrorEnum_0);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000680C File Offset: 0x00004A0C
		private byte[] method_0(EventErrorEnum eventErrorEnum_1, Account account_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				if (eventErrorEnum_1.Equals(EventErrorEnum.SUCCESS))
				{
					syncServerPacket.WriteC((byte)string.Format("{0}", account_1.PlayerId).Length);
					syncServerPacket.WriteS(string.Format("{0}", account_1.PlayerId), string.Format("{0}", account_1.PlayerId).Length);
					syncServerPacket.WriteC(0);
					syncServerPacket.WriteC((byte)account_1.Username.Length);
					syncServerPacket.WriteS(account_1.Username, account_1.Username.Length);
					syncServerPacket.WriteQ(account_1.PlayerId);
				}
				else
				{
					syncServerPacket.WriteB(Bitwise.HexStringToByteArray("00 00 00 00 00 00 00 00 00 00 00"));
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x04000050 RID: 80
		private readonly EventErrorEnum eventErrorEnum_0;

		// Token: 0x04000051 RID: 81
		private readonly Account account_0;

		// Token: 0x04000052 RID: 82
		private readonly uint uint_0;
	}
}
