// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PlayerTitles
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class PlayerTitles
{
  [CompilerGenerated]
  [SpecialName]
  public int get_TicketCount() => ((PlayerReport) this).int_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_TicketCount(int Index) => ((PlayerReport) this).int_0 = Index;

  [CompilerGenerated]
  [SpecialName]
  public int get_ReportedCount() => ((PlayerReport) this).int_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_ReportedCount(int value) => ((PlayerReport) this).int_1 = value;

  public long OwnerId { get; set; }

  public long Flags { get; set; }

  public int Equiped1 { get; set; }

  public int Equiped2 { get; set; }

  public int Equiped3 { get; set; }

  public int Slots { get; set; }
}
