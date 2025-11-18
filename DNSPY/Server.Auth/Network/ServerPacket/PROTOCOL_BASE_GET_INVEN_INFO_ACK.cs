using System;
using System.Collections.Generic;
using Plugin.Core.Models;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x0200001E RID: 30
	public class PROTOCOL_BASE_GET_INVEN_INFO_ACK : AuthServerPacket
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00002779 File Offset: 0x00000979
		public PROTOCOL_BASE_GET_INVEN_INFO_ACK(uint uint_1, List<ItemsModel> list_1, int int_1)
		{
			this.uint_0 = uint_1;
			this.list_0 = list_1;
			this.int_0 = int_1;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004E50 File Offset: 0x00003050
		public override void Write()
		{
			base.WriteH(2319);
			base.WriteH(0);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteH((ushort)this.list_0.Count);
				foreach (ItemsModel itemsModel in this.list_0)
				{
					base.WriteD((uint)itemsModel.ObjectId);
					base.WriteD(itemsModel.Id);
					base.WriteC((byte)itemsModel.Equip);
					base.WriteD(itemsModel.Count);
				}
				base.WriteH((ushort)this.int_0);
				base.WriteH((ushort)this.list_0.Count);
				base.WriteH((ushort)this.list_0.Count);
				base.WriteH((ushort)this.list_0.Count);
				base.WriteH(0);
			}
		}

		// Token: 0x0400003C RID: 60
		private readonly uint uint_0;

		// Token: 0x0400003D RID: 61
		private readonly int int_0;

		// Token: 0x0400003E RID: 62
		private readonly List<ItemsModel> list_0;
	}
}
