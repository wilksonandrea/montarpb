using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;

namespace Server.Game.Data.Sync.Client;

public class ServerCache
{
	public static void Load(SyncClientPacket C)
	{
		int id = C.ReadD();
		int lastPlayers = C.ReadD();
		SChannelModel server = SChannelXML.GetServer(id);
		if (server != null)
		{
			server.LastPlayers = lastPlayers;
		}
	}
}
