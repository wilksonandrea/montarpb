// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_POINT_RESET_RESULT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_POINT_RESET_RESULT_ACK : GameServerPacket
{
  public PROTOCOL_CS_POINT_RESET_RESULT_ACK(int account_1)
  {
    ((PROTOCOL_CS_NOTE_ACK) this).int_0 = account_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 893);
    this.WriteD(((PROTOCOL_CS_NOTE_ACK) this).int_0);
  }
}
