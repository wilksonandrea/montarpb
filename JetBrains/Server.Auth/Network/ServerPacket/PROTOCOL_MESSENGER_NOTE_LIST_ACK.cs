// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_MESSENGER_NOTE_LIST_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Utility;
using Server.Auth.Data.Models;
using System.Collections.Generic;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_MESSENGER_NOTE_LIST_ACK : AuthServerPacket
{
  private readonly int int_0;
  private readonly int int_1;
  private readonly byte[] byte_0;
  private readonly byte[] byte_1;

  public PROTOCOL_MESSENGER_NOTE_LIST_ACK(List<Account> serverConfig_1)
  {
    ((PROTOCOL_CS_MEMBER_INFO_ACK) this).list_0 = serverConfig_1;
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
