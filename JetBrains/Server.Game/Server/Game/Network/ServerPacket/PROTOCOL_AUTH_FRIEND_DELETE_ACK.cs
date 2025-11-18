// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_FRIEND_DELETE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_FRIEND_DELETE_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 1836);
    this.WriteC((byte) ((PROTOCOL_AUTH_CHANGE_NICKNAME_ACK) this).string_0.Length);
    this.WriteU(((PROTOCOL_AUTH_CHANGE_NICKNAME_ACK) this).string_0, ((PROTOCOL_AUTH_CHANGE_NICKNAME_ACK) this).string_0.Length * 2);
  }

  public PROTOCOL_AUTH_FRIEND_DELETE_ACK([In] uint obj0)
  {
    ((PROTOCOL_AUTH_FIND_USER_ACK) this).uint_0 = obj0;
  }
}
