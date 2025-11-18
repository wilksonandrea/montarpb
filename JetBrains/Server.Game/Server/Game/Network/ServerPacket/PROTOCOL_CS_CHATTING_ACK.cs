// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_CHATTING_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CHATTING_ACK : GameServerPacket
{
  private readonly string string_0;
  private readonly Account account_0;
  private readonly int int_0;
  private readonly int int_1;

  public virtual void Write()
  {
    this.WriteH((short) 824);
    this.WriteD(((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.Id);
    this.WriteU(((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.Name, 34);
    this.WriteC((byte) ((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.Rank);
    this.WriteC((byte) ((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).int_0);
    this.WriteC((byte) ((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.MaxPlayers);
    this.WriteD(((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.CreationDate);
    this.WriteD(((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.Logo);
    this.WriteC((byte) ((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.NameColor);
    this.WriteC((byte) ((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.Effect);
    this.WriteC((byte) ((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.GetClanUnit());
    this.WriteD(((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.Exp);
    this.WriteD(10);
    this.WriteQ(((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.OwnerId);
    this.WriteU(((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).account_0 != null ? ((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).account_0.Nickname : "", 66);
    this.WriteC(((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).account_0 != null ? (byte) ((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).account_0.NickColor : (byte) 0);
    this.WriteC(((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).account_0 != null ? (byte) ((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).account_0.Rank : (byte) 0);
    this.WriteU(((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.Info, 510);
    this.WriteU("Temp", 42);
    this.WriteC((byte) ((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.RankLimit);
    this.WriteC((byte) ((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.MinAgeLimit);
    this.WriteC((byte) ((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.MaxAgeLimit);
    this.WriteC((byte) ((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.Authority);
    this.WriteU(((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.News, 510);
    this.WriteD(((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.Matches);
    this.WriteD(((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.MatchWins);
    this.WriteD(((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.MatchLoses);
    this.WriteD(((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.Matches);
    this.WriteD(((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.MatchWins);
    this.WriteD(((PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK) this).clanModel_0.MatchLoses);
  }

  public PROTOCOL_CS_CHATTING_ACK(uint clanModel_1)
  {
    ((PROTOCOL_CS_CANCEL_REQUEST_ACK) this).uint_0 = clanModel_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 815);
    this.WriteD(((PROTOCOL_CS_CANCEL_REQUEST_ACK) this).uint_0);
  }
}
