using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;

namespace Plugin.Core.RAW;

public class MissionCardRAW
{
	private static List<MissionItemAward> list_0 = new List<MissionItemAward>();

	private static List<MissionCardModel> list_1 = new List<MissionCardModel>();

	private static List<MissionCardAwards> list_2 = new List<MissionCardAwards>();

	private static void smethod_0(string string_0, int int_0)
	{
		string text = "Data/Missions/" + string_0 + ".mqf";
		if (File.Exists(text))
		{
			smethod_2(text, string_0, int_0);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
	}

	public static void LoadBasicCards(int Type)
	{
		smethod_0("TutorialCard_Russia", Type);
		smethod_0("Dino_Tutorial", Type);
		smethod_0("Human_Tutorial", Type);
		smethod_0("AssaultCard", Type);
		smethod_0("BackUpCard", Type);
		smethod_0("InfiltrationCard", Type);
		smethod_0("SpecialCard", Type);
		smethod_0("DefconCard", Type);
		smethod_0("Commissioned_o", Type);
		smethod_0("Company_o", Type);
		smethod_0("Field_o", Type);
		smethod_0("EventCard", Type);
		smethod_0("Dino_Basic", Type);
		smethod_0("Human_Basic", Type);
		smethod_0("Dino_Intensify", Type);
		smethod_0("Human_Intensify", Type);
		CLogger.Print($"Plugin Loaded: {list_1.Count} Mission Card List", LoggerType.Info);
		switch (Type)
		{
		case 1:
			CLogger.Print($"Plugin Loaded: {list_2.Count} Mission Card Awards", LoggerType.Info);
			break;
		case 2:
			CLogger.Print($"Plugin Loaded: {list_0.Count} Mission Reward Items", LoggerType.Info);
			break;
		}
	}

	private static int smethod_1(string string_0)
	{
		int result = 0;
		if (string_0 != null)
		{
			switch (string_0.Length)
			{
			case 7:
				if (string_0 == "Field_o")
				{
					result = 12;
				}
				break;
			case 9:
				switch (string_0[0])
				{
				case 'E':
					if (string_0 == "EventCard")
					{
						result = 13;
					}
					break;
				case 'C':
					if (string_0 == "Company_o")
					{
						result = 11;
					}
					break;
				}
				break;
			case 10:
				switch (string_0[1])
				{
				case 'i':
					if (string_0 == "Dino_Basic")
					{
						result = 14;
					}
					break;
				case 'e':
					if (string_0 == "DefconCard")
					{
						result = 9;
					}
					break;
				case 'a':
					if (string_0 == "BackUpCard")
					{
						result = 6;
					}
					break;
				}
				break;
			case 11:
				switch (string_0[0])
				{
				case 'S':
					if (string_0 == "SpecialCard")
					{
						result = 8;
					}
					break;
				case 'H':
					if (string_0 == "Human_Basic")
					{
						result = 15;
					}
					break;
				case 'A':
					if (string_0 == "AssaultCard")
					{
						result = 5;
					}
					break;
				}
				break;
			case 13:
				if (string_0 == "Dino_Tutorial")
				{
					result = 2;
				}
				break;
			case 14:
				switch (string_0[0])
				{
				case 'H':
					if (string_0 == "Human_Tutorial")
					{
						result = 3;
					}
					break;
				case 'D':
					if (string_0 == "Dino_Intensify")
					{
						result = 16;
					}
					break;
				case 'C':
					if (string_0 == "Commissioned_o")
					{
						result = 10;
					}
					break;
				}
				break;
			case 15:
				if (string_0 == "Human_Intensify")
				{
					result = 17;
				}
				break;
			case 16:
				if (string_0 == "InfiltrationCard")
				{
					result = 7;
				}
				break;
			case 19:
				if (string_0 == "TutorialCard_Russia")
				{
					result = 1;
				}
				break;
			}
		}
		return result;
	}

	public static List<ItemsModel> GetMissionAwards(int MissionId)
	{
		List<ItemsModel> list = new List<ItemsModel>();
		lock (list_0)
		{
			foreach (MissionItemAward item in list_0)
			{
				if (item.MissionId == MissionId)
				{
					list.Add(item.Item);
				}
			}
			return list;
		}
	}

	public static List<MissionCardModel> GetCards(int MissionId, int CardBasicId)
	{
		List<MissionCardModel> list = new List<MissionCardModel>();
		lock (list_1)
		{
			foreach (MissionCardModel item in list_1)
			{
				if (item.MissionId == MissionId && ((CardBasicId >= 0 && item.CardBasicId == CardBasicId) || CardBasicId == -1))
				{
					list.Add(item);
				}
			}
			return list;
		}
	}

	public static List<MissionCardModel> GetCards(List<MissionCardModel> Cards, int CardBasicId)
	{
		if (CardBasicId == -1)
		{
			return Cards;
		}
		List<MissionCardModel> list = new List<MissionCardModel>();
		foreach (MissionCardModel item in list_1)
		{
			if ((CardBasicId >= 0 && item.CardBasicId == CardBasicId) || CardBasicId == -1)
			{
				list.Add(item);
			}
		}
		return list;
	}

	public static List<MissionCardModel> GetCards(int MissionId)
	{
		List<MissionCardModel> list = new List<MissionCardModel>();
		lock (list_1)
		{
			foreach (MissionCardModel item in list_1)
			{
				if (item.MissionId == MissionId)
				{
					list.Add(item);
				}
			}
			return list;
		}
	}

	private static void smethod_2(string string_0, string string_1, int int_0)
	{
		int num = smethod_1(string_1);
		if (num == 0)
		{
			CLogger.Print("Invalid: " + string_1, LoggerType.Warning);
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
				int int_ = num4++;
				int int_2 = num3;
				if (num4 == 4)
				{
					num4 = 0;
					num3++;
				}
				syncClientPacket.ReadUH();
				int missionType = syncClientPacket.ReadC();
				int mapId = syncClientPacket.ReadC();
				int missionLimit = syncClientPacket.ReadC();
				ClassType weaponReq = (ClassType)syncClientPacket.ReadC();
				int weaponReqId = syncClientPacket.ReadUH();
				MissionCardModel item = new MissionCardModel(int_2, int_)
				{
					MapId = mapId,
					WeaponReq = weaponReq,
					WeaponReqId = weaponReqId,
					MissionType = (MissionType)missionType,
					MissionLimit = missionLimit,
					MissionId = num
				};
				list_1.Add(item);
				if (num2 == 1)
				{
					syncClientPacket.ReadB(24);
				}
			}
			int num5 = ((num2 != 2) ? 1 : 5);
			for (int j = 0; j < 10; j++)
			{
				int gold = syncClientPacket.ReadD();
				int num6 = syncClientPacket.ReadD();
				int int_3 = syncClientPacket.ReadD();
				for (int k = 0; k < num5; k++)
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
						Exp = ((num2 == 1) ? (num6 * 10) : num6),
						Gold = gold
					};
					smethod_3(missionCardAwards, int_3);
					if (!missionCardAwards.Unusable())
					{
						list_2.Add(missionCardAwards);
					}
				}
			}
			if (num2 != 2)
			{
				return;
			}
			syncClientPacket.ReadD();
			syncClientPacket.ReadB(8);
			for (int l = 0; l < 5; l++)
			{
				int num7 = syncClientPacket.ReadD();
				syncClientPacket.ReadD();
				int int_4 = syncClientPacket.ReadD();
				uint uint_ = syncClientPacket.ReadUD();
				if (num7 > 0 && int_0 == 1)
				{
					MissionItemAward item2 = new MissionItemAward
					{
						MissionId = num,
						Item = new ItemsModel(int_4, "Mission Item", ItemEquipType.Durable, uint_)
					};
					list_0.Add(item2);
				}
			}
		}
		catch (XmlException ex)
		{
			CLogger.Print("File error: " + string_0 + "; " + ex.Message, LoggerType.Error, ex);
		}
	}

	private static void smethod_3(MissionCardAwards missionCardAwards_0, int int_0)
	{
		switch (int_0)
		{
		case 0:
			return;
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
		case 9:
		case 10:
		case 11:
		case 12:
		case 13:
		case 14:
		case 15:
		case 16:
		case 17:
		case 18:
		case 19:
		case 20:
		case 21:
		case 22:
		case 23:
		case 24:
		case 25:
		case 26:
		case 27:
		case 28:
		case 29:
		case 30:
		case 31:
		case 32:
		case 33:
		case 34:
		case 35:
		case 36:
		case 37:
		case 38:
		case 39:
		case 40:
		case 41:
		case 42:
		case 43:
		case 44:
		case 45:
		case 46:
		case 47:
		case 48:
		case 49:
		case 50:
			missionCardAwards_0.Ribbon++;
			return;
		}
		if (int_0 >= 51 && int_0 <= 100)
		{
			missionCardAwards_0.Ensign++;
		}
		else if (int_0 >= 101 && int_0 <= 116)
		{
			missionCardAwards_0.Medal++;
		}
	}

	public static MissionCardAwards GetAward(int mission, int cartao)
	{
		foreach (MissionCardAwards item in list_2)
		{
			if (item.Id == mission && item.Card == cartao)
			{
				return item;
			}
		}
		return null;
	}
}
