// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.CouponFlag
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Utility;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class CouponFlag
{
  [CompilerGenerated]
  [SpecialName]
  public DateTime get_StartDate() => ((BanHistory) this).dateTime_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_StartDate(DateTime value) => ((BanHistory) this).dateTime_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public DateTime get_EndDate() => ((BanHistory) this).dateTime_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_EndDate(DateTime value) => ((BanHistory) this).dateTime_1 = value;

  public CouponFlag()
  {
    this.set_StartDate(DBQuery.Now());
    ((BanHistory) this).Type = "";
    ((BanHistory) this).Value = "";
    ((BanHistory) this).Reason = "";
  }

  public int ItemId
  {
    [CompilerGenerated, SpecialName] get => ((CouponFlag) this).int_0;
    [CompilerGenerated, SpecialName] set => ((CouponFlag) this).int_0 = value;
  }

  public CouponEffects EffectFlag
  {
    [CompilerGenerated, SpecialName] get => ((CouponFlag) this).couponEffects_0;
    [CompilerGenerated, SpecialName] set => ((CouponFlag) this).couponEffects_0 = value;
  }
}
