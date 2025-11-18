using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML;

public class SChannelXML
{
	public static List<SChannelModel> Servers = new List<SChannelModel>();

	public static void Load(bool Update = false)
	{
		string text = "Data/Server/SChannels.xml";
		if (File.Exists(text))
		{
			smethod_0(text, Update);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {Servers.Count} Server Channel", LoggerType.Info);
	}

	public static void UpdateServer(int ServerId)
	{
		string text = "Data/Server/SChannels.xml";
		if (File.Exists(text))
		{
			smethod_1(text, ServerId);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
	}

	public static void Reload()
	{
		Servers.Clear();
		Load(Update: true);
	}

	public static SChannelModel GetServer(int id)
	{
		lock (Servers)
		{
			foreach (SChannelModel server in Servers)
			{
				if (server.Id == id)
				{
					return server;
				}
			}
			return null;
		}
	}

	private static void smethod_0(string string_0, bool bool_0)
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
							if ("Server".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								SChannelModel sChannelModel = new SChannelModel(attributes.GetNamedItem("Host").Value, ushort.Parse(attributes.GetNamedItem("Port").Value))
								{
									Id = int.Parse(attributes.GetNamedItem("Id").Value),
									State = bool.Parse(attributes.GetNamedItem("State").Value),
									Type = ComDiv.ParseEnum<SChannelType>(attributes.GetNamedItem("Type").Value),
									IsMobile = bool.Parse(attributes.GetNamedItem("Mobile").Value),
									MaxPlayers = int.Parse(attributes.GetNamedItem("MaxPlayers").Value),
									ChannelPlayers = int.Parse(attributes.GetNamedItem("MaxPlayers").Value)
								};
								if (bool_0)
								{
									SChannelModel server = GetServer(sChannelModel.Id);
									if (server != null)
									{
										lock (Servers)
										{
											server.State = bool.Parse(attributes.GetNamedItem("State").Value);
											server.Host = attributes.GetNamedItem("Host").Value;
											server.Port = ushort.Parse(attributes.GetNamedItem("Port").Value);
											server.Type = ComDiv.ParseEnum<SChannelType>(attributes.GetNamedItem("Type").Value);
											server.IsMobile = bool.Parse(attributes.GetNamedItem("Mobile").Value);
											server.MaxPlayers = int.Parse(attributes.GetNamedItem("MaxPlayers").Value);
											server.ChannelPlayers = int.Parse(attributes.GetNamedItem("ChannelPlayers").Value);
										}
									}
									else
									{
										Servers.Add(sChannelModel);
									}
								}
								else
								{
									Servers.Add(sChannelModel);
								}
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

	private static void smethod_1(string string_0, int int_0)
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
							if ("Server".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								SChannelModel server = GetServer(int_0);
								if (server != null)
								{
									server.State = bool.Parse(attributes.GetNamedItem("State").Value);
									server.Host = attributes.GetNamedItem("Host").Value;
									server.Port = ushort.Parse(attributes.GetNamedItem("Port").Value);
									server.Type = ComDiv.ParseEnum<SChannelType>(attributes.GetNamedItem("Type").Value);
									server.IsMobile = bool.Parse(attributes.GetNamedItem("Mobile").Value);
									server.MaxPlayers = int.Parse(attributes.GetNamedItem("MaxPlayers").Value);
									server.ChannelPlayers = int.Parse(attributes.GetNamedItem("ChannelPlayers").Value);
								}
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
