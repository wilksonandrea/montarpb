using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML
{
	// Token: 0x02000025 RID: 37
	public class SChannelXML
	{
		// Token: 0x0600015B RID: 347 RVA: 0x000146BC File Offset: 0x000128BC
		public static void Load(bool Update = false)
		{
			string text = "Data/Server/SChannels.xml";
			if (File.Exists(text))
			{
				SChannelXML.smethod_0(text, Update);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Server Channel", SChannelXML.Servers.Count), LoggerType.Info, null);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00014714 File Offset: 0x00012914
		public static void UpdateServer(int ServerId)
		{
			string text = "Data/Server/SChannels.xml";
			if (File.Exists(text))
			{
				SChannelXML.smethod_1(text, ServerId);
				return;
			}
			CLogger.Print("File not found: " + text, LoggerType.Warning, null);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00002975 File Offset: 0x00000B75
		public static void Reload()
		{
			SChannelXML.Servers.Clear();
			SChannelXML.Load(true);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0001474C File Offset: 0x0001294C
		public static SChannelModel GetServer(int id)
		{
			List<SChannelModel> servers = SChannelXML.Servers;
			SChannelModel schannelModel2;
			lock (servers)
			{
				foreach (SChannelModel schannelModel in SChannelXML.Servers)
				{
					if (schannelModel.Id == id)
					{
						return schannelModel;
					}
				}
				schannelModel2 = null;
			}
			return schannelModel2;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000147D8 File Offset: 0x000129D8
		private static void smethod_0(string string_0, bool bool_0)
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
									if ("Server".Equals(xmlNode2.Name))
									{
										XmlNamedNodeMap attributes = xmlNode2.Attributes;
										SChannelModel schannelModel = new SChannelModel(attributes.GetNamedItem("Host").Value, ushort.Parse(attributes.GetNamedItem("Port").Value))
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
											SChannelModel server = SChannelXML.GetServer(schannelModel.Id);
											if (server != null)
											{
												List<SChannelModel> servers = SChannelXML.Servers;
												lock (servers)
												{
													server.State = bool.Parse(attributes.GetNamedItem("State").Value);
													server.Host = attributes.GetNamedItem("Host").Value;
													server.Port = ushort.Parse(attributes.GetNamedItem("Port").Value);
													server.Type = ComDiv.ParseEnum<SChannelType>(attributes.GetNamedItem("Type").Value);
													server.IsMobile = bool.Parse(attributes.GetNamedItem("Mobile").Value);
													server.MaxPlayers = int.Parse(attributes.GetNamedItem("MaxPlayers").Value);
													server.ChannelPlayers = int.Parse(attributes.GetNamedItem("ChannelPlayers").Value);
													goto IL_271;
												}
											}
											SChannelXML.Servers.Add(schannelModel);
										}
										else
										{
											SChannelXML.Servers.Add(schannelModel);
										}
									}
									IL_271:;
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

		// Token: 0x06000160 RID: 352 RVA: 0x00014AEC File Offset: 0x00012CEC
		private static void smethod_1(string string_0, int int_0)
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
									if ("Server".Equals(xmlNode2.Name))
									{
										XmlNamedNodeMap attributes = xmlNode2.Attributes;
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
					catch (XmlException ex)
					{
						CLogger.Print(ex.Message, LoggerType.Error, ex);
					}
				}
				fileStream.Dispose();
				fileStream.Close();
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00002116 File Offset: 0x00000316
		public SChannelXML()
		{
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00002987 File Offset: 0x00000B87
		// Note: this type is marked as 'beforefieldinit'.
		static SChannelXML()
		{
		}

		// Token: 0x04000075 RID: 117
		public static List<SChannelModel> Servers = new List<SChannelModel>();
	}
}
