using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK : GameServerPacket
	{
		private readonly MatchModel matchModel_0;

		public PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(MatchModel matchModel_1)
		{
			this.matchModel_0 = matchModel_1;
		}

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
			SlotMatch[] slots = this.matchModel_0.Slots;
			for (int i = 0; i < (int)slots.Length; i++)
			{
				SlotMatch slotMatch = slots[i];
				Account playerBySlot = this.matchModel_0.GetPlayerBySlot(slotMatch);
				if (playerBySlot == null)
				{
					base.WriteB(new byte[43]);
				}
				else
				{
					base.WriteC((byte)playerBySlot.Rank);
					base.WriteS(playerBySlot.Nickname, 33);
					base.WriteQ(slotMatch.PlayerId);
					base.WriteC((byte)slotMatch.State);
				}
			}
		}
	}
}