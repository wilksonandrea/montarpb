using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK : GameServerPacket
	{
		private readonly RoomModel roomModel_0;

		public PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
		}

		public override void Write()
		{
			base.WriteH(3636);
			base.WriteC(0);
			base.WriteU(this.roomModel_0.LeaderName, 66);
			base.WriteD(this.roomModel_0.KillTime);
			base.WriteC(this.roomModel_0.Limit);
			base.WriteC(this.roomModel_0.WatchRuleFlag);
			base.WriteH((ushort)this.roomModel_0.BalanceType);
			base.WriteB(this.roomModel_0.RandomMaps);
			base.WriteC(this.roomModel_0.CountdownIG);
			base.WriteB(this.roomModel_0.LeaderAddr);
			base.WriteC(this.roomModel_0.KillCam);
			base.WriteH(0);
		}
	}
}