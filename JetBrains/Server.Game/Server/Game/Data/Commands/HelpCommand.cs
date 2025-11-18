// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Commands.HelpCommand
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Commands;

public class HelpCommand : ICommand
{
  private readonly int int_0;

  [SpecialName]
  public string get_Description() => "End work in progress battle";

  [SpecialName]
  public string get_Permission() => "moderatorcommand";

  [SpecialName]
  public string get_Args() => "";

  public string Execute(string igrouping_0, [In] string[] obj1, [In] Account obj2)
  {
    RoomModel room = obj2.Room;
    if (room == null)
      return "A room is required to execute the command.";
    if (!room.IsPreparing() || !AllUtils.PlayerIsBattle(obj2))
      return "Oops! the battle hasn't started.";
    AllUtils.EndBattle(room);
    return "Battle ended.";
  }

  public string Command => "help";

  public string Description
  {
    [SpecialName] get => "Show available commands";
  }

  public string Permission
  {
    [SpecialName] get => "helpcommand";
  }

  public string Args
  {
    [SpecialName] get => "%page% (optional)";
  }
}
