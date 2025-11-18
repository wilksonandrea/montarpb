// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CLAN_WAR_CREATE_ROOM_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_CREATE_ROOM_ACK : GameServerPacket
{
  public readonly MatchModel match;

  public virtual void Write()
  {
    this.WriteH((short) 1559);
    this.WriteD(((PROTOCOL_CLAN_WAR_ACCEPT_BATTLE_ACK) this).uint_0);
  }
}
