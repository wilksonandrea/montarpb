// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_ROOM_INVITED_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Managers;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_ROOM_INVITED_ACK : GameServerPacket
{
  private readonly int int_0;

  public PROTOCOL_CS_ROOM_INVITED_ACK(long account_1, [In] string obj1)
  {
    ((PROTOCOL_CS_REQUEST_INFO_ACK) this).string_0 = obj1;
    ((PROTOCOL_CS_REQUEST_INFO_ACK) this).account_0 = ClanManager.GetAccount(account_1, 31 /*0x1F*/);
    if (((PROTOCOL_CS_REQUEST_INFO_ACK) this).account_0 != null && obj1 != null)
      return;
    ((PROTOCOL_CS_REQUEST_INFO_ACK) this).uint_0 = 2147483648U /*0x80000000*/;
  }

  public virtual void Write()
  {
    this.WriteH((short) 821);
    this.WriteD(((PROTOCOL_CS_REQUEST_INFO_ACK) this).uint_0);
    if (((PROTOCOL_CS_REQUEST_INFO_ACK) this).uint_0 != 0U)
      return;
    this.WriteQ(((PROTOCOL_CS_REQUEST_INFO_ACK) this).account_0.PlayerId);
    this.WriteU(((PROTOCOL_CS_REQUEST_INFO_ACK) this).account_0.Nickname, 66);
    this.WriteC((byte) ((PROTOCOL_CS_REQUEST_INFO_ACK) this).account_0.Rank);
    this.WriteD(((PROTOCOL_CS_REQUEST_INFO_ACK) this).account_0.Statistic.Basic.KillsCount);
    this.WriteD(((PROTOCOL_CS_REQUEST_INFO_ACK) this).account_0.Statistic.Basic.DeathsCount);
    this.WriteD(((PROTOCOL_CS_REQUEST_INFO_ACK) this).account_0.Statistic.Basic.Matches);
    this.WriteD(((PROTOCOL_CS_REQUEST_INFO_ACK) this).account_0.Statistic.Basic.MatchWins);
    this.WriteD(((PROTOCOL_CS_REQUEST_INFO_ACK) this).account_0.Statistic.Basic.MatchLoses);
    this.WriteN(((PROTOCOL_CS_REQUEST_INFO_ACK) this).string_0, ((PROTOCOL_CS_REQUEST_INFO_ACK) this).string_0.Length + 2, "UTF-16LE");
  }
}
