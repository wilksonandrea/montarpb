using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML;

public class MissionConfigXML
{
	public static uint MissionPage1;

	public static uint MissionPage2;

	private static List<MissionStore> list_0 = new List<MissionStore>();

	public static void Load()
	{
		string text = "Data/MissionConfig.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {list_0.Count} Mission Stores", LoggerType.Info);
	}

	public static void Reload()
	{
		MissionPage1 = 0u;
		MissionPage2 = 0u;
		list_0.Clear();
		Load();
	}

	public static MissionStore GetMission(int MissionId)
	{
		lock (list_0)
		{
			foreach (MissionStore item in list_0)
			{
				if (item.Id == MissionId)
				{
					return item;
				}
			}
			return null;
		}
	}

	private static void smethod_0(string string_0)
	{
		XmlDocument xmlDocument = new XmlDocument();
		using FileStream fileStream = new FileStream(string_0, FileMode.Open);
		if (fileStream.Length == 0L)
		{
			CLogger.Print("File is empty: " + string_0, LoggerType.Warning);
		}
		else
		{
			try
			{
				xmlDocument.Load(fileStream);
				for (XmlNode xmlNode = xmlDocument.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					if ("List".Equals(xmlNode.Name))
					{
						for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
						{
							if ("Mission".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								MissionStore missionStore = new MissionStore
								{
									Id = int.Parse(attributes.GetNamedItem("Id").Value),
									ItemId = int.Parse(attributes.GetNamedItem("ItemId").Value),
									Enable = bool.Parse(attributes.GetNamedItem("Enable").Value)
								};
								uint num = (uint)(1 << missionStore.Id);
								int num2 = (int)Math.Ceiling((double)missionStore.Id / 32.0);
								if (missionStore.Enable)
								{
									switch (num2)
									{
									case 1:
										MissionPage1 += num;
										break;
									case 2:
										MissionPage2 += num;
										break;
									}
								}
								list_0.Add(missionStore);
							}
						}
					}
				}
			}
			catch (XmlException ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}
		fileStream.Dispose();
		fileStream.Close();
	}
}
