using System;
using System.Net;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Game.Data.Models;

namespace Server.Game.Data.Sync.Server;

public class SendItemInfo
{
	public static void LoadItem(Account Player, ItemsModel Item)
	{
		try
		{
			if (Player == null || Player.Status.ServerId == 0)
			{
				return;
			}
			SChannelModel server = GameXender.Sync.GetServer(Player.Status);
			if (server != null)
			{
				IPEndPoint connection = SynchronizeXML.GetServer(server.Port).Connection;
				using SyncServerPacket syncServerPacket = new SyncServerPacket();
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
				return;
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public static void LoadGoldCash(Account Player)
	{
		try
		{
			if (Player == null)
			{
				return;
			}
			SChannelModel server = GameXender.Sync.GetServer(Player.Status);
			if (server == null)
			{
				return;
			}
			IPEndPoint connection = SynchronizeXML.GetServer(server.Port).Connection;
			using SyncServerPacket syncServerPacket = new SyncServerPacket();
			syncServerPacket.WriteH(19);
			syncServerPacket.WriteQ(Player.PlayerId);
			syncServerPacket.WriteC(0);
			syncServerPacket.WriteC((byte)Player.Rank);
			syncServerPacket.WriteD(Player.Gold);
			syncServerPacket.WriteD(Player.Cash);
			syncServerPacket.WriteD(Player.Tags);
			GameXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
