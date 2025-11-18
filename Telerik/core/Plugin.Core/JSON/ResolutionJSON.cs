using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Plugin.Core.JSON
{
	public class ResolutionJSON
	{
		public static List<string> ARS;

		static ResolutionJSON()
		{
			ResolutionJSON.ARS = new List<string>();
		}

		public ResolutionJSON()
		{
		}

		public static string GetDisplay(string R)
		{
			string str;
			lock (ResolutionJSON.ARS)
			{
				foreach (string aR in ResolutionJSON.ARS)
				{
					string[] strArrays = ComDiv.SplitObjects(R, "x");
					if (aR != ComDiv.AspectRatio(int.Parse(strArrays[0]), int.Parse(strArrays[1])))
					{
						continue;
					}
					str = aR;
					return str;
				}
				str = "Invalid";
			}
			return str;
		}

		public static void Load()
		{
			string str = "Data/DisplayRes.json";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				ResolutionJSON.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Allowed DRAR", ResolutionJSON.ARS.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			ResolutionJSON.ARS.Clear();
			ResolutionJSON.Load();
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
							rootElement = rootElement.GetProperty("Resolution");
							foreach (JsonElement jsonElement in rootElement.EnumerateArray())
							{
								rootElement = jsonElement.GetProperty("AspectRatio");
								string str = rootElement.GetString();
								ResolutionJSON.ARS.Add(str);
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