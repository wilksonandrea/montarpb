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
	public class SendItemInfo
	{
		public SendItemInfo()
		{
		}

		public static void LoadGoldCash(Account Player)
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
							syncServerPacket.WriteH(19);
							syncServerPacket.WriteQ(Player.PlayerId);
							syncServerPacket.WriteC(0);
							syncServerPacket.WriteC((byte)Player.Rank);
							syncServerPacket.WriteD(Player.Gold);
							syncServerPacket.WriteD(Player.Cash);
							syncServerPacket.WriteD(Player.Tags);
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

		public static void LoadItem(Account Player, ItemsModel Item)
		{
			try
			{
				if (Player != null && Player.Status.ServerId != 0)
				{
					SChannelModel server = GameXender.Sync.GetServer(Player.Status);
					if (server != null)
					{
						IPEndPoint connection = SynchronizeXML.GetServer((int)server.Port).Connection;
						using (SyncServerPacket syncServerPacket = new SyncServerPacket())
						{
							syncServerPacket.WriteH(18);
							syncServerPacket.WriteQ(Player.PlayerId);
							syncServerPacket.WriteQ(Item.ObjectId);
							syncServerPacket.WriteD(Item.Id);
							syncServerPacket.WriteC((byte)Item.Equip);
							syncServerPacket.WriteC((byte)Item.Category);
							syncServerPacket.WriteD(Item.Count);
							syncServerPacket.WriteC((byte)Item.Name.Length);
							syncServerPacket.WriteS(Item.Name, Item.Name.Length);
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