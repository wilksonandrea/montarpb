using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML
{
	// Token: 0x0200000C RID: 12
	public static class CouponEffectXML
	{
		// Token: 0x0600009F RID: 159 RVA: 0x0000F59C File Offset: 0x0000D79C
		public static void Load()
		{
			string text = "Data/CouponFlags.xml";
			if (File.Exists(text))
			{
				CouponEffectXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Coupon Effects", CouponEffectXML.list_0.Count), LoggerType.Info, null);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000257F File Offset: 0x0000077F
		public static void Reload()
		{
			CouponEffectXML.list_0.Clear();
			CouponEffectXML.Load();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000F5F4 File Offset: 0x0000D7F4
		public static CouponFlag GetCouponEffect(int id)
		{
			List<CouponFlag> list = CouponEffectXML.list_0;
			CouponFlag couponFlag2;
			lock (list)
			{
				for (int i = 0; i < CouponEffectXML.list_0.Count; i++)
				{
					CouponFlag couponFlag = CouponEffectXML.list_0[i];
					if (couponFlag.ItemId == id)
					{
						return couponFlag;
					}
				}
				couponFlag2 = null;
			}
			return couponFlag2;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000F664 File Offset: 0x0000D864
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
									if ("Coupon".Equals(xmlNode2.Name))
									{
										XmlNamedNodeMap attributes = xmlNode2.Attributes;
										CouponFlag couponFlag = new CouponFlag
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
					catch (XmlException ex)
					{
						CLogger.Print(ex.Message, LoggerType.Error, ex);
					}
				}
				fileStream.Dispose();
				fileStream.Close();
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00002590 File Offset: 0x00000790
		// Note: this type is marked as 'beforefieldinit'.
		static CouponEffectXML()
		{
		}

		// Token: 0x04000052 RID: 82
		private static List<CouponFlag> list_0 = new List<CouponFlag>();
	}
}
