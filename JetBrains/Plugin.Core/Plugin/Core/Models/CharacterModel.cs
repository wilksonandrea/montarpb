// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.CharacterModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Utility;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class CharacterModel
{
  public CharacterModel()
  {
    ((MessageModel) this).SenderName = "";
    ((MessageModel) this).Text = "";
  }

  public CharacterModel(long value, [In] DateTime obj1)
  {
    ((MessageModel) this).SenderName = "";
    ((MessageModel) this).Text = "";
    ((MessageModel) this).ExpireDate = value;
    this.method_0(obj1);
  }

  public CharacterModel(double value)
  {
    ((MessageModel) this).SenderName = "";
    ((MessageModel) this).Text = "";
    DateTime dateTime = DBQuery.Now().AddDays(value);
    ((MessageModel) this).ExpireDate = long.Parse(dateTime.ToString("yyMMddHHmm"));
    this.method_1(dateTime, DBQuery.Now());
  }

  private void method_0(DateTime value)
  {
    this.method_1(DateTime.ParseExact(((MessageModel) this).ExpireDate.ToString(), "yyMMddHHmm", (IFormatProvider) CultureInfo.InvariantCulture), value);
  }

  private void method_1(DateTime value, [In] DateTime obj1)
  {
    int num = (int) Math.Ceiling((value - obj1).TotalDays);
    ((MessageModel) this).DaysRemaining = num < 0 ? 0 : num;
  }

  public long ObjectId { get; [param: In] set; }

  public int Id { get; set; }

  public int Slot { get; set; }

  public string Name { get; set; }

  public uint CreateDate
  {
    [CompilerGenerated, SpecialName] get => ((CharacterModel) this).uint_0;
    [CompilerGenerated, SpecialName] [param: In] set => ((CharacterModel) this).uint_0 = value;
  }

  public uint PlayTime
  {
    [CompilerGenerated, SpecialName] get => ((CharacterModel) this).uint_1;
    [CompilerGenerated, SpecialName] set => ((CharacterModel) this).uint_1 = value;
  }
}
