using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.RAW
{
	public class MissionCardRAW
	{
		private static List<MissionItemAward> list_0;

		private static List<MissionCardModel> list_1;

		private static List<MissionCardAwards> list_2;

		static MissionCardRAW()
		{
			MissionCardRAW.list_0 = new List<MissionItemAward>();
			MissionCardRAW.list_1 = new List<MissionCardModel>();
			MissionCardRAW.list_2 = new List<MissionCardAwards>();
		}

		public MissionCardRAW()
		{
		}

		public static MissionCardAwards GetAward(int mission, int cartao)
		{
			MissionCardAwards missionCardAward;
			List<MissionCardAwards>.Enumerator enumerator = MissionCardRAW.list_2.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					MissionCardAwards current = enumerator.Current;
					if (current.Id != mission || current.Card != cartao)
					{
						continue;
					}
					missionCardAward = current;
					return missionCardAward;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return missionCardAward;
		}

		public static List<MissionCardModel> GetCards(int MissionId, int CardBasicId)
		{
			List<MissionCardModel> missionCardModels = new List<MissionCardModel>();
			lock (MissionCardRAW.list_1)
			{
				foreach (MissionCardModel list1 in MissionCardRAW.list_1)
				{
					if (list1.MissionId != MissionId || (CardBasicId < 0 || list1.CardBasicId != CardBasicId) && CardBasicId != -1)
					{
						continue;
					}
					missionCardModels.Add(list1);
				}
			}
			return missionCardModels;
		}

		public static List<MissionCardModel> GetCards(List<MissionCardModel> Cards, int CardBasicId)
		{
			if (CardBasicId == -1)
			{
				return Cards;
			}
			List<MissionCardModel> missionCardModels = new List<MissionCardModel>();
			foreach (MissionCardModel list1 in MissionCardRAW.list_1)
			{
				if ((CardBasicId < 0 || list1.CardBasicId != CardBasicId) && CardBasicId != -1)
				{
					continue;
				}
				missionCardModels.Add(list1);
			}
			return missionCardModels;
		}

		public static List<MissionCardModel> GetCards(int MissionId)
		{
			List<MissionCardModel> missionCardModels = new List<MissionCardModel>();
			lock (MissionCardRAW.list_1)
			{
				foreach (MissionCardModel list1 in MissionCardRAW.list_1)
				{
					if (list1.MissionId != MissionId)
					{
						continue;
					}
					missionCardModels.Add(list1);
				}
			}
			return missionCardModels;
		}

		public static List<ItemsModel> GetMissionAwards(int MissionId)
		{
			List<ItemsModel> ıtemsModels = new List<ItemsModel>();
			lock (MissionCardRAW.list_0)
			{
				foreach (MissionItemAward list0 in MissionCardRAW.list_0)
				{
					if (list0.MissionId != MissionId)
					{
						continue;
					}
					ıtemsModels.Add(list0.Item);
				}
			}
			return ıtemsModels;
		}

		public static void LoadBasicCards(int Type)
		{
			MissionCardRAW.smethod_0("TutorialCard_Russia", Type);
			MissionCardRAW.smethod_0("Dino_Tutorial", Type);
			MissionCardRAW.smethod_0("Human_Tutorial", Type);
			MissionCardRAW.smethod_0("AssaultCard", Type);
			MissionCardRAW.smethod_0("BackUpCard", Type);
			MissionCardRAW.smethod_0("InfiltrationCard", Type);
			MissionCardRAW.smethod_0("SpecialCard", Type);
			MissionCardRAW.smethod_0("DefconCard", Type);
			MissionCardRAW.smethod_0("Commissioned_o", Type);
			MissionCardRAW.smethod_0("Company_o", Type);
			MissionCardRAW.smethod_0("Field_o", Type);
			MissionCardRAW.smethod_0("EventCard", Type);
			MissionCardRAW.smethod_0("Dino_Basic", Type);
			MissionCardRAW.smethod_0("Human_Basic", Type);
			MissionCardRAW.smethod_0("Dino_Intensify", Type);
			MissionCardRAW.smethod_0("Human_Intensify", Type);
			CLogger.Print(string.Format("Plugin Loaded: {0} Mission Card List", MissionCardRAW.list_1.Count), LoggerType.Info, null);
			if (Type == 1)
			{
				CLogger.Print(string.Format("Plugin Loaded: {0} Mission Card Awards", MissionCardRAW.list_2.Count), LoggerType.Info, null);
				return;
			}
			if (Type == 2)
			{
				CLogger.Print(string.Format("Plugin Loaded: {0} Mission Reward Items", MissionCardRAW.list_0.Count), LoggerType.Info, null);
			}
		}

		private static void smethod_0(string string_0, int int_0)
		{
			string str = string.Concat("Data/Missions/", string_0, ".mqf");
			if (File.Exists(str))
			{
				MissionCardRAW.smethod_2(str, string_0, int_0);
				return;
			}
			CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
		}

		private static int smethod_1(string string_0)
		{
			char string0;
			int ınt32 = 0;
			if (string_0 != null)
			{
				switch (string_0.Length)
				{
					case 7:
					{
						if (string_0 != "Field_o")
						{
							break;
						}
						ınt32 = 12;
						break;
					}
					case 9:
					{
						string0 = string_0[0];
						if (string0 == 'C')
						{
							if (string_0 != "Company_o")
							{
								break;
							}
							ınt32 = 11;
							break;
						}
						else if (string0 == 'E')
						{
							if (string_0 != "EventCard")
							{
								break;
							}
							ınt32 = 13;
							break;
						}
						else
						{
							break;
						}
					}
					case 10:
					{
						string0 = string_0[1];
						if (string0 == 'a')
						{
							if (string_0 != "BackUpCard")
							{
								break;
							}
							ınt32 = 6;
							break;
						}
						else if (string0 == 'e')
						{
							if (string_0 != "DefconCard")
							{
								break;
							}
							ınt32 = 9;
							break;
						}
						else if (string0 == 'i')
						{
							if (string_0 != "Dino_Basic")
							{
								break;
							}
							ınt32 = 14;
							break;
						}
						else
						{
							break;
						}
					}
					case 11:
					{
						string0 = string_0[0];
						if (string0 == 'A')
						{
							if (string_0 != "AssaultCard")
							{
								break;
							}
							ınt32 = 5;
							break;
						}
						else if (string0 == 'H')
						{
							if (string_0 != "Human_Basic")
							{
								break;
							}
							ınt32 = 15;
							break;
						}
						else if (string0 == 'S')
						{
							if (string_0 != "SpecialCard")
							{
								break;
							}
							ınt32 = 8;
							break;
						}
						else
						{
							break;
						}
					}
					case 13:
					{
						if (string_0 != "Dino_Tutorial")
						{
							break;
						}
						ınt32 = 2;
						break;
					}
					case 14:
					{
						string0 = string_0[0];
						if (string0 == 'C')
						{
							if (string_0 != "Commissioned_o")
							{
								break;
							}
							ınt32 = 10;
							break;
						}
						else if (string0 == 'D')
						{
							if (string_0 != "Dino_Intensify")
							{
								break;
							}
							ınt32 = 16;
							break;
						}
						else if (string0 == 'H')
						{
							if (string_0 != "Human_Tutorial")
							{
								break;
							}
							ınt32 = 3;
							break;
						}
						else
						{
							break;
						}
					}
					case 15:
					{
						if (string_0 != "Human_Intensify")
						{
							break;
						}
						ınt32 = 17;
						break;
					}
					case 16:
					{
						if (string_0 != "InfiltrationCard")
						{
							break;
						}
						ınt32 = 7;
						break;
					}
					case 19:
					{
						if (string_0 != "TutorialCard_Russia")
						{
							break;
						}
						ınt32 = 1;
						break;
					}
				}
			}
			return ınt32;
		}

		private static void smethod_2(string string_0, string string_1, int int_0)
		{
			byte[] numArray;
			int ınt32 = MissionCardRAW.smethod_1(string_1);
			if (ınt32 == 0)
			{
				CLogger.Print(string.Concat("Invalid: ", string_1), LoggerType.Warning, null);
			}
			try
			{
				numArray = File.ReadAllBytes(string_0);
			}
			catch
			{
				numArray = new byte[0];
			}
			if (numArray.Length == 0)
			{
				return;
			}
			try
			{
				SyncClientPacket syncClientPacket = new SyncClientPacket(numArray);
				syncClientPacket.ReadS(4);
				int ınt321 = syncClientPacket.ReadD();
				syncClientPacket.ReadB(16);
				int ınt322 = 0;
				int ınt323 = 0;
				for (int i = 0; i < 40; i++)
				{
					int ınt324 = ınt323;
					ınt323 = ınt324 + 1;
					int ınt325 = ınt324;
					int ınt326 = ınt322;
					if (ınt323 == 4)
					{
						ınt323 = 0;
						ınt322++;
					}
					syncClientPacket.ReadUH();
					int ınt327 = syncClientPacket.ReadC();
					int ınt328 = syncClientPacket.ReadC();
					int ınt329 = syncClientPacket.ReadC();
					ClassType classType = (ClassType)syncClientPacket.ReadC();
					int ınt3210 = syncClientPacket.ReadUH();
					MissionCardModel missionCardModel = new MissionCardModel(ınt326, ınt325)
					{
						MapId = ınt328,
						WeaponReq = classType,
						WeaponReqId = ınt3210,
						MissionType = (MissionType)ınt327,
						MissionLimit = ınt329,
						MissionId = ınt32
					};
					MissionCardRAW.list_1.Add(missionCardModel);
					if (ınt321 == 1)
					{
						syncClientPacket.ReadB(24);
					}
				}
				int ınt3211 = (ınt321 == 2 ? 5 : 1);
				for (int j = 0; j < 10; j++)
				{
					int ınt3212 = syncClientPacket.ReadD();
					int ınt3213 = syncClientPacket.ReadD();
					int ınt3214 = syncClientPacket.ReadD();
					for (int k = 0; k < ınt3211; k++)
					{
						syncClientPacket.ReadD();
						syncClientPacket.ReadD();
						syncClientPacket.ReadD();
						syncClientPacket.ReadD();
					}
					if (int_0 == 1)
					{
						MissionCardAwards missionCardAward = new MissionCardAwards()
						{
							Id = ınt32,
							Card = j,
							Exp = (ınt321 == 1 ? ınt3213 * 10 : ınt3213),
							Gold = ınt3212
						};
						MissionCardRAW.smethod_3(missionCardAward, ınt3214);
						if (!missionCardAward.Unusable())
						{
							MissionCardRAW.list_2.Add(missionCardAward);
						}
					}
				}
				if (ınt321 == 2)
				{
					syncClientPacket.ReadD();
					syncClientPacket.ReadB(8);
					for (int l = 0; l < 5; l++)
					{
						int ınt3215 = syncClientPacket.ReadD();
						syncClientPacket.ReadD();
						int ınt3216 = syncClientPacket.ReadD();
						uint uInt32 = syncClientPacket.ReadUD();
						if (ınt3215 > 0 && int_0 == 1)
						{
							MissionItemAward missionItemAward = new MissionItemAward()
							{
								MissionId = ınt32,
								Item = new ItemsModel(ınt3216, "Mission Item", ItemEquipType.Durable, uInt32)
							};
							MissionCardRAW.list_0.Add(missionItemAward);
						}
					}
				}
			}
			catch (XmlException xmlException1)
			{
				XmlException xmlException = xmlException1;
				CLogger.Print(string.Concat("File error: ", string_0, "; ", xmlException.Message), LoggerType.Error, xmlException);
			}
		}

		private static void smethod_3(MissionCardAwards missionCardAwards_0, int int_0)
		{
			if (int_0 == 0)
			{
				return;
			}
			if (int_0 >= 1 && int_0 <= 50)
			{
				MissionCardAwards missionCardAwards0 = missionCardAwards_0;
				missionCardAwards0.Ribbon = missionCardAwards0.Ribbon + 1;
				return;
			}
			if (int_0 >= 51 && int_0 <= 100)
			{
				MissionCardAwards ensign = missionCardAwards_0;
				ensign.Ensign = ensign.Ensign + 1;
				return;
			}
			if (int_0 >= 101 && int_0 <= 116)
			{
				MissionCardAwards medal = missionCardAwards_0;
				medal.Medal = medal.Medal + 1;
			}
		}
	}
}