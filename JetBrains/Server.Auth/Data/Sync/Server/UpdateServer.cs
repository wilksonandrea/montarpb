// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.Sync.Server.UpdateServer
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Auth.Data.Models;
using System;
using System.Net;

#nullable disable
namespace Server.Auth.Data.Sync.Server;

public class UpdateServer
{
  private static DateTime dateTime_0;

  public static void SendLoginKickInfo(Account Player)
  {
    try
    {
      int serverId = (int) Player.Status.ServerId;
      switch (serverId)
      {
        case 0:
        case (int) byte.MaxValue:
          Player.SetOnlineStatus(false);
          break;
        default:
          SChannelModel server = SChannelXML.GetServer(serverId);
          if (server == null)
            break;
          IPEndPoint connection = SynchronizeXML.GetServer((int) server.Port).Connection;
          using (SyncServerPacket syncServerPacket = new SyncServerPacket())
          {
            syncServerPacket.WriteH((short) 10);
            syncServerPacket.WriteQ(Player.PlayerId);
            // ISSUE: reference to a compiler-generated method
            ((AuthSync.Class3) AuthXender.Sync).SendPacket(syncServerPacket.ToArray(), connection);
            break;
          }
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
