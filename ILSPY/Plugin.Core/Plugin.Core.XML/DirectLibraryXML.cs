using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;

namespace Plugin.Core.XML;

public class DirectLibraryXML
{
	public static List<string> HashFiles = new List<string>();

	public static void Load()
	{
		string text = "Data/DirectLibrary.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {HashFiles.Count} Lib Hases", LoggerType.Info);
	}

	public static void Reload()
	{
		HashFiles.Clear();
		Load();
	}

	public static bool IsValid(string md5)
	{
		if (string.IsNullOrEmpty(md5))
		{
			return true;
		}
		int num = 0;
		while (true)
		{
			if (num < HashFiles.Count)
			{
				if (HashFiles[num] == md5)
				{
					break;
				}
				num++;
				continue;
			}
			return false;
		}
		return true;
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
							if ("D3D9".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								HashFiles.Add(attributes.GetNamedItem("MD5").Value);
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
