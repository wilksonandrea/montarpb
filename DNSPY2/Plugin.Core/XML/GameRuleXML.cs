using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;

namespace Plugin.Core.XML
{
	// Token: 0x02000014 RID: 20
	public class GameRuleXML
	{
		// Token: 0x060000DC RID: 220 RVA: 0x000112C0 File Offset: 0x0000F4C0
		public static void Load()
		{
			string text = "Data/ClassicMode.xml";
			if (File.Exists(text))
			{
				GameRuleXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Game Rules", GameRuleXML.GameRules.Count), LoggerType.Info, null);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00002682 File Offset: 0x00000882
		public static void Reload()
		{
			GameRuleXML.GameRules.Clear();
			GameRuleXML.Load();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00011318 File Offset: 0x0000F518
		public static TRuleModel CheckTRuleByRoomName(string RoomName)
		{
			List<TRuleModel> gameRules = GameRuleXML.GameRules;
			TRuleModel truleModel2;
			lock (gameRules)
			{
				foreach (TRuleModel truleModel in GameRuleXML.GameRules)
				{
					if (RoomName.ToLower().Contains(truleModel.Name.ToLower()))
					{
						return truleModel;
					}
				}
				truleModel2 = null;
			}
			return truleModel2;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00002693 File Offset: 0x00000893
		public static bool IsBlocked(int ListId, int ItemId)
		{
			return ListId == ItemId;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00002699 File Offset: 0x00000899
		public static bool IsBlocked(int ListId, int ItemId, ref List<string> List, string Category)
		{
			if (ListId == ItemId)
			{
				List.Add(Category);
				return true;
			}
			return false;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000113B0 File Offset: 0x0000F5B0
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
									if ("Rule".Equals(xmlNode2.Name))
									{
										XmlNamedNodeMap attributes = xmlNode2.Attributes;
										TRuleModel truleModel = new TRuleModel
										{
											Name = attributes.GetNamedItem("Name").Value,
											BanIndexes = new List<int>()
										};
										GameRuleXML.smethod_1(xmlNode2, truleModel);
										GameRuleXML.GameRules.Add(truleModel);
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

		// Token: 0x060000E2 RID: 226 RVA: 0x000114D0 File Offset: 0x0000F6D0
		private static void smethod_1(XmlNode xmlNode_0, TRuleModel truleModel_0)
		{
			for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if ("Extensions".Equals(xmlNode.Name))
				{
					for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
					{
						if ("Ban".Equals(xmlNode2.Name))
						{
							ShopManager.IsBlocked(xmlNode2.Attributes.GetNamedItem("Filter").Value, truleModel_0.BanIndexes);
						}
					}
				}
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00002116 File Offset: 0x00000316
		public GameRuleXML()
		{
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000026AA File Offset: 0x000008AA
		// Note: this type is marked as 'beforefieldinit'.
		static GameRuleXML()
		{
		}

		// Token: 0x0400005A RID: 90
		public static List<TRuleModel> GameRules = new List<TRuleModel>();
	}
}
