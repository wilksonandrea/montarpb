using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Sync;
using System;
using System.Net;

namespace Server.Game.Data.Sync.Server
{
	public class SendFriendInfo
	{
		public SendFriendInfo()
		{
		}

		public static void Load(Account Player, FriendModel Friend, int Type)
		{
			try
			{
				if (Player != null)
				{
					SChannelModel server = GameXender.Sync.GetServer(Player.Status);
					if (server != null)
					{
						IPEndPoint connection = SynchronizeXML.GetServer((int)server.Port).Connection;
						using (SyncServerPacket syncServerPacket = new SyncServerPacket())
						{
							syncServerPacket.WriteH(17);
							syncServerPacket.WriteQ(Player.PlayerId);
							syncServerPacket.WriteC((byte)Type);
							syncServerPacket.WriteQ(Friend.PlayerId);
							if (Type != 2)
							{
								syncServerPacket.WriteC((byte)Friend.State);
								syncServerPacket.WriteC((byte)Friend.Removed);
							}
							GameXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}