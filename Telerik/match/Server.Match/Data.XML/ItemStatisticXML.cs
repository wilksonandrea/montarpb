using Plugin.Core;
using Plugin.Core.Enums;
using Server.Match.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Server.Match.Data.XML
{
	public class ItemStatisticXML
	{
		public static List<ItemsStatistic> Stats;

		static ItemStatisticXML()
		{
			ItemStatisticXML.Stats = new List<ItemsStatistic>();
		}

		public ItemStatisticXML()
		{
		}

		public static ItemsStatistic GetItemStats(int ItemId)
		{
			ItemsStatistic ıtemsStatistic;
			lock (ItemStatisticXML.Stats)
			{
				foreach (ItemsStatistic stat in ItemStatisticXML.Stats)
				{
					if (stat.Id != ItemId)
					{
						continue;
					}
					ıtemsStatistic = stat;
					return ıtemsStatistic;
				}
				ıtemsStatistic = null;
			}
			return ıtemsStatistic;
		}

		public static void Load()
		{
			string str = "Data/Match/ItemStatistics.xml";
			if (File.Exists(str))
			{
				ItemStatisticXML.smethod_0(str);
				return;
			}
			CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
		}

		public static void Reload()
		{
			ItemStatisticXML.Stats.Clear();
			ItemStatisticXML.Load();
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
									if ("Statistic".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										ItemsStatistic ıtemsStatistic = new ItemsStatistic()
										{
											Id = int.Parse(attributes.GetNamedItem("Id").Value),
											Name = attributes.GetNamedItem("Name").Value,
											BulletLoaded = int.Parse(attributes.GetNamedItem("LoadedBullet").Value),
											BulletTotal = int.Parse(attributes.GetNamedItem("TotalBullet").Value),
											Damage = int.Parse(attributes.GetNamedItem("Damage").Value),
											FireDelay = float.Parse(attributes.GetNamedItem("FireDelay").Value),
											HelmetPenetrate = int.Parse(attributes.GetNamedItem("HelmetPenetrate").Value),
											Range = float.Parse(attributes.GetNamedItem("Range").Value)
										};
										ItemStatisticXML.Stats.Add(ıtemsStatistic);
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