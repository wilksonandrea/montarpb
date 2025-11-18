using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.XML
{
	public class GameRuleXML
	{
		public static List<TRuleModel> GameRules;

		static GameRuleXML()
		{
			GameRuleXML.GameRules = new List<TRuleModel>();
		}

		public GameRuleXML()
		{
		}

		public static TRuleModel CheckTRuleByRoomName(string RoomName)
		{
			TRuleModel tRuleModel;
			lock (GameRuleXML.GameRules)
			{
				foreach (TRuleModel gameRule in GameRuleXML.GameRules)
				{
					if (!RoomName.ToLower().Contains(gameRule.Name.ToLower()))
					{
						continue;
					}
					tRuleModel = gameRule;
					return tRuleModel;
				}
				tRuleModel = null;
			}
			return tRuleModel;
		}

		public static bool IsBlocked(int ListId, int ItemId)
		{
			return ListId == ItemId;
		}

		public static bool IsBlocked(int ListId, int ItemId, ref List<string> List, string Category)
		{
			if (ListId != ItemId)
			{
				return false;
			}
			List.Add(Category);
			return true;
		}

		public static void Load()
		{
			string str = "Data/ClassicMode.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				GameRuleXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Game Rules", GameRuleXML.GameRules.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			GameRuleXML.GameRules.Clear();
			GameRuleXML.Load();
		}

		private static void smethod_0(string string_0)
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
									if ("Rule".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										TRuleModel tRuleModel = new TRuleModel()
										{
											Name = attributes.GetNamedItem("Name").Value,
											BanIndexes = new List<int>()
										};
										GameRuleXML.smethod_1(j, tRuleModel);
										GameRuleXML.GameRules.Add(tRuleModel);
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

		private static void smethod_1(XmlNode xmlNode_0, TRuleModel truleModel_0)
		{
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("Extensions".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Ban".Equals(j.Name))
						{
							ShopManager.IsBlocked(j.Attributes.GetNamedItem("Filter").Value, truleModel_0.BanIndexes);
						}
					}
				}
			}
		}
	}
}