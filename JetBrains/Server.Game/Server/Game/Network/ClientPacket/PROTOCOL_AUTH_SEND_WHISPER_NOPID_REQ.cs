// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_AUTH_SEND_WHISPER_NOPID_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_SEND_WHISPER_NOPID_REQ : GameClientPacket
{
  private long long_0;
  private string string_0;
  private string string_1;

  private Account method_0([In] Account obj0)
  {
    FriendModel friend = obj0.Friend.GetFriend(((PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_REQ) this).int_0);
    return friend == null ? (Account) null : ClanManager.GetAccount(friend.get_PlayerId(), 287);
  }

  public virtual void Read()
  {
  }
}
