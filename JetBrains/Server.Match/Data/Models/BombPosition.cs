// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.BombPosition
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core.SharpDX;
using System.Runtime.CompilerServices;

#nullable disable
namespace Server.Match.Data.Models;

public class BombPosition
{
  [CompilerGenerated]
  [SpecialName]
  public bool get_IsKiller() => ((AssistServerData) this).bool_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_IsKiller(bool value) => ((AssistServerData) this).bool_1 = value;

  [CompilerGenerated]
  [SpecialName]
  public bool get_VictimDead() => ((AssistServerData) this).bool_2;

  [CompilerGenerated]
  [SpecialName]
  public void set_VictimDead(bool value) => ((AssistServerData) this).bool_2 = value;

  public BombPosition()
  {
    ((AssistServerData) this).Killer = -1;
    ((AssistServerData) this).Victim = -1;
  }

  public float X { get; set; }

  public float Y { get; set; }

  public float Z { get; set; }

  public Half3 Position
  {
    [CompilerGenerated, SpecialName] get => ((BombPosition) this).half3_0;
    [CompilerGenerated, SpecialName] set => ((BombPosition) this).half3_0 = value;
  }

  public bool EveryWhere
  {
    [CompilerGenerated, SpecialName] get => ((BombPosition) this).bool_0;
    [CompilerGenerated, SpecialName] set => ((BombPosition) this).bool_0 = value;
  }
}
