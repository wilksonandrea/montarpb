// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_FRIEND_INSERT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_FRIEND_INSERT_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 1810);
    this.WriteC((byte) ((PROTOCOL_AUTH_FRIEND_INFO_ACK) this).list_0.Count);
    foreach (FriendModel friendModel in ((PROTOCOL_AUTH_FRIEND_INFO_ACK) this).list_0)
    {
      PlayerInfo info = friendModel.Info;
      if (info == null)
      {
        this.WriteB(new byte[24]);
      }
      else
      {
        this.WriteC((byte) (info.Nickname.Length + 1));
        this.WriteN(info.Nickname, info.Nickname.Length + 2, "UTF-16LE");
        this.WriteQ(friendModel.get_PlayerId());
        this.WriteD(ComDiv.GetFriendStatus(friendModel));
        this.WriteD(uint.MaxValue);
        this.WriteC((byte) info.Rank);
        this.WriteB(new byte[6]);
      }
    }
  }

  public PROTOCOL_AUTH_FRIEND_INSERT_ACK(FriendChangeState string_1, [In] FriendModel obj1, [In] int obj2)
  {
    ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).friendChangeState_0 = string_1;
    ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).friendState_0 = (FriendState) obj1.State;
    ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).friendModel_0 = obj1;
    ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).int_0 = obj2;
  }
}
