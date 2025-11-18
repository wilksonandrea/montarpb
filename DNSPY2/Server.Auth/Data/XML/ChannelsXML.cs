using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Auth.Data.Models;

namespace Server.Auth.Data.XML
{
	// Token: 0x02000049 RID: 73
	public static class ChannelsXML
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00008CB8 File Offset: 0x00006EB8
		public static void Load()
		{
			string text = "Data/Server/Channels.xml";
			if (File.Exists(text))
			{
				ChannelsXML.smethod_0(text);
				return;
			}
			CLogger.Print("File not found: " + text, LoggerType.Warning, null);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public static void Reload()
		{
			ChannelsXML.Channels.Clear();
			ChannelsXML.Load();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00008CEC File Offset: 0x00006EEC
		public static ChannelModel GetChannel(int ServerId, int Id)
		{
			List<ChannelModel> channels = ChannelsXML.Channels;
			ChannelModel channelModel2;
			lock (channels)
			{
				foreach (ChannelModel channelModel in ChannelsXML.Channels)
				{
					if (channelModel.ServerId == ServerId && channelModel.Id == Id)
					{
						return channelModel;
					}
				}
				channelModel2 = null;
			}
			return channelModel2;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00008D80 File Offset: 0x00006F80
		public static List<ChannelModel> GetChannels(int ServerId)
		{
			List<ChannelModel> list = new List<ChannelModel>(11);
			for (int i = 0; i < ChannelsXML.Channels.Count; i++)
			{
				ChannelModel channelModel = ChannelsXML.Channels[i];
				if (channelModel.ServerId == ServerId)
				{
					list.Add(channelModel);
				}
			}
			return list;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00008DC8 File Offset: 0x00006FC8
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
									if ("Channel".Equals(xmlNode2.Name))
									{
										int num = int.Parse(xmlNode2.Attributes.GetNamedItem("ServerId").Value);
										ChannelsXML.smethod_1(xmlNode2, num);
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

		// Token: 0x060000FE RID: 254 RVA: 0x00008EC4 File Offset: 0x000070C4
		private static void smethod_1(XmlNode xmlNode_0, int int_0)
		{
			for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if ("Count".Equals(xmlNode.Name))
				{
					for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
					{
						if ("Setting".Equals(xmlNode2.Name))
						{
							XmlNamedNodeMap attributes = xmlNode2.Attributes;
							ChannelModel channelModel = new ChannelModel(int_0)
							{
								Id = int.Parse(attributes.GetNamedItem("Id").Value),
								Type = ComDiv.ParseEnum<ChannelType>(attributes.GetNamedItem("Type").Value),
								MaxRooms = int.Parse(attributes.GetNamedItem("MaxRooms").Value),
								ExpBonus = int.Parse(attributes.GetNamedItem("ExpBonus").Value),
								GoldBonus = int.Parse(attributes.GetNamedItem("GoldBonus").Value),
								CashBonus = int.Parse(attributes.GetNamedItem("CashBonus").Value)
							};
							try
							{
								if (channelModel.Type == ChannelType.CH_PW)
								{
									channelModel.Password = attributes.GetNamedItem("Password").Value;
								}
								goto IL_1B4;
							}
							catch (XmlException ex)
							{
								CLogger.Print(ex.Message, LoggerType.Error, ex);
								goto IL_1B4;
							}
							IL_12F:
							List<ChannelModel> channels = ChannelsXML.Channels;
							ChannelModel channel;
							lock (channels)
							{
								channel.Type = channelModel.Type;
								channel.MaxRooms = channelModel.MaxRooms;
								channel.ExpBonus = channelModel.ExpBonus;
								channel.GoldBonus = channelModel.GoldBonus;
								channel.CashBonus = channelModel.CashBonus;
								goto IL_19C;
							}
							goto IL_191;
							IL_1B4:
							channel = ChannelsXML.GetChannel(channelModel.ServerId, channelModel.Id);
							if (channel != null)
							{
								goto IL_12F;
							}
							IL_191:
							ChannelsXML.Channels.Add(channelModel);
						}
						IL_19C:;
					}
				}
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00002B05 File Offset: 0x00000D05
		// Note: this type is marked as 'beforefieldinit'.
		static ChannelsXML()
		{
		}

		// Token: 0x04000096 RID: 150
		public static List<ChannelModel> Channels = new List<ChannelModel>();
	}
}
