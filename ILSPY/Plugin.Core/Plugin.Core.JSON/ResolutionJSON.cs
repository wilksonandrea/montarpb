using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Plugin.Core.Enums;
using Plugin.Core.Utility;

namespace Plugin.Core.JSON;

public class ResolutionJSON
{
	public static List<string> ARS = new List<string>();

	public static void Load()
	{
		string text = "Data/DisplayRes.json";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {ARS.Count} Allowed DRAR", LoggerType.Info);
	}

	public static void Reload()
	{
		ARS.Clear();
		Load();
	}

	public static string GetDisplay(string R)
	{
		lock (ARS)
		{
			foreach (string aR in ARS)
			{
				string[] array = ComDiv.SplitObjects(R, "x");
				if (aR == ComDiv.AspectRatio(int.Parse(array[0]), int.Parse(array[1])))
				{
					return aR;
				}
			}
			return "Invalid";
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
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
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
				val = ((JsonElement)(ref val)).GetProperty("Resolution");
				ArrayEnumerator val2 = ((JsonElement)(ref val)).EnumerateArray();
				ArrayEnumerator enumerator = ((ArrayEnumerator)(ref val2)).GetEnumerator();
				try
				{
					while (((ArrayEnumerator)(ref enumerator)).MoveNext())
					{
						JsonElement current = ((ArrayEnumerator)(ref enumerator)).Current;
						val = ((JsonElement)(ref current)).GetProperty("AspectRatio");
						string @string = ((JsonElement)(ref val)).GetString();
						ARS.Add(@string);
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
