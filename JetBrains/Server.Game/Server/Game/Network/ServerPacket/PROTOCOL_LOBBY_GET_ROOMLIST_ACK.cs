// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_LOBBY_GET_ROOMLIST_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_LOBBY_GET_ROOMLIST_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly int int_1;
  private readonly int int_2;
  private readonly int int_3;
  private readonly int int_4;
  private readonly int int_5;
  private readonly byte[] byte_0;
  private readonly byte[] byte_1;

  public virtual void Write()
  {
    this.WriteH((short) 2571);
    this.WriteD(((PROTOCOL_LOBBY_CHATTING_ACK) this).int_0);
    this.WriteC((byte) (((PROTOCOL_LOBBY_CHATTING_ACK) this).string_0.Length + 1));
    this.WriteN(((PROTOCOL_LOBBY_CHATTING_ACK) this).string_0, ((PROTOCOL_LOBBY_CHATTING_ACK) this).string_0.Length + 2, "UTF-16LE");
    this.WriteC((byte) ((PROTOCOL_LOBBY_CHATTING_ACK) this).int_1);
    this.WriteC((byte) ((PROTOCOL_LOBBY_CHATTING_ACK) this).bool_0);
    this.WriteH((ushort) (((PROTOCOL_LOBBY_CHATTING_ACK) this).string_1.Length + 1));
    this.WriteN(((PROTOCOL_LOBBY_CHATTING_ACK) this).string_1, ((PROTOCOL_LOBBY_CHATTING_ACK) this).string_1.Length + 2, "UTF-16LE");
  }

  public PROTOCOL_LOBBY_GET_ROOMLIST_ACK(uint string_2)
  {
    ((PROTOCOL_LOBBY_ENTER_ACK) this).uint_0 = string_2;
  }
}
