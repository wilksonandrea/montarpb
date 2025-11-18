// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.RHistoryModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class RHistoryModel
{
  [CompilerGenerated]
  [SpecialName]
  public void set_RecordValue(int value) => ((RecordInfo) this).int_0 = value;

  public RHistoryModel(string[] value)
  {
    ((RecordInfo) this).PlayerId = this.GetPlayerId(value);
    this.set_RecordValue(this.GetPlayerValue(value));
  }

  public long GetPlayerId(string[] value)
  {
    try
    {
      return long.Parse(value[0]);
    }
    catch
    {
      return 0;
    }
  }

  public int GetPlayerValue(string[] value)
  {
    try
    {
      return int.Parse(value[1]);
    }
    catch
    {
      return 0;
    }
  }

  public string GetSplit()
  {
    return $"{((RecordInfo) this).PlayerId.ToString()}-{((RecordInfo) this).RecordValue.ToString()}";
  }

  public long ObjectId { get; set; }

  public long OwnerId { get; set; }

  public long SenderId { get; set; }

  public uint Date { get; set; }

  public string OwnerNick { get; set; }

  public string SenderNick { get; set; }

  public string Message
  {
    [CompilerGenerated, SpecialName] get => ((RHistoryModel) this).string_2;
    [CompilerGenerated, SpecialName] set => ((RHistoryModel) this).string_2 = value;
  }

  public ReportType Type
  {
    [CompilerGenerated, SpecialName] get => ((RHistoryModel) this).reportType_0;
    [CompilerGenerated, SpecialName] set => ((RHistoryModel) this).reportType_0 = value;
  }
}
