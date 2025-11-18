// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.ClanTeam
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class ClanTeam
{
  [CompilerGenerated]
  [SpecialName]
  public string get_ChatAnnounce() => ((ServerConfig) this).string_8;

  [CompilerGenerated]
  [SpecialName]
  public void set_ChatAnnounce(string value) => ((ServerConfig) this).string_8 = value;

  [CompilerGenerated]
  [SpecialName]
  public ShowroomView get_Showroom() => ((ServerConfig) this).showroomView_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Showroom(ShowroomView value) => ((ServerConfig) this).showroomView_0 = value;

  public ClanTeam()
  {
    ((ServerConfig) this).UserFileList = "";
    ((ServerConfig) this).ClientVersion = "";
    ((ServerConfig) this).ExitURL = "";
    ((ServerConfig) this).ShopURL = "";
    ((ServerConfig) this).OfficialSteam = "";
    ((ServerConfig) this).OfficialBanner = "";
    ((ServerConfig) this).OfficialAddress = "";
    ((ServerConfig) this).ChannelAnnounce = "";
    this.set_ChatAnnounce("");
    this.set_Showroom(ShowroomView.S_Default);
  }

  public int ClanId { get; set; }

  public int PlayersFR
  {
    [CompilerGenerated, SpecialName] get => ((ClanTeam) this).int_1;
    [CompilerGenerated, SpecialName] set => ((ClanTeam) this).int_1 = value;
  }

  public int PlayersCT
  {
    [CompilerGenerated, SpecialName] get => ((ClanTeam) this).int_2;
    [CompilerGenerated, SpecialName] set => ((ClanTeam) this).int_2 = value;
  }
}
