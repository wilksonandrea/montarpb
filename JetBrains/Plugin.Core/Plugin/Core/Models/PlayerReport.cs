// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PlayerReport
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class PlayerReport
{
  [CompilerGenerated]
  [SpecialName]
  public void set_OwnerId(long value) => ((PlayerQuickstart) this).long_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public List<QuickstartModel> get_Quickjoins() => ((PlayerQuickstart) this).list_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Quickjoins(List<QuickstartModel> value)
  {
    // ISSUE: reference to a compiler-generated field
    ((PlayerQuickstart) this).list_0 = value;
  }

  public PlayerReport() => this.set_Quickjoins(new List<QuickstartModel>());

  public QuickstartModel GetMapList(byte value)
  {
    lock (this.get_Quickjoins())
    {
      foreach (QuickstartModel mapList in this.get_Quickjoins())
      {
        if (mapList.MapId == (int) value)
          return mapList;
      }
    }
    return (QuickstartModel) null;
  }

  public long OwnerId { get; set; }

  public int TicketCount
  {
    [CompilerGenerated, SpecialName] get => ((PlayerReport) this).int_0;
    [CompilerGenerated, SpecialName] set => ((PlayerReport) this).int_0 = value;
  }

  public int ReportedCount
  {
    [CompilerGenerated, SpecialName] get => ((PlayerReport) this).int_1;
    [CompilerGenerated, SpecialName] set => ((PlayerReport) this).int_1 = value;
  }
}
