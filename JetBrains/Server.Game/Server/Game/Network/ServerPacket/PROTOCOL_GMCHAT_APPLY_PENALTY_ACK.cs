// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_GMCHAT_APPLY_PENALTY_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_GMCHAT_APPLY_PENALTY_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 829);
    this.WriteD(((PROTOCOL_CS_SECESSION_CLAN_ACK) this).uint_0);
  }

  public PROTOCOL_GMCHAT_APPLY_PENALTY_ACK([In] uint obj0, [In] Account obj1)
  {
    ((PROTOCOL_GMCHAT_END_CHAT_ACK) this).uint_0 = obj0;
    ((PROTOCOL_GMCHAT_END_CHAT_ACK) this).account_0 = obj1;
  }
}
