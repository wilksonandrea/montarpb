// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_BOOSTEVENT_INFO_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.XML;
using Server.Auth.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_BOOSTEVENT_INFO_ACK : AuthServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 1058);
    this.WriteD(((PROTOCOL_AUTH_GET_POINT_CASH_ACK) this).uint_0);
    this.WriteD(((PROTOCOL_AUTH_GET_POINT_CASH_ACK) this).account_0.Gold);
    this.WriteD(((PROTOCOL_AUTH_GET_POINT_CASH_ACK) this).account_0.Cash);
    this.WriteD(((PROTOCOL_AUTH_GET_POINT_CASH_ACK) this).account_0.Tags);
    this.WriteD(0);
  }

  public PROTOCOL_BASE_BOOSTEVENT_INFO_ACK([In] Account obj0)
  {
    if (obj0 == null)
      return;
    ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).battlePassModel_0 = SeasonChallengeXML.GetActiveSeasonPass();
    ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).playerBattlepass_0 = obj0.Battlepass;
    if (((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).battlePassModel_0 == null || ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).playerBattlepass_0 == null)
      return;
    ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).valueTuple_0 = ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).battlePassModel_0.GetLevelProgression(((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).playerBattlepass_0.TotalPoints);
    ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).int_0 = ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).playerBattlepass_0.TotalPoints - ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).valueTuple_0.Item3;
    ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).int_1 = ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).valueTuple_0.Item4 - ((PROTOCOL_SEASON_CHALLENGE_INFO_ACK) this).valueTuple_0.Item3;
  }
}
