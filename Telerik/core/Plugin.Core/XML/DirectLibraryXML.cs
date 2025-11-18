using Plugin.Core;
using Plugin.Core.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.XML
{
	public class DirectLibraryXML
	{
		public static List<string> HashFiles;

		static DirectLibraryXML()
		{
			DirectLibraryXML.HashFiles = new List<string>();
		}

		public DirectLibraryXML()
		{
		}

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

		public static void Load()
		{
			string str = "Data/DirectLibrary.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				DirectLibraryXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Lib Hases", DirectLibraryXML.HashFiles.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			DirectLibraryXML.HashFiles.Clear();
			DirectLibraryXML.Load();
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
									if ("D3D9".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										DirectLibraryXML.HashFiles.Add(attributes.GetNamedItem("MD5").Value);
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