// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_REPLACE_COLOR_NAME_RESULT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_REPLACE_COLOR_NAME_RESULT_ACK : GameServerPacket
{
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 887);
    this.WriteC((byte) ((PROTOCOL_CS_PAGE_CHATTING_ACK) this).int_0);
    if (((PROTOCOL_CS_PAGE_CHATTING_ACK) this).int_0 == 0)
    {
      this.WriteC((byte) (((PROTOCOL_CS_PAGE_CHATTING_ACK) this).string_0.Length + 1));
      this.WriteN(((PROTOCOL_CS_PAGE_CHATTING_ACK) this).string_0, ((PROTOCOL_CS_PAGE_CHATTING_ACK) this).string_0.Length + 2, "UTF-16LE");
      this.WriteC((byte) ((PROTOCOL_CS_PAGE_CHATTING_ACK) this).bool_0);
      this.WriteC((byte) ((PROTOCOL_CS_PAGE_CHATTING_ACK) this).int_2);
      this.WriteC((byte) (((PROTOCOL_CS_PAGE_CHATTING_ACK) this).string_1.Length + 1));
      this.WriteN(((PROTOCOL_CS_PAGE_CHATTING_ACK) this).string_1, ((PROTOCOL_CS_PAGE_CHATTING_ACK) this).string_1.Length + 2, "UTF-16LE");
    }
    else
      this.WriteD(((PROTOCOL_CS_PAGE_CHATTING_ACK) this).int_1);
  }
}
