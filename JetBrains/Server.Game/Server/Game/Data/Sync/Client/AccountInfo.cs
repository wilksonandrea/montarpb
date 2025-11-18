// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Client.AccountInfo
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
namespace Server.Game.Data.Sync.Client;

public class AccountInfo
{
  public static void RefreshChannel([In] int obj0, int Friend, int Type)
  {
    try
    {
      SChannelModel server = GameXender.Sync.GetServer(0);
      if (server == null)
        return;
      IPEndPoint connection = SynchronizeXML.GetServer((int) server.Port).Connection;
      using (SyncServerPacket syncServerPacket = new SyncServerPacket())
      {
        syncServerPacket.WriteH((short) 33);
        syncServerPacket.WriteD(obj0);
        syncServerPacket.WriteD(Friend);
        syncServerPacket.WriteD(Type);
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
