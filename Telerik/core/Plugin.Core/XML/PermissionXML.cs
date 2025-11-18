using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.XML
{
	public class PermissionXML
	{
		private readonly static SortedList<int, string> sortedList_0;

		private readonly static SortedList<AccessLevel, List<string>> sortedList_1;

		private readonly static SortedList<int, int> sortedList_2;

		static PermissionXML()
		{
			PermissionXML.sortedList_0 = new SortedList<int, string>();
			PermissionXML.sortedList_1 = new SortedList<AccessLevel, List<string>>();
			PermissionXML.sortedList_2 = new SortedList<int, int>();
		}

		public PermissionXML()
		{
		}

		public static int GetFakeRank(int Level)
		{
			int ınt32;
			lock (PermissionXML.sortedList_2)
			{
				ınt32 = (!PermissionXML.sortedList_2.ContainsKey(Level) ? -1 : PermissionXML.sortedList_2[Level]);
			}
			return ınt32;
		}

		public static bool HavePermission(string Permission, AccessLevel Level)
		{
			if (!PermissionXML.sortedList_1.ContainsKey(Level))
			{
				return false;
			}
			return PermissionXML.sortedList_1[Level].Contains(Permission);
		}

		public static void Load()
		{
			PermissionXML.smethod_0();
			PermissionXML.smethod_1();
			PermissionXML.smethod_2();
		}

		public static void Reload()
		{
			PermissionXML.sortedList_0.Clear();
			PermissionXML.sortedList_1.Clear();
			PermissionXML.sortedList_2.Clear();
			PermissionXML.Load();
		}

		private static void smethod_0()
		{
			string str = "Data/Access/Permission.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				PermissionXML.smethod_3(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Permissions", PermissionXML.sortedList_0.Count), LoggerType.Info, null);
		}

		private static void smethod_1()
		{
			string str = "Data/Access/PermissionLevel.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				PermissionXML.smethod_4(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Permission Ranks", PermissionXML.sortedList_2.Count), LoggerType.Info, null);
		}

		private static void smethod_2()
		{
			string str = "Data/Access/PermissionRight.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				PermissionXML.smethod_5(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Level Permission", PermissionXML.sortedList_1.Count), LoggerType.Info, null);
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
									if ("Permission".Equals(j.Name))
									{
										XmlAttributeCollection attributes = j.Attributes;
										int ınt32 = int.Parse(attributes.GetNamedItem("Key").Value);
										string value = attributes.GetNamedItem("Name").Value;
										string str = attributes.GetNamedItem("Description").Value;
										if (!PermissionXML.sortedList_0.ContainsKey(ınt32))
										{
											PermissionXML.sortedList_0.Add(ınt32, value);
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

		private static void smethod_4(string string_0)
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
									if ("Permission".Equals(j.Name))
									{
										XmlAttributeCollection attributes = j.Attributes;
										int ınt32 = int.Parse(attributes.GetNamedItem("Key").Value);
										string value = attributes.GetNamedItem("Name").Value;
										string str = attributes.GetNamedItem("Description").Value;
										int ınt321 = int.Parse(attributes.GetNamedItem("FakeRank").Value);
										PermissionXML.sortedList_2.Add(ınt32, ınt321);
										PermissionXML.sortedList_1.Add((AccessLevel)ınt32, new List<string>());
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

		private static void smethod_5(string string_0)
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
									if ("Access".Equals(j.Name))
									{
										AccessLevel accessLevel = ComDiv.ParseEnum<AccessLevel>(j.Attributes.GetNamedItem("Level").Value);
										PermissionXML.smethod_6(j, accessLevel);
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

		private static void smethod_6(XmlNode xmlNode_0, AccessLevel accessLevel_0)
		{
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("Permission".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Right".Equals(j.Name))
						{
							int ınt32 = int.Parse(j.Attributes.GetNamedItem("LevelKey").Value);
							if (PermissionXML.sortedList_0.ContainsKey(ınt32))
							{
								PermissionXML.sortedList_1[accessLevel_0].Add(PermissionXML.sortedList_0[ınt32]);
							}
						}
					}
				}
			}
		}
	}
}