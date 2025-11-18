// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_ACK : GameServerPacket
{
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 1815);
    this.WriteC((byte) ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).friendChangeState_0);
    this.WriteC((byte) ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).int_0);
    if (((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).friendChangeState_0 != FriendChangeState.Insert && ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).friendChangeState_0 != FriendChangeState.Update)
    {
      this.WriteB(new byte[24]);
    }
    else
    {
      PlayerInfo info = ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).friendModel_0.Info;
      if (info == null)
      {
        this.WriteB(new byte[24]);
      }
      else
      {
        this.WriteC((byte) (info.Nickname.Length + 1));
        this.WriteN(info.Nickname, info.Nickname.Length + 2, "UTF-16LE");
        this.WriteQ(((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).friendModel_0.get_PlayerId());
        this.WriteD(ComDiv.GetFriendStatus(((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).friendModel_0, ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).friendState_0));
        this.WriteD(uint.MaxValue);
        this.WriteC((byte) info.Rank);
        this.WriteB(new byte[6]);
      }
    }
  }

  public PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_ACK([In] uint obj0)
  {
    ((PROTOCOL_AUTH_FRIEND_INSERT_ACK) this).uint_0 = obj0;
  }
}
