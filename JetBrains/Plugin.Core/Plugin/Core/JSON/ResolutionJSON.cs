// Decompiled with JetBrains decompiler
// Type: Plugin.Core.JSON.ResolutionJSON
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

#nullable disable
namespace Plugin.Core.JSON;

public class ResolutionJSON
{
  public static List<string> ARS;

  public static void Reload()
  {
    CommandHelperJSON.Helpers.Clear();
    CommandHelperJSON.Load();
  }

  public static CommandHelper GetTag(string gparam_0)
  {
    lock (CommandHelperJSON.Helpers)
    {
      foreach (CommandHelper helper in CommandHelperJSON.Helpers)
      {
        if (helper.Tag == gparam_0)
          return helper;
      }
      return (CommandHelper) null;
    }
  }

  private static void smethod_0([In] string obj0)
  {
    using (FileStream fileStream = new FileStream(obj0, FileMode.Open, FileAccess.Read))
    {
      if (fileStream.Length == 0L)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print("File is empty: " + obj0, LoggerType.Warning, (Exception) null);
      }
      else
      {
        try
        {
          using (StreamReader streamReader = new StreamReader((Stream) fileStream, Encoding.UTF8))
          {
            JsonElement jsonElement = JsonDocument.Parse(streamReader.ReadToEnd()).RootElement;
            jsonElement = jsonElement.GetProperty("Command");
            using (JsonElement.ArrayEnumerator enumerator = jsonElement.EnumerateArray().GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                JsonElement current = enumerator.Current;
                JsonElement property = current.GetProperty("Tag");
                string str = property.GetString();
                if (!string.IsNullOrEmpty(str))
                {
                  if (str.Equals("WeaponsFlag"))
                  {
                    CompetitiveRank competitiveRank = new CompetitiveRank(str);
                    property = current.GetProperty("AllWeapons");
                    ((CommandHelper) competitiveRank).AllWeapons = int.Parse(property.GetString());
                    property = current.GetProperty("AssaultRifle");
                    ((CommandHelper) competitiveRank).AssaultRifle = int.Parse(property.GetString());
                    property = current.GetProperty("SubMachineGun");
                    ((CommandHelper) competitiveRank).SubMachineGun = int.Parse(property.GetString());
                    property = current.GetProperty("SniperRifle");
                    ((CommandHelper) competitiveRank).SniperRifle = int.Parse(property.GetString());
                    property = current.GetProperty("ShotGun");
                    ((CommandHelper) competitiveRank).ShotGun = int.Parse(property.GetString());
                    property = current.GetProperty("MachineGun");
                    ((CommandHelper) competitiveRank).MachineGun = int.Parse(property.GetString());
                    property = current.GetProperty("Secondary");
                    ((CommandHelper) competitiveRank).Secondary = int.Parse(property.GetString());
                    property = current.GetProperty("Melee");
                    ((CommandHelper) competitiveRank).Melee = int.Parse(property.GetString());
                    property = current.GetProperty("Knuckle");
                    ((CommandHelper) competitiveRank).Knuckle = int.Parse(property.GetString());
                    property = current.GetProperty("RPG7");
                    ((CommandHelper) competitiveRank).RPG7 = int.Parse(property.GetString());
                    CommandHelper commandHelper = (CommandHelper) competitiveRank;
                    CommandHelperJSON.Helpers.Add(commandHelper);
                  }
                  if (str.Equals("PlayTime"))
                  {
                    CompetitiveRank competitiveRank = new CompetitiveRank(str);
                    property = current.GetProperty("Minutes05");
                    ((CommandHelper) competitiveRank).Minutes05 = int.Parse(property.GetString());
                    property = current.GetProperty("Minutes10");
                    ((CommandHelper) competitiveRank).Minutes10 = int.Parse(property.GetString());
                    property = current.GetProperty("Minutes15");
                    ((CommandHelper) competitiveRank).Minutes15 = int.Parse(property.GetString());
                    property = current.GetProperty("Minutes20");
                    ((CommandHelper) competitiveRank).Minutes20 = int.Parse(property.GetString());
                    property = current.GetProperty("Minutes25");
                    competitiveRank.set_Minutes25(int.Parse(property.GetString()));
                    property = current.GetProperty("Minutes30");
                    competitiveRank.set_Minutes30(int.Parse(property.GetString()));
                    CommandHelper commandHelper = (CommandHelper) competitiveRank;
                    CommandHelperJSON.Helpers.Add(commandHelper);
                  }
                }
                else
                {
                  // ISSUE: reference to a compiler-generated method
                  CLogger.Class1.Print("Invalid Command Helper Tag: " + str, LoggerType.Warning, (Exception) null);
                  return;
                }
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

  static ResolutionJSON() => CommandHelperJSON.Helpers = new List<CommandHelper>();

  public static void Load()
  {
    string str = "Data/DisplayRes.json";
    if (File.Exists(str))
    {
      ServerConfigJSON.smethod_0(str);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + str, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {ResolutionJSON.ARS.Count} Allowed DRAR", LoggerType.Info, (Exception) null);
  }
}
