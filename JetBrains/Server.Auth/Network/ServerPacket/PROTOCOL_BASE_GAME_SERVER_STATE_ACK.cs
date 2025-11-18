// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_GAME_SERVER_STATE_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Models;
using Plugin.Core.Utility;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_GAME_SERVER_STATE_ACK : AuthServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 2306);
    this.WriteH((short) 0);
    this.WriteC((byte) 11);
    this.WriteB(Bitwise.HexStringToByteArray("00 00 00 00 00 00 00 00 00 00 00"));
    this.WriteH((ushort) (((PROTOCOL_BASE_CONNECT_ACK) this).list_0[0].Length + ((PROTOCOL_BASE_CONNECT_ACK) this).list_0[1].Length + 2));
    this.WriteH((ushort) ((PROTOCOL_BASE_CONNECT_ACK) this).list_0[0].Length);
    this.WriteB(((PROTOCOL_BASE_CONNECT_ACK) this).list_0[0]);
    this.WriteB(((PROTOCOL_BASE_CONNECT_ACK) this).list_0[1]);
    this.WriteC((byte) 3);
    this.WriteH((short) 80 /*0x50*/);
    this.WriteH(((PROTOCOL_BASE_CONNECT_ACK) this).ushort_0);
    this.WriteD(((PROTOCOL_BASE_CONNECT_ACK) this).int_0);
  }

  public PROTOCOL_BASE_GAME_SERVER_STATE_ACK(StatisticDaily uint_1, byte account_1, [In] uint obj2)
  {
    ((PROTOCOL_BASE_DAILY_RECORD_ACK) this).statisticDaily_0 = uint_1;
    ((PROTOCOL_BASE_DAILY_RECORD_ACK) this).byte_0 = account_1;
    ((PROTOCOL_BASE_DAILY_RECORD_ACK) this).uint_0 = obj2;
  }

  public virtual void Write()
  {
    this.WriteH((short) 2415);
    this.WriteH((ushort) ((PROTOCOL_BASE_DAILY_RECORD_ACK) this).statisticDaily_0.Matches);
    this.WriteH((ushort) ((PROTOCOL_BASE_DAILY_RECORD_ACK) this).statisticDaily_0.get_MatchWins());
    this.WriteH((ushort) ((PROTOCOL_BASE_DAILY_RECORD_ACK) this).statisticDaily_0.get_MatchLoses());
    this.WriteH((ushort) ((PROTOCOL_BASE_DAILY_RECORD_ACK) this).statisticDaily_0.MatchDraws);
    this.WriteH((ushort) ((PROTOCOL_BASE_DAILY_RECORD_ACK) this).statisticDaily_0.KillsCount);
    this.WriteH((ushort) ((PROTOCOL_BASE_DAILY_RECORD_ACK) this).statisticDaily_0.HeadshotsCount);
    this.WriteH((ushort) ((PROTOCOL_BASE_DAILY_RECORD_ACK) this).statisticDaily_0.DeathsCount);
    this.WriteD(((PROTOCOL_BASE_DAILY_RECORD_ACK) this).statisticDaily_0.ExpGained);
    this.WriteD(((PROTOCOL_BASE_DAILY_RECORD_ACK) this).statisticDaily_0.PointGained);
    this.WriteD(((PROTOCOL_BASE_DAILY_RECORD_ACK) this).statisticDaily_0.Playtime);
    this.WriteC(((PROTOCOL_BASE_DAILY_RECORD_ACK) this).byte_0);
    this.WriteD(((PROTOCOL_BASE_DAILY_RECORD_ACK) this).uint_0);
  }
}
