// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Commands.TitleCommand
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Commands;

public class TitleCommand : ICommand
{
  [SpecialName]
  public string get_Description() => "Send cash to a team";

  [SpecialName]
  public string get_Permission() => "gamemastercommand";

  [SpecialName]
  public string get_Args() => "%team% %cash% (Team = FR/CT)";

  public string Execute([In] string obj0, string[] Args, Account Player)
  {
    if (Player.Room == null)
      return "Please execute the command in a room";
    if (Args.Length != 2)
      return "Please use correct command usage, :teamcash %team% %cash%";
    int result;
    if (!int.TryParse(Args[1], out result))
      return "Please use correct number as value";
    string lower = Args[0].ToLower();
    if (lower != "red" && lower != "blue")
      return "Please use correct team, 'red' or 'blue'";
    int num = !(lower == "red") ? 1 : 0;
    RoomModel room = Player.Room;
    for (int int_0 = 0; int_0 < 18; ++int_0)
    {
      if (int_0 % 2 == num)
      {
        SlotModel slot = room.GetSlot(int_0);
        if (slot.PlayerId > 0L)
        {
          Account playerBySlot = room.GetPlayerBySlot(slot);
          if (playerBySlot != null && DaoManagerSQL.UpdateAccountCash(playerBySlot.PlayerId, playerBySlot.Cash + result))
          {
            playerBySlot.Cash += result;
            playerBySlot.SendPacket((GameServerPacket) new PROTOCOL_AUTH_RECV_WHISPER_ACK(0U, playerBySlot));
            UpdateServer.LoadGoldCash(playerBySlot);
          }
        }
      }
    }
    return $"'{result}' cash sended to team {lower}";
  }

  public string Command => "title";

  public string Description
  {
    [SpecialName] get => "Unlock all title";
  }

  public string Permission
  {
    [SpecialName] get => "hostcommand";
  }

  public string Args
  {
    [SpecialName] get => "";
  }
}
