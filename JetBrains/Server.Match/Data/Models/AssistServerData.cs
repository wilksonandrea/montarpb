// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.AssistServerData
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Server.Match.Data.Models;

public class AssistServerData
{
  [CompilerGenerated]
  [SpecialName]
  public int get_OtherAnim() => ((AnimModel) this).int_3;

  [CompilerGenerated]
  [SpecialName]
  public void set_OtherAnim(int value) => ((AnimModel) this).int_3 = value;

  [CompilerGenerated]
  [SpecialName]
  public float get_Duration() => ((AnimModel) this).float_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Duration(float value) => ((AnimModel) this).float_0 = value;

  public int RoomId { get; set; }

  public int Killer { get; set; }

  public int Victim { get; set; }

  public int Damage { get; set; }

  public bool IsAssist { get; set; }

  public bool IsKiller
  {
    [CompilerGenerated, SpecialName] get => ((AssistServerData) this).bool_1;
    [CompilerGenerated, SpecialName] set => ((AssistServerData) this).bool_1 = value;
  }

  public bool VictimDead
  {
    [CompilerGenerated, SpecialName] get => ((AssistServerData) this).bool_2;
    [CompilerGenerated, SpecialName] set => ((AssistServerData) this).bool_2 = value;
  }
}
