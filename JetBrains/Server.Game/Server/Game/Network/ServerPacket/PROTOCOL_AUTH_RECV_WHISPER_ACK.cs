// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_RECV_WHISPER_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_RECV_WHISPER_ACK : GameServerPacket
{
  private readonly string string_0;
  private readonly string string_1;
  private readonly bool bool_0;

  public PROTOCOL_AUTH_RECV_WHISPER_ACK([In] uint obj0, [In] Account obj1)
  {
    ((PROTOCOL_AUTH_GET_POINT_CASH_ACK) this).uint_0 = obj0;
    ((PROTOCOL_AUTH_GET_POINT_CASH_ACK) this).account_0 = obj1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 1058);
    this.WriteD(((PROTOCOL_AUTH_GET_POINT_CASH_ACK) this).uint_0);
    this.WriteD(((PROTOCOL_AUTH_GET_POINT_CASH_ACK) this).account_0.Gold);
    this.WriteD(((PROTOCOL_AUTH_GET_POINT_CASH_ACK) this).account_0.Cash);
    this.WriteD(((PROTOCOL_AUTH_GET_POINT_CASH_ACK) this).account_0.Tags);
    this.WriteD(0);
  }
}
