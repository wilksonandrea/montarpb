// Decompiled with JetBrains decompiler
// Type: Plugin.Core.JSON.ServerConfigJSON
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Colorful;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

#nullable disable
namespace Plugin.Core.JSON;

public class ServerConfigJSON
{
  public static List<ServerConfig> Configs;

  public static void Reload()
  {
    ResolutionJSON.ARS.Clear();
    ResolutionJSON.Load();
  }

  public static string GetDisplay(string igrouping_0)
  {
    lock (ResolutionJSON.ARS)
    {
      foreach (string display in ResolutionJSON.ARS)
      {
        // ISSUE: reference to a compiler-generated method
        string[] strArray = ComDiv.Class5.SplitObjects(igrouping_0, "x");
        // ISSUE: reference to a compiler-generated method
        if (display == ComDiv.Class5.AspectRatio(int.Parse(strArray[0]), int.Parse(strArray[1])))
          return display;
      }
      return "Invalid";
    }
  }

  private static void smethod_0(string class0_0)
  {
    using (FileStream fileStream = new FileStream(class0_0, FileMode.Open, FileAccess.Read))
    {
      if (fileStream.Length == 0L)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print("File is empty: " + class0_0, LoggerType.Warning, (Exception) null);
      }
      else
      {
        try
        {
          using (StreamReader streamReader = new StreamReader((Stream) fileStream, Encoding.UTF8))
          {
            JsonElement jsonElement = JsonDocument.Parse(streamReader.ReadToEnd()).RootElement;
            jsonElement = jsonElement.GetProperty("Resolution");
            using (JsonElement.ArrayEnumerator enumerator = jsonElement.EnumerateArray().GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                string str = enumerator.Current.GetProperty("AspectRatio").GetString();
                ResolutionJSON.ARS.Add(str);
              }
            }
            streamReader.Dispose();
            streamReader.Close();
          }
        }
        catch (Exception ex)
        {
          // ISSUE: reference to a compiler-generated method
          CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
        }
      }
      fileStream.Dispose();
      fileStream.Close();
    }
  }

  static ServerConfigJSON() => ResolutionJSON.ARS = new List<string>();

  public static void Load()
  {
    string str = "Data/ServerConfig.json";
    if (File.Exists(str))
    {
      ColorAlternator.smethod_0(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {ServerConfigJSON.Configs.Count} Server Configs", LoggerType.Info, (Exception) null);
  }
}
