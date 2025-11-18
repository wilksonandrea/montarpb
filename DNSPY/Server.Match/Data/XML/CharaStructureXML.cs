using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Match.Data.Models;

namespace Server.Match.Data.XML
{
	// Token: 0x02000026 RID: 38
	public class CharaStructureXML
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00008748 File Offset: 0x00006948
		public static void Load()
		{
			string text = "Data/Match/CharaHealth.xml";
			if (File.Exists(text))
			{
				CharaStructureXML.smethod_0(text);
				return;
			}
			CLogger.Print("File not found: " + text, LoggerType.Warning, null);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002295 File Offset: 0x00000495
		public static void Reload()
		{
			CharaStructureXML.Charas.Clear();
			CharaStructureXML.Load();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000877C File Offset: 0x0000697C
		public static int GetCharaHP(int CharaId)
		{
			foreach (CharaModel charaModel in CharaStructureXML.Charas)
			{
				if (charaModel.Id == CharaId)
				{
					return charaModel.HP;
				}
			}
			return 100;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000087E0 File Offset: 0x000069E0
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
									if ("Chara".Equals(xmlNode2.Name))
									{
										XmlNamedNodeMap attributes = xmlNode2.Attributes;
										CharaModel charaModel = new CharaModel
										{
											Id = int.Parse(attributes.GetNamedItem("Id").Value),
											HP = int.Parse(attributes.GetNamedItem("HP").Value)
										};
										CharaStructureXML.Charas.Add(charaModel);
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

		// Token: 0x06000088 RID: 136 RVA: 0x000020A2 File Offset: 0x000002A2
		public CharaStructureXML()
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000022A6 File Offset: 0x000004A6
		// Note: this type is marked as 'beforefieldinit'.
		static CharaStructureXML()
		{
		}

		// Token: 0x0400000B RID: 11
		public static List<CharaModel> Charas = new List<CharaModel>();
	}
}
