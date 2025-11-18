using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.JSON
{
	// Token: 0x020000A7 RID: 167
	public class CommandHelperJSON
	{
		// Token: 0x060007F4 RID: 2036 RVA: 0x0001F1CC File Offset: 0x0001D3CC
		public static void Load()
		{
			string text = "Data/CommandHelper.json";
			if (File.Exists(text))
			{
				CommandHelperJSON.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Command Helpers", CommandHelperJSON.Helpers.Count), LoggerType.Info, null);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0000666D File Offset: 0x0000486D
		public static void Reload()
		{
			CommandHelperJSON.Helpers.Clear();
			CommandHelperJSON.Load();
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001F224 File Offset: 0x0001D424
		public static CommandHelper GetTag(string HelperTag)
		{
			List<CommandHelper> helpers = CommandHelperJSON.Helpers;
			CommandHelper commandHelper2;
			lock (helpers)
			{
				foreach (CommandHelper commandHelper in CommandHelperJSON.Helpers)
				{
					if (commandHelper.Tag == HelperTag)
					{
						return commandHelper;
					}
				}
				commandHelper2 = null;
			}
			return commandHelper2;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001F2B0 File Offset: 0x0001D4B0
		private static void smethod_0(string string_0)
		{
			using (FileStream fileStream = new FileStream(string_0, FileMode.Open, FileAccess.Read))
			{
				if (fileStream.Length == 0L)
				{
					CLogger.Print("File is empty: " + string_0, LoggerType.Warning, null);
				}
				else
				{
					try
					{
						using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
						{
							foreach (JsonElement jsonElement in JsonDocument.Parse(streamReader.ReadToEnd(), default(JsonDocumentOptions)).RootElement.GetProperty("Command").EnumerateArray())
							{
								string @string = jsonElement.GetProperty("Tag").GetString();
								if (string.IsNullOrEmpty(@string))
								{
									CLogger.Print("Invalid Command Helper Tag: " + @string, LoggerType.Warning, null);
									return;
								}
								if (@string.Equals("WeaponsFlag"))
								{
									CommandHelper commandHelper = new CommandHelper(@string)
									{
										AllWeapons = int.Parse(jsonElement.GetProperty("AllWeapons").GetString()),
										AssaultRifle = int.Parse(jsonElement.GetProperty("AssaultRifle").GetString()),
										SubMachineGun = int.Parse(jsonElement.GetProperty("SubMachineGun").GetString()),
										SniperRifle = int.Parse(jsonElement.GetProperty("SniperRifle").GetString()),
										ShotGun = int.Parse(jsonElement.GetProperty("ShotGun").GetString()),
										MachineGun = int.Parse(jsonElement.GetProperty("MachineGun").GetString()),
										Secondary = int.Parse(jsonElement.GetProperty("Secondary").GetString()),
										Melee = int.Parse(jsonElement.GetProperty("Melee").GetString()),
										Knuckle = int.Parse(jsonElement.GetProperty("Knuckle").GetString()),
										RPG7 = int.Parse(jsonElement.GetProperty("RPG7").GetString())
									};
									CommandHelperJSON.Helpers.Add(commandHelper);
								}
								if (@string.Equals("PlayTime"))
								{
									CommandHelper commandHelper2 = new CommandHelper(@string)
									{
										Minutes05 = int.Parse(jsonElement.GetProperty("Minutes05").GetString()),
										Minutes10 = int.Parse(jsonElement.GetProperty("Minutes10").GetString()),
										Minutes15 = int.Parse(jsonElement.GetProperty("Minutes15").GetString()),
										Minutes20 = int.Parse(jsonElement.GetProperty("Minutes20").GetString()),
										Minutes25 = int.Parse(jsonElement.GetProperty("Minutes25").GetString()),
										Minutes30 = int.Parse(jsonElement.GetProperty("Minutes30").GetString())
									};
									CommandHelperJSON.Helpers.Add(commandHelper2);
								}
							}
							streamReader.Dispose();
							streamReader.Close();
						}
					}
					catch (Exception ex)
					{
						CLogger.Print(ex.Message, LoggerType.Error, ex);
					}
				}
				fileStream.Dispose();
				fileStream.Close();
			}
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00002116 File Offset: 0x00000316
		public CommandHelperJSON()
		{
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0000667E File Offset: 0x0000487E
		// Note: this type is marked as 'beforefieldinit'.
		static CommandHelperJSON()
		{
		}

		// Token: 0x04000393 RID: 915
		public static List<CommandHelper> Helpers = new List<CommandHelper>();
	}
}
