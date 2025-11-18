using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Utility;

namespace Plugin.Core.XML;

public class PermissionXML
{
	private static readonly SortedList<int, string> sortedList_0 = new SortedList<int, string>();

	private static readonly SortedList<AccessLevel, List<string>> sortedList_1 = new SortedList<AccessLevel, List<string>>();

	private static readonly SortedList<int, int> sortedList_2 = new SortedList<int, int>();

	public static void Load()
	{
		smethod_0();
		smethod_1();
		smethod_2();
	}

	public static void Reload()
	{
		sortedList_0.Clear();
		sortedList_1.Clear();
		sortedList_2.Clear();
		Load();
	}

	private static void smethod_0()
	{
		string text = "Data/Access/Permission.xml";
		if (File.Exists(text))
		{
			smethod_3(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {sortedList_0.Count} Permissions", LoggerType.Info);
	}

	private static void smethod_1()
	{
		string text = "Data/Access/PermissionLevel.xml";
		if (File.Exists(text))
		{
			smethod_4(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {sortedList_2.Count} Permission Ranks", LoggerType.Info);
	}

	private static void smethod_2()
	{
		string text = "Data/Access/PermissionRight.xml";
		if (File.Exists(text))
		{
			smethod_5(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {sortedList_1.Count} Level Permission", LoggerType.Info);
	}

	public static int GetFakeRank(int Level)
	{
		lock (sortedList_2)
		{
			if (sortedList_2.ContainsKey(Level))
			{
				return sortedList_2[Level];
			}
			return -1;
		}
	}

	public static bool HavePermission(string Permission, AccessLevel Level)
	{
		if (sortedList_1.ContainsKey(Level))
		{
			return sortedList_1[Level].Contains(Permission);
		}
		return false;
	}

	private static void smethod_3(string string_0)
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
							if ("Permission".Equals(xmlNode2.Name))
							{
								XmlAttributeCollection attributes = xmlNode2.Attributes;
								int key = int.Parse(attributes.GetNamedItem("Key").Value);
								string value = attributes.GetNamedItem("Name").Value;
								_ = attributes.GetNamedItem("Description").Value;
								if (!sortedList_0.ContainsKey(key))
								{
									sortedList_0.Add(key, value);
								}
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

	private static void smethod_4(string string_0)
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
							if ("Permission".Equals(xmlNode2.Name))
							{
								XmlAttributeCollection attributes = xmlNode2.Attributes;
								int key = int.Parse(attributes.GetNamedItem("Key").Value);
								_ = attributes.GetNamedItem("Name").Value;
								_ = attributes.GetNamedItem("Description").Value;
								int value = int.Parse(attributes.GetNamedItem("FakeRank").Value);
								sortedList_2.Add(key, value);
								sortedList_1.Add((AccessLevel)key, new List<string>());
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

	private static void smethod_5(string string_0)
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
							if ("Access".Equals(xmlNode2.Name))
							{
								AccessLevel accessLevel_ = ComDiv.ParseEnum<AccessLevel>(xmlNode2.Attributes.GetNamedItem("Level").Value);
								smethod_6(xmlNode2, accessLevel_);
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

	private static void smethod_6(XmlNode xmlNode_0, AccessLevel accessLevel_0)
	{
		for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
		{
			if ("Permission".Equals(xmlNode.Name))
			{
				for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
				{
					if ("Right".Equals(xmlNode2.Name))
					{
						int key = int.Parse(xmlNode2.Attributes.GetNamedItem("LevelKey").Value);
						if (sortedList_0.ContainsKey(key))
						{
							sortedList_1[accessLevel_0].Add(sortedList_0[key]);
						}
					}
				}
			}
		}
	}
}
