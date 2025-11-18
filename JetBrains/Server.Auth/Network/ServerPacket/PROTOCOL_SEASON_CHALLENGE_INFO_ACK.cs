// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_SEASON_CHALLENGE_INFO_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_SEASON_CHALLENGE_INFO_ACK : AuthServerPacket
{
  private readonly BattlePassModel battlePassModel_0;
  private readonly PlayerBattlepass playerBattlepass_0;
  private readonly (int, int, int, int) valueTuple_0;
  private readonly int int_0;
  private readonly int int_1;

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
        this.WriteQ(info.PlayerId);
        this.WriteD(ComDiv.GetFriendStatus(friendModel));
        this.WriteD(uint.MaxValue);
        this.WriteC((byte) info.Rank);
        this.WriteB(new byte[6]);
      }
    }
  }

  public PROTOCOL_SEASON_CHALLENGE_INFO_ACK(
    [In] FriendChangeState obj0,
    [In] FriendModel obj1,
    FriendState byte_3,
    [In] int obj3)
  {
    ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).friendState_0 = byte_3;
    ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).friendModel_0 = obj1;
    ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).friendChangeState_0 = obj0;
    ((PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK) this).int_0 = obj3;
  }
}
