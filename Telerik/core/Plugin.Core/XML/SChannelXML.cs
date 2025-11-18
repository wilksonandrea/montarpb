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
	public class SChannelXML
	{
		public static List<SChannelModel> Servers;

		static SChannelXML()
		{
			SChannelXML.Servers = new List<SChannelModel>();
		}

		public SChannelXML()
		{
		}

		public static SChannelModel GetServer(int id)
		{
			SChannelModel sChannelModel;
			lock (SChannelXML.Servers)
			{
				foreach (SChannelModel server in SChannelXML.Servers)
				{
					if (server.Id != id)
					{
						continue;
					}
					sChannelModel = server;
					return sChannelModel;
				}
				sChannelModel = null;
			}
			return sChannelModel;
		}

		public static void Load(bool Update = false)
		{
			string str = "Data/Server/SChannels.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				SChannelXML.smethod_0(str, Update);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Server Channel", SChannelXML.Servers.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			SChannelXML.Servers.Clear();
			SChannelXML.Load(true);
		}

		private static void smethod_0(string string_0, bool bool_0)
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
									if ("Server".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										SChannelModel sChannelModel = new SChannelModel(attributes.GetNamedItem("Host").Value, ushort.Parse(attributes.GetNamedItem("Port").Value))
										{
											Id = int.Parse(attributes.GetNamedItem("Id").Value),
											State = bool.Parse(attributes.GetNamedItem("State").Value),
											Type = ComDiv.ParseEnum<SChannelType>(attributes.GetNamedItem("Type").Value),
											IsMobile = bool.Parse(attributes.GetNamedItem("Mobile").Value),
											MaxPlayers = int.Parse(attributes.GetNamedItem("MaxPlayers").Value),
											ChannelPlayers = int.Parse(attributes.GetNamedItem("MaxPlayers").Value)
										};
										if (!bool_0)
										{
											SChannelXML.Servers.Add(sChannelModel);
										}
										else
										{
											SChannelModel server = SChannelXML.GetServer(sChannelModel.Id);
											if (server == null)
											{
												SChannelXML.Servers.Add(sChannelModel);
											}
											else
											{
												lock (SChannelXML.Servers)
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

		private static void smethod_1(string string_0, int int_0)
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
									if ("Server".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										SChannelModel server = SChannelXML.GetServer(int_0);
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

		public static void UpdateServer(int ServerId)
		{
			string str = "Data/Server/SChannels.xml";
			if (File.Exists(str))
			{
				SChannelXML.smethod_1(str, ServerId);
				return;
			}
			CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
		}
	}
}