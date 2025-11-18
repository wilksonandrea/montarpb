using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Models.Map;

namespace Plugin.Core.XML
{
	// Token: 0x02000020 RID: 32
	public static class SystemMapXML
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00013970 File Offset: 0x00011B70
		public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int limit)
		{
			SystemMapXML.Class3<T> @class = new SystemMapXML.Class3<T>();
			@class.int_0 = limit;
			return list.Select(new Func<T, int, Class0<T, int>>(SystemMapXML.Class2<T>.<>9.method_0)).GroupBy(new Func<Class0<T, int>, int>(@class.method_0)).Select(new Func<IGrouping<int, Class0<T, int>>, IEnumerable<T>>(SystemMapXML.Class2<T>.<>9.method_1));
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000289E File Offset: 0x00000A9E
		public static void Load()
		{
			SystemMapXML.smethod_0();
			SystemMapXML.smethod_1();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000028AA File Offset: 0x00000AAA
		public static void Reload()
		{
			SystemMapXML.Rules.Clear();
			SystemMapXML.Matches.Clear();
			SystemMapXML.Load();
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000139E4 File Offset: 0x00011BE4
		private static void smethod_0()
		{
			string text = "Data/Maps/Rules.xml";
			if (File.Exists(text))
			{
				SystemMapXML.smethod_2(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Map Rules", SystemMapXML.Rules.Count), LoggerType.Info, null);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00013A3C File Offset: 0x00011C3C
		private static void smethod_1()
		{
			string text = "Data/Maps/Matches.xml";
			if (File.Exists(text))
			{
				SystemMapXML.smethod_3(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Map Matches", SystemMapXML.Matches.Count), LoggerType.Info, null);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00013A94 File Offset: 0x00011C94
		public static MapRule GetMapRule(int RuleId)
		{
			List<MapRule> rules = SystemMapXML.Rules;
			MapRule mapRule2;
			lock (rules)
			{
				foreach (MapRule mapRule in SystemMapXML.Rules)
				{
					if (mapRule.Id == RuleId)
					{
						return mapRule;
					}
				}
				mapRule2 = null;
			}
			return mapRule2;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00013B20 File Offset: 0x00011D20
		public static MapMatch GetMapLimit(int MapId, int RuleId)
		{
			List<MapRule> rules = SystemMapXML.Rules;
			MapMatch mapMatch2;
			lock (rules)
			{
				foreach (MapMatch mapMatch in SystemMapXML.Matches)
				{
					if (mapMatch.Id == MapId && SystemMapXML.GetMapRule(mapMatch.Mode).Rule == RuleId)
					{
						return mapMatch;
					}
				}
				mapMatch2 = null;
			}
			return mapMatch2;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00013BBC File Offset: 0x00011DBC
		private static void smethod_2(string string_0)
		{
			XmlDocument xmlDocument = new XmlDocument();
			using (FileStream fileStream = new FileStream(string_0, FileMode.Open))
			{
				if (fileStream.Length == 0L)
				{
					CLogger.Print("File is empty: " + string_0, LoggerType.Warning, null);
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
										MapRule mapRule = new MapRule
										{
											Id = int.Parse(attributes.GetNamedItem("Id").Value),
											Rule = (int)byte.Parse(attributes.GetNamedItem("Rule").Value),
											StageOptions = (int)byte.Parse(attributes.GetNamedItem("StageOptions").Value),
											Conditions = (int)byte.Parse(attributes.GetNamedItem("Conditions").Value),
											Name = attributes.GetNamedItem("Name").Value
										};
										SystemMapXML.Rules.Add(mapRule);
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

		// Token: 0x06000140 RID: 320 RVA: 0x00013D60 File Offset: 0x00011F60
		private static void smethod_3(string string_0)
		{
			XmlDocument xmlDocument = new XmlDocument();
			using (FileStream fileStream = new FileStream(string_0, FileMode.Open))
			{
				if (fileStream.Length == 0L)
				{
					CLogger.Print("File is empty: " + string_0, LoggerType.Warning, null);
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
											CLogger.Print(string.Format("Invalid Mode: {0}; Mode Name: {1}; Please check and try again!", num, value), LoggerType.Warning, null);
											return;
										}
										SystemMapXML.smethod_4(xmlNode2, num);
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

		// Token: 0x06000141 RID: 321 RVA: 0x00013E9C File Offset: 0x0001209C
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
							MapMatch mapMatch = new MapMatch(int_0)
							{
								Id = (int)byte.Parse(attributes.GetNamedItem("Id").Value),
								Limit = (int)byte.Parse(attributes.GetNamedItem("Limit").Value),
								Tag = (int)byte.Parse(attributes.GetNamedItem("Tag").Value),
								Name = attributes.GetNamedItem("Name").Value
							};
							SystemMapXML.Matches.Add(mapMatch);
						}
					}
				}
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000028C5 File Offset: 0x00000AC5
		// Note: this type is marked as 'beforefieldinit'.
		static SystemMapXML()
		{
		}

		// Token: 0x0400006C RID: 108
		public static List<MapRule> Rules = new List<MapRule>();

		// Token: 0x0400006D RID: 109
		public static List<MapMatch> Matches = new List<MapMatch>();

		// Token: 0x02000021 RID: 33
		[CompilerGenerated]
		[Serializable]
		private sealed class Class2<T>
		{
			// Token: 0x06000143 RID: 323 RVA: 0x000028DB File Offset: 0x00000ADB
			// Note: this type is marked as 'beforefieldinit'.
			static Class2()
			{
			}

			// Token: 0x06000144 RID: 324 RVA: 0x00002116 File Offset: 0x00000316
			public Class2()
			{
			}

			// Token: 0x06000145 RID: 325 RVA: 0x000028E7 File Offset: 0x00000AE7
			internal Class0<T, int> method_0(T gparam_0, int int_0)
			{
				return new Class0<T, int>(gparam_0, int_0);
			}

			// Token: 0x06000146 RID: 326 RVA: 0x000028F0 File Offset: 0x00000AF0
			internal IEnumerable<T> method_1(IGrouping<int, Class0<T, int>> igrouping_0)
			{
				return igrouping_0.Select(new Func<Class0<T, int>, T>(SystemMapXML.Class2<T>.<>9.method_2));
			}

			// Token: 0x06000147 RID: 327 RVA: 0x00002917 File Offset: 0x00000B17
			internal T method_2(Class0<T, int> class0_0)
			{
				return class0_0.item;
			}

			// Token: 0x0400006E RID: 110
			public static readonly SystemMapXML.Class2<T> <>9 = new SystemMapXML.Class2<T>();

			// Token: 0x0400006F RID: 111
			public static Func<T, int, Class0<T, int>> <>9__2_0;

			// Token: 0x04000070 RID: 112
			public static Func<Class0<T, int>, T> <>9__2_3;

			// Token: 0x04000071 RID: 113
			public static Func<IGrouping<int, Class0<T, int>>, IEnumerable<T>> <>9__2_2;
		}

		// Token: 0x02000022 RID: 34
		[CompilerGenerated]
		private sealed class Class3<T>
		{
			// Token: 0x06000148 RID: 328 RVA: 0x00002116 File Offset: 0x00000316
			public Class3()
			{
			}

			// Token: 0x06000149 RID: 329 RVA: 0x0000291F File Offset: 0x00000B1F
			internal int method_0(Class0<T, int> class0_0)
			{
				return class0_0.inx / this.int_0;
			}

			// Token: 0x04000072 RID: 114
			public int int_0;
		}
	}
}
