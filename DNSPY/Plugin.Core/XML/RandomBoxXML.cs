using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML
{
	// Token: 0x02000023 RID: 35
	public class RandomBoxXML
	{
		// Token: 0x0600014A RID: 330 RVA: 0x00013F88 File Offset: 0x00012188
		public static void Load()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Data\\RBoxes");
			if (!directoryInfo.Exists)
			{
				return;
			}
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				try
				{
					RandomBoxXML.smethod_0(int.Parse(fileInfo.Name.Substring(0, fileInfo.Name.Length - 4)));
				}
				catch (Exception ex)
				{
					CLogger.Print(ex.Message, LoggerType.Error, ex);
				}
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Random Boxes", RandomBoxXML.RBoxes.Count), LoggerType.Info, null);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000292E File Offset: 0x00000B2E
		public static void Reload()
		{
			RandomBoxXML.RBoxes.Clear();
			RandomBoxXML.Load();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00014034 File Offset: 0x00012234
		private static void smethod_0(int int_0)
		{
			string text = string.Format("Data/RBoxes/{0}.xml", int_0);
			if (File.Exists(text))
			{
				RandomBoxXML.smethod_1(text, int_0);
				return;
			}
			CLogger.Print("File not found: " + text, LoggerType.Warning, null);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00014074 File Offset: 0x00012274
		private static void smethod_1(string string_0, int int_0)
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
									if ("Item".Equals(xmlNode2.Name))
									{
										XmlNamedNodeMap attributes = xmlNode2.Attributes;
										RandomBoxModel randomBoxModel = new RandomBoxModel
										{
											ItemsCount = int.Parse(attributes.GetNamedItem("Count").Value),
											Items = new List<RandomBoxItem>()
										};
										RandomBoxXML.smethod_2(xmlNode2, randomBoxModel);
										randomBoxModel.SetTopPercent();
										RandomBoxXML.RBoxes.Add(int_0, randomBoxModel);
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						CLogger.Print(ex.Message, LoggerType.Error, ex);
					}
				}
				fileStream.Dispose();
				fileStream.Close();
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000141A4 File Offset: 0x000123A4
		private static void smethod_2(XmlNode xmlNode_0, RandomBoxModel randomBoxModel_0)
		{
			for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if ("Rewards".Equals(xmlNode.Name))
				{
					for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
					{
						if ("Good".Equals(xmlNode2.Name))
						{
							XmlNamedNodeMap attributes = xmlNode2.Attributes;
							RandomBoxItem randomBoxItem = new RandomBoxItem
							{
								Index = int.Parse(attributes.GetNamedItem("Index").Value),
								GoodsId = int.Parse(attributes.GetNamedItem("Id").Value),
								Percent = int.Parse(attributes.GetNamedItem("Percent").Value),
								Special = bool.Parse(attributes.GetNamedItem("Special").Value)
							};
							randomBoxModel_0.Items.Add(randomBoxItem);
						}
					}
				}
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000293F File Offset: 0x00000B3F
		public static bool ContainsBox(int Id)
		{
			return RandomBoxXML.RBoxes.ContainsKey(Id);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00014294 File Offset: 0x00012494
		public static RandomBoxModel GetBox(int Id)
		{
			RandomBoxModel randomBoxModel;
			try
			{
				randomBoxModel = RandomBoxXML.RBoxes[Id];
			}
			catch
			{
				randomBoxModel = null;
			}
			return randomBoxModel;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00002116 File Offset: 0x00000316
		public RandomBoxXML()
		{
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000294C File Offset: 0x00000B4C
		// Note: this type is marked as 'beforefieldinit'.
		static RandomBoxXML()
		{
		}

		// Token: 0x04000073 RID: 115
		public static SortedList<int, RandomBoxModel> RBoxes = new SortedList<int, RandomBoxModel>();
	}
}
