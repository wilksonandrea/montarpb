using System.Collections.Generic;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Auth.Data.Models;
using Server.Auth.Data.XML;

namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_GET_SYSTEM_INFO_ACK : AuthServerPacket
{
	private readonly ServerConfig serverConfig_0;

	private readonly List<SChannelModel> list_0;

	private readonly List<RankModel> list_1;

	private readonly EventPlaytimeModel eventPlaytimeModel_0;

	private readonly string[] string_0;

	public PROTOCOL_BASE_GET_SYSTEM_INFO_ACK(ServerConfig serverConfig_1)
	{
		serverConfig_0 = serverConfig_1;
		if (serverConfig_1 != null)
		{
			list_0 = SChannelXML.Servers;
			list_1 = PlayerRankXML.Ranks;
			eventPlaytimeModel_0 = EventPlaytimeXML.GetRunningEvent();
		}
		string_0 = new string[2] { "ded9a5bc68c44c6b885ac376be4f08c6", "5c67549f9ea01f1c7429d2a6bb121844" };
	}

	public override void Write()
	{
		WriteH(2315);
		WriteH(0);
		WriteC((byte)string_0[0].Length);
		WriteS(string_0[0], string_0[0].Length);
		WriteC((byte)string_0[1].Length);
		WriteS(string_0[1], string_0[1].Length);
		WriteD(0);
		WriteD(16);
		WriteB(new byte[61]);
		WriteH(5);
		WriteH(120);
		WriteH(1026);
		WriteC(0);
		WriteH(770);
		WriteC(1);
		WriteH(200);
		WriteH(0);
		WriteD(50);
		WriteD(50);
		WriteC(1);
		WriteH(1000);
		WriteC(0);
		WriteD(153699);
		WriteC(0);
		WriteC(1);
		WriteB(new byte[373]);
		WriteC((byte)serverConfig_0.Showroom);
		WriteC(5);
		WriteC(4);
		WriteH(3500);
		WriteH(0);
		WriteH(1450);
		WriteH(0);
		WriteD(49);
		WriteD(1);
		WriteH(1793);
		WriteC(1);
		WriteH(8483);
		WriteH(0);
		WriteB(new byte[52]);
		WriteH(2565);
		WriteB(new byte[229]);
		WriteB(method_2(eventPlaytimeModel_0));
		WriteC(serverConfig_0.Missions ? ((byte)1) : ((byte)0));
		WriteH((ushort)MissionConfigXML.MissionPage1);
		WriteH((ushort)MissionConfigXML.MissionPage2);
		WriteH((ushort)SECURITY_KEY);
		WriteB(method_0(list_0));
		WriteC(1);
		WriteC((byte)NATIONS);
		WriteC(0);
		WriteH((short)serverConfig_0.ShopURL.Length);
		WriteS(serverConfig_0.ShopURL, serverConfig_0.ShopURL.Length);
		WriteB(method_1(list_1));
		WriteC(0);
		WriteC(6);
	}

	private byte[] method_0(List<SChannelModel> list_2)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteC((byte)list_2.Count);
		foreach (SChannelModel item in list_2)
		{
			syncServerPacket.WriteD(item.State ? 1 : 0);
			syncServerPacket.WriteB(ComDiv.AddressBytes(item.Host));
			syncServerPacket.WriteB(ComDiv.AddressBytes(item.Host));
			syncServerPacket.WriteH(item.Port);
			syncServerPacket.WriteC((byte)item.Type);
			syncServerPacket.WriteH((ushort)item.MaxPlayers);
			syncServerPacket.WriteD(item.LastPlayers);
			if (item.Id == 0)
			{
				syncServerPacket.WriteB(Bitwise.HexStringToByteArray("01 01 01 01 01 01 01 01 01 01 0E 00 00 00 00"));
				continue;
			}
			foreach (ChannelModel channel in ChannelsXML.GetChannels(item.Id))
			{
				syncServerPacket.WriteC((byte)channel.Type);
			}
			syncServerPacket.WriteC((byte)item.Type);
			syncServerPacket.WriteC(0);
			syncServerPacket.WriteH(0);
		}
		return syncServerPacket.ToArray();
	}

	private byte[] method_1(List<RankModel> list_2)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteC((byte)list_2.Count);
		foreach (RankModel item in list_2)
		{
			syncServerPacket.WriteC((byte)item.Id);
			List<int> rewards = PlayerRankXML.GetRewards(item.Id);
			foreach (int item2 in rewards)
			{
				syncServerPacket.WriteD(ShopManager.GetGood(item2)?.Id ?? 0);
			}
			for (int i = rewards.Count; 4 - i > 0; i++)
			{
				syncServerPacket.WriteD(0);
			}
		}
		return syncServerPacket.ToArray();
	}

	private byte[] method_2(EventPlaytimeModel eventPlaytimeModel_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
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
		return syncServerPacket.ToArray();
	}
}
