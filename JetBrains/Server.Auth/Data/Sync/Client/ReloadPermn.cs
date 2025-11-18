// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.Sync.Client.ReloadPermn
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Auth.Network;
using Server.Auth.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Auth.Data.Sync.Client;

public class ReloadPermn
{
  public static void Load(SyncClientPacket C)
  {
    byte mission = C.ReadC();
    string str = C.ReadS((int) mission);
    if (string.IsNullOrEmpty(str) || mission > (byte) 60)
      return;
    int num = 0;
    using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK messageAnnounceAck = (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK) new PROTOCOL_SERVER_MESSAGE_ERROR_ACK(str))
      num = ((AuthXender) AuthXender.Client).SendPacketToAllClients((AuthServerPacket) messageAnnounceAck);
    CLogger.Print($"Message sent to '{num}' Players", LoggerType.Command, (Exception) null);
  }
}
