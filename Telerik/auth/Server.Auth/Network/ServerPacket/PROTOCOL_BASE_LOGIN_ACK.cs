using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Auth.Data.Models;
using Server.Auth.Data.Utils;
using Server.Auth.Network;
using System;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_BASE_LOGIN_ACK : AuthServerPacket
	{
		private readonly EventErrorEnum eventErrorEnum_0;

		private readonly Account account_0;

		private readonly uint uint_0;

		public PROTOCOL_BASE_LOGIN_ACK(EventErrorEnum eventErrorEnum_1, Account account_1, uint uint_1)
		{
			this.eventErrorEnum_0 = eventErrorEnum_1;
			this.account_0 = account_1;
			this.uint_0 = uint_1;
		}

		private byte[] method_0(EventErrorEnum eventErrorEnum_1, Account account_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				if (!eventErrorEnum_1.Equals(EventErrorEnum.SUCCESS))
				{
					syncServerPacket.WriteB(Bitwise.HexStringToByteArray("00 00 00 00 00 00 00 00 00 00 00"));
				}
				else
				{
					syncServerPacket.WriteC((byte)string.Format("{0}", account_1.PlayerId).Length);
					syncServerPacket.WriteS(string.Format("{0}", account_1.PlayerId), string.Format("{0}", account_1.PlayerId).Length);
					syncServerPacket.WriteC(0);
					syncServerPacket.WriteC((byte)account_1.Username.Length);
					syncServerPacket.WriteS(account_1.Username, account_1.Username.Length);
					syncServerPacket.WriteQ(account_1.PlayerId);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

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
	}
}