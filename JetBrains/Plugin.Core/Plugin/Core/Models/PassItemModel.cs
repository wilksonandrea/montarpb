// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PassItemModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class PassItemModel
{
  [CompilerGenerated]
  [SpecialName]
  public void set_RewardCount(int value) => ((PassBoxModel) this).int_1 = value;

  [CompilerGenerated]
  [SpecialName]
  public int get_Card() => ((PassBoxModel) this).int_2;

  [CompilerGenerated]
  [SpecialName]
  public void set_Card(int value) => ((PassBoxModel) this).int_2 = value;

  public PassItemModel()
  {
    ((PassBoxModel) this).Normal = (PassItemModel) new PCCafeModel();
    ((PassBoxModel) this).PremiumA = (PassItemModel) new PCCafeModel();
    ((PassBoxModel) this).PremiumB = (PassItemModel) new PCCafeModel();
  }

  public void SetCount()
  {
    if (((PassBoxModel) this).Normal != null && ((PassBoxModel) this).Normal.GoodId > 0)
      this.set_RewardCount(((PassBoxModel) this).RewardCount + 1);
    if (((PassBoxModel) this).PremiumA != null && ((PassBoxModel) this).PremiumA.GoodId > 0)
      this.set_RewardCount(((PassBoxModel) this).RewardCount + 1);
    if (((PassBoxModel) this).PremiumB == null || ((PassBoxModel) this).PremiumB.GoodId <= 0)
      return;
    this.set_RewardCount(((PassBoxModel) this).RewardCount + 1);
  }

  public int GoodId
  {
    get => this.int_0;
    [CompilerGenerated, SpecialName] set => ((PassItemModel) this).int_0 = value;
  }

  public bool IsReward
  {
    [CompilerGenerated, SpecialName] get => ((PassItemModel) this).bool_0;
    [CompilerGenerated, SpecialName] set => ((PassItemModel) this).bool_0 = value;
  }
}
