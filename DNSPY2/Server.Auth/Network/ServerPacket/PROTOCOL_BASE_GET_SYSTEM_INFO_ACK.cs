using System;
using System.Collections.Generic;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Auth.Data.Models;
using Server.Auth.Data.XML;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000020 RID: 32
	public class PROTOCOL_BASE_GET_SYSTEM_INFO_ACK : AuthServerPacket
	{
		// Token: 0x06000074 RID: 116 RVA: 0x000051F4 File Offset: 0x000033F4
		public PROTOCOL_BASE_GET_SYSTEM_INFO_ACK(ServerConfig serverConfig_1)
		{
			this.serverConfig_0 = serverConfig_1;
			if (serverConfig_1 != null)
			{
				this.list_0 = SChannelXML.Servers;
				this.list_1 = PlayerRankXML.Ranks;
				this.eventPlaytimeModel_0 = EventPlaytimeXML.GetRunningEvent();
			}
			this.string_0 = new string[] { "ded9a5bc68c44c6b885ac376be4f08c6", "5c67549f9ea01f1c7429d2a6bb121844" };
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00005250 File Offset: 0x00003450
		public override void Write()
		{
			base.WriteH(2315);
			base.WriteH(0);
			base.WriteC((byte)this.string_0[0].Length);
			base.WriteS(this.string_0[0], this.string_0[0].Length);
			base.WriteC((byte)this.string_0[1].Length);
			base.WriteS(this.string_0[1], this.string_0[1].Length);
			base.WriteD(0);
			base.WriteD(16);
			base.WriteB(new byte[61]);
			base.WriteH(5);
			base.WriteH(120);
			base.WriteH(1026);
			base.WriteC(0);
			base.WriteH(770);
			base.WriteC(1);
			base.WriteH(200);
			base.WriteH(0);
			base.WriteD(50);
			base.WriteD(50);
			base.WriteC(1);
			base.WriteH(1000);
			base.WriteC(0);
			base.WriteD(153699);
			base.WriteC(0);
			base.WriteC(1);
			base.WriteB(new byte[373]);
			base.WriteC((byte)this.serverConfig_0.Showroom);
			base.WriteC(5);
			base.WriteC(4);
			base.WriteH(3500);
			base.WriteH(0);
			base.WriteH(1450);
			base.WriteH(0);
			base.WriteD(49);
			base.WriteD(1);
			base.WriteH(1793);
			base.WriteC(1);
			base.WriteH(8483);
			base.WriteH(0);
			base.WriteB(new byte[52]);
			base.WriteH(2565);
			base.WriteB(new byte[229]);
			base.WriteB(this.method_2(this.eventPlaytimeModel_0));
			base.WriteC((this.serverConfig_0.Missions > false) ? 1 : 0);
			base.WriteH((ushort)MissionConfigXML.MissionPage1);
			base.WriteH((ushort)MissionConfigXML.MissionPage2);
			base.WriteH((ushort)this.SECURITY_KEY);
			base.WriteB(this.method_0(this.list_0));
			base.WriteC(1);
			base.WriteC((byte)this.NATIONS);
			base.WriteC(0);
			base.WriteH((short)this.serverConfig_0.ShopURL.Length);
			base.WriteS(this.serverConfig_0.ShopURL, this.serverConfig_0.ShopURL.Length);
			base.WriteB(this.method_1(this.list_1));
			base.WriteC(0);
			base.WriteC(6);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004958 File Offset: 0x00002B58
		private byte[] method_0(List<SChannelModel> list_2)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)list_2.Count);
				foreach (SChannelModel schannelModel in list_2)
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

		// Token: 0x06000077 RID: 119 RVA: 0x000054F0 File Offset: 0x000036F0
		private byte[] method_1(List<RankModel> list_2)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)list_2.Count);
				foreach (RankModel rankModel in list_2)
				{
					syncServerPacket.WriteC((byte)rankModel.Id);
					List<int> rewards = PlayerRankXML.GetRewards(rankModel.Id);
					foreach (int num in rewards)
					{
						GoodsItem good = ShopManager.GetGood(num);
						syncServerPacket.WriteD((good == null) ? 0 : good.Id);
					}
					int num2 = rewards.Count;
					while (4 - num2 > 0)
					{
						syncServerPacket.WriteD(0);
						num2++;
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000055FC File Offset: 0x000037FC
		private byte[] method_2(EventPlaytimeModel eventPlaytimeModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC(3);
				if (eventPlaytimeModel_1 != null && eventPlaytimeModel_1.EventIsEnabled())
				{
					syncServerPacket.WriteD(eventPlaytimeModel_1.Minutes1 * 60);
					syncServerPacket.WriteD(eventPlaytimeModel_1.Minutes2 * 60);
					syncServerPacket.WriteD(eventPlaytimeModel_1.Minutes3 * 60);
				}
				else
				{
					syncServerPacket.WriteB(new byte[12]);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x04000041 RID: 65
		private readonly ServerConfig serverConfig_0;

		// Token: 0x04000042 RID: 66
		private readonly List<SChannelModel> list_0;

		// Token: 0x04000043 RID: 67
		private readonly List<RankModel> list_1;

		// Token: 0x04000044 RID: 68
		private readonly EventPlaytimeModel eventPlaytimeModel_0;

		// Token: 0x04000045 RID: 69
		private readonly string[] string_0;
	}
}
