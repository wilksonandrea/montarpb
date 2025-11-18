using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Match.Data.Models;

namespace Server.Match.Data.XML;

public class CharaStructureXML
{
	public static List<CharaModel> Charas = new List<CharaModel>();

	public static void Load()
	{
		string text = "Data/Match/CharaHealth.xml";
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
		Charas.Clear();
		Load();
	}

	public static int GetCharaHP(int CharaId)
	{
		foreach (CharaModel chara in Charas)
		{
			if (chara.Id == CharaId)
			{
				return chara.HP;
			}
		}
		return 100;
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
							if ("Chara".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								CharaModel item = new CharaModel
								{
									Id = int.Parse(attributes.GetNamedItem("Id").Value),
									HP = int.Parse(attributes.GetNamedItem("HP").Value)
								};
								Charas.Add(item);
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
