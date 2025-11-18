using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Plugin.Core.JSON
{
	public class CommandHelperJSON
	{
		public static List<CommandHelper> Helpers;

		static CommandHelperJSON()
		{
			CommandHelperJSON.Helpers = new List<CommandHelper>();
		}

		public CommandHelperJSON()
		{
		}

		public static CommandHelper GetTag(string HelperTag)
		{
			CommandHelper commandHelper;
			lock (CommandHelperJSON.Helpers)
			{
				foreach (CommandHelper helper in CommandHelperJSON.Helpers)
				{
					if (helper.Tag != HelperTag)
					{
						continue;
					}
					commandHelper = helper;
					return commandHelper;
				}
				commandHelper = null;
			}
			return commandHelper;
		}

		public static void Load()
		{
			string str = "Data/CommandHelper.json";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				CommandHelperJSON.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Command Helpers", CommandHelperJSON.Helpers.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			CommandHelperJSON.Helpers.Clear();
			CommandHelperJSON.Load();
		}

		private static void smethod_0(string string_0)
		{
			using (FileStream fileStream = new FileStream(string_0, FileMode.Open, FileAccess.Read))
			{
				if (fileStream.Length != 0)
				{
					try
					{
						using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
						{
							string end = streamReader.ReadToEnd();
							JsonDocumentOptions jsonDocumentOption = new JsonDocumentOptions();
							JsonElement rootElement = JsonDocument.Parse(end, jsonDocumentOption).get_RootElement();
							rootElement = rootElement.GetProperty("Command");
							foreach (JsonElement jsonElement in rootElement.EnumerateArray())
							{
								rootElement = jsonElement.GetProperty("Tag");
								string str = rootElement.GetString();
								if (string.IsNullOrEmpty(str))
								{
									CLogger.Print(string.Concat("Invalid Command Helper Tag: ", str), LoggerType.Warning, null);
									return;
								}
								else
								{
									if (str.Equals("WeaponsFlag"))
									{
										CommandHelper commandHelper = new CommandHelper(str);
										rootElement = jsonElement.GetProperty("AllWeapons");
										commandHelper.AllWeapons = int.Parse(rootElement.GetString());
										rootElement = jsonElement.GetProperty("AssaultRifle");
										commandHelper.AssaultRifle = int.Parse(rootElement.GetString());
										rootElement = jsonElement.GetProperty("SubMachineGun");
										commandHelper.SubMachineGun = int.Parse(rootElement.GetString());
										rootElement = jsonElement.GetProperty("SniperRifle");
										commandHelper.SniperRifle = int.Parse(rootElement.GetString());
										rootElement = jsonElement.GetProperty("ShotGun");
										commandHelper.ShotGun = int.Parse(rootElement.GetString());
										rootElement = jsonElement.GetProperty("MachineGun");
										commandHelper.MachineGun = int.Parse(rootElement.GetString());
										rootElement = jsonElement.GetProperty("Secondary");
										commandHelper.Secondary = int.Parse(rootElement.GetString());
										rootElement = jsonElement.GetProperty("Melee");
										commandHelper.Melee = int.Parse(rootElement.GetString());
										rootElement = jsonElement.GetProperty("Knuckle");
										commandHelper.Knuckle = int.Parse(rootElement.GetString());
										rootElement = jsonElement.GetProperty("RPG7");
										commandHelper.RPG7 = int.Parse(rootElement.GetString());
										CommandHelperJSON.Helpers.Add(commandHelper);
									}
									if (!str.Equals("PlayTime"))
									{
										continue;
									}
									CommandHelper commandHelper1 = new CommandHelper(str);
									rootElement = jsonElement.GetProperty("Minutes05");
									commandHelper1.Minutes05 = int.Parse(rootElement.GetString());
									rootElement = jsonElement.GetProperty("Minutes10");
									commandHelper1.Minutes10 = int.Parse(rootElement.GetString());
									rootElement = jsonElement.GetProperty("Minutes15");
									commandHelper1.Minutes15 = int.Parse(rootElement.GetString());
									rootElement = jsonElement.GetProperty("Minutes20");
									commandHelper1.Minutes20 = int.Parse(rootElement.GetString());
									rootElement = jsonElement.GetProperty("Minutes25");
									commandHelper1.Minutes25 = int.Parse(rootElement.GetString());
									rootElement = jsonElement.GetProperty("Minutes30");
									commandHelper1.Minutes30 = int.Parse(rootElement.GetString());
									CommandHelperJSON.Helpers.Add(commandHelper1);
								}
							}
							streamReader.Dispose();
							streamReader.Close();
						}
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						CLogger.Print(exception.Message, LoggerType.Error, exception);
					}
				}
				else
				{
					CLogger.Print(string.Concat("File is empty: ", string_0), LoggerType.Warning, null);
				}
				fileStream.Dispose();
				fileStream.Close();
			}
		}
	}
}