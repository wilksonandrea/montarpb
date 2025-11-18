// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.EventLoginXML
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

#nullable disable
namespace Plugin.Core.XML;

public class EventLoginXML
{
  public static List<EventLoginModel> Events;

  public static void Load()
  {
    string str = "Data/CouponFlags.xml";
    if (File.Exists(str))
    {
      EventLoginXML.smethod_0(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {CouponEffectXML.list_0.Count} Coupon Effects", LoggerType.Info, (Exception) null);
  }

  public static void Reload()
  {
    CouponEffectXML.list_0.Clear();
    EventLoginXML.Load();
  }

  public static CouponFlag GetCouponEffect(int string_0)
  {
    lock (CouponEffectXML.list_0)
    {
      for (int index = 0; index < CouponEffectXML.list_0.Count; ++index)
      {
        CouponFlag couponEffect = CouponEffectXML.list_0[index];
        if (((AccountStatus) couponEffect).get_ItemId() == string_0)
          return couponEffect;
      }
      return (CouponFlag) null;
    }
  }

  private static void smethod_0(string xmlNode_0)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(xmlNode_0, FileMode.Open))
    {
      if (inStream.Length == 0L)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print("File is empty: " + xmlNode_0, LoggerType.Warning, (Exception) null);
      }
      else
      {
        try
        {
          xmlDocument.Load((Stream) inStream);
          for (XmlNode xmlNode1 = xmlDocument.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
          {
            if ("List".Equals(xmlNode1.Name))
            {
              for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
              {
                if ("Coupon".Equals(xmlNode2.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
                  AccountStatus accountStatus = new AccountStatus();
                  accountStatus.set_ItemId(int.Parse(attributes.GetNamedItem("ItemId").Value));
                  accountStatus.set_EffectFlag(ComDiv.ParseEnum<CouponEffects>(attributes.GetNamedItem("EffectFlag").Value));
                  CouponFlag couponFlag = (CouponFlag) accountStatus;
                  CouponEffectXML.list_0.Add(couponFlag);
                }
              }
            }
          }
        }
        catch (XmlException ex)
        {
          // ISSUE: reference to a compiler-generated method
          CLogger.Class1.Print(ex.Message, LoggerType.Error, (Exception) ex);
        }
      }
      inStream.Dispose();
      inStream.Close();
    }
  }

  static EventLoginXML() => CouponEffectXML.list_0 = new List<CouponFlag>();

  public static void Load()
  {
    string str = "Data/Events/Login.xml";
    if (File.Exists(str))
    {
      EventBoostXML.smethod_0(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {EventLoginXML.Events.Count} Event Login", LoggerType.Info, (Exception) null);
  }

  public static void Reload()
  {
    EventLoginXML.Events.Clear();
    EventLoginXML.Load();
  }

  public static EventLoginModel GetRunningEvent()
  {
    lock (EventLoginXML.Events)
    {
      uint num = uint.Parse(DBQuery.Now("yyMMddHHmm"));
      foreach (EventLoginModel runningEvent in EventLoginXML.Events)
      {
        if (runningEvent.BeginDate <= num && num < runningEvent.EndedDate)
          return runningEvent;
      }
      return (EventLoginModel) null;
    }
  }
}
