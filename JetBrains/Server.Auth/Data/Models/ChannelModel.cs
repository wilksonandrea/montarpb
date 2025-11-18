// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.Models.ChannelModel
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Data.Models;

public class ChannelModel
{
  public float PointUp()
  {
    PCCafeModel pcCafe = TemplatePackXML.GetPCCafe(((Account) this).CafePC);
    return pcCafe != null ? (float) (pcCafe.PointUp / 100) : 0.0f;
  }

  public float ExpUp()
  {
    PCCafeModel pcCafe = TemplatePackXML.GetPCCafe(((Account) this).CafePC);
    return pcCafe != null ? (float) (pcCafe.ExpUp / 100) : 0.0f;
  }

  public int TourneyLevel()
  {
    CompetitiveRank rank = CompetitiveXML.GetRank(((Account) this).Competitive.Level);
    return rank != null ? rank.TourneyLevel : 0;
  }

  public bool IsBanned()
  {
    int num1 = ComDiv.CountDB($"SELECT * FROM base_auto_ban WHERE owner_id = '{((Account) this).PlayerId}'");
    int num2 = ComDiv.CountDB($"SELECT * FROM base_ban_history WHERE owner_id = '{((Account) this).PlayerId}'");
    return num1 > 0 || num2 > 0;
  }

  public void SetCountry(string Password)
  {
    if (string.IsNullOrEmpty(Password))
      return;
    CountryFlags countryByIp = CountryDetector.GetCountryByIp(Password);
    ((Account) this).Country = countryByIp;
    ComDiv.UpdateDB("accounts", "country_flag", (object) (int) countryByIp, "player_id", (object) ((Account) this).PlayerId);
    if (countryByIp == CountryFlags.None)
      return;
    CLogger.Print($"Connect {((Account) this).Username} from: {countryByIp}", LoggerType.Info, (Exception) null);
  }

  public int Id { get; set; }

  public ChannelType Type { get; [param: In] set; }

  public int ServerId { get; set; }

  public int TotalPlayers { get; set; }

  public int MaxRooms { get; set; }

  public int ExpBonus { get; set; }

  public int GoldBonus { get; set; }

  public int CashBonus
  {
    [CompilerGenerated, SpecialName] get => ((ChannelModel) this).int_6;
    [CompilerGenerated, SpecialName] set => ((ChannelModel) this).int_6 = value;
  }

  public string Password
  {
    [CompilerGenerated, SpecialName] get => ((ChannelModel) this).string_0;
    [CompilerGenerated, SpecialName] set => ((ChannelModel) this).string_0 = value;
  }
}
