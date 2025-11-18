// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_GM_LOG_ROOM_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_GM_LOG_ROOM_ACK : GameServerPacket
{
  private readonly Account account_0;
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 6658);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_GMCHAT_START_CHAT_ACK) this).uint_0);
    if (((PROTOCOL_GMCHAT_START_CHAT_ACK) this).uint_0 != 0U)
      return;
    this.WriteC((byte) (((PROTOCOL_GMCHAT_START_CHAT_ACK) this).account_0.Nickname.Length + 1));
    this.WriteN(((PROTOCOL_GMCHAT_START_CHAT_ACK) this).account_0.Nickname, ((PROTOCOL_GMCHAT_START_CHAT_ACK) this).account_0.Nickname.Length + 2, "UTF-16LE");
    this.WriteQ(((PROTOCOL_GMCHAT_START_CHAT_ACK) this).account_0.PlayerId);
  }

  public PROTOCOL_GM_LOG_ROOM_ACK(uint uint_1)
  {
    ((PROTOCOL_GMCHAT_APPLY_PENALTY_ACK) this).uint_0 = uint_1;
  }
}
