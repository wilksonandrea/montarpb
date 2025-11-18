// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;

  public PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK([In] Account obj0, string account_0, bool list_1)
  {
    if (!list_1)
    {
      ((PROTOCOL_LOBBY_CHATTING_ACK) this).int_1 = obj0.NickColor;
      ((PROTOCOL_LOBBY_CHATTING_ACK) this).bool_0 = obj0.UseChatGM();
    }
    else
      ((PROTOCOL_LOBBY_CHATTING_ACK) this).bool_0 = true;
    ((PROTOCOL_LOBBY_CHATTING_ACK) this).string_0 = obj0.Nickname;
    ((PROTOCOL_LOBBY_CHATTING_ACK) this).int_0 = obj0.GetSessionId();
    ((PROTOCOL_LOBBY_CHATTING_ACK) this).string_1 = account_0;
  }

  public PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK(
    [In] string obj0,
    int account_0,
    int itemsModel_0,
    [In] bool obj3,
    [In] string obj4)
  {
    ((PROTOCOL_LOBBY_CHATTING_ACK) this).string_0 = obj0;
    ((PROTOCOL_LOBBY_CHATTING_ACK) this).int_0 = account_0;
    ((PROTOCOL_LOBBY_CHATTING_ACK) this).int_1 = itemsModel_0;
    ((PROTOCOL_LOBBY_CHATTING_ACK) this).bool_0 = obj3;
    ((PROTOCOL_LOBBY_CHATTING_ACK) this).string_1 = obj4;
  }
}
