// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Auth.Data.Models;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK : AuthServerPacket
{
  private readonly string string_0;

  public PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(Account serverConfig_1)
  {
    ((PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK) this).account_0 = serverConfig_1;
    ((PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK) this).ulong_0 = ComDiv.GetClanStatus(serverConfig_1.Status, serverConfig_1.IsOnline);
  }

  public PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(Account int_1, FriendState list_1)
  {
    ((PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK) this).account_0 = int_1;
    ((PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK) this).ulong_0 = list_1 == FriendState.None ? ComDiv.GetClanStatus(int_1.Status, int_1.IsOnline) : ComDiv.GetClanStatus(list_1);
  }
}
