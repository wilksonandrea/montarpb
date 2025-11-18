// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_USER_LEAVE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_USER_LEAVE_ACK : GameServerPacket
{
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 2343);
    this.WriteD(((PROTOCOL_BASE_RANK_UP_ACK) this).int_0);
    this.WriteD(((PROTOCOL_BASE_RANK_UP_ACK) this).int_0);
    this.WriteD(((PROTOCOL_BASE_RANK_UP_ACK) this).int_1);
  }

  public PROTOCOL_BASE_USER_LEAVE_ACK([In] uint obj0, int account_1, [In] int obj2)
  {
    ((PROTOCOL_BASE_SELECT_CHANNEL_ACK) this).uint_0 = obj0;
    ((PROTOCOL_BASE_SELECT_CHANNEL_ACK) this).int_0 = account_1;
    ((PROTOCOL_BASE_SELECT_CHANNEL_ACK) this).ushort_0 = (ushort) obj2;
  }
}
