using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Models.Map;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Plugin.Core.XML
{
	public static class SystemMapXML
	{
		public static List<MapRule> Rules;

		public static List<MapMatch> Matches;

		static SystemMapXML()
		{
			SystemMapXML.Rules = new List<MapRule>();
			SystemMapXML.Matches = new List<MapMatch>();
		}

		public static MapMatch GetMapLimit(int MapId, int RuleId)
		{
			MapMatch mapMatch;
			lock (SystemMapXML.Rules)
			{
				foreach (MapMatch match in SystemMapXML.Matches)
				{
					if (match.Id != MapId || SystemMapXML.GetMapRule(match.Mode).Rule != RuleId)
					{
						continue;
					}
					mapMatch = match;
					return mapMatch;
				}
				mapMatch = null;
			}
			return mapMatch;
		}

		public static MapRule GetMapRule(int RuleId)
		{
			MapRule mapRule;
			lock (SystemMapXML.Rules)
			{
				foreach (MapRule rule in SystemMapXML.Rules)
				{
					if (rule.Id != RuleId)
					{
						continue;
					}
					mapRule = rule;
					return mapRule;
				}
				mapRule = null;
			}
			return mapRule;
		}

		public static void Load()
		{
			SystemMapXML.smethod_0();
			SystemMapXML.smethod_1();
		}

		public static void Reload()
		{
			SystemMapXML.Rules.Clear();
			SystemMapXML.Matches.Clear();
			SystemMapXML.Load();
		}

		private static void smethod_0()
		{
			string str = "Data/Maps/Rules.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				SystemMapXML.smethod_2(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Map Rules", SystemMapXML.Rules.Count), LoggerType.Info, null);
		}

		private static void smethod_1()
		{
			string str = "Data/Maps/Matches.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				SystemMapXML.smethod_3(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Map Matches", SystemMapXML.Matches.Count), LoggerType.Info, null);
		}

		private static void smethod_2(string string_0)
		{
			XmlDocument xmlDocument = new XmlDocument();
			using (FileStream fileStream = new FileStream(string_0, FileMode.Open))
			{
				if (fileStream.Length != 0)
				{
					try
					{
						xmlDocument.Load(fileStream);
						for (XmlNode i = xmlDocument.FirstChild; i != null; i = i.NextSibling)
						{
							if ("List".Equals(i.Name))
							{
								for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
								{
									if ("Mode".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										MapRule mapRule = new MapRule()
										{
											Id = int.Parse(attributes.GetNamedItem("Id").Value),
											Rule = byte.Parse(attributes.GetNamedItem("Rule").Value),
											StageOptions = byte.Parse(attributes.GetNamedItem("StageOptions").Value),
											Conditions = byte.Parse(attributes.GetNamedItem("Conditions").Value),
											Name = attributes.GetNamedItem("Name").Value
										};
										SystemMapXML.Rules.Add(mapRule);
									}
								}
							}
						}
					}
					catch (XmlException xmlException1)
					{
						XmlException xmlException = xmlException1;
						CLogger.Print(xmlException.Message, LoggerType.Error, xmlException);
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

		private static void smethod_3(string string_0)
		{
			XmlDocument xmlDocument = new XmlDocument();
			using (FileStream fileStream = new FileStream(string_0, FileMode.Open))
			{
				if (fileStream.Length != 0)
				{
					try
					{
						xmlDocument.Load(fileStream);
						for (XmlNode i = xmlDocument.FirstChild; i != null; i = i.NextSibling)
						{
							if ("List".Equals(i.Name))
							{
								for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
								{
									if ("Match".Equals(j.Name))
									{
										XmlAttributeCollection attributes = j.Attributes;
										int ınt32 = int.Parse(attributes.GetNamedItem("Rule").Value);
										string value = attributes.GetNamedItem("Mode").Value;
										if (ınt32 == 0 || string.IsNullOrEmpty(value))
										{
											CLogger.Print(string.Format("Invalid Mode: {0}; Mode Name: {1}; Please check and try again!", ınt32, value), LoggerType.Warning, null);
											return;
										}
										else
										{
											SystemMapXML.smethod_4(j, ınt32);
										}
									}
								}
							}
						}
					}
					catch (XmlException xmlException1)
					{
						XmlException xmlException = xmlException1;
						CLogger.Print(xmlException.Message, LoggerType.Error, xmlException);
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

		private static void smethod_4(XmlNode xmlNode_0, int int_0)
		{
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("Count".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Map".Equals(j.Name))
						{
							XmlNamedNodeMap attributes = j.Attributes;
							MapMatch mapMatch = new MapMatch(int_0)
							{
								Id = byte.Parse(attributes.GetNamedItem("Id").Value),
								Limit = byte.Parse(attributes.GetNamedItem("Limit").Value),
								Tag = byte.Parse(attributes.GetNamedItem("Tag").Value),
								Name = attributes.GetNamedItem("Name").Value
							};
							SystemMapXML.Matches.Add(mapMatch);
						}
					}
				}
			}
		}

		public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int limit)
		{
			return list.Select((T gparam_0, int int_0) => new { item = gparam_0, inx = int_0 }).GroupBy((class0_0) => class0_0.inx / limit).Select((igrouping_0) => 
				from  in igrouping_0
				select class0_0.item);
		}
	}
}