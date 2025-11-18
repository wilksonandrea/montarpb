using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML;

public class TitleSystemXML
{
	private static List<TitleModel> list_0 = new List<TitleModel>();

	public static void Load()
	{
		string text = "Data/Titles/System.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {list_0.Count} Title System", LoggerType.Info);
	}

	public static void Reload()
	{
		list_0.Clear();
		Load();
	}

	public static TitleModel GetTitle(int titleId, bool ReturnNull = true)
	{
		if (titleId == 0)
		{
			if (!ReturnNull)
			{
				return new TitleModel();
			}
			return null;
		}
		foreach (TitleModel item in list_0)
		{
			if (item.Id == titleId)
			{
				return item;
			}
		}
		return null;
	}

	public static void Get2Titles(int titleId1, int titleId2, out TitleModel title1, out TitleModel title2, bool ReturnNull = true)
	{
		if (!ReturnNull)
		{
			title1 = new TitleModel();
			title2 = new TitleModel();
		}
		else
		{
			title1 = null;
			title2 = null;
		}
		if (titleId1 == 0 && titleId2 == 0)
		{
			return;
		}
		foreach (TitleModel item in list_0)
		{
			if (item.Id == titleId1)
			{
				title1 = item;
			}
			else if (item.Id == titleId2)
			{
				title2 = item;
			}
		}
	}

	public static void Get3Titles(int titleId1, int titleId2, int titleId3, out TitleModel title1, out TitleModel title2, out TitleModel title3, bool ReturnNull)
	{
		if (!ReturnNull)
		{
			title1 = new TitleModel();
			title2 = new TitleModel();
			title3 = new TitleModel();
		}
		else
		{
			title1 = null;
			title2 = null;
			title3 = null;
		}
		if (titleId1 == 0 && titleId2 == 0 && titleId3 == 0)
		{
			return;
		}
		foreach (TitleModel item in list_0)
		{
			if (item.Id == titleId1)
			{
				title1 = item;
			}
			else if (item.Id == titleId2)
			{
				title2 = item;
			}
			else if (item.Id == titleId3)
			{
				title3 = item;
			}
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
							if ("Title".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								TitleModel item = new TitleModel(int.Parse(attributes.GetNamedItem("Id").Value))
								{
									ClassId = int.Parse(attributes.GetNamedItem("List").Value),
									Ribbon = int.Parse(attributes.GetNamedItem("Ribbon").Value),
									Ensign = int.Parse(attributes.GetNamedItem("Ensign").Value),
									Medal = int.Parse(attributes.GetNamedItem("Medal").Value),
									MasterMedal = int.Parse(attributes.GetNamedItem("MasterMedal").Value),
									Rank = int.Parse(attributes.GetNamedItem("Rank").Value),
									Slot = int.Parse(attributes.GetNamedItem("Slot").Value),
									Req1 = int.Parse(attributes.GetNamedItem("ReqT1").Value),
									Req2 = int.Parse(attributes.GetNamedItem("ReqT2").Value)
								};
								list_0.Add(item);
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
