// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.ServerConfig
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class ServerConfig
{
  internal int method_2([In] PassBoxModel obj0) => obj0.RequiredPoints;

  internal bool method_0(PassBoxModel PassPoints)
  {
    // ISSUE: reference to a compiler-generated field
    return ((PassItemModel) PassPoints).get_Card() == ((BattlePassModel.Class12) this).int_0;
  }

  internal bool method_1(PassBoxModel PassPoints)
  {
    // ISSUE: reference to a compiler-generated field
    return ((PassItemModel) PassPoints).get_Card() == ((BattlePassModel.Class12) this).int_0;
  }

  internal bool method_2(PassBoxModel passBoxModel_0)
  {
    // ISSUE: reference to a compiler-generated field
    return ((PassItemModel) passBoxModel_0).get_Card() == ((BattlePassModel.Class12) this).int_0;
  }

  public int ConfigId { get; set; }

  public int ChannelAnnounceColor { get; set; }

  public int ChatAnnounceColor { get; set; }

  public bool OnlyGM { get; set; }

  public bool AccessUFL { get; set; }

  public bool Missions { get; set; }

  public bool GiftSystem { get; set; }

  public bool EnableClan { get; set; }

  public bool EnableTicket { get; set; }

  public bool EnablePlaytime { get; set; }

  public bool EnableTags { get; set; }

  public bool EnableBlood { get; set; }

  public bool OfficialBannerEnabled { get; set; }

  public string UserFileList { get; set; }

  public string ClientVersion { get; set; }

  public string ExitURL { get; set; }

  public string ShopURL { get; set; }

  public string OfficialSteam { get; set; }

  public string OfficialBanner { get; set; }

  public string OfficialAddress { get; set; }

  public string ChannelAnnounce { get; set; }

  public string ChatAnnounce
  {
    [CompilerGenerated, SpecialName] get => ((ServerConfig) this).string_8;
    [CompilerGenerated, SpecialName] set => ((ServerConfig) this).string_8 = value;
  }

  public ShowroomView Showroom
  {
    [CompilerGenerated, SpecialName] get => ((ServerConfig) this).showroomView_0;
    [CompilerGenerated, SpecialName] set => ((ServerConfig) this).showroomView_0 = value;
  }
}
