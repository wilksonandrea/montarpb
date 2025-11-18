// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_RECORD_RESET_RESULT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_RECORD_RESET_RESULT_ACK : GameServerPacket
{
  public PROTOCOL_CS_RECORD_RESET_RESULT_ACK(Account uint_1, string byte_3)
  {
    ((PROTOCOL_CS_PAGE_CHATTING_ACK) this).string_0 = uint_1.Nickname;
    ((PROTOCOL_CS_PAGE_CHATTING_ACK) this).string_1 = byte_3;
    ((PROTOCOL_CS_PAGE_CHATTING_ACK) this).bool_0 = uint_1.UseChatGM();
    ((PROTOCOL_CS_PAGE_CHATTING_ACK) this).int_2 = uint_1.NickColor;
  }

  public PROTOCOL_CS_RECORD_RESET_RESULT_ACK([In] int obj0, [In] int obj1)
  {
    ((PROTOCOL_CS_PAGE_CHATTING_ACK) this).int_0 = obj0;
    ((PROTOCOL_CS_PAGE_CHATTING_ACK) this).int_1 = obj1;
  }
}
