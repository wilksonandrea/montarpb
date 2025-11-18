using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Auth.Data.Models;
using Server.Auth.Data.XML;
using Server.Auth.Network;
using System;
using System.Collections.Generic;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_BASE_GAME_SERVER_STATE_ACK : AuthServerPacket
	{
		public PROTOCOL_BASE_GAME_SERVER_STATE_ACK()
		{
		}

		private byte[] method_0(List<SChannelModel> list_0)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)list_0.Count);
				foreach (SChannelModel list0 in list_0)
				{
					syncServerPacket.WriteD(list0.State);
					syncServerPacket.WriteB(ComDiv.AddressBytes(list0.Host));
					syncServerPacket.WriteB(ComDiv.AddressBytes(list0.Host));
					syncServerPacket.WriteH(list0.Port);
					syncServerPacket.WriteC((byte)list0.Type);
					syncServerPacket.WriteH((ushort)list0.MaxPlayers);
					syncServerPacket.WriteD(list0.LastPlayers);
					if (list0.Id != 0)
					{
						foreach (ChannelModel channel in ChannelsXML.GetChannels(list0.Id))
						{
							syncServerPacket.WriteC((byte)channel.Type);
						}
						syncServerPacket.WriteC((byte)list0.Type);
						syncServerPacket.WriteC(0);
						syncServerPacket.WriteH(0);
					}
					else
					{
						syncServerPacket.WriteB(Bitwise.HexStringToByteArray("01 01 01 01 01 01 01 01 01 01 0E 00 00 00 00"));
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		public override void Write()
		{
			base.WriteH(2400);
			base.WriteB(this.method_0(SChannelXML.Servers));
			base.WriteC(0);
		}
	}
}