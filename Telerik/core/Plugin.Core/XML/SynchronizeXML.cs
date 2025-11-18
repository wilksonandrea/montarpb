using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.XML
{
	public class SynchronizeXML
	{
		public static List<Synchronize> Servers;

		static SynchronizeXML()
		{
			SynchronizeXML.Servers = new List<Synchronize>();
		}

		public SynchronizeXML()
		{
		}

		public static Synchronize GetServer(int Port)
		{
			Synchronize synchronize;
			if (SynchronizeXML.Servers.Count == 0)
			{
				return null;
			}
			try
			{
				lock (SynchronizeXML.Servers)
				{
					foreach (Synchronize server in SynchronizeXML.Servers)
					{
						if (server.RemotePort != Port)
						{
							continue;
						}
						synchronize = server;
						return synchronize;
					}
					synchronize = null;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				synchronize = null;
			}
			return synchronize;
		}

		public static void Load()
		{
			string str = "Data/Synchronize.xml";
			if (File.Exists(str))
			{
				SynchronizeXML.smethod_0(str);
				return;
			}
			CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
		}

		public static void Reload()
		{
			SynchronizeXML.Servers.Clear();
			SynchronizeXML.Load();
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
								XmlAttributeCollection attributes = i.Attributes;
								for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
								{
									if ("Sync".Equals(j.Name))
									{
										XmlNamedNodeMap xmlNamedNodeMaps = j.Attributes;
										Synchronize synchronize = new Synchronize(xmlNamedNodeMaps.GetNamedItem("Host").Value, int.Parse(xmlNamedNodeMaps.GetNamedItem("Port").Value))
										{
											RemotePort = int.Parse(xmlNamedNodeMaps.GetNamedItem("RemotePort").Value)
										};
										SynchronizeXML.Servers.Add(synchronize);
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