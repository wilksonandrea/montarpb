using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.XML
{
	public class RandomBoxXML
	{
		public static SortedList<int, RandomBoxModel> RBoxes;

		static RandomBoxXML()
		{
			RandomBoxXML.RBoxes = new SortedList<int, RandomBoxModel>();
		}

		public RandomBoxXML()
		{
		}

		public static bool ContainsBox(int Id)
		{
			return RandomBoxXML.RBoxes.ContainsKey(Id);
		}

		public static RandomBoxModel GetBox(int Id)
		{
			RandomBoxModel ıtem;
			try
			{
				ıtem = RandomBoxXML.RBoxes[Id];
			}
			catch
			{
				ıtem = null;
			}
			return ıtem;
		}

		public static void Load()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(string.Concat(Directory.GetCurrentDirectory(), "\\Data\\RBoxes"));
			if (!directoryInfo.Exists)
			{
				return;
			}
			FileInfo[] files = directoryInfo.GetFiles();
			for (int i = 0; i < (int)files.Length; i++)
			{
				FileInfo fileInfo = files[i];
				try
				{
					RandomBoxXML.smethod_0(int.Parse(fileInfo.Name.Substring(0, fileInfo.Name.Length - 4)));
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					CLogger.Print(exception.Message, LoggerType.Error, exception);
				}
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Random Boxes", RandomBoxXML.RBoxes.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			RandomBoxXML.RBoxes.Clear();
			RandomBoxXML.Load();
		}

		private static void smethod_0(int int_0)
		{
			string str = string.Format("Data/RBoxes/{0}.xml", int_0);
			if (File.Exists(str))
			{
				RandomBoxXML.smethod_1(str, int_0);
				return;
			}
			CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
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
									if ("Item".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										RandomBoxModel randomBoxModel = new RandomBoxModel()
										{
											ItemsCount = int.Parse(attributes.GetNamedItem("Count").Value),
											Items = new List<RandomBoxItem>()
										};
										RandomBoxXML.smethod_2(j, randomBoxModel);
										randomBoxModel.SetTopPercent();
										RandomBoxXML.RBoxes.Add(int_0, randomBoxModel);
									}
								}
							}
						}
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						CLogger.Print(exception.Message, LoggerType.Error, exception);
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

		private static void smethod_2(XmlNode xmlNode_0, RandomBoxModel randomBoxModel_0)
		{
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("Rewards".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Good".Equals(j.Name))
						{
							XmlNamedNodeMap attributes = j.Attributes;
							RandomBoxItem randomBoxItem = new RandomBoxItem()
							{
								Index = int.Parse(attributes.GetNamedItem("Index").Value),
								GoodsId = int.Parse(attributes.GetNamedItem("Id").Value),
								Percent = int.Parse(attributes.GetNamedItem("Percent").Value),
								Special = bool.Parse(attributes.GetNamedItem("Special").Value)
							};
							randomBoxModel_0.Items.Add(randomBoxItem);
						}
					}
				}
			}
		}
	}
}