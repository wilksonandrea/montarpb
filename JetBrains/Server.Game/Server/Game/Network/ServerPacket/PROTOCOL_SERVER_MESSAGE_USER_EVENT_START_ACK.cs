// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 3084);
    this.WriteD(((PROTOCOL_SERVER_MESSAGE_ITEM_RECEIVE_ACK) this).uint_0);
  }

  public PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK([In] EventErrorEnum obj0)
  {
    ((PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK) this).eventErrorEnum_0 = obj0;
  }
}
