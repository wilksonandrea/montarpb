using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Data.XML;

public static class ChannelsXML
{
	public static List<ChannelModel> Channels = new List<ChannelModel>();

	public static void Load()
	{
		string text = "Data/Server/Channels.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
	}

	public static void Reload()
	{
		Channels.Clear();
		Load();
	}

	public static ChannelModel GetChannel(int ServerId, int Id)
	{
		lock (Channels)
		{
			foreach (ChannelModel channel in Channels)
			{
				if (channel.ServerId == ServerId && channel.Id == Id)
				{
					return channel;
				}
			}
			return null;
		}
	}

	public static List<ChannelModel> GetChannels(int ServerId)
	{
		List<ChannelModel> list = new List<ChannelModel>(11);
		for (int i = 0; i < Channels.Count; i++)
		{
			ChannelModel channelModel = Channels[i];
			if (channelModel.ServerId == ServerId)
			{
				list.Add(channelModel);
			}
		}
		return list;
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
							if ("Channel".Equals(xmlNode2.Name))
							{
								int int_ = int.Parse(xmlNode2.Attributes.GetNamedItem("ServerId").Value);
								smethod_1(xmlNode2, int_);
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
						}
						catch (XmlException ex)
						{
							CLogger.Print(ex.Message, LoggerType.Error, ex);
						}
						ChannelModel channel = GetChannel(channelModel.ServerId, channelModel.Id);
						if (channel != null)
						{
							lock (Channels)
							{
								channel.Type = channelModel.Type;
								channel.MaxRooms = channelModel.MaxRooms;
								channel.ExpBonus = channelModel.ExpBonus;
								channel.GoldBonus = channelModel.GoldBonus;
								channel.CashBonus = channelModel.CashBonus;
							}
						}
						else
						{
							Channels.Add(channelModel);
						}
					}
				}
			}
		}
	}
}
