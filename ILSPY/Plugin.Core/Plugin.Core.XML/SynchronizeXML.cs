using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML;

public class SynchronizeXML
{
	public static List<Synchronize> Servers = new List<Synchronize>();

	public static void Load()
	{
		string text = "Data/Synchronize.xml";
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
		Servers.Clear();
		Load();
	}

	public static Synchronize GetServer(int Port)
	{
		if (Servers.Count == 0)
		{
			return null;
		}
		try
		{
			lock (Servers)
			{
				foreach (Synchronize server in Servers)
				{
					if (server.RemotePort == Port)
					{
						return server;
					}
				}
				return null;
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return null;
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
						_ = xmlNode.Attributes;
						for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
						{
							if ("Sync".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								Synchronize item = new Synchronize(attributes.GetNamedItem("Host").Value, int.Parse(attributes.GetNamedItem("Port").Value))
								{
									RemotePort = int.Parse(attributes.GetNamedItem("RemotePort").Value)
								};
								Servers.Add(item);
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
