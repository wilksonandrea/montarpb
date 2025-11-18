using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML
{
	// Token: 0x02000017 RID: 23
	public class MissionConfigXML
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00011A24 File Offset: 0x0000FC24
		public static void Load()
		{
			string text = "Data/MissionConfig.xml";
			if (File.Exists(text))
			{
				MissionConfigXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Mission Stores", MissionConfigXML.list_0.Count), LoggerType.Info, null);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000026F9 File Offset: 0x000008F9
		public static void Reload()
		{
			MissionConfigXML.MissionPage1 = 0U;
			MissionConfigXML.MissionPage2 = 0U;
			MissionConfigXML.list_0.Clear();
			MissionConfigXML.Load();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00011A7C File Offset: 0x0000FC7C
		public static MissionStore GetMission(int MissionId)
		{
			List<MissionStore> list = MissionConfigXML.list_0;
			MissionStore missionStore2;
			lock (list)
			{
				foreach (MissionStore missionStore in MissionConfigXML.list_0)
				{
					if (missionStore.Id == MissionId)
					{
						return missionStore;
					}
				}
				missionStore2 = null;
			}
			return missionStore2;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00011B08 File Offset: 0x0000FD08
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
										XmlNamedNodeMap attributes = xmlNode2.Attributes;
										MissionStore missionStore = new MissionStore
										{
											Id = int.Parse(attributes.GetNamedItem("Id").Value),
											ItemId = int.Parse(attributes.GetNamedItem("ItemId").Value),
											Enable = bool.Parse(attributes.GetNamedItem("Enable").Value)
										};
										uint num = 1U << missionStore.Id;
										int num2 = (int)Math.Ceiling((double)missionStore.Id / 32.0);
										if (missionStore.Enable)
										{
											if (num2 == 1)
											{
												MissionConfigXML.MissionPage1 += num;
											}
											else if (num2 == 2)
											{
												MissionConfigXML.MissionPage2 += num;
											}
										}
										MissionConfigXML.list_0.Add(missionStore);
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

		// Token: 0x060000F5 RID: 245 RVA: 0x00002116 File Offset: 0x00000316
		public MissionConfigXML()
		{
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00002716 File Offset: 0x00000916
		// Note: this type is marked as 'beforefieldinit'.
		static MissionConfigXML()
		{
		}

		// Token: 0x0400005D RID: 93
		public static uint MissionPage1;

		// Token: 0x0400005E RID: 94
		public static uint MissionPage2;

		// Token: 0x0400005F RID: 95
		private static List<MissionStore> list_0 = new List<MissionStore>();
	}
}
