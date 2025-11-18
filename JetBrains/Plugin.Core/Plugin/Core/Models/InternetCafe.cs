// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.InternetCafe
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class InternetCafe
{
  [CompilerGenerated]
  [SpecialName]
  public uint get_PlayerRation() => ((TicketModel) this).uint_1;

  [CompilerGenerated]
  [SpecialName]
  public void set_PlayerRation(uint value) => ((TicketModel) this).uint_1 = value;

  [CompilerGenerated]
  [SpecialName]
  public List<int> get_Rewards() => ((TicketModel) this).list_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Rewards(List<int> value) => ((TicketModel) this).list_0 = value;

  public int ConfigId { get; set; }

  public int BasicExp { get; set; }

  public int BasicGold { get; set; }

  public int PremiumExp
  {
    [CompilerGenerated, SpecialName] get => ((InternetCafe) this).int_3;
    [CompilerGenerated, SpecialName] set => ((InternetCafe) this).int_3 = value;
  }

  public int PremiumGold
  {
    [CompilerGenerated, SpecialName] get => ((InternetCafe) this).int_4;
    [CompilerGenerated, SpecialName] set => ((InternetCafe) this).int_4 = value;
  }
}
