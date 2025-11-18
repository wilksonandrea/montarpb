// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.ClanBestPlayers
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.SQL;
using Plugin.Core.Utility;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class ClanBestPlayers
{
  public void UpdateClanMatch(byte Buffer)
  {
    ((AccountStatus) this).ClanMatchId = Buffer;
    ((AccountStatus) this).Buffer[3] = Buffer;
    this.method_0();
  }

  private void method_0()
  {
    ComDiv.UpdateDB("accounts", "status", (object) (long) BitConverter.ToUInt32(((AccountStatus) this).Buffer, 0), "player_id", (object) ((AccountStatus) this).PlayerId);
  }

  public ClanBestPlayers()
  {
    ((ClanModel) this).BestPlayers = (ClanBestPlayers) new ClanInvite();
    // ISSUE: explicit constructor call
    base.\u002Ector();
    ((ClanModel) this).MaxPlayers = 50;
    ((ClanModel) this).Logo = uint.MaxValue;
    ((ClanModel) this).Name = "";
    ((ClanModel) this).Info = "";
    ((ClanModel) this).News = "";
    ((ClanModel) this).Points = 1000f;
  }

  public int GetClanUnit() => this.GetClanUnit(DaoManagerSQL.GetClanPlayers(((ClanModel) this).Id));

  public int GetClanUnit([In] int obj0)
  {
    if (obj0 >= 250)
      return 7;
    if (obj0 >= 200)
      return 6;
    if (obj0 >= 150)
      return 5;
    if (obj0 >= 100)
      return 4;
    if (obj0 >= 50)
      return 3;
    if (obj0 >= 30)
      return 2;
    return obj0 >= 10 ? 1 : 0;
  }

  public RecordInfo Exp { get; set; }

  public RecordInfo Participation { get; set; }

  public RecordInfo Wins { get; set; }

  public RecordInfo Kills { get; set; }

  public RecordInfo Headshots { get; set; }

  public void SetPlayers(string value, [In] string obj1, [In] string obj2, [In] string obj3, [In] string obj4)
  {
    this.Exp = (RecordInfo) new RHistoryModel(value.Split('-'));
    this.Participation = (RecordInfo) new RHistoryModel(obj1.Split('-'));
    this.Wins = (RecordInfo) new RHistoryModel(obj2.Split('-'));
    this.Kills = (RecordInfo) new RHistoryModel(obj3.Split('-'));
    this.Headshots = (RecordInfo) new RHistoryModel(obj4.Split('-'));
  }

  public void SetDefault()
  {
    string[] strArray = new string[2]{ "0", "0" };
    this.Exp = (RecordInfo) new RHistoryModel(strArray);
    this.Participation = (RecordInfo) new RHistoryModel(strArray);
    this.Wins = (RecordInfo) new RHistoryModel(strArray);
    this.Kills = (RecordInfo) new RHistoryModel(strArray);
    this.Headshots = (RecordInfo) new RHistoryModel(strArray);
  }

  public long GetPlayerId(string[] Exp)
  {
    try
    {
      return long.Parse(Exp[0]);
    }
    catch
    {
      return 0;
    }
  }

  public int GetPlayerValue([In] string[] obj0)
  {
    try
    {
      return int.Parse(obj0[1]);
    }
    catch
    {
      return 0;
    }
  }

  public void SetBestExp([In] SlotModel obj0)
  {
    if (obj0.Exp <= this.Exp.RecordValue)
      return;
    this.Exp.PlayerId = obj0.PlayerId;
    ((RHistoryModel) this.Exp).set_RecordValue(obj0.Exp);
  }
}
