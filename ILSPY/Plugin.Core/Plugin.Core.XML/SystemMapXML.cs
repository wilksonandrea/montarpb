using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Models.Map;

namespace Plugin.Core.XML;

public static class SystemMapXML
{
	[Serializable]
	[CompilerGenerated]
	private sealed class Class2<T>
	{
		public static readonly Class2<T> _003C_003E9 = new Class2<T>();

		public static Func<T, int, Class0<T, int>> _003C_003E9__2_0;

		public static Func<Class0<T, int>, T> _003C_003E9__2_3;

		public static Func<IGrouping<int, Class0<T, int>>, IEnumerable<T>> _003C_003E9__2_2;

		internal Class0<T, int> method_0(T gparam_0, int int_0)
		{
			return new Class0<T, int>(gparam_0, int_0);
		}

		internal IEnumerable<T> method_1(IGrouping<int, Class0<T, int>> igrouping_0)
		{
			return igrouping_0.Select((Class0<T, int> class0_0) => class0_0.item);
		}

		internal T method_2(Class0<T, int> class0_0)
		{
			return class0_0.item;
		}
	}

	[CompilerGenerated]
	private sealed class Class3<T>
	{
		public int int_0;

		internal int method_0(Class0<T, int> class0_0)
		{
			return class0_0.inx / int_0;
		}
	}

	public static List<MapRule> Rules = new List<MapRule>();

	public static List<MapMatch> Matches = new List<MapMatch>();

	public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int limit)
	{
		return from class0_0 in list.Select((T gparam_0, int int_0) => new Class0<T, int>(gparam_0, int_0))
			group class0_0 by class0_0.inx / limit into igrouping_0
			select from class0_0 in igrouping_0
				select class0_0.item;
	}

	public static void Load()
	{
		smethod_0();
		smethod_1();
	}

	public static void Reload()
	{
		Rules.Clear();
		Matches.Clear();
		Load();
	}

	private static void smethod_0()
	{
		string text = "Data/Maps/Rules.xml";
		if (File.Exists(text))
		{
			smethod_2(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {Rules.Count} Map Rules", LoggerType.Info);
	}

	private static void smethod_1()
	{
		string text = "Data/Maps/Matches.xml";
		if (File.Exists(text))
		{
			smethod_3(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {Matches.Count} Map Matches", LoggerType.Info);
	}

	public static MapRule GetMapRule(int RuleId)
	{
		lock (Rules)
		{
			foreach (MapRule rule in Rules)
			{
				if (rule.Id == RuleId)
				{
					return rule;
				}
			}
			return null;
		}
	}

	public static MapMatch GetMapLimit(int MapId, int RuleId)
	{
		lock (Rules)
		{
			foreach (MapMatch match in Matches)
			{
				if (match.Id == MapId && GetMapRule(match.Mode).Rule == RuleId)
				{
					return match;
				}
			}
			return null;
		}
	}

	private static void smethod_2(string string_0)
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
							if ("Mode".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								MapRule item = new MapRule
								{
									Id = int.Parse(attributes.GetNamedItem("Id").Value),
									Rule = byte.Parse(attributes.GetNamedItem("Rule").Value),
									StageOptions = byte.Parse(attributes.GetNamedItem("StageOptions").Value),
									Conditions = byte.Parse(attributes.GetNamedItem("Conditions").Value),
									Name = attributes.GetNamedItem("Name").Value
								};
								Rules.Add(item);
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
							if ("Match".Equals(xmlNode2.Name))
							{
								XmlAttributeCollection attributes = xmlNode2.Attributes;
								int num = int.Parse(attributes.GetNamedItem("Rule").Value);
								string value = attributes.GetNamedItem("Mode").Value;
								if (num == 0 || string.IsNullOrEmpty(value))
								{
									CLogger.Print($"Invalid Mode: {num}; Mode Name: {value}; Please check and try again!", LoggerType.Warning);
									return;
								}
								smethod_4(xmlNode2, num);
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

	private static void smethod_4(XmlNode xmlNode_0, int int_0)
	{
		for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
		{
			if ("Count".Equals(xmlNode.Name))
			{
				for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
				{
					if ("Map".Equals(xmlNode2.Name))
					{
						XmlNamedNodeMap attributes = xmlNode2.Attributes;
						MapMatch item = new MapMatch(int_0)
						{
							Id = byte.Parse(attributes.GetNamedItem("Id").Value),
							Limit = byte.Parse(attributes.GetNamedItem("Limit").Value),
							Tag = byte.Parse(attributes.GetNamedItem("Tag").Value),
							Name = attributes.GetNamedItem("Name").Value
						};
						Matches.Add(item);
					}
				}
			}
		}
	}
}
