using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;

namespace Plugin.Core.RAW
{
	// Token: 0x02000044 RID: 68
	public class MissionCardRAW
	{
		// Token: 0x06000256 RID: 598 RVA: 0x00019D6C File Offset: 0x00017F6C
		private static void smethod_0(string string_0, int int_0)
		{
			string text = "Data/Missions/" + string_0 + ".mqf";
			if (File.Exists(text))
			{
				MissionCardRAW.smethod_2(text, string_0, int_0);
				return;
			}
			CLogger.Print("File not found: " + text, LoggerType.Warning, null);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00019DB0 File Offset: 0x00017FB0
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

		// Token: 0x06000258 RID: 600 RVA: 0x00019ED8 File Offset: 0x000180D8
		private static int smethod_1(string string_0)
		{
			int num = 0;
			if (string_0 != null)
			{
				switch (string_0.Length)
				{
				case 7:
					if (string_0 == "Field_o")
					{
						num = 12;
					}
					break;
				case 9:
				{
					char c = string_0[0];
					if (c != 'C')
					{
						if (c == 'E')
						{
							if (string_0 == "EventCard")
							{
								num = 13;
							}
						}
					}
					else if (string_0 == "Company_o")
					{
						num = 11;
					}
					break;
				}
				case 10:
				{
					char c = string_0[1];
					if (c != 'a')
					{
						if (c != 'e')
						{
							if (c == 'i')
							{
								if (string_0 == "Dino_Basic")
								{
									num = 14;
								}
							}
						}
						else if (string_0 == "DefconCard")
						{
							num = 9;
						}
					}
					else if (string_0 == "BackUpCard")
					{
						num = 6;
					}
					break;
				}
				case 11:
				{
					char c = string_0[0];
					if (c != 'A')
					{
						if (c != 'H')
						{
							if (c == 'S')
							{
								if (string_0 == "SpecialCard")
								{
									num = 8;
								}
							}
						}
						else if (string_0 == "Human_Basic")
						{
							num = 15;
						}
					}
					else if (string_0 == "AssaultCard")
					{
						num = 5;
					}
					break;
				}
				case 13:
					if (string_0 == "Dino_Tutorial")
					{
						num = 2;
					}
					break;
				case 14:
				{
					char c = string_0[0];
					if (c != 'C')
					{
						if (c != 'D')
						{
							if (c == 'H')
							{
								if (string_0 == "Human_Tutorial")
								{
									num = 3;
								}
							}
						}
						else if (string_0 == "Dino_Intensify")
						{
							num = 16;
						}
					}
					else if (string_0 == "Commissioned_o")
					{
						num = 10;
					}
					break;
				}
				case 15:
					if (string_0 == "Human_Intensify")
					{
						num = 17;
					}
					break;
				case 16:
					if (string_0 == "InfiltrationCard")
					{
						num = 7;
					}
					break;
				case 19:
					if (string_0 == "TutorialCard_Russia")
					{
						num = 1;
					}
					break;
				}
			}
			return num;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0001A0F4 File Offset: 0x000182F4
		public static List<ItemsModel> GetMissionAwards(int MissionId)
		{
			List<ItemsModel> list = new List<ItemsModel>();
			List<MissionItemAward> list2 = MissionCardRAW.list_0;
			lock (list2)
			{
				foreach (MissionItemAward missionItemAward in MissionCardRAW.list_0)
				{
					if (missionItemAward.MissionId == MissionId)
					{
						list.Add(missionItemAward.Item);
					}
				}
			}
			return list;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0001A188 File Offset: 0x00018388
		public static List<MissionCardModel> GetCards(int MissionId, int CardBasicId)
		{
			List<MissionCardModel> list = new List<MissionCardModel>();
			List<MissionCardModel> list2 = MissionCardRAW.list_1;
			lock (list2)
			{
				foreach (MissionCardModel missionCardModel in MissionCardRAW.list_1)
				{
					if (missionCardModel.MissionId == MissionId && ((CardBasicId >= 0 && missionCardModel.CardBasicId == CardBasicId) || CardBasicId == -1))
					{
						list.Add(missionCardModel);
					}
				}
			}
			return list;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0001A228 File Offset: 0x00018428
		public static List<MissionCardModel> GetCards(List<MissionCardModel> Cards, int CardBasicId)
		{
			if (CardBasicId == -1)
			{
				return Cards;
			}
			List<MissionCardModel> list = new List<MissionCardModel>();
			foreach (MissionCardModel missionCardModel in MissionCardRAW.list_1)
			{
				if ((CardBasicId >= 0 && missionCardModel.CardBasicId == CardBasicId) || CardBasicId == -1)
				{
					list.Add(missionCardModel);
				}
			}
			return list;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0001A29C File Offset: 0x0001849C
		public static List<MissionCardModel> GetCards(int MissionId)
		{
			List<MissionCardModel> list = new List<MissionCardModel>();
			List<MissionCardModel> list2 = MissionCardRAW.list_1;
			lock (list2)
			{
				foreach (MissionCardModel missionCardModel in MissionCardRAW.list_1)
				{
					if (missionCardModel.MissionId == MissionId)
					{
						list.Add(missionCardModel);
					}
				}
			}
			return list;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0001A328 File Offset: 0x00018528
		private static void smethod_2(string string_0, string string_1, int int_0)
		{
			int num = MissionCardRAW.smethod_1(string_1);
			if (num == 0)
			{
				CLogger.Print("Invalid: " + string_1, LoggerType.Warning, null);
			}
			byte[] array;
			try
			{
				array = File.ReadAllBytes(string_0);
			}
			catch
			{
				array = new byte[0];
			}
			if (array.Length == 0)
			{
				return;
			}
			try
			{
				SyncClientPacket syncClientPacket = new SyncClientPacket(array);
				syncClientPacket.ReadS(4);
				int num2 = syncClientPacket.ReadD();
				syncClientPacket.ReadB(16);
				int num3 = 0;
				int num4 = 0;
				for (int i = 0; i < 40; i++)
				{
					int num5 = num4++;
					int num6 = num3;
					if (num4 == 4)
					{
						num4 = 0;
						num3++;
					}
					syncClientPacket.ReadUH();
					int num7 = (int)syncClientPacket.ReadC();
					int num8 = (int)syncClientPacket.ReadC();
					int num9 = (int)syncClientPacket.ReadC();
					ClassType classType = (ClassType)syncClientPacket.ReadC();
					int num10 = (int)syncClientPacket.ReadUH();
					MissionCardModel missionCardModel = new MissionCardModel(num6, num5)
					{
						MapId = num8,
						WeaponReq = classType,
						WeaponReqId = num10,
						MissionType = (MissionType)num7,
						MissionLimit = num9,
						MissionId = num
					};
					MissionCardRAW.list_1.Add(missionCardModel);
					if (num2 == 1)
					{
						syncClientPacket.ReadB(24);
					}
				}
				int num11 = ((num2 == 2) ? 5 : 1);
				for (int j = 0; j < 10; j++)
				{
					int num12 = syncClientPacket.ReadD();
					int num13 = syncClientPacket.ReadD();
					int num14 = syncClientPacket.ReadD();
					for (int k = 0; k < num11; k++)
					{
						syncClientPacket.ReadD();
						syncClientPacket.ReadD();
						syncClientPacket.ReadD();
						syncClientPacket.ReadD();
					}
					if (int_0 == 1)
					{
						MissionCardAwards missionCardAwards = new MissionCardAwards
						{
							Id = num,
							Card = j,
							Exp = ((num2 == 1) ? (num13 * 10) : num13),
							Gold = num12
						};
						MissionCardRAW.smethod_3(missionCardAwards, num14);
						if (!missionCardAwards.Unusable())
						{
							MissionCardRAW.list_2.Add(missionCardAwards);
						}
					}
				}
				if (num2 == 2)
				{
					syncClientPacket.ReadD();
					syncClientPacket.ReadB(8);
					for (int l = 0; l < 5; l++)
					{
						int num15 = syncClientPacket.ReadD();
						syncClientPacket.ReadD();
						int num16 = syncClientPacket.ReadD();
						uint num17 = syncClientPacket.ReadUD();
						if (num15 > 0 && int_0 == 1)
						{
							MissionItemAward missionItemAward = new MissionItemAward
							{
								MissionId = num,
								Item = new ItemsModel(num16, "Mission Item", ItemEquipType.Durable, num17)
							};
							MissionCardRAW.list_0.Add(missionItemAward);
						}
					}
				}
			}
			catch (XmlException ex)
			{
				CLogger.Print("File error: " + string_0 + "; " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0001A5CC File Offset: 0x000187CC
		private static void smethod_3(MissionCardAwards missionCardAwards_0, int int_0)
		{
			if (int_0 == 0)
			{
				return;
			}
			if (int_0 >= 1 && int_0 <= 50)
			{
				int num = missionCardAwards_0.Ribbon;
				missionCardAwards_0.Ribbon = num + 1;
				return;
			}
			if (int_0 >= 51 && int_0 <= 100)
			{
				int num = missionCardAwards_0.Ensign;
				missionCardAwards_0.Ensign = num + 1;
				return;
			}
			if (int_0 >= 101 && int_0 <= 116)
			{
				int num = missionCardAwards_0.Medal;
				missionCardAwards_0.Medal = num + 1;
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0001A62C File Offset: 0x0001882C
		public static MissionCardAwards GetAward(int mission, int cartao)
		{
			foreach (MissionCardAwards missionCardAwards in MissionCardRAW.list_2)
			{
				if (missionCardAwards.Id == mission && missionCardAwards.Card == cartao)
				{
					return missionCardAwards;
				}
			}
			return null;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00002116 File Offset: 0x00000316
		public MissionCardRAW()
		{
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000331B File Offset: 0x0000151B
		// Note: this type is marked as 'beforefieldinit'.
		static MissionCardRAW()
		{
		}

		// Token: 0x040000D9 RID: 217
		private static List<MissionItemAward> list_0 = new List<MissionItemAward>();

		// Token: 0x040000DA RID: 218
		private static List<MissionCardModel> list_1 = new List<MissionCardModel>();

		// Token: 0x040000DB RID: 219
		private static List<MissionCardAwards> list_2 = new List<MissionCardAwards>();
	}
}
