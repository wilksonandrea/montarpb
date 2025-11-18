using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML
{
	// Token: 0x02000016 RID: 22
	public class MissionAwardXML
	{
		// Token: 0x060000EB RID: 235 RVA: 0x000117CC File Offset: 0x0000F9CC
		public static void Load()
		{
			string text = "Data/Cards/MissionAwards.xml";
			if (File.Exists(text))
			{
				MissionAwardXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Mission Awards", MissionAwardXML.list_0.Count), LoggerType.Info, null);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000026DC File Offset: 0x000008DC
		public static void Reload()
		{
			MissionAwardXML.list_0.Clear();
			MissionAwardXML.Load();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00011824 File Offset: 0x0000FA24
		public static MissionAwards GetAward(int MissionId)
		{
			List<MissionAwards> list = MissionAwardXML.list_0;
			MissionAwards missionAwards2;
			lock (list)
			{
				foreach (MissionAwards missionAwards in MissionAwardXML.list_0)
				{
					if (missionAwards.Id == MissionId)
					{
						return missionAwards;
					}
				}
				missionAwards2 = null;
			}
			return missionAwards2;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000118B0 File Offset: 0x0000FAB0
		private static void smethod_0(string string_0)
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
									if ("Mission".Equals(xmlNode2.Name))
									{
										XmlAttributeCollection attributes = xmlNode2.Attributes;
										int num = int.Parse(attributes.GetNamedItem("Id").Value);
										int num2 = int.Parse(attributes.GetNamedItem("MasterMedal").Value);
										int num3 = int.Parse(attributes.GetNamedItem("Exp").Value);
										int num4 = int.Parse(attributes.GetNamedItem("Point").Value);
										MissionAwardXML.list_0.Add(new MissionAwards(num, num2, num3, num4));
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

		// Token: 0x060000EF RID: 239 RVA: 0x00002116 File Offset: 0x00000316
		public MissionAwardXML()
		{
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000026ED File Offset: 0x000008ED
		// Note: this type is marked as 'beforefieldinit'.
		static MissionAwardXML()
		{
		}

		// Token: 0x0400005C RID: 92
		private static List<MissionAwards> list_0 = new List<MissionAwards>();
	}
}
