using Plugin.Core.Network;
using Server.Auth.Data.Models;
using Server.Auth.Data.XML;
using System;

namespace Server.Auth.Data.Sync.Client
{
	public class ChannelCache
	{
		public ChannelCache()
		{
		}

		public static void Load(SyncClientPacket C)
		{
			int ınt32 = C.ReadD();
			int ınt321 = C.ReadD();
			int ınt322 = C.ReadD();
			ChannelModel channel = ChannelsXML.GetChannel(ınt32, ınt321);
			if (channel != null)
			{
				channel.TotalPlayers = ınt322;
			}
		}
	}
}