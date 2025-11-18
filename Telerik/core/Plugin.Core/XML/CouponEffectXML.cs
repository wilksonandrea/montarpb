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
	public static class CouponEffectXML
	{
		private static List<CouponFlag> list_0;

		static CouponEffectXML()
		{
			CouponEffectXML.list_0 = new List<CouponFlag>();
		}

		public static CouponFlag GetCouponEffect(int id)
		{
			CouponFlag couponFlag;
			lock (CouponEffectXML.list_0)
			{
				int ınt32 = 0;
				while (ınt32 < CouponEffectXML.list_0.Count)
				{
					CouponFlag ıtem = CouponEffectXML.list_0[ınt32];
					if (ıtem.ItemId != id)
					{
						ınt32++;
					}
					else
					{
						couponFlag = ıtem;
						return couponFlag;
					}
				}
				couponFlag = null;
			}
			return couponFlag;
		}

		public static void Load()
		{
			string str = "Data/CouponFlags.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				CouponEffectXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Coupon Effects", CouponEffectXML.list_0.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			CouponEffectXML.list_0.Clear();
			CouponEffectXML.Load();
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
									if ("Coupon".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										CouponFlag couponFlag = new CouponFlag()
										{
											ItemId = int.Parse(attributes.GetNamedItem("ItemId").Value),
											EffectFlag = ComDiv.ParseEnum<CouponEffects>(attributes.GetNamedItem("EffectFlag").Value)
										};
										CouponEffectXML.list_0.Add(couponFlag);
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