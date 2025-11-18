// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Models;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK : AuthServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 8450);
    this.WriteH((short) 0);
    if (((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).battlePassModel_0 == null)
      return;
    this.WriteD(((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).battlePassModel_0.SeasonIsEnabled() ? 1 : 0);
    this.WriteC((byte) ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).valueTuple_0.Item2);
    this.WriteD(((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).playerBattlepass_0.TotalPoints);
    this.WriteC((byte) ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).valueTuple_0.Item1);
    this.WriteC(((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).playerBattlepass_0.IsPremium ? (byte) ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).valueTuple_0.Item1 : (byte) 0);
    this.WriteC((byte) ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).playerBattlepass_0.IsPremium);
    this.WriteC((byte) 0);
    this.WriteD(((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).playerBattlepass_0.LastRecord);
    this.WriteD(((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).playerBattlepass_0.DailyPoints);
    this.WriteD(1);
    this.WriteC(((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).battlePassModel_0.IsCompleted(((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).playerBattlepass_0.TotalPoints) ? (byte) 0 : (((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).battlePassModel_0.Enable ? (byte) 1 : (byte) 2));
    this.WriteU(((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).battlePassModel_0.Name, 42);
    this.WriteH((short) ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).battlePassModel_0.GetCardCount());
    this.WriteD(((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).battlePassModel_0.MaxDailyPoints);
    this.WriteD(0);
    for (int index = 0; index < 99; ++index)
    {
      PassBoxModel card = ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).battlePassModel_0.Cards[index];
      this.WriteD(card.Normal.GoodId);
      this.WriteD(card.PremiumA.GoodId);
      this.WriteD(card.PremiumB.GoodId);
      this.WriteD(card.RequiredPoints);
    }
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).battlePassModel_0.BeginDate);
    this.WriteD(((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).battlePassModel_0.EndedDate);
    this.WriteD(((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).int_0);
    this.WriteD(((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).int_1);
  }

  public virtual void Write()
  {
    this.WriteH((short) 8451);
    this.WriteH((short) 0);
    this.WriteC((byte) 1);
  }
}
