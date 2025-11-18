// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.ColorAlternator
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.JSON;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;

#nullable disable
namespace Plugin.Core.Colorful;

public abstract class ColorAlternator : IPrototypable<ColorAlternator>
{
  protected int nextColorIndex;

  public static void Reload()
  {
    ServerConfigJSON.Configs.Clear();
    ServerConfigJSON.Load();
  }

  public static ServerConfig GetConfig(int class0_0)
  {
    lock (ServerConfigJSON.Configs)
    {
      foreach (ServerConfig config in ServerConfigJSON.Configs)
      {
        if (config.ConfigId == class0_0)
          return config;
      }
      return (ServerConfig) null;
    }
  }

  private static void smethod_0(string HelperTag)
  {
    using (FileStream fileStream = new FileStream(HelperTag, FileMode.Open, FileAccess.Read))
    {
      if (fileStream.Length == 0L)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print("File is empty: " + HelperTag, LoggerType.Warning, (Exception) null);
      }
      else
      {
        try
        {
          using (StreamReader streamReader = new StreamReader((Stream) fileStream, Encoding.UTF8))
          {
            JsonElement jsonElement = JsonDocument.Parse(streamReader.ReadToEnd()).RootElement;
            jsonElement = jsonElement.GetProperty("Server");
            using (JsonElement.ArrayEnumerator enumerator = jsonElement.EnumerateArray().GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                JsonElement current = enumerator.Current;
                JsonElement property = current.GetProperty("ConfigId");
                int num = int.Parse(property.GetString());
                if (num != 0)
                {
                  ClanTeam clanTeam = new ClanTeam();
                  ((ServerConfig) clanTeam).ConfigId = num;
                  property = current.GetProperty("ChannelOnlyGM");
                  ((ServerConfig) clanTeam).OnlyGM = bool.Parse(property.GetString());
                  property = current.GetProperty("EnableMissions");
                  ((ServerConfig) clanTeam).Missions = bool.Parse(property.GetString());
                  property = current.GetProperty("AccessUFL");
                  ((ServerConfig) clanTeam).AccessUFL = bool.Parse(property.GetString());
                  property = current.GetProperty("UserFileList");
                  ((ServerConfig) clanTeam).UserFileList = property.GetString();
                  property = current.GetProperty("ClientVersion");
                  ((ServerConfig) clanTeam).ClientVersion = property.GetString();
                  property = current.GetProperty("EnableGiftSystem");
                  ((ServerConfig) clanTeam).GiftSystem = bool.Parse(property.GetString());
                  property = current.GetProperty("EnableClan");
                  ((ServerConfig) clanTeam).EnableClan = bool.Parse(property.GetString());
                  property = current.GetProperty("EnableTicket");
                  ((ServerConfig) clanTeam).EnableTicket = bool.Parse(property.GetString());
                  property = current.GetProperty("EnablePlaytime");
                  ((ServerConfig) clanTeam).EnablePlaytime = bool.Parse(property.GetString());
                  property = current.GetProperty("EnableTags");
                  ((ServerConfig) clanTeam).EnableTags = bool.Parse(property.GetString());
                  property = current.GetProperty("EnableBlood");
                  ((ServerConfig) clanTeam).EnableBlood = bool.Parse(property.GetString());
                  property = current.GetProperty("ExitURL");
                  ((ServerConfig) clanTeam).ExitURL = property.GetString();
                  property = current.GetProperty("ShopURL");
                  ((ServerConfig) clanTeam).ShopURL = property.GetString();
                  property = current.GetProperty("OfficialSteam");
                  ((ServerConfig) clanTeam).OfficialSteam = property.GetString();
                  property = current.GetProperty("OfficialBanner");
                  ((ServerConfig) clanTeam).OfficialBanner = property.GetString();
                  property = current.GetProperty("OfficialBannerEnabled");
                  ((ServerConfig) clanTeam).OfficialBannerEnabled = bool.Parse(property.GetString());
                  property = current.GetProperty("OfficialAddress");
                  ((ServerConfig) clanTeam).OfficialAddress = property.GetString();
                  property = current.GetProperty("ChatAnnoucementColor");
                  ((ServerConfig) clanTeam).ChatAnnounceColor = int.Parse(property.GetString());
                  property = current.GetProperty("ChannelAnnoucementColor");
                  ((ServerConfig) clanTeam).ChannelAnnounceColor = int.Parse(property.GetString());
                  property = current.GetProperty("ChatAnnountcement");
                  clanTeam.set_ChatAnnounce(property.GetString());
                  property = current.GetProperty("ChannelAnnouncement");
                  ((ServerConfig) clanTeam).ChannelAnnounce = property.GetString();
                  property = current.GetProperty("Showroom");
                  clanTeam.set_Showroom(ComDiv.ParseEnum<ShowroomView>(property.GetString()));
                  ServerConfig serverConfig = (ServerConfig) clanTeam;
                  ServerConfigJSON.Configs.Add(serverConfig);
                }
                else
                {
                  // ISSUE: reference to a compiler-generated method
                  CLogger.Class1.Print($"Invalid Config Id: {num}", LoggerType.Warning, (Exception) null);
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

  public ColorAlternator()
  {
  }

  static ColorAlternator() => ServerConfigJSON.Configs = new List<ServerConfig>();

  public Color[] Colors { get; set; }

  public ColorAlternator() => this.Colors = new Color[0];
}
