// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Commands.MessageCommand
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Commands;

public class MessageCommand : ICommand
{
  [SpecialName]
  public abstract string get_Command();

  [SpecialName]
  public abstract string get_Description();

  [SpecialName]
  public abstract string get_Permission();

  [SpecialName]
  public abstract string get_Args();

  public abstract string Execute([In] string obj0, string[] Args, Account Player);

  public string Command => "sendmsg";

  public string Description
  {
    [SpecialName] get => "Send messages";
  }

  public string Permission
  {
    [SpecialName] get => "moderatorcommand";
  }

  public string Args
  {
    [SpecialName] get => "%options% %text%";
  }
}
