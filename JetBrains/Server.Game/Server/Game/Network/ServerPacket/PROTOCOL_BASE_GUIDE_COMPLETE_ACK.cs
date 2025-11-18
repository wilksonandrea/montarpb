// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_GUIDE_COMPLETE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GUIDE_COMPLETE_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 2369);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Season.Matches);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Season.MatchWins);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Season.MatchLoses);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Season.MatchDraws);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Season.KillsCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Season.HeadshotsCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Season.DeathsCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Season.TotalMatchesCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Season.TotalKillsCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Season.EscapesCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Season.AssistsCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Season.MvpCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Basic.Matches);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Basic.MatchWins);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Basic.MatchLoses);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Basic.MatchDraws);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Basic.KillsCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Basic.HeadshotsCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Basic.DeathsCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Basic.get_TotalMatchesCount());
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Basic.get_TotalKillsCount());
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Basic.EscapesCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Basic.AssistsCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0.Basic.MvpCount);
  }

  public PROTOCOL_BASE_GUIDE_COMPLETE_ACK([In] Account obj0)
  {
    ((PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK) this).playerStatistic_0 = obj0.Statistic;
  }
}
