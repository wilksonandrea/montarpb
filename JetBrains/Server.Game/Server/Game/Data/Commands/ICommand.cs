// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Commands.ICommand
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Commands;

public interface ICommand
{
  [SpecialName]
  sealed string get_Description() => "Show available commands";

  [SpecialName]
  sealed string get_Permission() => "helpcommand";

  [SpecialName]
  sealed string get_Args() => "%page% (optional)";

  sealed string Execute(string icommand_0, [In] string[] obj1, [In] Account obj2)
  {
    int num1 = 1;
    if (obj1.Length != 0)
    {
      int result;
      if (int.TryParse(obj1[0], out result))
      {
        if (result == 0)
          result = 1;
        num1 = result;
      }
      else
        num1 = 1;
    }
    // ISSUE: reference to a compiler-generated method
    IEnumerable<ICommand> commandsForPlayer = CommandManager.Class14.GetCommandsForPlayer(obj2);
    int num2 = (commandsForPlayer.Count<ICommand>() + ((HelpCommand) this).int_0 - 1) / ((HelpCommand) this).int_0;
    if (num1 > num2)
      return $"Please insert a valid page. Pages: {num2}";
    // ISSUE: reference to a compiler-generated method
    IEnumerable<ICommand> commands = commandsForPlayer.Split<ICommand>(((HelpCommand) this).int_0).ToArray<IEnumerable<ICommand>>()[num1 - 1];
    string str = $"Commands ({num1}/{num2}):\n";
    foreach (ICommand command in commands)
      str = $"{str}:{((MessageCommand) command).get_Command()} {((MessageCommand) command).get_Args()} -> {((MessageCommand) command).get_Description()}\n";
    obj2.Connection.SendPacket((GameServerPacket) new PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK(str));
    return $"Displayed commands page '{num1}' of '{num2}'";
  }

  ICommand()
  {
    ((HelpCommand) this).int_0 = 5;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  string Command { [SpecialName] get; }

  string Description { [SpecialName] get; }

  string Permission { [SpecialName] get; }

  string Args { [SpecialName] get; }
}
