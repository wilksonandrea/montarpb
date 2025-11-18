// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Commands.BattleEndCommand
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Commands;

public class BattleEndCommand : ICommand
{
  internal bool method_0(Type type_0)
  {
    // ISSUE: reference to a compiler-generated field
    return ((CommandManager.Class16) this).type_0.IsAssignableFrom(type_0) && !type_0.IsInterface && !type_0.IsAbstract;
  }

  public BattleEndCommand()
  {
  }

  internal bool method_0(ICommand gparam_0)
  {
    // ISSUE: reference to a compiler-generated field
    return ((ChannelModel) ((CommandManager.Class17) this).account_0).HavePermission(((MessageCommand) gparam_0).get_Permission());
  }

  public BattleEndCommand()
  {
  }

  internal int method_0([In] Class0<T0, int> obj0)
  {
    // ISSUE: reference to a compiler-generated field
    return obj0.inx / ((CommandManager.Class18<T0>) this).int_0;
  }

  public string Command => "endbattle";

  public string Description
  {
    [SpecialName] get => "End work in progress battle";
  }

  public string Permission
  {
    [SpecialName] get => "moderatorcommand";
  }

  public string Args
  {
    [SpecialName] get => "";
  }
}
