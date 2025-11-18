// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.MissionAwards
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class MissionAwards
{
  [CompilerGenerated]
  [SpecialName]
  public uint get_EndedDate() => ((EventXmasModel) this).uint_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_EndedDate(uint value) => ((EventXmasModel) this).uint_1 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_GoodId() => ((EventXmasModel) this).int_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_GoodId(int value) => ((EventXmasModel) this).int_0 = value;

  public int Id { get; set; }

  public int MasterMedal { get; [param: In] set; }

  public int Exp
  {
    [CompilerGenerated, SpecialName] get => ((MissionAwards) this).int_2;
    [CompilerGenerated, SpecialName] set => ((MissionAwards) this).int_2 = value;
  }

  public int Gold
  {
    [CompilerGenerated, SpecialName] get => ((MissionAwards) this).int_3;
    [CompilerGenerated, SpecialName] set => ((MissionAwards) this).int_3 = value;
  }
}
