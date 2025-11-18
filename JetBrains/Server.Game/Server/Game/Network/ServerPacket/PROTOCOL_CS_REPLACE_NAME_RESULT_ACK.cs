// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_REPLACE_NAME_RESULT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_REPLACE_NAME_RESULT_ACK : GameServerPacket
{
  private readonly string string_0;

  public virtual void Write()
  {
    this.WriteH((short) 980);
    this.WriteC((byte) ((PROTOCOL_CS_REPLACE_MARKEFFECT_RESULT_ACK) this).int_0);
  }

  public PROTOCOL_CS_REPLACE_NAME_RESULT_ACK([In] uint obj0)
  {
    ((PROTOCOL_CS_REPLACE_MARK_RESULT_ACK) this).uint_0 = obj0;
  }
}
