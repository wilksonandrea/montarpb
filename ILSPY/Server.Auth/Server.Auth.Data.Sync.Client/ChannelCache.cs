using Plugin.Core.Network;
using Server.Auth.Data.Models;
using Server.Auth.Data.XML;

namespace Server.Auth.Data.Sync.Client;

public class ChannelCache
{
	public static void Load(SyncClientPacket C)
	{
		int serverId = C.ReadD();
		int ıd = C.ReadD();
		int totalPlayers = C.ReadD();
		ChannelModel channel = ChannelsXML.GetChannel(serverId, ıd);
		if (channel != null)
		{
			channel.TotalPlayers = totalPlayers;
		}
	}
}
