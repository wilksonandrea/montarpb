// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.BattleRewardXML
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.XML;

public class BattleRewardXML
{
  public static List<BattleRewardModel> Rewards;

  private static void smethod_4()
  {
    List<BattleBoxModel> battleBoxModelList = new List<BattleBoxModel>();
    lock (BattleBoxXML.BBoxes)
    {
      foreach (BattleBoxModel bbox in BattleBoxXML.BBoxes)
        battleBoxModelList.Add(bbox);
    }
    BattleBoxXML.TotalBoxes = battleBoxModelList.Count;
    int num1 = (int) Math.Ceiling((double) battleBoxModelList.Count / 100.0);
    int num2 = 0;
    for (int int_0 = 0; int_0 < num1; ++int_0)
    {
      byte[] numArray = BattleBoxXML.smethod_3(100, int_0, ref num2, battleBoxModelList);
      SlotChange slotChange = new SlotChange();
      ((ShopData) slotChange).Buffer = numArray;
      slotChange.set_ItemsCount(num2);
      slotChange.set_Offset(int_0 * 100);
      ShopData shopData = (ShopData) slotChange;
      BattleBoxXML.ShopDataBattleBoxes.Add(shopData);
    }
  }

  private static void smethod_5([In] BattleBoxModel obj0, SyncServerPacket battleBoxModel_0)
  {
    battleBoxModel_0.WriteD(obj0.CouponId);
    battleBoxModel_0.WriteH((ushort) obj0.RequireTags);
    battleBoxModel_0.WriteH((short) 0);
    battleBoxModel_0.WriteH((short) 0);
    battleBoxModel_0.WriteC((byte) 0);
  }

  public static BattleBoxModel GetBattleBox([In] int obj0)
  {
    if (obj0 == 0)
      return (BattleBoxModel) null;
    lock (BattleBoxXML.BBoxes)
    {
      foreach (BattleBoxModel bbox in BattleBoxXML.BBoxes)
      {
        if (bbox.CouponId == obj0)
          return bbox;
      }
    }
    return (BattleBoxModel) null;
  }

  static BattleRewardXML()
  {
    BattleBoxXML.BBoxes = new List<BattleBoxModel>();
    BattleBoxXML.ShopDataBattleBoxes = new List<ShopData>();
  }

  public static void Load()
  {
    string path = "Data/BattleRewards.xml";
    if (File.Exists(path))
    {
      CompetitiveXML.smethod_0(path);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + path, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {BattleRewardXML.Rewards.Count} Battle Rewards", LoggerType.Info, (Exception) null);
  }

  public static void Reload()
  {
    BattleRewardXML.Rewards.Clear();
    BattleRewardXML.Load();
  }
}
