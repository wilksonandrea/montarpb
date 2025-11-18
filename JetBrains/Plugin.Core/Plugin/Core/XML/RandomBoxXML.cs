// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.RandomBoxXML
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace Plugin.Core.XML;

public class RandomBoxXML
{
  public static SortedList<int, RandomBoxModel> RBoxes;

  internal Class0<T0, int> method_0([In] T0 obj0, int RuleId) => new Class0<T0, int>(obj0, RuleId);

  internal IEnumerable<T0> method_1(IGrouping<int, Class0<T0, int>> string_0); // Unable to render the method body

  internal T0 method_2(Class0<T0, int> xmlNode_0) => xmlNode_0.item;

  internal int method_0([In] Class0<T0, int> obj0)
  {
    // ISSUE: reference to a compiler-generated field
    return obj0.inx / ((SystemMapXML.Class3<T0>) this).int_0;
  }

  public static void Load()
  {
    DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Data\\RBoxes");
    if (!directoryInfo.Exists)
      return;
    foreach (FileInfo file in directoryInfo.GetFiles())
    {
      try
      {
        RandomBoxXML.smethod_0(int.Parse(file.Name.Substring(0, file.Name.Length - 4)));
      }
      catch (Exception ex)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      }
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {RandomBoxXML.RBoxes.Count} Random Boxes", LoggerType.Info, (Exception) null);
  }

  public static void Reload()
  {
    RandomBoxXML.RBoxes.Clear();
    RandomBoxXML.Load();
  }

  private static void smethod_0(int gparam_0)
  {
    string path = $"Data/RBoxes/{gparam_0}.xml";
    if (File.Exists(path))
    {
      RandomBoxXML.smethod_1(path, gparam_0);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + path, LoggerType.Warning, (Exception) null);
    }
  }

  private static void smethod_1([In] string obj0, int int_0)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(obj0, FileMode.Open))
    {
      if (inStream.Length == 0L)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print("File is empty: " + obj0, LoggerType.Warning, (Exception) null);
      }
      else
      {
        try
        {
          xmlDocument.Load((Stream) inStream);
          for (XmlNode xmlNode = xmlDocument.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
          {
            if ("List".Equals(xmlNode.Name))
            {
              for (XmlNode class0_0 = xmlNode.FirstChild; class0_0 != null; class0_0 = class0_0.NextSibling)
              {
                if ("Item".Equals(class0_0.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) class0_0.Attributes;
                  FragModel fragModel = new FragModel();
                  ((RandomBoxModel) fragModel).ItemsCount = int.Parse(attributes.GetNamedItem("Count").Value);
                  fragModel.set_Items(new List<RandomBoxItem>());
                  RandomBoxModel randomBoxModel = (RandomBoxModel) fragModel;
                  PlayerRankXML.smethod_2(class0_0, randomBoxModel);
                  ((FragModel) randomBoxModel).SetTopPercent();
                  RandomBoxXML.RBoxes.Add(int_0, randomBoxModel);
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
          // ISSUE: reference to a compiler-generated method
          CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
        }
      }
      inStream.Dispose();
      inStream.Close();
    }
  }
}
