// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Server.SendItemInfo
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using System;
using System.Net;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Sync.Server;

public class SendItemInfo
{
  public static void Update([In] ClanModel obj0, [In] int obj1)
  {
    try
    {
      foreach (SChannelModel server in SChannelXML.Servers)
      {
        if (server.Id != 0 && server.Id != GameXender.Client.ServerId)
        {
          IPEndPoint connection = SynchronizeXML.GetServer((int) server.Port).Connection;
          using (SyncServerPacket syncServerPacket = new SyncServerPacket())
          {
            syncServerPacket.WriteH((short) 22);
            syncServerPacket.WriteC((byte) obj1);
            switch (obj1)
            {
              case 0:
                syncServerPacket.WriteQ(obj0.OwnerId);
                break;
              case 1:
                syncServerPacket.WriteC((byte) (obj0.Name.Length + 1));
                syncServerPacket.WriteS(obj0.Name, obj0.Name.Length + 1);
                break;
              case 2:
                syncServerPacket.WriteC((byte) obj0.NameColor);
                break;
            }
            // ISSUE: reference to a compiler-generated method
            ((GameSync.Class10) GameXender.Sync).SendPacket(syncServerPacket.ToArray(), connection);
          }
        }
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public static void Load(ClanModel Player, int Member)
  {
    try
    {
      foreach (SChannelModel server in SChannelXML.Servers)
      {
        if (server.Id != 0 && server.Id != GameXender.Client.ServerId)
        {
          IPEndPoint connection = SynchronizeXML.GetServer((int) server.Port).Connection;
          using (SyncServerPacket syncServerPacket = new SyncServerPacket())
          {
            syncServerPacket.WriteH((short) 21);
            syncServerPacket.WriteC((byte) Member);
            syncServerPacket.WriteD(Player.Id);
            if (Member == 0)
            {
              syncServerPacket.WriteQ(Player.OwnerId);
              syncServerPacket.WriteD(Player.CreationDate);
              syncServerPacket.WriteC((byte) (Player.Name.Length + 1));
              syncServerPacket.WriteS(Player.Name, Player.Name.Length + 1);
              syncServerPacket.WriteC((byte) (Player.Info.Length + 1));
              syncServerPacket.WriteS(Player.Info, Player.Info.Length + 1);
            }
            // ISSUE: reference to a compiler-generated method
            ((GameSync.Class10) GameXender.Sync).SendPacket(syncServerPacket.ToArray(), connection);
          }
        }
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
