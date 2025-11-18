using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.JSON;

public class CommandHelperJSON
{
	public static List<CommandHelper> Helpers = new List<CommandHelper>();

	public static void Load()
	{
		string text = "Data/CommandHelper.json";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {Helpers.Count} Command Helpers", LoggerType.Info);
	}

	public static void Reload()
	{
		Helpers.Clear();
		Load();
	}

	public static CommandHelper GetTag(string HelperTag)
	{
		lock (Helpers)
		{
			foreach (CommandHelper helper in Helpers)
			{
				if (helper.Tag == HelperTag)
				{
					return helper;
				}
			}
			return null;
		}
	}

	private static void smethod_0(string string_0)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		using FileStream fileStream = new FileStream(string_0, FileMode.Open, FileAccess.Read);
		if (fileStream.Length == 0L)
		{
			CLogger.Print("File is empty: " + string_0, LoggerType.Warning);
		}
		else
		{
			try
			{
				using StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8);
				JsonElement val = JsonDocument.Parse(streamReader.ReadToEnd(), default(JsonDocumentOptions)).RootElement;
				val = ((JsonElement)(ref val)).GetProperty("Command");
				ArrayEnumerator val2 = ((JsonElement)(ref val)).EnumerateArray();
				ArrayEnumerator enumerator = ((ArrayEnumerator)(ref val2)).GetEnumerator();
				try
				{
					while (((ArrayEnumerator)(ref enumerator)).MoveNext())
					{
						JsonElement current = ((ArrayEnumerator)(ref enumerator)).Current;
						val = ((JsonElement)(ref current)).GetProperty("Tag");
						string @string = ((JsonElement)(ref val)).GetString();
						if (!string.IsNullOrEmpty(@string))
						{
							if (@string.Equals("WeaponsFlag"))
							{
								CommandHelper commandHelper = new CommandHelper(@string);
								val = ((JsonElement)(ref current)).GetProperty("AllWeapons");
								commandHelper.AllWeapons = int.Parse(((JsonElement)(ref val)).GetString());
								val = ((JsonElement)(ref current)).GetProperty("AssaultRifle");
								commandHelper.AssaultRifle = int.Parse(((JsonElement)(ref val)).GetString());
								val = ((JsonElement)(ref current)).GetProperty("SubMachineGun");
								commandHelper.SubMachineGun = int.Parse(((JsonElement)(ref val)).GetString());
								val = ((JsonElement)(ref current)).GetProperty("SniperRifle");
								commandHelper.SniperRifle = int.Parse(((JsonElement)(ref val)).GetString());
								val = ((JsonElement)(ref current)).GetProperty("ShotGun");
								commandHelper.ShotGun = int.Parse(((JsonElement)(ref val)).GetString());
								val = ((JsonElement)(ref current)).GetProperty("MachineGun");
								commandHelper.MachineGun = int.Parse(((JsonElement)(ref val)).GetString());
								val = ((JsonElement)(ref current)).GetProperty("Secondary");
								commandHelper.Secondary = int.Parse(((JsonElement)(ref val)).GetString());
								val = ((JsonElement)(ref current)).GetProperty("Melee");
								commandHelper.Melee = int.Parse(((JsonElement)(ref val)).GetString());
								val = ((JsonElement)(ref current)).GetProperty("Knuckle");
								commandHelper.Knuckle = int.Parse(((JsonElement)(ref val)).GetString());
								val = ((JsonElement)(ref current)).GetProperty("RPG7");
								commandHelper.RPG7 = int.Parse(((JsonElement)(ref val)).GetString());
								CommandHelper item = commandHelper;
								Helpers.Add(item);
							}
							if (@string.Equals("PlayTime"))
							{
								CommandHelper commandHelper2 = new CommandHelper(@string);
								val = ((JsonElement)(ref current)).GetProperty("Minutes05");
								commandHelper2.Minutes05 = int.Parse(((JsonElement)(ref val)).GetString());
								val = ((JsonElement)(ref current)).GetProperty("Minutes10");
								commandHelper2.Minutes10 = int.Parse(((JsonElement)(ref val)).GetString());
								val = ((JsonElement)(ref current)).GetProperty("Minutes15");
								commandHelper2.Minutes15 = int.Parse(((JsonElement)(ref val)).GetString());
								val = ((JsonElement)(ref current)).GetProperty("Minutes20");
								commandHelper2.Minutes20 = int.Parse(((JsonElement)(ref val)).GetString());
								val = ((JsonElement)(ref current)).GetProperty("Minutes25");
								commandHelper2.Minutes25 = int.Parse(((JsonElement)(ref val)).GetString());
								val = ((JsonElement)(ref current)).GetProperty("Minutes30");
								commandHelper2.Minutes30 = int.Parse(((JsonElement)(ref val)).GetString());
								CommandHelper item2 = commandHelper2;
								Helpers.Add(item2);
							}
							continue;
						}
						CLogger.Print("Invalid Command Helper Tag: " + @string, LoggerType.Warning);
						return;
					}
				}
				finally
				{
					((IDisposable)(ArrayEnumerator)(ref enumerator)).Dispose();
				}
				streamReader.Dispose();
				streamReader.Close();
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
