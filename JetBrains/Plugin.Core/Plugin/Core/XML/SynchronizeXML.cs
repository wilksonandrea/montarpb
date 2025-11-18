// Decompiled with JetBrains decompiler
// Type: Plugin.Core.XML.SynchronizeXML
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.XML;

public class SynchronizeXML
{
  public static List<Synchronize> Servers;

  public static void Load()
  {
    DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Data\\Seasons");
    if (!directoryInfo.Exists)
      return;
    foreach (FileInfo file in directoryInfo.GetFiles())
    {
      try
      {
        SynchronizeXML.smethod_0(int.Parse(file.Name.Substring(0, file.Name.Length - 4)));
      }
      catch (Exception ex)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      }
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {SeasonPassXML.Seasons.Count} Season Challenges", LoggerType.Info, (Exception) null);
  }

  public static void Reload()
  {
    SeasonPassXML.Seasons.Clear();
    SynchronizeXML.Load();
  }

  private static void smethod_0([In] int obj0)
  {
    string path = $"Data/Seasons/{obj0}.xml";
    if (File.Exists(path))
      return;
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print("File not found: " + path, LoggerType.Warning, (Exception) null);
  }

  static SynchronizeXML() => SeasonPassXML.Seasons = new List<BattlePassModel>();

  public static void Load()
  {
    string str = "Data/Synchronize.xml";
    if (File.Exists(str))
    {
      TemplatePackXML.smethod_0(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
  }
}
