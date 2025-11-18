// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_CHECK_MARK_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CHECK_MARK_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 855);
    if (((PROTOCOL_CS_CHATTING_ACK) this).int_0 == 0)
    {
      this.WriteC((byte) (((PROTOCOL_CS_CHATTING_ACK) this).account_0.Nickname.Length + 1));
      this.WriteN(((PROTOCOL_CS_CHATTING_ACK) this).account_0.Nickname, ((PROTOCOL_CS_CHATTING_ACK) this).account_0.Nickname.Length + 2, "UTF-16LE");
      this.WriteC((byte) ((PROTOCOL_CS_CHATTING_ACK) this).account_0.UseChatGM());
      this.WriteC((byte) ((PROTOCOL_CS_CHATTING_ACK) this).account_0.NickColor);
      this.WriteC((byte) (((PROTOCOL_CS_CHATTING_ACK) this).string_0.Length + 1));
      this.WriteN(((PROTOCOL_CS_CHATTING_ACK) this).string_0, ((PROTOCOL_CS_CHATTING_ACK) this).string_0.Length + 2, "UTF-16LE");
    }
    else
      this.WriteD(((PROTOCOL_CS_CHATTING_ACK) this).int_1);
  }

  public PROTOCOL_CS_CHECK_MARK_ACK(uint int_1)
  {
    ((PROTOCOL_CS_CHECK_DUPLICATE_ACK) this).uint_0 = int_1;
  }
}
