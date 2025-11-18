// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_MEMBER_INFO_DELETE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Utility;
using Server.Game.Data.Models;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_MEMBER_INFO_DELETE_ACK : GameServerPacket
{
  private readonly long long_0;

  public PROTOCOL_CS_MEMBER_INFO_DELETE_ACK(List<Account> uint_1)
  {
    ((PROTOCOL_CS_MEMBER_INFO_ACK) this).list_0 = uint_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 845);
    this.WriteC((byte) ((PROTOCOL_CS_MEMBER_INFO_ACK) this).list_0.Count);
    foreach (Account account in ((PROTOCOL_CS_MEMBER_INFO_ACK) this).list_0)
    {
      this.WriteC((byte) (account.Nickname.Length + 1));
      this.WriteN(account.Nickname, account.Nickname.Length + 2, "UTF-16LE");
      this.WriteQ(account.PlayerId);
      this.WriteQ(ComDiv.GetClanStatus(account.Status, account.IsOnline));
      this.WriteC((byte) account.Rank);
      this.WriteC((byte) account.NickColor);
      this.WriteD(account.Statistic.Clan.MatchWins);
      this.WriteD(account.Statistic.Clan.MatchLoses);
      this.WriteD(account.Equipment.NameCardId);
      this.WriteC((byte) account.Bonus.NickBorderColor);
      this.WriteD(0);
      this.WriteD(0);
      this.WriteD(0);
    }
  }
}
