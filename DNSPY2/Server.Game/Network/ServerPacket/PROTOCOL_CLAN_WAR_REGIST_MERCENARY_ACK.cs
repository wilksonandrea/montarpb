using System;
using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000096 RID: 150
	public class PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK : GameServerPacket
	{
		// Token: 0x06000185 RID: 389 RVA: 0x0000392B File Offset: 0x00001B2B
		public PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(MatchModel matchModel_1)
		{
			this.matchModel_0 = matchModel_1;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000F1D4 File Offset: 0x0000D3D4
		public override void Write()
		{
			base.WriteH(6939);
			base.WriteH((short)this.matchModel_0.GetServerInfo());
			base.WriteC((byte)this.matchModel_0.State);
			base.WriteC((byte)this.matchModel_0.FriendId);
			base.WriteC((byte)this.matchModel_0.Training);
			base.WriteC((byte)this.matchModel_0.GetCountPlayers());
			base.WriteD(this.matchModel_0.Leader);
			base.WriteC(0);
			foreach (SlotMatch slotMatch in this.matchModel_0.Slots)
			{
				Account playerBySlot = this.matchModel_0.GetPlayerBySlot(slotMatch);
				if (playerBySlot != null)
				{
					base.WriteC((byte)playerBySlot.Rank);
					base.WriteS(playerBySlot.Nickname, 33);
					base.WriteQ(slotMatch.PlayerId);
					base.WriteC((byte)slotMatch.State);
				}
				else
				{
					base.WriteB(new byte[43]);
				}
			}
		}

		// Token: 0x04000121 RID: 289
		private readonly MatchModel matchModel_0;
	}
}
