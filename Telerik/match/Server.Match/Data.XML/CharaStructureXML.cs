using Plugin.Core;
using Plugin.Core.Enums;
using Server.Match.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Server.Match.Data.XML
{
	public class CharaStructureXML
	{
		public static List<CharaModel> Charas;

		static CharaStructureXML()
		{
			CharaStructureXML.Charas = new List<CharaModel>();
		}

		public CharaStructureXML()
		{
		}

		public static int GetCharaHP(int CharaId)
		{
			int hP;
			List<CharaModel>.Enumerator enumerator = CharaStructureXML.Charas.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					CharaModel current = enumerator.Current;
					if (current.Id != CharaId)
					{
						continue;
					}
					hP = current.HP;
					return hP;
				}
				return 100;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return hP;
		}

		public static void Load()
		{
			string str = "Data/Match/CharaHealth.xml";
			if (File.Exists(str))
			{
				CharaStructureXML.smethod_0(str);
				return;
			}
			CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
		}

		public static void Reload()
		{
			CharaStructureXML.Charas.Clear();
			CharaStructureXML.Load();
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
									if ("Chara".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										CharaModel charaModel = new CharaModel()
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
	}
}