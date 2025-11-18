// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_DAILY_RECORD_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Utility;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_DAILY_RECORD_ACK : GameServerPacket
{
  private readonly StatisticDaily statisticDaily_0;
  private readonly byte byte_0;
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 2339);
    this.WriteD((uint) ((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK) this).eventErrorEnum_0);
  }

  public PROTOCOL_BASE_DAILY_RECORD_ACK(GameClient uint_1)
  {
    ((PROTOCOL_BASE_CONNECT_ACK) this).int_0 = uint_1.ServerId;
    ((PROTOCOL_BASE_CONNECT_ACK) this).int_1 = uint_1.SessionId;
    ((PROTOCOL_BASE_CONNECT_ACK) this).ushort_0 = uint_1.SessionSeed;
    ((PROTOCOL_BASE_CONNECT_ACK) this).list_0 = Bitwise.GenerateRSAKeyPair(((PROTOCOL_BASE_CONNECT_ACK) this).int_1, this.SECURITY_KEY, this.SEED_LENGTH);
  }
}
