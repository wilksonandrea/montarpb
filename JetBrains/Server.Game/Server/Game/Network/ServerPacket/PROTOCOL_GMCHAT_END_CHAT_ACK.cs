// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_GMCHAT_END_CHAT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_GMCHAT_END_CHAT_ACK : GameServerPacket
{
  private readonly Account account_0;
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 1902);
    this.WriteD(((PROTOCOL_CS_ROOM_INVITED_ACK) this).int_0);
  }

  public PROTOCOL_GMCHAT_END_CHAT_ACK([In] long obj0)
  {
    ((PROTOCOL_CS_ROOM_INVITED_RESULT_ACK) this).long_0 = obj0;
  }
}
