using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML;

public static class CouponEffectXML
{
	private static List<CouponFlag> list_0 = new List<CouponFlag>();

	public static void Load()
	{
		string text = "Data/CouponFlags.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {list_0.Count} Coupon Effects", LoggerType.Info);
	}

	public static void Reload()
	{
		list_0.Clear();
		Load();
	}

	public static CouponFlag GetCouponEffect(int id)
	{
		lock (list_0)
		{
			int num = 0;
			CouponFlag couponFlag;
			while (true)
			{
				if (num < list_0.Count)
				{
					couponFlag = list_0[num];
					if (couponFlag.ItemId == id)
					{
						break;
					}
					num++;
					continue;
				}
				return null;
			}
			return couponFlag;
		}
	}

	private static void smethod_0(string string_0)
	{
		XmlDocument xmlDocument = new XmlDocument();
		using FileStream fileStream = new FileStream(string_0, FileMode.Open);
		if (fileStream.Length == 0L)
		{
			CLogger.Print("File is empty: " + string_0, LoggerType.Warning);
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
							if ("Coupon".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								CouponFlag item = new CouponFlag
								{
									ItemId = int.Parse(attributes.GetNamedItem("ItemId").Value),
									EffectFlag = ComDiv.ParseEnum<CouponEffects>(attributes.GetNamedItem("EffectFlag").Value)
								};
								list_0.Add(item);
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
