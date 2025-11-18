using System;
using System.Collections.Generic;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000009 RID: 9
	public class PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK : GameServerPacket
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK()
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000267A File Offset: 0x0000087A
		public override void Write()
		{
			base.WriteH(2490);
			base.WriteB(this.method_0(SChannelXML.Servers));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00008D8C File Offset: 0x00006F8C
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
