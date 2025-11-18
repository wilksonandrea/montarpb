using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using System;

namespace Server.Game.Data.Sync.Client
{
	public class ServerCache
	{
		public ServerCache()
		{
		}

		public static void Load(SyncClientPacket C)
		{
			int ınt32 = C.ReadD();
			int ınt321 = C.ReadD();
			SChannelModel server = SChannelXML.GetServer(ınt32);
			if (server != null)
			{
				server.LastPlayers = ınt321;
			}
		}
	}
}