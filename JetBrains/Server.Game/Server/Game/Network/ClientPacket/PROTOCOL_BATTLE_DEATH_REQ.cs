// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLE_DEATH_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_DEATH_REQ : GameClientPacket
{
  private FragInfos fragInfos_0;
  private bool bool_0;

  public MessageModel CreateMessage([In] string obj0, long playerConfig_0, [In] string obj2)
  {
    MessageModel messageModel = new MessageModel(15.0)
    {
      SenderName = obj0,
      Text = obj2,
      Type = NoteMessageType.Gift,
      State = NoteMessageState.Unreaded
    };
    return !DaoManagerSQL.CreateMessage(playerConfig_0, messageModel) ? (MessageModel) null : messageModel;
  }

  public virtual void Read()
  {
  }
}
