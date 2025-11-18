using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using Server.Match.Data.Models;

namespace Server.Match.Data.XML
{
	// Token: 0x02000028 RID: 40
	public class MapStructureXML
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00008BC8 File Offset: 0x00006DC8
		public static void Load()
		{
			string text = "Data/Match/MapStructure.xml";
			if (File.Exists(text))
			{
				MapStructureXML.smethod_0(text);
				return;
			}
			CLogger.Print("File not found: " + text, LoggerType.Warning, null);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000022CF File Offset: 0x000004CF
		public static void Reload()
		{
			MapStructureXML.Maps.Clear();
			MapStructureXML.Load();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00008BFC File Offset: 0x00006DFC
		public static MapModel GetMapId(int MapId)
		{
			List<MapModel> maps = MapStructureXML.Maps;
			MapModel mapModel2;
			lock (maps)
			{
				foreach (MapModel mapModel in MapStructureXML.Maps)
				{
					if (mapModel.Id == MapId)
					{
						return mapModel;
					}
				}
				mapModel2 = null;
			}
			return mapModel2;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00008C88 File Offset: 0x00006E88
		public static void SetObjectives(ObjectModel obj, RoomModel room)
		{
			if (obj.UltraSync == 0)
			{
				return;
			}
			if (obj.UltraSync != 1)
			{
				if (obj.UltraSync != 3)
				{
					if (obj.UltraSync == 2 || obj.UltraSync == 4)
					{
						room.Bar2 = obj.Life;
						room.Default2 = room.Bar2;
					}
					return;
				}
			}
			room.Bar1 = obj.Life;
			room.Default1 = room.Bar1;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00008CF8 File Offset: 0x00006EF8
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
									if ("Map".Equals(xmlNode2.Name))
									{
										XmlNamedNodeMap attributes = xmlNode2.Attributes;
										MapModel mapModel = new MapModel
										{
											Id = int.Parse(attributes.GetNamedItem("Id").Value),
											Objects = new List<ObjectModel>(),
											Bombs = new List<BombPosition>()
										};
										MapStructureXML.smethod_1(xmlNode2, mapModel);
										MapStructureXML.smethod_2(xmlNode2, mapModel);
										MapStructureXML.Maps.Add(mapModel);
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

		// Token: 0x06000095 RID: 149 RVA: 0x00008E34 File Offset: 0x00007034
		private static void smethod_1(XmlNode xmlNode_0, MapModel mapModel_0)
		{
			for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if ("BombPositions".Equals(xmlNode.Name))
				{
					for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
					{
						if ("Bomb".Equals(xmlNode2.Name))
						{
							XmlNamedNodeMap attributes = xmlNode2.Attributes;
							BombPosition bombPosition = new BombPosition
							{
								X = float.Parse(attributes.GetNamedItem("X").Value),
								Y = float.Parse(attributes.GetNamedItem("Y").Value),
								Z = float.Parse(attributes.GetNamedItem("Z").Value)
							};
							bombPosition.Position = new Half3(bombPosition.X, bombPosition.Y, bombPosition.Z);
							if (bombPosition.X == 0f && bombPosition.Y == 0f && bombPosition.Z == 0f)
							{
								bombPosition.EveryWhere = true;
							}
							mapModel_0.Bombs.Add(bombPosition);
						}
					}
				}
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00008F54 File Offset: 0x00007154
		private static void smethod_2(XmlNode xmlNode_0, MapModel mapModel_0)
		{
			for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if ("Objects".Equals(xmlNode.Name))
				{
					for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
					{
						if ("Obj".Equals(xmlNode2.Name))
						{
							XmlNamedNodeMap attributes = xmlNode2.Attributes;
							ObjectModel objectModel = new ObjectModel(bool.Parse(attributes.GetNamedItem("NeedSync").Value))
							{
								Id = int.Parse(attributes.GetNamedItem("Id").Value),
								Life = int.Parse(attributes.GetNamedItem("Life").Value),
								Animation = int.Parse(attributes.GetNamedItem("Animation").Value)
							};
							if (objectModel.Life > -1)
							{
								objectModel.Destroyable = true;
							}
							if (objectModel.Animation > 255)
							{
								if (objectModel.Animation == 256)
								{
									objectModel.UltraSync = 1;
								}
								else if (objectModel.Animation == 257)
								{
									objectModel.UltraSync = 2;
								}
								else if (objectModel.Animation == 258)
								{
									objectModel.UltraSync = 3;
								}
								else if (objectModel.Animation == 259)
								{
									objectModel.UltraSync = 4;
								}
								objectModel.Animation = 255;
							}
							MapStructureXML.smethod_3(xmlNode2, objectModel);
							MapStructureXML.smethod_4(xmlNode2, objectModel);
							mapModel_0.Objects.Add(objectModel);
						}
					}
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000090C8 File Offset: 0x000072C8
		private static void smethod_3(XmlNode xmlNode_0, ObjectModel objectModel_0)
		{
			for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if ("Anims".Equals(xmlNode.Name))
				{
					for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
					{
						if ("Sync".Equals(xmlNode2.Name))
						{
							XmlNamedNodeMap attributes = xmlNode2.Attributes;
							AnimModel animModel = new AnimModel
							{
								Id = int.Parse(attributes.GetNamedItem("Id").Value),
								Duration = float.Parse(attributes.GetNamedItem("Date").Value),
								NextAnim = int.Parse(attributes.GetNamedItem("Next").Value),
								OtherObj = int.Parse(attributes.GetNamedItem("OtherOBJ").Value),
								OtherAnim = int.Parse(attributes.GetNamedItem("OtherANIM").Value)
							};
							if (animModel.Id == 0)
							{
								objectModel_0.NoInstaSync = true;
							}
							if (animModel.Id != 255)
							{
								objectModel_0.UpdateId = 3;
							}
							objectModel_0.Animations.Add(animModel);
						}
					}
				}
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000091F4 File Offset: 0x000073F4
		private static void smethod_4(XmlNode xmlNode_0, ObjectModel objectModel_0)
		{
			for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if ("DestroyEffects".Equals(xmlNode.Name))
				{
					for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
					{
						if ("Effect".Equals(xmlNode2.Name))
						{
							XmlNamedNodeMap attributes = xmlNode2.Attributes;
							DeffectModel deffectModel = new DeffectModel
							{
								Id = int.Parse(attributes.GetNamedItem("Id").Value),
								Life = int.Parse(attributes.GetNamedItem("Percent").Value)
							};
							objectModel_0.Effects.Add(deffectModel);
						}
					}
				}
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000020A2 File Offset: 0x000002A2
		public MapStructureXML()
		{
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000022E0 File Offset: 0x000004E0
		// Note: this type is marked as 'beforefieldinit'.
		static MapStructureXML()
		{
		}

		// Token: 0x0400000D RID: 13
		public static List<MapModel> Maps = new List<MapModel>();
	}
}
