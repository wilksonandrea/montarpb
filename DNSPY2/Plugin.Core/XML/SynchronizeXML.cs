using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML
{
	// Token: 0x0200001C RID: 28
	public class SynchronizeXML
	{
		// Token: 0x06000118 RID: 280 RVA: 0x00012BA4 File Offset: 0x00010DA4
		public static void Load()
		{
			string text = "Data/Synchronize.xml";
			if (File.Exists(text))
			{
				SynchronizeXML.smethod_0(text);
				return;
			}
			CLogger.Print("File not found: " + text, LoggerType.Warning, null);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000027F1 File Offset: 0x000009F1
		public static void Reload()
		{
			SynchronizeXML.Servers.Clear();
			SynchronizeXML.Load();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00012BD8 File Offset: 0x00010DD8
		public static Synchronize GetServer(int Port)
		{
			if (SynchronizeXML.Servers.Count == 0)
			{
				return null;
			}
			Synchronize synchronize2;
			try
			{
				List<Synchronize> servers = SynchronizeXML.Servers;
				lock (servers)
				{
					foreach (Synchronize synchronize in SynchronizeXML.Servers)
					{
						if (synchronize.RemotePort == Port)
						{
							return synchronize;
						}
					}
					synchronize2 = null;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				synchronize2 = null;
			}
			return synchronize2;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00012C94 File Offset: 0x00010E94
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
								XmlAttributeCollection attributes = xmlNode.Attributes;
								for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
								{
									if ("Sync".Equals(xmlNode2.Name))
									{
										XmlNamedNodeMap attributes2 = xmlNode2.Attributes;
										Synchronize synchronize = new Synchronize(attributes2.GetNamedItem("Host").Value, int.Parse(attributes2.GetNamedItem("Port").Value))
										{
											RemotePort = int.Parse(attributes2.GetNamedItem("RemotePort").Value)
										};
										SynchronizeXML.Servers.Add(synchronize);
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

		// Token: 0x0600011C RID: 284 RVA: 0x00002116 File Offset: 0x00000316
		public SynchronizeXML()
		{
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00002802 File Offset: 0x00000A02
		// Note: this type is marked as 'beforefieldinit'.
		static SynchronizeXML()
		{
		}

		// Token: 0x04000066 RID: 102
		public static List<Synchronize> Servers = new List<Synchronize>();
	}
}
