// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Commands.HostCommand
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Commands;

public class HostCommand : ICommand
{
  [SpecialName]
  public string get_Description() => "Change room options";

  [SpecialName]
  public string get_Permission() => "moderatorcommand";

  [SpecialName]
  public string get_Args() => "%options% %value%";

  public string Execute([In] string obj0, string[] Args, Account Player)
  {
    RoomModel room = Player.Room;
    if (room == null)
      return "A room is required to execute the command.";
    string lower1 = Args[0].ToLower();
    string lower2 = Args[1].ToLower();
    switch (lower1)
    {
      case "time":
        int num1 = int.Parse(lower2) * 6;
        if (num1 < 0)
          return $"Oops! Map 'index' Isn't Supposed To Be: {lower2}. Try an Higher Value.";
        if (room.IsPreparing() || AllUtils.PlayerIsBattle(Player))
          return "Oops! You Can't Change The 'time' While The Room Has Started Game Match.";
        room.KillTime = num1;
        room.UpdateRoomInfo();
        return $"{ComDiv.ToTitleCase(lower1)} Changed To {room.GetTimeByMask() % 60} Minutes";
      case "flags":
        RoomWeaponsFlag roomWeaponsFlag = (RoomWeaponsFlag) int.Parse(lower2);
        room.WeaponsFlag = roomWeaponsFlag;
        room.UpdateRoomInfo();
        return $"{ComDiv.ToTitleCase(lower1)} Changed To {roomWeaponsFlag}";
      case "killcam":
        byte num2 = byte.Parse(lower2);
        room.KillCam = num2;
        room.UpdateRoomInfo();
        return $"{ComDiv.ToTitleCase(lower1)} Changed To {num2}";
      default:
        return $"Command {ComDiv.ToTitleCase(lower1)} was not founded!";
    }
  }

  public string Command => "host";

  public string Description
  {
    [SpecialName] get => "Change room options (AI Only)";
  }

  public string Permission
  {
    [SpecialName] get => "hostcommand";
  }

  public string Args
  {
    [SpecialName] get => "%options% %value%";
  }
}
