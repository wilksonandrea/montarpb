// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Server.UpdateServer
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using System;
using System.Net;

#nullable disable
namespace Server.Game.Data.Sync.Server;

public class UpdateServer
{
  private static DateTime dateTime_0;

  public static void LoadItem(Account Clan, ItemsModel Type)
  {
    try
    {
      if (Clan == null || Clan.Status.ServerId == (byte) 0)
        return;
      SChannelModel server = GameXender.Sync.GetServer(Clan.Status);
      if (server == null)
        return;
      IPEndPoint connection = SynchronizeXML.GetServer((int) server.Port).Connection;
      using (SyncServerPacket syncServerPacket = new SyncServerPacket())
      {
        syncServerPacket.WriteH((short) 18);
        syncServerPacket.WriteQ(Clan.PlayerId);
        syncServerPacket.WriteQ(Type.ObjectId);
        syncServerPacket.WriteD(Type.Id);
        syncServerPacket.WriteC((byte) Type.Equip);
        syncServerPacket.WriteC((byte) Type.Category);
        syncServerPacket.WriteD(Type.Count);
        syncServerPacket.WriteC((byte) Type.Name.Length);
        syncServerPacket.WriteS(Type.Name, Type.Name.Length);
        // ISSUE: reference to a compiler-generated method
        ((GameSync.Class10) GameXender.Sync).SendPacket(syncServerPacket.ToArray(), connection);
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
        return;
      SChannelModel server = GameXender.Sync.GetServer(Player.Status);
      if (server == null)
        return;
      IPEndPoint connection = SynchronizeXML.GetServer((int) server.Port).Connection;
      using (SyncServerPacket syncServerPacket = new SyncServerPacket())
      {
        syncServerPacket.WriteH((short) 19);
        syncServerPacket.WriteQ(Player.PlayerId);
        syncServerPacket.WriteC((byte) 0);
        syncServerPacket.WriteC((byte) Player.Rank);
        syncServerPacket.WriteD(Player.Gold);
        syncServerPacket.WriteD(Player.Cash);
        syncServerPacket.WriteD(Player.Tags);
        // ISSUE: reference to a compiler-generated method
        ((GameSync.Class10) GameXender.Sync).SendPacket(syncServerPacket.ToArray(), connection);
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
