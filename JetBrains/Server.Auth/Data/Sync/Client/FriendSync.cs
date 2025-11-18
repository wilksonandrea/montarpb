// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.Sync.Client.FriendSync
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Data.Sync.Client;

public static class FriendSync
{
  public static void LoadEventInfo([In] SyncClientPacket obj0)
  {
    int ServerId = (int) obj0.ReadC();
    if (!Class4.smethod_0(ServerId))
      return;
    CLogger.Print($"Refresh event; Type: {ServerId};", LoggerType.Command, (Exception) null);
  }
}
