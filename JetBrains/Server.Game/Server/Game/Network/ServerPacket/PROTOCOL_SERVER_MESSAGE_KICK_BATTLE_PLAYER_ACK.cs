// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK : GameServerPacket
{
  private readonly EventErrorEnum eventErrorEnum_0;

  public virtual void Write()
  {
    this.WriteH((short) 3078);
    this.WriteD(((PROTOCOL_SERVER_MESSAGE_ERROR_ACK) this).uint_0);
  }

  public PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK(Account account_0, RoomModel slotModel_0)
  {
    ((PROTOCOL_SERVER_MESSAGE_INVITED_ACK) this).account_0 = account_0;
    ((PROTOCOL_SERVER_MESSAGE_INVITED_ACK) this).roomModel_0 = slotModel_0;
  }
}
