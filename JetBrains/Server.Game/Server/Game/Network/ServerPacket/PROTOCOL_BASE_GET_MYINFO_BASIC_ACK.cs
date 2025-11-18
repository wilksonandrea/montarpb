// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_GET_MYINFO_BASIC_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GET_MYINFO_BASIC_ACK : GameServerPacket
{
  private readonly Account account_0;
  private readonly ClanModel clanModel_0;

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

  public PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(int uint_1, byte[] account_1)
  {
    ((PROTOCOL_BASE_GAMEGUARD_ACK) this).int_0 = uint_1;
    ((PROTOCOL_BASE_GAMEGUARD_ACK) this).byte_0 = account_1;
  }
}
