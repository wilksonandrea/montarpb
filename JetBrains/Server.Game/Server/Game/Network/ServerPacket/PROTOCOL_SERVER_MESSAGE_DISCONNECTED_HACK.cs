// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly bool bool_0;

  public virtual void Write()
  {
    this.WriteH((short) 8453);
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_SEASON_CHALLENGE_SEND_REWARD_ACK) this).uint_0);
    if (((PROTOCOL_SEASON_CHALLENGE_SEND_REWARD_ACK) this).uint_0 != 0U)
      return;
    this.WriteC((byte) 2);
    this.WriteD(((PROTOCOL_SEASON_CHALLENGE_SEND_REWARD_ACK) this).int_0[1]);
    this.WriteD(((PROTOCOL_SEASON_CHALLENGE_SEND_REWARD_ACK) this).int_0[2]);
    this.WriteD(((PROTOCOL_SEASON_CHALLENGE_SEND_REWARD_ACK) this).int_0[0]);
    this.WriteC((byte) 1);
    this.WriteC((byte) 1);
    this.WriteC((byte) 1);
  }

  public PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK([In] string obj0)
  {
    ((PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK) this).string_0 = obj0;
  }
}
