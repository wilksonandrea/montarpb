using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		private readonly List<QuickstartModel> list_0;

		private readonly QuickstartModel quickstartModel_0;

		private readonly RoomModel roomModel_0;

		public PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK(uint uint_1, List<QuickstartModel> list_1, RoomModel roomModel_1, QuickstartModel quickstartModel_1)
		{
			this.uint_0 = uint_1;
			this.list_0 = list_1;
			this.quickstartModel_0 = quickstartModel_1;
			this.roomModel_0 = roomModel_1;
		}

		public override void Write()
		{
			base.WriteH(5378);
			base.WriteD(this.uint_0);
			foreach (QuickstartModel list0 in this.list_0)
			{
				base.WriteC((byte)list0.MapId);
				base.WriteC((byte)list0.Rule);
				base.WriteC((byte)list0.StageOptions);
				base.WriteC((byte)list0.Type);
			}
			if (this.uint_0 == 0)
			{
				base.WriteC((byte)this.roomModel_0.ChannelId);
				base.WriteD(this.roomModel_0.RoomId);
				base.WriteH(1);
				if (this.uint_0 == 0)
				{
					base.WriteC(0);
					base.WriteC(0);
					base.WriteC(0);
					base.WriteC(0);
				}
				else
				{
					base.WriteC((byte)this.quickstartModel_0.MapId);
					base.WriteC((byte)this.quickstartModel_0.Rule);
					base.WriteC((byte)this.quickstartModel_0.StageOptions);
					base.WriteC((byte)this.quickstartModel_0.Type);
				}
				base.WriteD(0);
				base.WriteD(0);
				base.WriteD(0);
				base.WriteD(0);
			}
		}
	}
}