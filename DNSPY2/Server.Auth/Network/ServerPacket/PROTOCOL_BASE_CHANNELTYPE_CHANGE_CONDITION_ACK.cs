using System;
using System.Collections.Generic;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000016 RID: 22
	public class PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK : AuthServerPacket
	{
		// Token: 0x0600005E RID: 94 RVA: 0x0000249C File Offset: 0x0000069C
		public PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK()
		{
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000263D File Offset: 0x0000083D
		public override void Write()
		{
			base.WriteH(2490);
			base.WriteB(this.method_0(SChannelXML.Servers));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000046E0 File Offset: 0x000028E0
		private byte[] method_0(List<SChannelModel> list_0)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)list_0.Count);
				foreach (SChannelModel schannelModel in list_0)
				{
					syncServerPacket.WriteD((schannelModel.State > false) ? 1 : 0);
					syncServerPacket.WriteB(ComDiv.AddressBytes(schannelModel.Host));
					syncServerPacket.WriteH(schannelModel.Port);
					syncServerPacket.WriteC((byte)schannelModel.Type);
					syncServerPacket.WriteH((ushort)schannelModel.MaxPlayers);
					syncServerPacket.WriteD(schannelModel.LastPlayers);
				}
				syncServerPacket.WriteC(0);
				array = syncServerPacket.ToArray();
			}
			return array;
		}
	}
}
