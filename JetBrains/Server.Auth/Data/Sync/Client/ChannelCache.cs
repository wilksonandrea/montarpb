// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.Sync.Client.ChannelCache
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Auth.Data.Sync.Server;
using System;
using System.Net;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Data.Sync.Client;

public class ChannelCache
{
  public static void SendRefreshPacket(
    [In] int obj0,
    long PlayerId,
    [In] long obj2,
    [In] bool obj3,
    [In] SChannelModel obj4)
  {
    IPEndPoint connection = SynchronizeXML.GetServer((int) obj4.Port).Connection;
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteH((short) 11);
      syncServerPacket.WriteC((byte) obj0);
      syncServerPacket.WriteC((byte) obj3);
      syncServerPacket.WriteQ(PlayerId);
      syncServerPacket.WriteQ(obj2);
      // ISSUE: reference to a compiler-generated method
      ((AuthSync.Class3) AuthXender.Sync).SendPacket(syncServerPacket.ToArray(), connection);
    }
  }

  public static void RefreshSChannel(int Type)
  {
    try
    {
      if (ComDiv.GetDuration(UpdateServer.dateTime_0) < (double) ConfigLoader.UpdateIntervalPlayersServer)
        return;
      UpdateServer.dateTime_0 = DateTimeUtil.Now();
      int count = AuthXender.SocketSessions.Count;
      foreach (SChannelModel server in SChannelXML.Servers)
      {
        if (server.Id == Type)
        {
          server.LastPlayers = count;
        }
        else
        {
          IPEndPoint connection = SynchronizeXML.GetServer((int) server.Port).Connection;
          using (SyncServerPacket syncServerPacket = new SyncServerPacket())
          {
            syncServerPacket.WriteH((short) 15);
            syncServerPacket.WriteD(Type);
            syncServerPacket.WriteD(count);
            // ISSUE: reference to a compiler-generated method
            ((AuthSync.Class3) AuthXender.Sync).SendPacket(syncServerPacket.ToArray(), connection);
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
