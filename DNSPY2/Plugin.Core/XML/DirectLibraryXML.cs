using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;

namespace Plugin.Core.XML
{
	// Token: 0x0200001F RID: 31
	public class DirectLibraryXML
	{
		// Token: 0x06000132 RID: 306 RVA: 0x000137DC File Offset: 0x000119DC
		public static void Load()
		{
			string text = "Data/DirectLibrary.xml";
			if (File.Exists(text))
			{
				DirectLibraryXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Lib Hases", DirectLibraryXML.HashFiles.Count), LoggerType.Info, null);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00002881 File Offset: 0x00000A81
		public static void Reload()
		{
			DirectLibraryXML.HashFiles.Clear();
			DirectLibraryXML.Load();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00013834 File Offset: 0x00011A34
		public static bool IsValid(string md5)
		{
			if (string.IsNullOrEmpty(md5))
			{
				return true;
			}
			for (int i = 0; i < DirectLibraryXML.HashFiles.Count; i++)
			{
				if (DirectLibraryXML.HashFiles[i] == md5)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00013878 File Offset: 0x00011A78
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
									if ("D3D9".Equals(xmlNode2.Name))
									{
										XmlNamedNodeMap attributes = xmlNode2.Attributes;
										DirectLibraryXML.HashFiles.Add(attributes.GetNamedItem("MD5").Value);
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

		// Token: 0x06000136 RID: 310 RVA: 0x00002116 File Offset: 0x00000316
		public DirectLibraryXML()
		{
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00002892 File Offset: 0x00000A92
		// Note: this type is marked as 'beforefieldinit'.
		static DirectLibraryXML()
		{
		}

		// Token: 0x0400006B RID: 107
		public static List<string> HashFiles = new List<string>();
	}
}
