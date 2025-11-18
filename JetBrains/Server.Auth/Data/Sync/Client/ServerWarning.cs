// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.Sync.Client.ServerWarning
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using System;

#nullable disable
namespace Server.Auth.Data.Sync.Client;

public static class ServerWarning
{
  public ServerWarning()
  {
  }

  public static void Load(SyncClientPacket C)
  {
    int num = (int) C.ReadC();
    switch (num)
    {
      case 1:
        EventVisitXML.Reload();
        EventLoginXML.Reload();
        EventBoostXML.Reload();
        EventPlaytimeXML.Reload();
        EventQuestXML.Reload();
        EventRankUpXML.Reload();
        EventXmasXML.Reload();
        CLogger.Print("All Events Successfully Reloaded!", LoggerType.Command, (Exception) null);
        break;
      case 2:
        PermissionXML.Load();
        CLogger.Print("Permission Successfully Reloaded!", LoggerType.Command, (Exception) null);
        break;
    }
    CLogger.Print($"Updating null part: {num}", LoggerType.Command, (Exception) null);
  }

  public ServerWarning()
  {
  }

  public static void Load(SyncClientPacket C)
  {
    int num1 = C.ReadD();
    int num2 = C.ReadD();
    SChannelModel server = SChannelXML.GetServer(num1);
    if (server == null)
      return;
    server.LastPlayers = num2;
  }
}
