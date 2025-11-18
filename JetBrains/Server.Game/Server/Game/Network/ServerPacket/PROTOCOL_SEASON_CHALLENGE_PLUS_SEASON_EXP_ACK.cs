// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_SEASON_CHALLENGE_PLUS_SEASON_EXP_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.XML;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SEASON_CHALLENGE_PLUS_SEASON_EXP_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 3624);
    this.WriteD(0);
  }

  public PROTOCOL_SEASON_CHALLENGE_PLUS_SEASON_EXP_ACK(uint uint_1, [In] Account obj1)
  {
    ((PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK) this).uint_0 = uint_1;
    if (obj1 == null)
      return;
    ((PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK) this).battlePassModel_0 = SeasonChallengeXML.GetActiveSeasonPass();
    ((PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK) this).playerBattlepass_0 = obj1.Battlepass;
    if (((PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK) this).battlePassModel_0 == null || ((PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK) this).playerBattlepass_0 == null)
      return;
    ((PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK) this).valueTuple_0 = ((PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK) this).battlePassModel_0.GetLevelProgression(((PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK) this).playerBattlepass_0.TotalPoints);
  }
}
