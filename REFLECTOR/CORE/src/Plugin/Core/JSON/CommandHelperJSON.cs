namespace Plugin.Core.JSON
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.Json;

    public class CommandHelperJSON
    {
        public static List<CommandHelper> Helpers = new List<CommandHelper>();

        public static CommandHelper GetTag(string HelperTag)
        {
            List<CommandHelper> helpers = Helpers;
            lock (helpers)
            {
                using (List<CommandHelper>.Enumerator enumerator = Helpers.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        CommandHelper current = enumerator.Current;
                        if (current.Tag == HelperTag)
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
            string path = "Data/CommandHelper.json";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {Helpers.Count} Command Helpers", LoggerType.Info, null);
        }

        public static void Reload()
        {
            Helpers.Clear();
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
                            JsonElement property = JsonDocument.Parse(reader.ReadToEnd(), options).RootElement.GetProperty("Command");
                            using (JsonElement.ArrayEnumerator enumerator = property.EnumerateArray().GetEnumerator())
                            {
                                while (true)
                                {
                                    if (enumerator.MoveNext())
                                    {
                                        JsonElement current = enumerator.Current;
                                        string str = current.GetProperty("Tag").GetString();
                                        if (!string.IsNullOrEmpty(str))
                                        {
                                            if (str.Equals("WeaponsFlag"))
                                            {
                                                CommandHelper helper1 = new CommandHelper(str);
                                                helper1.AllWeapons = int.Parse(current.GetProperty("AllWeapons").GetString());
                                                helper1.AssaultRifle = int.Parse(current.GetProperty("AssaultRifle").GetString());
                                                helper1.SubMachineGun = int.Parse(current.GetProperty("SubMachineGun").GetString());
                                                helper1.SniperRifle = int.Parse(current.GetProperty("SniperRifle").GetString());
                                                helper1.ShotGun = int.Parse(current.GetProperty("ShotGun").GetString());
                                                helper1.MachineGun = int.Parse(current.GetProperty("MachineGun").GetString());
                                                helper1.Secondary = int.Parse(current.GetProperty("Secondary").GetString());
                                                helper1.Melee = int.Parse(current.GetProperty("Melee").GetString());
                                                helper1.Knuckle = int.Parse(current.GetProperty("Knuckle").GetString());
                                                property = current.GetProperty("RPG7");
                                                helper1.RPG7 = int.Parse(property.GetString());
                                                CommandHelper item = helper1;
                                                Helpers.Add(item);
                                            }
                                            if (str.Equals("PlayTime"))
                                            {
                                                CommandHelper helper3 = new CommandHelper(str);
                                                helper3.Minutes05 = int.Parse(current.GetProperty("Minutes05").GetString());
                                                helper3.Minutes10 = int.Parse(current.GetProperty("Minutes10").GetString());
                                                helper3.Minutes15 = int.Parse(current.GetProperty("Minutes15").GetString());
                                                helper3.Minutes20 = int.Parse(current.GetProperty("Minutes20").GetString());
                                                helper3.Minutes25 = int.Parse(current.GetProperty("Minutes25").GetString());
                                                helper3.Minutes30 = int.Parse(current.GetProperty("Minutes30").GetString());
                                                CommandHelper item = helper3;
                                                Helpers.Add(item);
                                            }
                                            continue;
                                        }
                                        CLogger.Print("Invalid Command Helper Tag: " + str, LoggerType.Warning, null);
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

