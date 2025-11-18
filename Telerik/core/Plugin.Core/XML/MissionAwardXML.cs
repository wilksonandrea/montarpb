using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.XML
{
	public class MissionAwardXML
	{
		private static List<MissionAwards> list_0;

		static MissionAwardXML()
		{
			MissionAwardXML.list_0 = new List<MissionAwards>();
		}

		public MissionAwardXML()
		{
		}

		public static MissionAwards GetAward(int MissionId)
		{
			MissionAwards missionAward;
			lock (MissionAwardXML.list_0)
			{
				foreach (MissionAwards list0 in MissionAwardXML.list_0)
				{
					if (list0.Id != MissionId)
					{
						continue;
					}
					missionAward = list0;
					return missionAward;
				}
				missionAward = null;
			}
			return missionAward;
		}

		public static void Load()
		{
			string str = "Data/Cards/MissionAwards.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				MissionAwardXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Mission Awards", MissionAwardXML.list_0.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			MissionAwardXML.list_0.Clear();
			MissionAwardXML.Load();
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
									if ("Mission".Equals(j.Name))
									{
										XmlAttributeCollection attributes = j.Attributes;
										int ınt32 = int.Parse(attributes.GetNamedItem("Id").Value);
										int ınt321 = int.Parse(attributes.GetNamedItem("MasterMedal").Value);
										int ınt322 = int.Parse(attributes.GetNamedItem("Exp").Value);
										int ınt323 = int.Parse(attributes.GetNamedItem("Point").Value);
										MissionAwardXML.list_0.Add(new MissionAwards(ınt32, ınt321, ınt322, ınt323));
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