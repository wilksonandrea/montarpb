using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Auth.Network;
using System;
using System.Collections.Generic;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_BASE_GET_INVEN_INFO_ACK : AuthServerPacket
	{
		private readonly uint uint_0;

		private readonly int int_0;

		private readonly List<ItemsModel> list_0;

		public PROTOCOL_BASE_GET_INVEN_INFO_ACK(uint uint_1, List<ItemsModel> list_1, int int_1)
		{
			this.uint_0 = uint_1;
			this.list_0 = list_1;
			this.int_0 = int_1;
		}

		public override void Write()
		{
			base.WriteH(2319);
			base.WriteH(0);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0)
			{
				base.WriteH((ushort)this.list_0.Count);
				foreach (ItemsModel list0 in this.list_0)
				{
					base.WriteD((uint)list0.ObjectId);
					base.WriteD(list0.Id);
					base.WriteC((byte)list0.Equip);
					base.WriteD(list0.Count);
				}
				base.WriteH((ushort)this.int_0);
				base.WriteH((ushort)this.list_0.Count);
				base.WriteH((ushort)this.list_0.Count);
				base.WriteH((ushort)this.list_0.Count);
				base.WriteH(0);
			}
		}
	}
}