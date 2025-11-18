// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_DENIAL_REQUEST_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_DENIAL_REQUEST_ACK : GameServerPacket
{
  private readonly int int_0;

  public virtual void Write() => this.WriteH((short) 838);

  public PROTOCOL_CS_DENIAL_REQUEST_ACK([In] uint obj0, ClanModel int_3, [In] Account obj2)
  {
    ((PROTOCOL_CS_CREATE_CLAN_ACK) this).uint_0 = obj0;
    ((PROTOCOL_CS_CREATE_CLAN_ACK) this).clanModel_0 = int_3;
    ((PROTOCOL_CS_CREATE_CLAN_ACK) this).account_0 = obj2;
  }
}
