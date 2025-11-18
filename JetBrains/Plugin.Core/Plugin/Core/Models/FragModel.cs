// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.FragModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class FragModel
{
  [CompilerGenerated]
  [SpecialName]
  public void set_Items(List<RandomBoxItem> value) => ((RandomBoxModel) this).list_0 = value;

  public List<RandomBoxItem> GetRewardList(List<RandomBoxItem> value, [In] int obj1)
  {
    List<RandomBoxItem> rewardList = new List<RandomBoxItem>();
    if (value.Count > 0)
    {
      int index = value[obj1].Index;
      foreach (RandomBoxItem randomBoxItem in value)
      {
        if (randomBoxItem.Index == index)
          rewardList.Add(randomBoxItem);
      }
    }
    return rewardList;
  }

  public List<RandomBoxItem> GetSortedList(int value)
  {
    if (value < ((RandomBoxModel) this).MinPercent)
      value = ((RandomBoxModel) this).MinPercent;
    List<RandomBoxItem> sortedList = new List<RandomBoxItem>();
    foreach (RandomBoxItem randomBoxItem in ((RandomBoxModel) this).Items)
    {
      if (value <= ((RandomBoxModel) randomBoxItem).get_Percent())
        sortedList.Add(randomBoxItem);
    }
    return sortedList;
  }

  public void SetTopPercent()
  {
    int num1 = 100;
    int num2 = 0;
    foreach (RandomBoxItem randomBoxItem in ((RandomBoxModel) this).Items)
    {
      if (((RandomBoxModel) randomBoxItem).get_Percent() < num1)
        num1 = ((RandomBoxModel) randomBoxItem).get_Percent();
      if (((RandomBoxModel) randomBoxItem).get_Percent() > num2)
        num2 = ((RandomBoxModel) randomBoxItem).get_Percent();
    }
    ((RandomBoxModel) this).MinPercent = num1;
    ((RandomBoxModel) this).MaxPercent = num2;
  }

  public byte WeaponClass { get; set; }

  public byte HitspotInfo { get; set; }

  public byte Unk { get; set; }

  public KillingMessage KillFlag { get; [param: In] set; }

  public float X { get; set; }

  public float Y { get; set; }

  public float Z { get; set; }

  public byte VictimSlot { get; set; }

  public byte AssistSlot
  {
    [CompilerGenerated, SpecialName] get => ((FragModel) this).byte_4;
    [CompilerGenerated, SpecialName] set => ((FragModel) this).byte_4 = value;
  }

  public byte[] Unks
  {
    [CompilerGenerated, SpecialName] get => ((FragModel) this).byte_5;
    [CompilerGenerated, SpecialName] set => ((FragModel) this).byte_5 = value;
  }
}
