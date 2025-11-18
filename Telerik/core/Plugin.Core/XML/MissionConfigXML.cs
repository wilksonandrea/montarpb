using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.XML
{
	public class MissionConfigXML
	{
		public static uint MissionPage1;

		public static uint MissionPage2;

		private static List<MissionStore> list_0;

		static MissionConfigXML()
		{
			MissionConfigXML.list_0 = new List<MissionStore>();
		}

		public MissionConfigXML()
		{
		}

		public static MissionStore GetMission(int MissionId)
		{
			MissionStore missionStore;
			lock (MissionConfigXML.list_0)
			{
				foreach (MissionStore list0 in MissionConfigXML.list_0)
				{
					if (list0.Id != MissionId)
					{
						continue;
					}
					missionStore = list0;
					return missionStore;
				}
				missionStore = null;
			}
			return missionStore;
		}

		public static void Load()
		{
			string str = "Data/MissionConfig.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				MissionConfigXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Mission Stores", MissionConfigXML.list_0.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			MissionConfigXML.MissionPage1 = 0;
			MissionConfigXML.MissionPage2 = 0;
			MissionConfigXML.list_0.Clear();
			MissionConfigXML.Load();
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
										XmlNamedNodeMap attributes = j.Attributes;
										MissionStore missionStore = new MissionStore()
										{
											Id = int.Parse(attributes.GetNamedItem("Id").Value),
											ItemId = int.Parse(attributes.GetNamedItem("ItemId").Value),
											Enable = bool.Parse(attributes.GetNamedItem("Enable").Value)
										};
										uint ıd = (uint)(1 << (missionStore.Id & 31));
										int ınt32 = (int)Math.Ceiling((double)missionStore.Id / 32);
										if (missionStore.Enable)
										{
											if (ınt32 == 1)
											{
												MissionConfigXML.MissionPage1 += ıd;
											}
											else if (ınt32 == 2)
											{
												MissionConfigXML.MissionPage2 += ıd;
											}
										}
										MissionConfigXML.list_0.Add(missionStore);
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