// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_AUTH_FRIEND_INSERT_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Network;
using Server.Game.Network.ServerPacket;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_FRIEND_INSERT_REQ : GameClientPacket
{
  private string string_0;
  private int int_0;
  private int int_1;

  public virtual void Write()
  {
    ((BaseServerPacket) this).WriteH((short) 1099);
    ((BaseServerPacket) this).WriteH((short) 0);
    ((BaseServerPacket) this).WriteC((byte) 7);
    ((BaseServerPacket) this).WriteC((byte) 5);
    ((BaseServerPacket) this).WriteH((short) 0);
    ((BaseServerPacket) this).WriteC((byte) 0);
    ((BaseServerPacket) this).WriteD(0);
    ((BaseServerPacket) this).WriteH((short) 0);
    ((BaseServerPacket) this).WriteC((byte) 3);
    ((BaseServerPacket) this).WriteQ(0L);
    ((BaseServerPacket) this).WriteC((byte) 0);
    ((BaseServerPacket) this).WriteC((byte) 4);
    ((BaseServerPacket) this).WriteQ(0L);
    ((BaseServerPacket) this).WriteC((byte) 0);
    ((BaseServerPacket) this).WriteC((byte) 2);
    ((BaseServerPacket) this).WriteQ(0L);
    ((BaseServerPacket) this).WriteC((byte) 0);
    ((BaseServerPacket) this).WriteC((byte) 6);
    ((BaseServerPacket) this).WriteQ(0L);
    ((BaseServerPacket) this).WriteC((byte) 0);
    ((BaseServerPacket) this).WriteC((byte) 1);
    ((BaseServerPacket) this).WriteQ(0L);
    ((BaseServerPacket) this).WriteD(0);
    ((BaseServerPacket) this).WriteC((byte) 0);
    ((BaseServerPacket) this).WriteC(byte.MaxValue);
    ((BaseServerPacket) this).WriteC(byte.MaxValue);
    ((BaseServerPacket) this).WriteC(byte.MaxValue);
    ((BaseServerPacket) this).WriteC((byte) 0);
    ((BaseServerPacket) this).WriteC(byte.MaxValue);
    ((BaseServerPacket) this).WriteC((byte) 1);
    ((BaseServerPacket) this).WriteC((byte) 7);
    ((BaseServerPacket) this).WriteC((byte) 2);
  }

  public PROTOCOL_AUTH_FRIEND_INSERT_REQ()
    : this()
  {
  }

  public virtual void Write()
  {
    ((BaseServerPacket) this).WriteH((short) 6658);
    ((BaseServerPacket) this).WriteH((short) 0);
  }

  public PROTOCOL_AUTH_FRIEND_INSERT_REQ(int int_2)
    : this()
  {
    ((PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_ACK) this).int_0 = int_2;
  }
}
