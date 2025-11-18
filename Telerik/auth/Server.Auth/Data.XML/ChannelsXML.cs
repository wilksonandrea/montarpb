using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Auth.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Server.Auth.Data.XML
{
	public static class ChannelsXML
	{
		public static List<ChannelModel> Channels;

		static ChannelsXML()
		{
			ChannelsXML.Channels = new List<ChannelModel>();
		}

		public static ChannelModel GetChannel(int ServerId, int Id)
		{
			ChannelModel channelModel;
			lock (ChannelsXML.Channels)
			{
				foreach (ChannelModel channel in ChannelsXML.Channels)
				{
					if (channel.ServerId != ServerId || channel.Id != Id)
					{
						continue;
					}
					channelModel = channel;
					return channelModel;
				}
				channelModel = null;
			}
			return channelModel;
		}

		public static List<ChannelModel> GetChannels(int ServerId)
		{
			List<ChannelModel> channelModels = new List<ChannelModel>(11);
			for (int i = 0; i < ChannelsXML.Channels.Count; i++)
			{
				ChannelModel ıtem = ChannelsXML.Channels[i];
				if (ıtem.ServerId == ServerId)
				{
					channelModels.Add(ıtem);
				}
			}
			return channelModels;
		}

		public static void Load()
		{
			string str = "Data/Server/Channels.xml";
			if (File.Exists(str))
			{
				ChannelsXML.smethod_0(str);
				return;
			}
			CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
		}

		public static void Reload()
		{
			ChannelsXML.Channels.Clear();
			ChannelsXML.Load();
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
									if ("Channel".Equals(j.Name))
									{
										int ınt32 = int.Parse(j.Attributes.GetNamedItem("ServerId").Value);
										ChannelsXML.smethod_1(j, ınt32);
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

		private static void smethod_1(XmlNode xmlNode_0, int int_0)
		{
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("Count".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Setting".Equals(j.Name))
						{
							XmlNamedNodeMap attributes = j.Attributes;
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
							}
							catch (XmlException xmlException1)
							{
								XmlException xmlException = xmlException1;
								CLogger.Print(xmlException.Message, LoggerType.Error, xmlException);
							}
							ChannelModel channel = ChannelsXML.GetChannel(channelModel.ServerId, channelModel.Id);
							if (channel == null)
							{
								ChannelsXML.Channels.Add(channelModel);
							}
							else
							{
								lock (ChannelsXML.Channels)
								{
									channel.Type = channelModel.Type;
									channel.MaxRooms = channelModel.MaxRooms;
									channel.ExpBonus = channelModel.ExpBonus;
									channel.GoldBonus = channelModel.GoldBonus;
									channel.CashBonus = channelModel.CashBonus;
								}
							}
						}
					}
				}
			}
		}
	}
}