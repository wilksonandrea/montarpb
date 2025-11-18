using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.XML
{
	public class TitleSystemXML
	{
		private static List<TitleModel> list_0;

		static TitleSystemXML()
		{
			TitleSystemXML.list_0 = new List<TitleModel>();
		}

		public TitleSystemXML()
		{
		}

		public static void Get2Titles(int titleId1, int titleId2, out TitleModel title1, out TitleModel title2, bool ReturnNull = true)
		{
			if (ReturnNull)
			{
				title1 = null;
				title2 = null;
			}
			else
			{
				title1 = new TitleModel();
				title2 = new TitleModel();
			}
			if (titleId1 == 0 && titleId2 == 0)
			{
				return;
			}
			foreach (TitleModel list0 in TitleSystemXML.list_0)
			{
				if (list0.Id != titleId1)
				{
					if (list0.Id != titleId2)
					{
						continue;
					}
					title2 = list0;
				}
				else
				{
					title1 = list0;
				}
			}
		}

		public static void Get3Titles(int titleId1, int titleId2, int titleId3, out TitleModel title1, out TitleModel title2, out TitleModel title3, bool ReturnNull)
		{
			if (ReturnNull)
			{
				title1 = null;
				title2 = null;
				title3 = null;
			}
			else
			{
				title1 = new TitleModel();
				title2 = new TitleModel();
				title3 = new TitleModel();
			}
			if (titleId1 == 0 && titleId2 == 0 && titleId3 == 0)
			{
				return;
			}
			foreach (TitleModel list0 in TitleSystemXML.list_0)
			{
				if (list0.Id == titleId1)
				{
					title1 = list0;
				}
				else if (list0.Id != titleId2)
				{
					if (list0.Id != titleId3)
					{
						continue;
					}
					title3 = list0;
				}
				else
				{
					title2 = list0;
				}
			}
		}

		public static TitleModel GetTitle(int titleId, bool ReturnNull = true)
		{
			TitleModel titleModel;
			if (titleId == 0)
			{
				if (!ReturnNull)
				{
					return new TitleModel();
				}
				return null;
			}
			List<TitleModel>.Enumerator enumerator = TitleSystemXML.list_0.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TitleModel current = enumerator.Current;
					if (current.Id != titleId)
					{
						continue;
					}
					titleModel = current;
					return titleModel;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return titleModel;
		}

		public static void Load()
		{
			string str = "Data/Titles/System.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				TitleSystemXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Title System", TitleSystemXML.list_0.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			TitleSystemXML.list_0.Clear();
			TitleSystemXML.Load();
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
									if ("Title".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
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