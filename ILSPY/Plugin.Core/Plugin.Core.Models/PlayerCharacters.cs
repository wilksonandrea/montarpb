using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models;

public class PlayerCharacters
{
	[Serializable]
	[CompilerGenerated]
	private sealed class Class10
	{
		public static readonly Class10 _003C_003E9 = new Class10();

		public static Func<CharacterModel, bool> _003C_003E9__11_0;

		public static Func<CharacterModel, bool> _003C_003E9__11_1;

		internal bool method_0(CharacterModel characterModel_0)
		{
			return characterModel_0.Slot == 0;
		}

		internal bool method_1(CharacterModel characterModel_0)
		{
			return characterModel_0.Slot == 1;
		}
	}

	[CompilerGenerated]
	private List<CharacterModel> list_0;

	public List<CharacterModel> Characters
	{
		[CompilerGenerated]
		get
		{
			return list_0;
		}
		[CompilerGenerated]
		set
		{
			list_0 = value;
		}
	}

	public PlayerCharacters()
	{
		Characters = new List<CharacterModel>();
	}

	public int GetCharacterIdx(int Slot)
	{
		lock (Characters)
		{
			for (int i = 0; i < Characters.Count; i++)
			{
				if (Characters[i].Slot == Slot)
				{
					return i;
				}
			}
		}
		return -1;
	}

	public CharacterModel GetCharacter(int Id)
	{
		lock (Characters)
		{
			foreach (CharacterModel character in Characters)
			{
				if (character.Id == Id)
				{
					return character;
				}
			}
		}
		return null;
	}

	public CharacterModel GetCharacterSlot(int Slot)
	{
		lock (Characters)
		{
			foreach (CharacterModel character in Characters)
			{
				if (character.Slot == Slot)
				{
					return character;
				}
			}
		}
		return null;
	}

	public CharacterModel GetCharacter(int Slot, out int Index)
	{
		lock (Characters)
		{
			for (int i = 0; i < Characters.Count; i++)
			{
				CharacterModel characterModel = Characters[i];
				if (characterModel.Slot == Slot)
				{
					Index = i;
					return characterModel;
				}
			}
		}
		Index = -1;
		return null;
	}

	public void AddCharacter(CharacterModel Character)
	{
		lock (Characters)
		{
			Characters.Add(Character);
		}
	}

	public bool RemoveCharacter(CharacterModel Character)
	{
		lock (Characters)
		{
			return Characters.Remove(Character);
		}
	}

	public int GenSlotId(int ItemId)
	{
		int num = -1;
		switch (ItemId)
		{
		case 601001:
			num = 0;
			break;
		case 602002:
			num = 1;
			break;
		default:
		{
			for (int i = 2; i < Characters.Count + 1; i++)
			{
				bool flag = false;
				foreach (CharacterModel character in Characters)
				{
					if (character.Slot == i)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				CLogger.Print($"No available slot for character ID: {ItemId}", LoggerType.Warning);
				return -1;
			}
			break;
		}
		}
		if ((ItemId == 601001 && Characters.Any((CharacterModel characterModel_0) => characterModel_0.Slot == 0)) || (ItemId == 602002 && Characters.Any((CharacterModel characterModel_0) => characterModel_0.Slot == 1)))
		{
			CLogger.Print($"Desired slot for character ID: {ItemId} is already taken.", LoggerType.Warning);
			return -1;
		}
		return num;
	}
}
