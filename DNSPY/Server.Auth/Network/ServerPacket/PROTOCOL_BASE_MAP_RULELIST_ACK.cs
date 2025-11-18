using System;
using Plugin.Core.Models.Map;
using Plugin.Core.XML;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000026 RID: 38
	public class PROTOCOL_BASE_MAP_RULELIST_ACK : AuthServerPacket
	{
		// Token: 0x06000089 RID: 137 RVA: 0x0000249C File Offset: 0x0000069C
		public PROTOCOL_BASE_MAP_RULELIST_ACK()
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00006A4C File Offset: 0x00004C4C
		public override void Write()
		{
			base.WriteH(2462);
			base.WriteH(0);
			base.WriteH((ushort)SystemMapXML.Rules.Count);
			foreach (MapRule mapRule in SystemMapXML.Rules)
			{
				base.WriteD(mapRule.Id);
				base.WriteC(0);
				base.WriteC((byte)mapRule.Rule);
				base.WriteC((byte)mapRule.StageOptions);
				base.WriteC((byte)mapRule.Conditions);
				base.WriteC(0);
				base.WriteS(mapRule.Name, 67);
			}
		}
	}
}
