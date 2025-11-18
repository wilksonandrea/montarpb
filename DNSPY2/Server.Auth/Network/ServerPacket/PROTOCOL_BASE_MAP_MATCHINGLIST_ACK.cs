using System;
using System.Collections.Generic;
using Plugin.Core.Models;
using Plugin.Core.XML;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000025 RID: 37
	public class PROTOCOL_BASE_MAP_MATCHINGLIST_ACK : AuthServerPacket
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00002814 File Offset: 0x00000A14
		public PROTOCOL_BASE_MAP_MATCHINGLIST_ACK(List<MapMatch> list_1, int int_1)
		{
			this.list_0 = list_1;
			this.int_0 = int_1;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00006900 File Offset: 0x00004B00
		public override void Write()
		{
			base.WriteH(2464);
			base.WriteH(0);
			base.WriteC((byte)this.list_0.Count);
			foreach (MapMatch mapMatch in this.list_0)
			{
				base.WriteD(mapMatch.Mode);
				base.WriteC((byte)mapMatch.Id);
				base.WriteC((byte)SystemMapXML.GetMapRule(mapMatch.Mode).Rule);
				base.WriteC((byte)SystemMapXML.GetMapRule(mapMatch.Mode).StageOptions);
				base.WriteC((byte)SystemMapXML.GetMapRule(mapMatch.Mode).Conditions);
				base.WriteC((byte)mapMatch.Limit);
				base.WriteC((byte)mapMatch.Tag);
				base.WriteC((mapMatch.Tag == 2 || mapMatch.Tag == 3) ? 1 : 0);
				base.WriteC(1);
			}
			base.WriteD((this.list_0.Count != 100) ? 1 : 0);
			base.WriteH((ushort)(this.int_0 - 100));
			base.WriteH((ushort)SystemMapXML.Matches.Count);
		}

		// Token: 0x04000054 RID: 84
		private readonly List<MapMatch> list_0;

		// Token: 0x04000055 RID: 85
		private readonly int int_0;
	}
}
