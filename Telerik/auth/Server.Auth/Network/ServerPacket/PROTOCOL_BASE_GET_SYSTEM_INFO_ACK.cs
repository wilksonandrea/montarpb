using Plugin.Core.Managers;
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
	public class PROTOCOL_BASE_GET_SYSTEM_INFO_ACK : AuthServerPacket
	{
		private readonly ServerConfig serverConfig_0;

		private readonly List<SChannelModel> list_0;

		private readonly List<RankModel> list_1;

		private readonly EventPlaytimeModel eventPlaytimeModel_0;

		private readonly string[] string_0;

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

		private byte[] method_0(List<SChannelModel> list_2)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)list_2.Count);
				foreach (SChannelModel list2 in list_2)
				{
					syncServerPacket.WriteD(list2.State);
					syncServerPacket.WriteB(ComDiv.AddressBytes(list2.Host));
					syncServerPacket.WriteB(ComDiv.AddressBytes(list2.Host));
					syncServerPacket.WriteH(list2.Port);
					syncServerPacket.WriteC((byte)list2.Type);
					syncServerPacket.WriteH((ushort)list2.MaxPlayers);
					syncServerPacket.WriteD(list2.LastPlayers);
					if (list2.Id != 0)
					{
						foreach (ChannelModel channel in ChannelsXML.GetChannels(list2.Id))
						{
							syncServerPacket.WriteC((byte)channel.Type);
						}
						syncServerPacket.WriteC((byte)list2.Type);
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

		private byte[] method_1(List<RankModel> list_2)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)list_2.Count);
				foreach (RankModel list2 in list_2)
				{
					syncServerPacket.WriteC((byte)list2.Id);
					List<int> rewards = PlayerRankXML.GetRewards(list2.Id);
					foreach (int reward in rewards)
					{
						GoodsItem good = ShopManager.GetGood(reward);
						syncServerPacket.WriteD((good == null ? 0 : good.Id));
					}
					for (int i = rewards.Count; 4 - i > 0; i++)
					{
						syncServerPacket.WriteD(0);
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private byte[] method_2(EventPlaytimeModel eventPlaytimeModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC(3);
				if (eventPlaytimeModel_1 == null || !eventPlaytimeModel_1.EventIsEnabled())
				{
					syncServerPacket.WriteB(new byte[12]);
				}
				else
				{
					syncServerPacket.WriteD(eventPlaytimeModel_1.Minutes1 * 60);
					syncServerPacket.WriteD(eventPlaytimeModel_1.Minutes2 * 60);
					syncServerPacket.WriteD(eventPlaytimeModel_1.Minutes3 * 60);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

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
			base.WriteC((byte)this.serverConfig_0.Missions);
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
	}
}