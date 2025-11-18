// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Commands.SystemCommand
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.JSON;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Commands;

public class SystemCommand : ICommand
{
  [SpecialName]
  public string get_Description() => "Change room options (AI Only)";

  [SpecialName]
  public string get_Permission() => "hostcommand";

  [SpecialName]
  public string get_Args() => "%options% %value%";

  public string Execute([In] string obj0, string[] Args, Account Player)
  {
    RoomModel room = Player.Room;
    if (room == null)
      return "A room is required to execute the command.";
    Account SlotId;
    if (!room.GetLeader(ref SlotId) || SlotId != Player)
      return "This Command Is Only Valid For Host (Room Leader).";
    string lower1 = Args[0].ToLower();
    string lower2 = Args[1].ToLower();
    switch (lower1)
    {
      case "wpn":
        CommandHelper tag1 = CommandHelperJSON.GetTag("WeaponsFlag");
        if (lower2 != null)
        {
          switch (lower2.Length)
          {
            case 2:
              switch (lower2[0])
              {
                case 'a':
                  if (lower2 == "ar")
                  {
                    room.WeaponsFlag = (RoomWeaponsFlag) tag1.AssaultRifle;
                    room.UpdateRoomInfo();
                    return "Weapon Assault Rifle (Only)";
                  }
                  break;
                case 'm':
                  if (lower2 == "mg")
                  {
                    room.WeaponsFlag = (RoomWeaponsFlag) tag1.MachineGun;
                    room.UpdateRoomInfo();
                    return "Weapon Machine Gun (Only)";
                  }
                  break;
                case 's':
                  switch (lower2)
                  {
                    case "sr":
                      room.WeaponsFlag = (RoomWeaponsFlag) tag1.SniperRifle;
                      room.UpdateRoomInfo();
                      return "Weapon Sniper Rifle (Only)";
                    case "sg":
                      room.WeaponsFlag = (RoomWeaponsFlag) tag1.ShotGun;
                      room.UpdateRoomInfo();
                      return "Weapon Shot Gun (Only)";
                  }
                  break;
              }
              break;
            case 3:
              switch (lower2[0])
              {
                case 'a':
                  if (lower2 == "all")
                  {
                    room.WeaponsFlag = (RoomWeaponsFlag) tag1.AllWeapons;
                    room.UpdateRoomInfo();
                    return "All Weapons (AR, SMG, SR, SG, MG, SHD)";
                  }
                  break;
                case 'r':
                  if (lower2 == "rpg")
                  {
                    room.WeaponsFlag = (RoomWeaponsFlag) tag1.RPG7;
                    room.UpdateRoomInfo();
                    return "Weapon RPG-7 (Only) - Hot Glitch";
                  }
                  break;
                case 's':
                  if (lower2 == "smg")
                  {
                    room.WeaponsFlag = (RoomWeaponsFlag) tag1.SubMachineGun;
                    room.UpdateRoomInfo();
                    return "Weapon Sub Machine Gun (Only)";
                  }
                  break;
              }
              break;
          }
        }
        room.WeaponsFlag = (RoomWeaponsFlag) tag1.AllWeapons;
        room.UpdateRoomInfo();
        return "Weapon Default (Value Not Founded)";
      case "time":
        CommandHelper tag2 = CommandHelperJSON.GetTag("PlayTime");
        switch (int.Parse(lower2))
        {
          case 5:
            room.KillTime = tag2.Minutes05;
            room.UpdateRoomInfo();
            return ComDiv.ToTitleCase(lower1) + " 5 Minutes";
          case 10:
            room.KillTime = tag2.Minutes10;
            room.UpdateRoomInfo();
            return ComDiv.ToTitleCase(lower1) + " 10 Minutes";
          case 15:
            room.KillTime = tag2.Minutes15;
            room.UpdateRoomInfo();
            return ComDiv.ToTitleCase(lower1) + " 15 Minutes";
          case 20:
            room.KillTime = tag2.Minutes20;
            room.UpdateRoomInfo();
            return ComDiv.ToTitleCase(lower1) + " 20 Minutes";
          case 25:
            room.KillTime = tag2.Minutes25;
            room.UpdateRoomInfo();
            return ComDiv.ToTitleCase(lower1) + " 25 Minutes";
          case 30:
            room.KillTime = tag2.Minutes30;
            room.UpdateRoomInfo();
            return ComDiv.ToTitleCase(lower1) + " 30 Minutes";
          default:
            return ComDiv.ToTitleCase(lower1) + " None! (Wrong Value)";
        }
      case "compe":
        switch (lower2.ToLower())
        {
          case "on":
            if (room.GetSlotCount() != 6 && room.GetSlotCount() != 8 && room.GetSlotCount() != 10)
              return "Please change the slots to (3v3) or (4v4) or (5v5)";
            room.Name = "Competitive!!!";
            room.Competitive = true;
            AllUtils.SendCompetitiveInfo(Player);
            return ComDiv.ToTitleCase(lower1) + "titive Enabled!";
          case "off":
            room.Name = room.RandomName(new Random().Next(1, 4));
            room.Competitive = false;
            return ComDiv.ToTitleCase(lower1) + "titive Disabled!";
          default:
            return $"Unable to use Competitive command! (Wrong Value: {lower2})";
        }
      default:
        return $"Command {ComDiv.ToTitleCase(lower1)} was not founded!";
    }
  }

  public string Command => "sys";

  public string Description
  {
    [SpecialName] get => "change server settings";
  }

  public string Permission
  {
    [SpecialName] get => "developercommand";
  }

  public string Args
  {
    [SpecialName] get => "%options% %value%";
  }
}
