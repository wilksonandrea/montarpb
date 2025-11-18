// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Server.UpdateChannel
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
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Sync.Server;

public class UpdateChannel
{
  public static void Load([In] Account obj0, [In] FriendModel obj1, int Type)
  {
    try
    {
      if (obj0 == null)
        return;
      SChannelModel server = GameXender.Sync.GetServer(obj0.Status);
      if (server == null)
        return;
      IPEndPoint connection = SynchronizeXML.GetServer((int) server.Port).Connection;
      using (SyncServerPacket syncServerPacket = new SyncServerPacket())
      {
        syncServerPacket.WriteH((short) 17);
        syncServerPacket.WriteQ(obj0.PlayerId);
        syncServerPacket.WriteC((byte) Type);
        syncServerPacket.WriteQ(obj1.get_PlayerId());
        if (Type != 2)
        {
          syncServerPacket.WriteC((byte) obj1.State);
          syncServerPacket.WriteC((byte) obj1.Removed);
        }
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
