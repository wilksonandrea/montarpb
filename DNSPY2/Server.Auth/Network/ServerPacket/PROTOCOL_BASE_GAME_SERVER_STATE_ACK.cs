using System;
using System.Collections.Generic;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Auth.Data.Models;
using Server.Auth.Data.XML;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x0200001B RID: 27
	public class PROTOCOL_BASE_GAME_SERVER_STATE_ACK : AuthServerPacket
	{
		// Token: 0x06000069 RID: 105 RVA: 0x0000249C File Offset: 0x0000069C
		public PROTOCOL_BASE_GAME_SERVER_STATE_ACK()
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000270D File Offset: 0x0000090D
		public override void Write()
		{
			base.WriteH(2400);
			base.WriteB(this.method_0(SChannelXML.Servers));
			base.WriteC(0);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004958 File Offset: 0x00002B58
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
					syncServerPacket.WriteB(ComDiv.AddressBytes(schannelModel.Host));
					syncServerPacket.WriteH(schannelModel.Port);
					syncServerPacket.WriteC((byte)schannelModel.Type);
					syncServerPacket.WriteH((ushort)schannelModel.MaxPlayers);
					syncServerPacket.WriteD(schannelModel.LastPlayers);
					if (schannelModel.Id == 0)
					{
						syncServerPacket.WriteB(Bitwise.HexStringToByteArray("01 01 01 01 01 01 01 01 01 01 0E 00 00 00 00"));
					}
					else
					{
						foreach (ChannelModel channelModel in ChannelsXML.GetChannels(schannelModel.Id))
						{
							syncServerPacket.WriteC((byte)channelModel.Type);
						}
						syncServerPacket.WriteC((byte)schannelModel.Type);
						syncServerPacket.WriteC(0);
						syncServerPacket.WriteH(0);
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}
	}
}
