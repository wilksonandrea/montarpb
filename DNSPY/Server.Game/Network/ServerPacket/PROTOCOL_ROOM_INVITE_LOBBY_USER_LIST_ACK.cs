using System;
using System.Collections.Generic;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000F3 RID: 243
	public class PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK : GameServerPacket
	{
		// Token: 0x0600024F RID: 591 RVA: 0x0001232C File Offset: 0x0001052C
		public PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK(ChannelModel channelModel_0)
		{
			this.list_0 = channelModel_0.GetWaitPlayers();
			this.list_1 = this.method_0(this.list_0.Count, (this.list_0.Count >= 8) ? 8 : this.list_0.Count);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00012380 File Offset: 0x00010580
		public override void Write()
		{
			base.WriteH(3676);
			base.WriteD(this.list_1.Count);
			foreach (int num in this.list_1)
			{
				Account account = this.list_0[num];
				base.WriteD(account.GetSessionId());
				base.WriteD(account.GetRank());
				base.WriteC((byte)(account.Nickname.Length + 1));
				base.WriteN(account.Nickname, account.Nickname.Length + 2, "UTF-16LE");
				base.WriteC((byte)account.NickColor);
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00012450 File Offset: 0x00010650
		private List<int> method_0(int int_0, int int_1)
		{
			if (int_0 != 0 && int_1 != 0)
			{
				Random random = new Random();
				List<int> list = new List<int>();
				for (int i = 0; i < int_0; i++)
				{
					list.Add(i);
				}
				for (int j = 0; j < list.Count; j++)
				{
					int num = random.Next(list.Count);
					int num2 = list[j];
					list[j] = list[num];
					list[num] = num2;
				}
				return list.GetRange(0, int_1);
			}
			return new List<int>();
		}

		// Token: 0x040001BE RID: 446
		private readonly List<Account> list_0;

		// Token: 0x040001BF RID: 447
		private readonly List<int> list_1;
	}
}
