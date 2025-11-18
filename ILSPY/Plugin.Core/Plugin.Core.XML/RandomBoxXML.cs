using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML;

public class RandomBoxXML
{
	public static SortedList<int, RandomBoxModel> RBoxes = new SortedList<int, RandomBoxModel>();

	public static void Load()
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Data\\RBoxes");
		if (!directoryInfo.Exists)
		{
			return;
		}
		FileInfo[] files = directoryInfo.GetFiles();
		foreach (FileInfo fileInfo in files)
		{
			try
			{
				smethod_0(int.Parse(fileInfo.Name.Substring(0, fileInfo.Name.Length - 4)));
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}
		CLogger.Print($"Plugin Loaded: {RBoxes.Count} Random Boxes", LoggerType.Info);
	}

	public static void Reload()
	{
		RBoxes.Clear();
		Load();
	}

	private static void smethod_0(int int_0)
	{
		string text = $"Data/RBoxes/{int_0}.xml";
		if (File.Exists(text))
		{
			smethod_1(text, int_0);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
	}

	private static void smethod_1(string string_0, int int_0)
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
							if ("Item".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								RandomBoxModel randomBoxModel = new RandomBoxModel
								{
									ItemsCount = int.Parse(attributes.GetNamedItem("Count").Value),
									Items = new List<RandomBoxItem>()
								};
								smethod_2(xmlNode2, randomBoxModel);
								randomBoxModel.SetTopPercent();
								RBoxes.Add(int_0, randomBoxModel);
							}
						}
					}
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

	private static void smethod_2(XmlNode xmlNode_0, RandomBoxModel randomBoxModel_0)
	{
		for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
		{
			if ("Rewards".Equals(xmlNode.Name))
			{
				for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
				{
					if ("Good".Equals(xmlNode2.Name))
					{
						XmlNamedNodeMap attributes = xmlNode2.Attributes;
						RandomBoxItem item = new RandomBoxItem
						{
							Index = int.Parse(attributes.GetNamedItem("Index").Value),
							GoodsId = int.Parse(attributes.GetNamedItem("Id").Value),
							Percent = int.Parse(attributes.GetNamedItem("Percent").Value),
							Special = bool.Parse(attributes.GetNamedItem("Special").Value)
						};
						randomBoxModel_0.Items.Add(item);
					}
				}
			}
		}
	}

	public static bool ContainsBox(int Id)
	{
		return RBoxes.ContainsKey(Id);
	}

	public static RandomBoxModel GetBox(int Id)
	{
		try
		{
			return RBoxes[Id];
		}
		catch
		{
			return null;
		}
	}
}
