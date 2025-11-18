// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Client.FriendSync
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.XML;
using System;

#nullable disable
namespace Server.Game.Data.Sync.Client;

public class FriendSync
{
  public static void LoadEventInfo(SyncClientPacket serverId)
  {
    int C = (int) serverId.ReadC();
    if (!FriendSync.smethod_0(C))
      return;
    CLogger.Print($"Refresh event; Type: {C};", LoggerType.Command, (Exception) null);
  }

  private static bool smethod_0(int C)
  {
    switch (C)
    {
      case 0:
        EventVisitXML.Reload();
        return true;
      case 1:
        EventLoginXML.Reload();
        return true;
      case 2:
        EventBoostXML.Reload();
        return true;
      case 3:
        EventPlaytimeXML.Reload();
        return true;
      case 4:
        EventQuestXML.Reload();
        return true;
      case 5:
        EventRankUpXML.Reload();
        return true;
      case 6:
        EventXmasXML.Reload();
        return true;
      default:
        return false;
    }
  }
}
