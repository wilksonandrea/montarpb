// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Auth.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE : AuthServerPacket
{
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

  public PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE(uint list_1, [In] Account obj1)
  {
    ((PROTOCOL_AUTH_GET_POINT_CASH_ACK) this).uint_0 = list_1;
    ((PROTOCOL_AUTH_GET_POINT_CASH_ACK) this).account_0 = obj1;
  }
}
