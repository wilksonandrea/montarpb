using Plugin.Core;
using Plugin.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class PlayerCharacters
	{
		public List<CharacterModel> Characters
		{
			get;
			set;
		}

		public PlayerCharacters()
		{
			this.Characters = new List<CharacterModel>();
		}

		public void AddCharacter(CharacterModel Character)
		{
			lock (this.Characters)
			{
				this.Characters.Add(Character);
			}
		}

		public int GenSlotId(int ItemId)
		{
			int ınt32 = -1;
			if (ItemId == 601001)
			{
				ınt32 = 0;
			}
			else if (ItemId != 602002)
			{
				int ınt321 = 2;
				while (ınt321 < this.Characters.Count + 1)
				{
					bool flag = false;
					foreach (CharacterModel character in this.Characters)
					{
						if (character.Slot != ınt321)
						{
							continue;
						}
						flag = true;
						goto Label0;
					}
				Label0:
					if (flag)
					{
						ınt321++;
					}
					else
					{
						ınt32 = ınt321;
						break;
					}
				}
				if (ınt32 == -1)
				{
					CLogger.Print(string.Format("No available slot for character ID: {0}", ItemId), LoggerType.Warning, null);
					return -1;
				}
			}
			else
			{
				ınt32 = 1;
			}
			if (ItemId == 601001)
			{
				if (this.Characters.Any<CharacterModel>((CharacterModel characterModel_0) => characterModel_0.Slot == 0))
				{
					CLogger.Print(string.Format("Desired slot for character ID: {0} is already taken.", ItemId), LoggerType.Warning, null);
					return -1;
				}
			}
			if (ItemId == 602002)
			{
				if (this.Characters.Any<CharacterModel>((CharacterModel characterModel_0) => characterModel_0.Slot == 1))
				{
					CLogger.Print(string.Format("Desired slot for character ID: {0} is already taken.", ItemId), LoggerType.Warning, null);
					return -1;
				}
			}
			return ınt32;
		}

		public CharacterModel GetCharacter(int Id)
		{
			CharacterModel characterModel;
			lock (this.Characters)
			{
				List<CharacterModel>.Enumerator enumerator = this.Characters.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						CharacterModel current = enumerator.Current;
						if (current.Id != Id)
						{
							continue;
						}
						characterModel = current;
						return characterModel;
					}
					return null;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
			}
			return characterModel;
		}

		public CharacterModel GetCharacter(int Slot, out int Index)
		{
			CharacterModel characterModel;
			lock (this.Characters)
			{
				int ınt32 = 0;
				while (ınt32 < this.Characters.Count)
				{
					CharacterModel ıtem = this.Characters[ınt32];
					if (ıtem.Slot != Slot)
					{
						ınt32++;
					}
					else
					{
						Index = ınt32;
						characterModel = ıtem;
						return characterModel;
					}
				}
				Index = -1;
				return null;
			}
			return characterModel;
		}

		public int GetCharacterIdx(int Slot)
		{
			int ınt32;
			lock (this.Characters)
			{
				int ınt321 = 0;
				while (ınt321 < this.Characters.Count)
				{
					if (this.Characters[ınt321].Slot != Slot)
					{
						ınt321++;
					}
					else
					{
						ınt32 = ınt321;
						return ınt32;
					}
				}
				return -1;
			}
			return ınt32;
		}

		public CharacterModel GetCharacterSlot(int Slot)
		{
			CharacterModel characterModel;
			lock (this.Characters)
			{
				List<CharacterModel>.Enumerator enumerator = this.Characters.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						CharacterModel current = enumerator.Current;
						if (current.Slot != Slot)
						{
							continue;
						}
						characterModel = current;
						return characterModel;
					}
					return null;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
			}
			return characterModel;
		}

		public bool RemoveCharacter(CharacterModel Character)
		{
			bool flag;
			lock (this.Characters)
			{
				flag = this.Characters.Remove(Character);
			}
			return flag;
		}
	}
}