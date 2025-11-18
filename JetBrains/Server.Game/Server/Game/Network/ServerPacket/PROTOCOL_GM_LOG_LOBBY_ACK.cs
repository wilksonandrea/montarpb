// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_GM_LOG_LOBBY_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_GM_LOG_LOBBY_ACK : GameServerPacket
{
  private readonly Account account_0;
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 6662);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_GMCHAT_END_CHAT_ACK) this).uint_0);
  }

  public PROTOCOL_GM_LOG_LOBBY_ACK(uint int_1, [In] Account obj1)
  {
    ((PROTOCOL_GMCHAT_START_CHAT_ACK) this).uint_0 = int_1;
    ((PROTOCOL_GMCHAT_START_CHAT_ACK) this).account_0 = obj1;
  }
}
