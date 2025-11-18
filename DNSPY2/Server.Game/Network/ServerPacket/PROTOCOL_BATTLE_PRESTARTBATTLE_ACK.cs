using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000075 RID: 117
	public class PROTOCOL_BATTLE_PRESTARTBATTLE_ACK : GameServerPacket
	{
		// Token: 0x0600013A RID: 314 RVA: 0x0000D7D0 File Offset: 0x0000B9D0
		public PROTOCOL_BATTLE_PRESTARTBATTLE_ACK(Account account_1, bool bool_2)
		{
			this.account_0 = account_1;
			this.bool_1 = bool_2;
			this.roomModel_0 = account_1.Room;
			if (this.roomModel_0 != null)
			{
				this.bool_0 = this.roomModel_0.IsPreparing();
				this.uint_0 = this.roomModel_0.UniqueRoomId;
				this.uint_1 = this.roomModel_0.Seed;
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_BATTLE_PRESTARTBATTLE_ACK()
		{
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000D838 File Offset: 0x0000BA38
		public override void Write()
		{
			base.WriteH(5130);
			base.WriteD((this.bool_0 > false) ? 1 : 0);
			if (this.bool_0)
			{
				base.WriteD(this.account_0.SlotId);
				base.WriteC((byte)((this.roomModel_0.RoomType == RoomCondition.Tutorial) ? UdpState.RENDEZVOUS : ConfigLoader.UdpType));
				base.WriteB(ComDiv.AddressBytes(ConfigLoader.HOST[1]));
				base.WriteB(ComDiv.AddressBytes(ConfigLoader.HOST[1]));
				base.WriteH((ushort)ConfigLoader.DEFAULT_PORT[2]);
				base.WriteD(this.uint_0);
				base.WriteD(this.uint_1);
				base.WriteB(this.method_0(this.bool_1));
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000D8F8 File Offset: 0x0000BAF8
		private byte[] method_0(bool bool_2)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				if (bool_2)
				{
					string text = "02 14 03 15 04 16 05 17 06 18 07 19 08 1A 09 1B0A 1C 0B 1D 0C 1E 0D 1F 0E 20 0F 21 10 22 11 0012 01 13";
					syncServerPacket.WriteB(Bitwise.HexStringToByteArray(text));
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x040000E2 RID: 226
		private readonly Account account_0;

		// Token: 0x040000E3 RID: 227
		private readonly RoomModel roomModel_0;

		// Token: 0x040000E4 RID: 228
		private readonly bool bool_0;

		// Token: 0x040000E5 RID: 229
		private readonly bool bool_1;

		// Token: 0x040000E6 RID: 230
		private readonly uint uint_0;

		// Token: 0x040000E7 RID: 231
		private readonly uint uint_1;
	}
}
