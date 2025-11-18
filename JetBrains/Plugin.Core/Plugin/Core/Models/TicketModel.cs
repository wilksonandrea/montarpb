// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.TicketModel
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class TicketModel
{
  [CompilerGenerated]
  [SpecialName]
  public void set_RewardCount(int value) => ((VisitBoxModel) this).int_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public bool get_IsBothReward() => ((VisitBoxModel) this).bool_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_IsBothReward(bool value) => ((VisitBoxModel) this).bool_0 = value;

  public TicketModel()
  {
    ((VisitBoxModel) this).Reward1 = (VisitItemModel) new BanHistory();
    ((VisitBoxModel) this).Reward2 = (VisitItemModel) new BanHistory();
  }

  public void SetCount()
  {
    if (((VisitBoxModel) this).Reward1 != null && ((VisitBoxModel) this).Reward1.GoodId > 0)
      this.set_RewardCount(((VisitBoxModel) this).RewardCount + 1);
    if (((VisitBoxModel) this).Reward2 == null || ((VisitBoxModel) this).Reward2.GoodId <= 0)
      return;
    this.set_RewardCount(((VisitBoxModel) this).RewardCount + 1);
  }

  public string Token { get; set; }

  public TicketType Type { get; set; }

  public int GoldReward { get; set; }

  public int CashReward { get; set; }

  public int TagsReward { get; set; }

  public uint TicketCount { get; set; }

  public uint PlayerRation
  {
    [CompilerGenerated, SpecialName] get => ((TicketModel) this).uint_1;
    [CompilerGenerated, SpecialName] set => ((TicketModel) this).uint_1 = value;
  }

  public List<int> Rewards
  {
    [CompilerGenerated, SpecialName] get => ((TicketModel) this).list_0;
    [CompilerGenerated, SpecialName] set => ((TicketModel) this).list_0 = value;
  }
}
