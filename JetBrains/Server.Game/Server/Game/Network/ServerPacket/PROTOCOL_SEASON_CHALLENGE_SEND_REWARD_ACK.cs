// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_SEASON_CHALLENGE_SEND_REWARD_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SEASON_CHALLENGE_SEND_REWARD_ACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly int[] int_0;

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

  public PROTOCOL_SEASON_CHALLENGE_SEND_REWARD_ACK([In] uint obj0)
  {
    ((PROTOCOL_SEASON_CHALLENGE_PLUS_SEASON_EXP_ACK) this).uint_0 = obj0;
  }
}
