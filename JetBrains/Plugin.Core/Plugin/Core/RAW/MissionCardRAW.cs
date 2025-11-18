// Decompiled with JetBrains decompiler
// Type: Plugin.Core.RAW.MissionCardRAW
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.RAW;

public class MissionCardRAW
{
  private static List<MissionItemAward> list_0;
  private static List<MissionCardModel> list_1;
  private static List<MissionCardAwards> list_2;

  public void WriteS([In] string obj0, string Value, string Section = null)
  {
    try
    {
      this.method_1(obj0, Value, Section);
    }
    catch
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Write Parameter Failure: " + obj0, LoggerType.Warning, (Exception) null);
    }
  }

  private void method_1([In] string obj0, string Value, string Section = null)
  {
    if (((ConfigEngine) this).fileAccess_0 == FileAccess.Read)
      throw new Exception("Can`t write to file! No access!");
    ConfigEngine.WritePrivateProfileString(Section ?? ((ConfigEngine) this).string_0, obj0, " " + Value, ((ConfigEngine) this).fileInfo_0.FullName);
  }

  public void DeleteKey([In] string obj0, string Value)
  {
    this.method_1(obj0, (string) null, Value ?? ((ConfigEngine) this).string_0);
  }

  public void DeleteSection(string string_1)
  {
    this.method_1((string) null, (string) null, string_1 ?? ((ConfigEngine) this).string_0);
  }

  public bool KeyExists([In] string obj0, string string_2)
  {
    return ((ConfigEngine) this).method_0(obj0, string_2).Length > 0;
  }

  private static void smethod_0(string Key, int Section = default (int))
  {
    string str = $"Data/Missions/{Key}.mqf";
    if (File.Exists(str))
    {
      BaseClientPacket.smethod_2(str, Key, Section);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
  }

  public static void LoadBasicCards(int Section = default (int))
  {
    MissionCardRAW.smethod_0("TutorialCard_Russia", Section);
    MissionCardRAW.smethod_0("Dino_Tutorial", Section);
    MissionCardRAW.smethod_0("Human_Tutorial", Section);
    MissionCardRAW.smethod_0("AssaultCard", Section);
    MissionCardRAW.smethod_0("BackUpCard", Section);
    MissionCardRAW.smethod_0("InfiltrationCard", Section);
    MissionCardRAW.smethod_0("SpecialCard", Section);
    MissionCardRAW.smethod_0("DefconCard", Section);
    MissionCardRAW.smethod_0("Commissioned_o", Section);
    MissionCardRAW.smethod_0("Company_o", Section);
    MissionCardRAW.smethod_0("Field_o", Section);
    MissionCardRAW.smethod_0("EventCard", Section);
    MissionCardRAW.smethod_0("Dino_Basic", Section);
    MissionCardRAW.smethod_0("Human_Basic", Section);
    MissionCardRAW.smethod_0("Dino_Intensify", Section);
    MissionCardRAW.smethod_0("Human_Intensify", Section);
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {MissionCardRAW.list_1.Count} Mission Card List", LoggerType.Info, (Exception) null);
    if (Section == 1)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print($"Plugin Loaded: {MissionCardRAW.list_2.Count} Mission Card Awards", LoggerType.Info, (Exception) null);
    }
    else
    {
      if (Section != 2)
        return;
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print($"Plugin Loaded: {MissionCardRAW.list_0.Count} Mission Reward Items", LoggerType.Info, (Exception) null);
    }
  }

  private static int smethod_1(string Key)
  {
    int num = 0;
    if (Key != null)
    {
      switch (Key.Length)
      {
        case 7:
          if (Key == "Field_o")
          {
            num = 12;
            break;
          }
          break;
        case 9:
          switch (Key[0])
          {
            case 'C':
              if (Key == "Company_o")
              {
                num = 11;
                break;
              }
              break;
            case 'E':
              if (Key == "EventCard")
              {
                num = 13;
                break;
              }
              break;
          }
          break;
        case 10:
          switch (Key[1])
          {
            case 'a':
              if (Key == "BackUpCard")
              {
                num = 6;
                break;
              }
              break;
            case 'e':
              if (Key == "DefconCard")
              {
                num = 9;
                break;
              }
              break;
            case 'i':
              if (Key == "Dino_Basic")
              {
                num = 14;
                break;
              }
              break;
          }
          break;
        case 11:
          switch (Key[0])
          {
            case 'A':
              if (Key == "AssaultCard")
              {
                num = 5;
                break;
              }
              break;
            case 'H':
              if (Key == "Human_Basic")
              {
                num = 15;
                break;
              }
              break;
            case 'S':
              if (Key == "SpecialCard")
              {
                num = 8;
                break;
              }
              break;
          }
          break;
        case 13:
          if (Key == "Dino_Tutorial")
          {
            num = 2;
            break;
          }
          break;
        case 14:
          switch (Key[0])
          {
            case 'C':
              if (Key == "Commissioned_o")
              {
                num = 10;
                break;
              }
              break;
            case 'D':
              if (Key == "Dino_Intensify")
              {
                num = 16 /*0x10*/;
                break;
              }
              break;
            case 'H':
              if (Key == "Human_Tutorial")
              {
                num = 3;
                break;
              }
              break;
          }
          break;
        case 15:
          if (Key == "Human_Intensify")
          {
            num = 17;
            break;
          }
          break;
        case 16 /*0x10*/:
          if (Key == "InfiltrationCard")
          {
            num = 7;
            break;
          }
          break;
        case 19:
          if (Key == "TutorialCard_Russia")
          {
            num = 1;
            break;
          }
          break;
      }
    }
    return num;
  }

  public static List<ItemsModel> GetMissionAwards([In] int obj0)
  {
    List<ItemsModel> missionAwards = new List<ItemsModel>();
    lock (MissionCardRAW.list_0)
    {
      foreach (MissionItemAward missionItemAward in MissionCardRAW.list_0)
      {
        if (((MissionStore) missionItemAward).get_MissionId() == obj0)
          missionAwards.Add(((MissionStore) missionItemAward).get_Item());
      }
    }
    return missionAwards;
  }

  public static List<MissionCardModel> GetCards(int string_0, int int_0)
  {
    List<MissionCardModel> cards = new List<MissionCardModel>();
    lock (MissionCardRAW.list_1)
    {
      foreach (MissionCardModel missionCardModel in MissionCardRAW.list_1)
      {
        if (missionCardModel.MissionId == string_0 && (int_0 >= 0 && missionCardModel.CardBasicId == int_0 || int_0 == -1))
          cards.Add(missionCardModel);
      }
    }
    return cards;
  }

  public static List<MissionCardModel> GetCards(List<MissionCardModel> Type, [In] int obj1)
  {
    if (obj1 == -1)
      return Type;
    List<MissionCardModel> cards = new List<MissionCardModel>();
    foreach (MissionCardModel missionCardModel in MissionCardRAW.list_1)
    {
      if (obj1 >= 0 && missionCardModel.CardBasicId == obj1 || obj1 == -1)
        cards.Add(missionCardModel);
    }
    return cards;
  }

  public static List<MissionCardModel> GetCards(int MissionId)
  {
    List<MissionCardModel> cards = new List<MissionCardModel>();
    lock (MissionCardRAW.list_1)
    {
      foreach (MissionCardModel missionCardModel in MissionCardRAW.list_1)
      {
        if (missionCardModel.MissionId == MissionId)
          cards.Add(missionCardModel);
      }
    }
    return cards;
  }
}
