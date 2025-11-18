using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML;

public static class InternetCafeXML
{
	public static readonly List<InternetCafe> Cafes = new List<InternetCafe>();

	public static void Load()
	{
		string text = "Data/InternetCafe.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {Cafes.Count} Internet Cafe Bonuses", LoggerType.Info);
	}

	public static void Reload()
	{
		Cafes.Clear();
		Load();
	}

	public static InternetCafe GetICafe(int ConfigId)
	{
		lock (Cafes)
		{
			foreach (InternetCafe cafe in Cafes)
			{
				if (cafe.ConfigId == ConfigId)
				{
					return cafe;
				}
			}
			return null;
		}
	}

	public static bool IsValidAddress(string PlayerAddress, string RegisteredAddress)
	{
		return PlayerAddress.Equals(RegisteredAddress);
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
							if ("Bonus".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								InternetCafe item = new InternetCafe(int.Parse(attributes.GetNamedItem("Id").Value))
								{
									BasicExp = int.Parse(attributes.GetNamedItem("BasicExp").Value),
									BasicGold = int.Parse(attributes.GetNamedItem("BasicGold").Value),
									PremiumExp = int.Parse(attributes.GetNamedItem("PremiumExp").Value),
									PremiumGold = int.Parse(attributes.GetNamedItem("PremiumGold").Value)
								};
								Cafes.Add(item);
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
