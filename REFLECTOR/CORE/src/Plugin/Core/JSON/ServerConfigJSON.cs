namespace Plugin.Core.JSON
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.Json;

    public class ServerConfigJSON
    {
        public static List<ServerConfig> Configs = new List<ServerConfig>();

        public static ServerConfig GetConfig(int ConfigId)
        {
            List<ServerConfig> configs = Configs;
            lock (configs)
            {
                using (List<ServerConfig>.Enumerator enumerator = Configs.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        ServerConfig current = enumerator.Current;
                        if (current.ConfigId == ConfigId)
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public static void Load()
        {
            string path = "Data/ServerConfig.json";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {Configs.Count} Server Configs", LoggerType.Info, null);
        }

        public static void Reload()
        {
            Configs.Clear();
            Load();
        }

        private static void smethod_0(string string_0)
        {
            using (FileStream stream = new FileStream(string_0, FileMode.Open, FileAccess.Read))
            {
                if (stream.Length != 0)
                {
                    try
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            JsonDocumentOptions options = new JsonDocumentOptions();
                            JsonElement property = JsonDocument.Parse(reader.ReadToEnd(), options).RootElement.GetProperty("Server");
                            using (JsonElement.ArrayEnumerator enumerator = property.EnumerateArray().GetEnumerator())
                            {
                                while (true)
                                {
                                    if (enumerator.MoveNext())
                                    {
                                        JsonElement current = enumerator.Current;
                                        int num = int.Parse(current.GetProperty("ConfigId").GetString());
                                        if (num != 0)
                                        {
                                            ServerConfig config1 = new ServerConfig();
                                            config1.ConfigId = num;
                                            config1.OnlyGM = bool.Parse(current.GetProperty("ChannelOnlyGM").GetString());
                                            config1.Missions = bool.Parse(current.GetProperty("EnableMissions").GetString());
                                            config1.AccessUFL = bool.Parse(current.GetProperty("AccessUFL").GetString());
                                            config1.UserFileList = current.GetProperty("UserFileList").GetString();
                                            config1.ClientVersion = current.GetProperty("ClientVersion").GetString();
                                            config1.GiftSystem = bool.Parse(current.GetProperty("EnableGiftSystem").GetString());
                                            config1.EnableClan = bool.Parse(current.GetProperty("EnableClan").GetString());
                                            config1.EnableTicket = bool.Parse(current.GetProperty("EnableTicket").GetString());
                                            config1.EnablePlaytime = bool.Parse(current.GetProperty("EnablePlaytime").GetString());
                                            config1.EnableTags = bool.Parse(current.GetProperty("EnableTags").GetString());
                                            config1.EnableBlood = bool.Parse(current.GetProperty("EnableBlood").GetString());
                                            config1.ExitURL = current.GetProperty("ExitURL").GetString();
                                            config1.ShopURL = current.GetProperty("ShopURL").GetString();
                                            config1.OfficialSteam = current.GetProperty("OfficialSteam").GetString();
                                            config1.OfficialBanner = current.GetProperty("OfficialBanner").GetString();
                                            config1.OfficialBannerEnabled = bool.Parse(current.GetProperty("OfficialBannerEnabled").GetString());
                                            config1.OfficialAddress = current.GetProperty("OfficialAddress").GetString();
                                            config1.ChatAnnounceColor = int.Parse(current.GetProperty("ChatAnnoucementColor").GetString());
                                            config1.ChannelAnnounceColor = int.Parse(current.GetProperty("ChannelAnnoucementColor").GetString());
                                            config1.ChatAnnounce = current.GetProperty("ChatAnnountcement").GetString();
                                            config1.ChannelAnnounce = current.GetProperty("ChannelAnnouncement").GetString();
                                            config1.Showroom = ComDiv.ParseEnum<ShowroomView>(current.GetProperty("Showroom").GetString());
                                            ServerConfig item = config1;
                                            Configs.Add(item);
                                            continue;
                                        }
                                        CLogger.Print($"Invalid Config Id: {num}", LoggerType.Warning, null);
                                    }
                                    else
                                    {
                                        goto TR_000B;
                                    }
                                    break;
                                }
                            }
                            return;
                        TR_000B:
                            reader.Dispose();
                            reader.Close();
                        }
                    }
                    catch (Exception exception)
                    {
                        CLogger.Print(exception.Message, LoggerType.Error, exception);
                    }
                }
                else
                {
                    CLogger.Print("File is empty: " + string_0, LoggerType.Warning, null);
                }
                stream.Dispose();
                stream.Close();
            }
        }
    }
}

