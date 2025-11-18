// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_MEDAL_INFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Managers;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_MEDAL_INFO_ACK : GameServerPacket
{
  public virtual void Write() => this.WriteH((short) 6664);

  public PROTOCOL_CS_MEDAL_INFO_ACK(uint uint_1, long account_1)
  {
    ((PROTOCOL_GM_LOG_LOBBY_ACK) this).uint_0 = uint_1;
    ((PROTOCOL_GM_LOG_LOBBY_ACK) this).account_0 = ClanManager.GetAccount(account_1, true);
  }
}
