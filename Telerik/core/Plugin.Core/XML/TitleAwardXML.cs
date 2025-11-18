using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.XML
{
	public class TitleAwardXML
	{
		public static List<TitleAward> Awards;

		static TitleAwardXML()
		{
			TitleAwardXML.Awards = new List<TitleAward>();
		}

		public TitleAwardXML()
		{
		}

		public static bool Contains(int TitleId, int ItemId)
		{
			bool flag;
			if (ItemId == 0)
			{
				return false;
			}
			List<TitleAward>.Enumerator enumerator = TitleAwardXML.Awards.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TitleAward current = enumerator.Current;
					if (current.Id != TitleId || current.Item.Id != ItemId)
					{
						continue;
					}
					flag = true;
					return flag;
				}
				return false;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return flag;
		}

		public static List<ItemsModel> GetAwards(int titleId)
		{
			List<ItemsModel> ıtemsModels = new List<ItemsModel>();
			lock (TitleAwardXML.Awards)
			{
				foreach (TitleAward award in TitleAwardXML.Awards)
				{
					if (award.Id != titleId)
					{
						continue;
					}
					ıtemsModels.Add(award.Item);
				}
			}
			return ıtemsModels;
		}

		public static void Load()
		{
			string str = "Data/Titles/Rewards.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				TitleAwardXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Title Awards", TitleAwardXML.Awards.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			TitleAwardXML.Awards.Clear();
			TitleAwardXML.Load();
		}

		private static void smethod_0(string string_0)
		{
			XmlDocument xmlDocument = new XmlDocument();
			using (FileStream fileStream = new FileStream(string_0, FileMode.Open))
			{
				if (fileStream.Length > 0L)
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
										int ınt32 = int.Parse(j.Attributes.GetNamedItem("Id").Value);
										TitleAwardXML.smethod_1(j, ınt32);
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
				fileStream.Dispose();
				fileStream.Close();
			}
		}

		private static void smethod_1(XmlNode xmlNode_0, int int_0)
		{
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("Rewards".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Item".Equals(j.Name))
						{
							XmlNamedNodeMap attributes = j.Attributes;
							TitleAward titleAward = new TitleAward()
							{
								Id = int_0
							};
							if (titleAward != null)
							{
								int ınt32 = int.Parse(attributes.GetNamedItem("Id").Value);
								ItemsModel ıtemsModel = new ItemsModel(ınt32)
								{
									Name = attributes.GetNamedItem("Name").Value,
									Count = uint.Parse(attributes.GetNamedItem("Count").Value),
									Equip = (ItemEquipType)int.Parse(attributes.GetNamedItem("Equip").Value)
								};
								if (ıtemsModel.Equip == ItemEquipType.Permanent)
								{
									ıtemsModel.ObjectId = (long)ComDiv.ValidateStockId(ınt32);
								}
								titleAward.Item = ıtemsModel;
								TitleAwardXML.Awards.Add(titleAward);
							}
						}
					}
				}
			}
		}
	}
}