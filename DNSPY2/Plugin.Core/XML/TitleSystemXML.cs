using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML
{
	// Token: 0x02000027 RID: 39
	public class TitleSystemXML
	{
		// Token: 0x0600016B RID: 363 RVA: 0x00015034 File Offset: 0x00013234
		public static void Load()
		{
			string text = "Data/Titles/System.xml";
			if (File.Exists(text))
			{
				TitleSystemXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Title System", TitleSystemXML.list_0.Count), LoggerType.Info, null);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000029B0 File Offset: 0x00000BB0
		public static void Reload()
		{
			TitleSystemXML.list_0.Clear();
			TitleSystemXML.Load();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0001508C File Offset: 0x0001328C
		public static TitleModel GetTitle(int titleId, bool ReturnNull = true)
		{
			if (titleId != 0)
			{
				foreach (TitleModel titleModel in TitleSystemXML.list_0)
				{
					if (titleModel.Id == titleId)
					{
						return titleModel;
					}
				}
				return null;
			}
			if (!ReturnNull)
			{
				return new TitleModel();
			}
			return null;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000150F8 File Offset: 0x000132F8
		public static void Get2Titles(int titleId1, int titleId2, out TitleModel title1, out TitleModel title2, bool ReturnNull = true)
		{
			if (!ReturnNull)
			{
				title1 = new TitleModel();
				title2 = new TitleModel();
			}
			else
			{
				title1 = null;
				title2 = null;
			}
			if (titleId1 == 0 && titleId2 == 0)
			{
				return;
			}
			foreach (TitleModel titleModel in TitleSystemXML.list_0)
			{
				if (titleModel.Id == titleId1)
				{
					title1 = titleModel;
				}
				else if (titleModel.Id == titleId2)
				{
					title2 = titleModel;
				}
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00015180 File Offset: 0x00013380
		public static void Get3Titles(int titleId1, int titleId2, int titleId3, out TitleModel title1, out TitleModel title2, out TitleModel title3, bool ReturnNull)
		{
			if (!ReturnNull)
			{
				title1 = new TitleModel();
				title2 = new TitleModel();
				title3 = new TitleModel();
			}
			else
			{
				title1 = null;
				title2 = null;
				title3 = null;
			}
			if (titleId1 == 0 && titleId2 == 0 && titleId3 == 0)
			{
				return;
			}
			foreach (TitleModel titleModel in TitleSystemXML.list_0)
			{
				if (titleModel.Id == titleId1)
				{
					title1 = titleModel;
				}
				else if (titleModel.Id == titleId2)
				{
					title2 = titleModel;
				}
				else if (titleModel.Id == titleId3)
				{
					title3 = titleModel;
				}
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00015228 File Offset: 0x00013428
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
									if ("Title".Equals(xmlNode2.Name))
									{
										XmlNamedNodeMap attributes = xmlNode2.Attributes;
										TitleModel titleModel = new TitleModel(int.Parse(attributes.GetNamedItem("Id").Value))
										{
											ClassId = int.Parse(attributes.GetNamedItem("List").Value),
											Ribbon = int.Parse(attributes.GetNamedItem("Ribbon").Value),
											Ensign = int.Parse(attributes.GetNamedItem("Ensign").Value),
											Medal = int.Parse(attributes.GetNamedItem("Medal").Value),
											MasterMedal = int.Parse(attributes.GetNamedItem("MasterMedal").Value),
											Rank = int.Parse(attributes.GetNamedItem("Rank").Value),
											Slot = int.Parse(attributes.GetNamedItem("Slot").Value),
											Req1 = int.Parse(attributes.GetNamedItem("ReqT1").Value),
											Req2 = int.Parse(attributes.GetNamedItem("ReqT2").Value)
										};
										TitleSystemXML.list_0.Add(titleModel);
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

		// Token: 0x06000171 RID: 369 RVA: 0x00002116 File Offset: 0x00000316
		public TitleSystemXML()
		{
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000029C1 File Offset: 0x00000BC1
		// Note: this type is marked as 'beforefieldinit'.
		static TitleSystemXML()
		{
		}

		// Token: 0x04000077 RID: 119
		private static List<TitleModel> list_0 = new List<TitleModel>();
	}
}
