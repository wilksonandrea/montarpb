// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_MEMBER_INFO_INSERT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_MEMBER_INFO_INSERT_ACK : GameServerPacket
{
  private readonly Account account_0;

  public PROTOCOL_CS_MEMBER_INFO_INSERT_ACK([In] Account obj0)
  {
    ((PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK) this).account_0 = obj0;
    if (obj0 == null)
      return;
    ((PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK) this).ulong_0 = ComDiv.GetClanStatus(obj0.Status, obj0.IsOnline);
  }

  public PROTOCOL_CS_MEMBER_INFO_INSERT_ACK(Account int_2, FriendState int_3)
  {
    ((PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK) this).account_0 = int_2;
    if (int_2 == null)
      return;
    ((PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK) this).ulong_0 = int_3 == FriendState.None ? ComDiv.GetClanStatus(int_2.Status, int_2.IsOnline) : ComDiv.GetClanStatus(int_3);
  }
}
