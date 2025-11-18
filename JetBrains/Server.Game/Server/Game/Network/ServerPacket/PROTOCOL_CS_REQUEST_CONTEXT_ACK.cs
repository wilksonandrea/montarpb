// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_REQUEST_CONTEXT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_REQUEST_CONTEXT_ACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 864);
    this.WriteU(((PROTOCOL_CS_REPLACE_NAME_RESULT_ACK) this).string_0, 34);
  }

  public PROTOCOL_CS_REQUEST_CONTEXT_ACK(EventErrorEnum uint_1)
  {
    ((PROTOCOL_CS_REPLACE_NOTICE_ACK) this).eventErrorEnum_0 = uint_1;
  }
}
