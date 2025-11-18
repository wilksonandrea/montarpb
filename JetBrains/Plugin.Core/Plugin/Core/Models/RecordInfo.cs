// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.RecordInfo
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class RecordInfo
{
  [CompilerGenerated]
  [SpecialName]
  public string get_Benefit() => ((PlayerVip) this).string_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_Benefit(string index) => ((PlayerVip) this).string_1 = index;

  [CompilerGenerated]
  [SpecialName]
  public uint get_Expirate() => ((PlayerVip) this).uint_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Expirate([In] uint obj0) => ((PlayerVip) this).uint_0 = obj0;

  public long PlayerId { get; set; }

  public int RecordValue
  {
    get => this.int_0;
    [CompilerGenerated, SpecialName] set => ((RecordInfo) this).int_0 = value;
  }
}
