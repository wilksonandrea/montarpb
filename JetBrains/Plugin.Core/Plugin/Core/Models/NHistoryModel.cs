// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.NHistoryModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class NHistoryModel
{
  [CompilerGenerated]
  [SpecialName]
  public int get_ItemId() => ((MissionStore) this).int_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_ItemId(int int_8) => ((MissionStore) this).int_1 = int_8;

  [CompilerGenerated]
  [SpecialName]
  public bool get_Enable() => ((MissionStore) this).bool_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Enable([In] bool obj0) => ((MissionStore) this).bool_0 = obj0;

  public string OldNick { get; set; }

  public string NewNick { get; set; }

  public string Motive { get; set; }

  public long ObjectId { get; set; }

  public long OwnerId
  {
    [CompilerGenerated, SpecialName] get => ((NHistoryModel) this).long_1;
    [CompilerGenerated, SpecialName] set => ((NHistoryModel) this).long_1 = value;
  }

  public uint ChangeDate
  {
    [CompilerGenerated, SpecialName] get => ((NHistoryModel) this).uint_0;
    [CompilerGenerated, SpecialName] set => ((NHistoryModel) this).uint_0 = value;
  }
}
