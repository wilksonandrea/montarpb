using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using Server.Match.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Server.Match.Data.XML
{
	public class MapStructureXML
	{
		public static List<MapModel> Maps;

		static MapStructureXML()
		{
			MapStructureXML.Maps = new List<MapModel>();
		}

		public MapStructureXML()
		{
		}

		public static MapModel GetMapId(int MapId)
		{
			MapModel mapModel;
			lock (MapStructureXML.Maps)
			{
				foreach (MapModel map in MapStructureXML.Maps)
				{
					if (map.Id != MapId)
					{
						continue;
					}
					mapModel = map;
					return mapModel;
				}
				mapModel = null;
			}
			return mapModel;
		}

		public static void Load()
		{
			string str = "Data/Match/MapStructure.xml";
			if (File.Exists(str))
			{
				MapStructureXML.smethod_0(str);
				return;
			}
			CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
		}

		public static void Reload()
		{
			MapStructureXML.Maps.Clear();
			MapStructureXML.Load();
		}

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
									if ("Map".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										MapModel mapModel = new MapModel()
										{
											Id = int.Parse(attributes.GetNamedItem("Id").Value),
											Objects = new List<ObjectModel>(),
											Bombs = new List<BombPosition>()
										};
										MapStructureXML.smethod_1(j, mapModel);
										MapStructureXML.smethod_2(j, mapModel);
										MapStructureXML.Maps.Add(mapModel);
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

		private static void smethod_1(XmlNode xmlNode_0, MapModel mapModel_0)
		{
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("BombPositions".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Bomb".Equals(j.Name))
						{
							XmlNamedNodeMap attributes = j.Attributes;
							BombPosition bombPosition = new BombPosition()
							{
								X = float.Parse(attributes.GetNamedItem("X").Value),
								Y = float.Parse(attributes.GetNamedItem("Y").Value),
								Z = float.Parse(attributes.GetNamedItem("Z").Value),
								Position = new Half3(bombPosition.X, bombPosition.Y, bombPosition.Z)
							};
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

		private static void smethod_2(XmlNode xmlNode_0, MapModel mapModel_0)
		{
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("Objects".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Obj".Equals(j.Name))
						{
							XmlNamedNodeMap attributes = j.Attributes;
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
							MapStructureXML.smethod_3(j, objectModel);
							MapStructureXML.smethod_4(j, objectModel);
							mapModel_0.Objects.Add(objectModel);
						}
					}
				}
			}
		}

		private static void smethod_3(XmlNode xmlNode_0, ObjectModel objectModel_0)
		{
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("Anims".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Sync".Equals(j.Name))
						{
							XmlNamedNodeMap attributes = j.Attributes;
							AnimModel animModel = new AnimModel()
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

		private static void smethod_4(XmlNode xmlNode_0, ObjectModel objectModel_0)
		{
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("DestroyEffects".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Effect".Equals(j.Name))
						{
							XmlNamedNodeMap attributes = j.Attributes;
							DeffectModel deffectModel = new DeffectModel()
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
	}
}