// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.XML;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 8455);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK) this).uint_0);
    if (((PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK) this).uint_0 != 0U)
      return;
    this.WriteD(((PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK) this).battlePassModel_0.SeasonIsEnabled() ? 1 : 0);
    this.WriteC((byte) ((PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK) this).valueTuple_0.Item2);
    this.WriteD(((PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK) this).playerBattlepass_0.TotalPoints);
    this.WriteC((byte) ((PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK) this).valueTuple_0.Item1);
    this.WriteC(((PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK) this).playerBattlepass_0.IsPremium ? (byte) ((PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK) this).valueTuple_0.Item1 : (byte) 0);
    this.WriteC((byte) ((PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK) this).playerBattlepass_0.IsPremium);
    this.WriteD(1);
    this.WriteD(1);
    this.WriteD(1);
    this.WriteD(1);
    this.WriteD(1);
  }

  public PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE(Account list_1)
  {
    if (list_1 == null)
      return;
    ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).battlePassModel_0 = SeasonChallengeXML.GetActiveSeasonPass();
    ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).playerBattlepass_0 = list_1.Battlepass;
    if (((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).battlePassModel_0 == null || ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).playerBattlepass_0 == null)
      return;
    ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).valueTuple_0 = ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).battlePassModel_0.GetLevelProgression(((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).playerBattlepass_0.TotalPoints);
    ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).int_0 = ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).playerBattlepass_0.TotalPoints - ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).valueTuple_0.Item3;
    ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).int_1 = ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).valueTuple_0.Item4 - ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).valueTuple_0.Item3;
  }
}
