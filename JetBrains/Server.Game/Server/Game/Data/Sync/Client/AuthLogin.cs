// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Client.AuthLogin
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Data.XML;
using System;
using System.Net;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Sync.Client;

public class AuthLogin
{
  public static void RefreshSChannel([In] int obj0)
  {
    try
    {
      if (ComDiv.GetDuration(UpdateServer.dateTime_0) < (double) ConfigLoader.UpdateIntervalPlayersServer)
        return;
      UpdateServer.dateTime_0 = DateTimeUtil.Now();
      int num = 0;
      foreach (ChannelModel channel in ChannelsXML.Channels)
        num += channel.Players.Count;
      foreach (SChannelModel server in SChannelXML.Servers)
      {
        if (server.Id == obj0)
        {
          server.LastPlayers = num;
        }
        else
        {
          IPEndPoint connection = SynchronizeXML.GetServer((int) server.Port).Connection;
          using (SyncServerPacket syncServerPacket = new SyncServerPacket())
          {
            syncServerPacket.WriteH((short) 15);
            syncServerPacket.WriteD(obj0);
            syncServerPacket.WriteD(num);
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
