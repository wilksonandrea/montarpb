// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Commands.RoomInfoCommand
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Commands;

public class RoomInfoCommand : ICommand
{
  [SpecialName]
  public string get_Description() => "Send messages";

  [SpecialName]
  public string get_Permission() => "moderatorcommand";

  [SpecialName]
  public string get_Args() => "%options% %text%";

  public string Execute([In] string obj0, string[] Args, Account Player)
  {
    string lower = Args[0].ToLower();
    string str = string.Join(" ", Args, 1, Args.Length - 1);
    switch (lower)
    {
      case "room":
        RoomModel room1 = Player.Room;
        if (room1 == null)
          return "A room is required to execute the command.";
        using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK Player1 = (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK) new PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK(str))
          room1.SendPacketToPlayers((GameServerPacket) Player1);
        return $"Message sended to current Room Id: {room1.RoomId + 1}";
      case "channel":
        ChannelModel channel = Player.GetChannel();
        if (channel == null)
          return "Please run the command in the lobby.";
        using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK room2 = (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK) new PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK(str))
          ((MatchModel) channel).SendPacketToWaitPlayers((GameServerPacket) room2);
        return $"Message sended to current Channel Id: {channel.Id + 1}";
      case "player":
        int num = 0;
        using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK messageAnnounceAck = (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK) new PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK(str))
        {
          SortedList<long, Account> accounts = AccountManager.Accounts;
          if (accounts.Count == 0)
            num = 0;
          byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) messageAnnounceAck).GetCompleteBytes("Player.MessageCommands");
          foreach (Account account in (IEnumerable<Account>) accounts.Values)
          {
            account.SendCompletePacket(completeBytes, messageAnnounceAck.GetType().Name);
            ++num;
          }
        }
        return $"Message sended to {num} total of player(s)";
      default:
        return $"Command {ComDiv.ToTitleCase(lower)} was not founded!";
    }
  }

  public string Command => "roominfo";

  public string Description
  {
    [SpecialName] get => "Change room options";
  }

  public string Permission
  {
    [SpecialName] get => "moderatorcommand";
  }

  public string Args
  {
    [SpecialName] get => "%options% %value%";
  }
}
