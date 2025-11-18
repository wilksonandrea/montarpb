// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_DEPORTATION_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.SQL;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_DEPORTATION_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 807);
    this.WriteD(((PROTOCOL_CS_CREATE_CLAN_ACK) this).uint_0);
    if (((PROTOCOL_CS_CREATE_CLAN_ACK) this).uint_0 != 0U)
      return;
    this.WriteD(((PROTOCOL_CS_CREATE_CLAN_ACK) this).clanModel_0.Id);
    this.WriteU(((PROTOCOL_CS_CREATE_CLAN_ACK) this).clanModel_0.Name, 34);
    this.WriteC((byte) ((PROTOCOL_CS_CREATE_CLAN_ACK) this).clanModel_0.Rank);
    this.WriteC((byte) DaoManagerSQL.GetClanPlayers(((PROTOCOL_CS_CREATE_CLAN_ACK) this).clanModel_0.Id));
    this.WriteC((byte) ((PROTOCOL_CS_CREATE_CLAN_ACK) this).clanModel_0.MaxPlayers);
    this.WriteD(((PROTOCOL_CS_CREATE_CLAN_ACK) this).clanModel_0.CreationDate);
    this.WriteD(((PROTOCOL_CS_CREATE_CLAN_ACK) this).clanModel_0.Logo);
    this.WriteB(new byte[11]);
    this.WriteQ(((PROTOCOL_CS_CREATE_CLAN_ACK) this).clanModel_0.OwnerId);
    this.WriteS(((PROTOCOL_CS_CREATE_CLAN_ACK) this).account_0.Nickname, 66);
    this.WriteC((byte) ((PROTOCOL_CS_CREATE_CLAN_ACK) this).account_0.NickColor);
    this.WriteC((byte) ((PROTOCOL_CS_CREATE_CLAN_ACK) this).account_0.Rank);
    this.WriteU(((PROTOCOL_CS_CREATE_CLAN_ACK) this).clanModel_0.Info, 510);
    this.WriteU("Temp", 42);
    this.WriteC((byte) ((PROTOCOL_CS_CREATE_CLAN_ACK) this).clanModel_0.RankLimit);
    this.WriteC((byte) ((PROTOCOL_CS_CREATE_CLAN_ACK) this).clanModel_0.MinAgeLimit);
    this.WriteC((byte) ((PROTOCOL_CS_CREATE_CLAN_ACK) this).clanModel_0.MaxAgeLimit);
    this.WriteC((byte) ((PROTOCOL_CS_CREATE_CLAN_ACK) this).clanModel_0.Authority);
    this.WriteU("", 510);
    this.WriteB(new byte[44]);
    this.WriteF((double) ((PROTOCOL_CS_CREATE_CLAN_ACK) this).clanModel_0.Points);
    this.WriteF(60.0);
    this.WriteB(new byte[16 /*0x10*/]);
    this.WriteF((double) ((PROTOCOL_CS_CREATE_CLAN_ACK) this).clanModel_0.Points);
    this.WriteF(60.0);
    this.WriteB(new byte[80 /*0x50*/]);
    this.WriteB(new byte[66]);
    this.WriteD(((PROTOCOL_CS_CREATE_CLAN_ACK) this).account_0.Gold);
  }
}
