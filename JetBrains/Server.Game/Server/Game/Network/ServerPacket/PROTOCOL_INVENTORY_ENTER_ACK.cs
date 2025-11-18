// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_INVENTORY_ENTER_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_INVENTORY_ENTER_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 2685);
    this.WriteD(((PROTOCOL_GM_LOG_LOBBY_ACK) this).uint_0);
    this.WriteQ(((PROTOCOL_GM_LOG_LOBBY_ACK) this).account_0.PlayerId);
  }

  public PROTOCOL_INVENTORY_ENTER_ACK(uint uint_1, Account account_1)
  {
    ((PROTOCOL_GM_LOG_ROOM_ACK) this).uint_0 = uint_1;
    ((PROTOCOL_GM_LOG_ROOM_ACK) this).account_0 = account_1;
  }
}
