using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Match.Data.Models;

namespace Server.Match.Data.XML
{
	// Token: 0x02000027 RID: 39
	public class ItemStatisticXML
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00008910 File Offset: 0x00006B10
		public static void Load()
		{
			string text = "Data/Match/ItemStatistics.xml";
			if (File.Exists(text))
			{
				ItemStatisticXML.smethod_0(text);
				return;
			}
			CLogger.Print("File not found: " + text, LoggerType.Warning, null);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000022B2 File Offset: 0x000004B2
		public static void Reload()
		{
			ItemStatisticXML.Stats.Clear();
			ItemStatisticXML.Load();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00008944 File Offset: 0x00006B44
		public static ItemsStatistic GetItemStats(int ItemId)
		{
			List<ItemsStatistic> stats = ItemStatisticXML.Stats;
			ItemsStatistic itemsStatistic2;
			lock (stats)
			{
				foreach (ItemsStatistic itemsStatistic in ItemStatisticXML.Stats)
				{
					if (itemsStatistic.Id == ItemId)
					{
						return itemsStatistic;
					}
				}
				itemsStatistic2 = null;
			}
			return itemsStatistic2;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000089D0 File Offset: 0x00006BD0
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
									if ("Statistic".Equals(xmlNode2.Name))
									{
										XmlNamedNodeMap attributes = xmlNode2.Attributes;
										ItemsStatistic itemsStatistic = new ItemsStatistic
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
										ItemStatisticXML.Stats.Add(itemsStatistic);
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

		// Token: 0x0600008E RID: 142 RVA: 0x000020A2 File Offset: 0x000002A2
		public ItemStatisticXML()
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000022C3 File Offset: 0x000004C3
		// Note: this type is marked as 'beforefieldinit'.
		static ItemStatisticXML()
		{
		}

		// Token: 0x0400000C RID: 12
		public static List<ItemsStatistic> Stats = new List<ItemsStatistic>();
	}
}
