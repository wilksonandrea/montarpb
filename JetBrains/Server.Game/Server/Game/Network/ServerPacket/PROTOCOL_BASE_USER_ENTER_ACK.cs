// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_USER_ENTER_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_USER_ENTER_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 2323);
    this.WriteD(0);
  }

  public PROTOCOL_BASE_USER_ENTER_ACK(int uint_1, [In] int obj1)
  {
    ((PROTOCOL_BASE_RANK_UP_ACK) this).int_0 = uint_1;
    ((PROTOCOL_BASE_RANK_UP_ACK) this).int_1 = obj1;
  }
}
