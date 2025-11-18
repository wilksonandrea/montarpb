using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Match.Data.Models;

namespace Server.Match.Data.XML;

public class ItemStatisticXML
{
	public static List<ItemsStatistic> Stats = new List<ItemsStatistic>();

	public static void Load()
	{
		string text = "Data/Match/ItemStatistics.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
	}

	public static void Reload()
	{
		Stats.Clear();
		Load();
	}

	public static ItemsStatistic GetItemStats(int ItemId)
	{
		lock (Stats)
		{
			foreach (ItemsStatistic stat in Stats)
			{
				if (stat.Id == ItemId)
				{
					return stat;
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
							if ("Statistic".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								ItemsStatistic item = new ItemsStatistic
								{
									Id = int.Parse(attributes.GetNamedItem("Id").Value),
									Name = attributes.GetNamedItem("Name").Value,
									BulletLoaded = int.Parse(attributes.GetNamedItem("LoadedBullet").Value),
									BulletTotal = int.Parse(attributes.GetNamedItem("TotalBullet").Value),
									Damage = int.Parse(attributes.GetNamedItem("Damage").Value),
									FireDelay = float.Parse(attributes.GetNamedItem("FireDelay").Value),
									HelmetPenetrate = int.Parse(attributes.GetNamedItem("HelmetPenetrate").Value),
									Range = float.Parse(attributes.GetNamedItem("Range").Value)
								};
								Stats.Add(item);
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
