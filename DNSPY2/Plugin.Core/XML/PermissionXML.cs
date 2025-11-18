using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Utility;

namespace Plugin.Core.XML
{
	// Token: 0x02000018 RID: 24
	public class PermissionXML
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00002722 File Offset: 0x00000922
		public static void Load()
		{
			PermissionXML.smethod_0();
			PermissionXML.smethod_1();
			PermissionXML.smethod_2();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00002733 File Offset: 0x00000933
		public static void Reload()
		{
			PermissionXML.sortedList_0.Clear();
			PermissionXML.sortedList_1.Clear();
			PermissionXML.sortedList_2.Clear();
			PermissionXML.Load();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00011CD0 File Offset: 0x0000FED0
		private static void smethod_0()
		{
			string text = "Data/Access/Permission.xml";
			if (File.Exists(text))
			{
				PermissionXML.smethod_3(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Permissions", PermissionXML.sortedList_0.Count), LoggerType.Info, null);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00011D28 File Offset: 0x0000FF28
		private static void smethod_1()
		{
			string text = "Data/Access/PermissionLevel.xml";
			if (File.Exists(text))
			{
				PermissionXML.smethod_4(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Permission Ranks", PermissionXML.sortedList_2.Count), LoggerType.Info, null);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00011D80 File Offset: 0x0000FF80
		private static void smethod_2()
		{
			string text = "Data/Access/PermissionRight.xml";
			if (File.Exists(text))
			{
				PermissionXML.smethod_5(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Level Permission", PermissionXML.sortedList_1.Count), LoggerType.Info, null);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00011DD8 File Offset: 0x0000FFD8
		public static int GetFakeRank(int Level)
		{
			SortedList<int, int> sortedList = PermissionXML.sortedList_2;
			int num;
			lock (sortedList)
			{
				if (PermissionXML.sortedList_2.ContainsKey(Level))
				{
					num = PermissionXML.sortedList_2[Level];
				}
				else
				{
					num = -1;
				}
			}
			return num;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00002758 File Offset: 0x00000958
		public static bool HavePermission(string Permission, AccessLevel Level)
		{
			return PermissionXML.sortedList_1.ContainsKey(Level) && PermissionXML.sortedList_1[Level].Contains(Permission);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00011E30 File Offset: 0x00010030
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
									if ("Permission".Equals(xmlNode2.Name))
									{
										XmlAttributeCollection attributes = xmlNode2.Attributes;
										int num = int.Parse(attributes.GetNamedItem("Key").Value);
										string value = attributes.GetNamedItem("Name").Value;
										string value2 = attributes.GetNamedItem("Description").Value;
										if (!PermissionXML.sortedList_0.ContainsKey(num))
										{
											PermissionXML.sortedList_0.Add(num, value);
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
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00011F6C File Offset: 0x0001016C
		private static void smethod_4(string string_0)
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
									if ("Permission".Equals(xmlNode2.Name))
									{
										XmlAttributeCollection attributes = xmlNode2.Attributes;
										int num = int.Parse(attributes.GetNamedItem("Key").Value);
										string value = attributes.GetNamedItem("Name").Value;
										string value2 = attributes.GetNamedItem("Description").Value;
										int num2 = int.Parse(attributes.GetNamedItem("FakeRank").Value);
										PermissionXML.sortedList_2.Add(num, num2);
										PermissionXML.sortedList_1.Add((AccessLevel)num, new List<string>());
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

		// Token: 0x06000100 RID: 256 RVA: 0x000120DC File Offset: 0x000102DC
		private static void smethod_5(string string_0)
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
									if ("Access".Equals(xmlNode2.Name))
									{
										AccessLevel accessLevel = ComDiv.ParseEnum<AccessLevel>(xmlNode2.Attributes.GetNamedItem("Level").Value);
										PermissionXML.smethod_6(xmlNode2, accessLevel);
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

		// Token: 0x06000101 RID: 257 RVA: 0x000121D8 File Offset: 0x000103D8
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
							int num = int.Parse(xmlNode2.Attributes.GetNamedItem("LevelKey").Value);
							if (PermissionXML.sortedList_0.ContainsKey(num))
							{
								PermissionXML.sortedList_1[accessLevel_0].Add(PermissionXML.sortedList_0[num]);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00002116 File Offset: 0x00000316
		public PermissionXML()
		{
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000277A File Offset: 0x0000097A
		// Note: this type is marked as 'beforefieldinit'.
		static PermissionXML()
		{
		}

		// Token: 0x04000060 RID: 96
		private static readonly SortedList<int, string> sortedList_0 = new SortedList<int, string>();

		// Token: 0x04000061 RID: 97
		private static readonly SortedList<AccessLevel, List<string>> sortedList_1 = new SortedList<AccessLevel, List<string>>();

		// Token: 0x04000062 RID: 98
		private static readonly SortedList<int, int> sortedList_2 = new SortedList<int, int>();
	}
}
