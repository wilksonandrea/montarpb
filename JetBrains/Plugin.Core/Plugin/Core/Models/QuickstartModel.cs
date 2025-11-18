// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.QuickstartModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class QuickstartModel
{
  [CompilerGenerated]
  [SpecialName]
  public ushort get_Port() => ((SChannelModel) this).ushort_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Port(ushort value) => ((SChannelModel) this).ushort_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public bool get_IsMobile() => ((SChannelModel) this).bool_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_IsMobile(bool value) => ((SChannelModel) this).bool_1 = value;

  public QuickstartModel(string value, [In] ushort obj1)
  {
    ((SChannelModel) this).Host = value;
    this.set_Port(obj1);
  }

  public int MapId { get; set; }

  public int Rule { get; set; }

  public int StageOptions
  {
    [CompilerGenerated, SpecialName] get => ((QuickstartModel) this).int_2;
    [CompilerGenerated, SpecialName] set => ((QuickstartModel) this).int_2 = value;
  }

  public int Type
  {
    [CompilerGenerated, SpecialName] get => ((QuickstartModel) this).int_3;
    [CompilerGenerated, SpecialName] set => ((QuickstartModel) this).int_3 = value;
  }
}
