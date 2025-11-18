using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML
{
	// Token: 0x02000015 RID: 21
	public static class InternetCafeXML
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00011548 File Offset: 0x0000F748
		public static void Load()
		{
			string text = "Data/InternetCafe.xml";
			if (File.Exists(text))
			{
				InternetCafeXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Internet Cafe Bonuses", InternetCafeXML.Cafes.Count), LoggerType.Info, null);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000026B6 File Offset: 0x000008B6
		public static void Reload()
		{
			InternetCafeXML.Cafes.Clear();
			InternetCafeXML.Load();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000115A0 File Offset: 0x0000F7A0
		public static InternetCafe GetICafe(int ConfigId)
		{
			List<InternetCafe> cafes = InternetCafeXML.Cafes;
			InternetCafe internetCafe2;
			lock (cafes)
			{
				foreach (InternetCafe internetCafe in InternetCafeXML.Cafes)
				{
					if (internetCafe.ConfigId == ConfigId)
					{
						return internetCafe;
					}
				}
				internetCafe2 = null;
			}
			return internetCafe2;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000026C7 File Offset: 0x000008C7
		public static bool IsValidAddress(string PlayerAddress, string RegisteredAddress)
		{
			return PlayerAddress.Equals(RegisteredAddress);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0001162C File Offset: 0x0000F82C
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
									if ("Bonus".Equals(xmlNode2.Name))
									{
										XmlNamedNodeMap attributes = xmlNode2.Attributes;
										InternetCafe internetCafe = new InternetCafe(int.Parse(attributes.GetNamedItem("Id").Value))
										{
											BasicExp = int.Parse(attributes.GetNamedItem("BasicExp").Value),
											BasicGold = int.Parse(attributes.GetNamedItem("BasicGold").Value),
											PremiumExp = int.Parse(attributes.GetNamedItem("PremiumExp").Value),
											PremiumGold = int.Parse(attributes.GetNamedItem("PremiumGold").Value)
										};
										InternetCafeXML.Cafes.Add(internetCafe);
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

		// Token: 0x060000EA RID: 234 RVA: 0x000026D0 File Offset: 0x000008D0
		// Note: this type is marked as 'beforefieldinit'.
		static InternetCafeXML()
		{
		}

		// Token: 0x0400005B RID: 91
		public static readonly List<InternetCafe> Cafes = new List<InternetCafe>();
	}
}
