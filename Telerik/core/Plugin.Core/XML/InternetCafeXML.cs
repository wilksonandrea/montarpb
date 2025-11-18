using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.XML
{
	public static class InternetCafeXML
	{
		public readonly static List<InternetCafe> Cafes;

		static InternetCafeXML()
		{
			InternetCafeXML.Cafes = new List<InternetCafe>();
		}

		public static InternetCafe GetICafe(int ConfigId)
		{
			InternetCafe ınternetCafe;
			lock (InternetCafeXML.Cafes)
			{
				foreach (InternetCafe cafe in InternetCafeXML.Cafes)
				{
					if (cafe.ConfigId != ConfigId)
					{
						continue;
					}
					ınternetCafe = cafe;
					return ınternetCafe;
				}
				ınternetCafe = null;
			}
			return ınternetCafe;
		}

		public static bool IsValidAddress(string PlayerAddress, string RegisteredAddress)
		{
			return PlayerAddress.Equals(RegisteredAddress);
		}

		public static void Load()
		{
			string str = "Data/InternetCafe.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				InternetCafeXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Internet Cafe Bonuses", InternetCafeXML.Cafes.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			InternetCafeXML.Cafes.Clear();
			InternetCafeXML.Load();
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
									if ("Bonus".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										InternetCafe ınternetCafe = new InternetCafe(int.Parse(attributes.GetNamedItem("Id").Value))
										{
											BasicExp = int.Parse(attributes.GetNamedItem("BasicExp").Value),
											BasicGold = int.Parse(attributes.GetNamedItem("BasicGold").Value),
											PremiumExp = int.Parse(attributes.GetNamedItem("PremiumExp").Value),
											PremiumGold = int.Parse(attributes.GetNamedItem("PremiumGold").Value)
										};
										InternetCafeXML.Cafes.Add(ınternetCafe);
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