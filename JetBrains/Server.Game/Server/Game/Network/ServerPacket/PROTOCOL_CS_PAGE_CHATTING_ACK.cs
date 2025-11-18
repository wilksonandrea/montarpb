// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_PAGE_CHATTING_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Utility;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_PAGE_CHATTING_ACK : GameServerPacket
{
  private readonly string string_0;
  private readonly string string_1;
  private readonly int int_0;
  private readonly int int_1;
  private readonly int int_2;
  private readonly bool bool_0;

  public virtual void Write()
  {
    this.WriteH((short) 847);
    this.WriteC((byte) (((PROTOCOL_CS_MEMBER_INFO_INSERT_ACK) this).account_0.Nickname.Length + 1));
    this.WriteN(((PROTOCOL_CS_MEMBER_INFO_INSERT_ACK) this).account_0.Nickname, ((PROTOCOL_CS_MEMBER_INFO_INSERT_ACK) this).account_0.Nickname.Length + 2, "UTF-16LE");
    this.WriteQ(((PROTOCOL_CS_MEMBER_INFO_INSERT_ACK) this).account_0.PlayerId);
    this.WriteQ(ComDiv.GetClanStatus(((PROTOCOL_CS_MEMBER_INFO_INSERT_ACK) this).account_0.Status, ((PROTOCOL_CS_MEMBER_INFO_INSERT_ACK) this).account_0.IsOnline));
    this.WriteC((byte) ((PROTOCOL_CS_MEMBER_INFO_INSERT_ACK) this).account_0.Rank);
    this.WriteC((byte) ((PROTOCOL_CS_MEMBER_INFO_INSERT_ACK) this).account_0.NickColor);
    this.WriteD(((PROTOCOL_CS_MEMBER_INFO_INSERT_ACK) this).account_0.Statistic.Clan.MatchWins);
    this.WriteD(((PROTOCOL_CS_MEMBER_INFO_INSERT_ACK) this).account_0.Statistic.Clan.MatchLoses);
    this.WriteD(((PROTOCOL_CS_MEMBER_INFO_INSERT_ACK) this).account_0.Equipment.NameCardId);
    this.WriteC((byte) ((PROTOCOL_CS_MEMBER_INFO_INSERT_ACK) this).account_0.Bonus.NickBorderColor);
    this.WriteD(10);
    this.WriteD(20);
    this.WriteD(30);
  }

  public PROTOCOL_CS_PAGE_CHATTING_ACK(uint account_1, byte friendState_0, [In] byte obj2, [In] byte[] obj3)
  {
    ((PROTOCOL_CS_MEMBER_LIST_ACK) this).uint_0 = account_1;
    ((PROTOCOL_CS_MEMBER_LIST_ACK) this).byte_1 = friendState_0;
    ((PROTOCOL_CS_MEMBER_LIST_ACK) this).byte_2 = obj2;
    ((PROTOCOL_CS_MEMBER_LIST_ACK) this).byte_0 = obj3;
  }

  public virtual void Write()
  {
    this.WriteH((short) 805);
    this.WriteD(((PROTOCOL_CS_MEMBER_LIST_ACK) this).uint_0);
    if (((PROTOCOL_CS_MEMBER_LIST_ACK) this).uint_0 != 0U)
      return;
    this.WriteC(((PROTOCOL_CS_MEMBER_LIST_ACK) this).byte_1);
    this.WriteC(((PROTOCOL_CS_MEMBER_LIST_ACK) this).byte_2);
    this.WriteB(((PROTOCOL_CS_MEMBER_LIST_ACK) this).byte_0);
  }
}
