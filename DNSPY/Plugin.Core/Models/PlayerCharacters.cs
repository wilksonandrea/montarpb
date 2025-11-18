using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models
{
	// Token: 0x02000061 RID: 97
	public class PlayerCharacters
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x000042CF File Offset: 0x000024CF
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x000042D7 File Offset: 0x000024D7
		public List<CharacterModel> Characters
		{
			[CompilerGenerated]
			get
			{
				return this.list_0;
			}
			[CompilerGenerated]
			set
			{
				this.list_0 = value;
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000042E0 File Offset: 0x000024E0
		public PlayerCharacters()
		{
			this.Characters = new List<CharacterModel>();
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001ADC8 File Offset: 0x00018FC8
		public int GetCharacterIdx(int Slot)
		{
			List<CharacterModel> characters = this.Characters;
			lock (characters)
			{
				for (int i = 0; i < this.Characters.Count; i++)
				{
					if (this.Characters[i].Slot == Slot)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001AE38 File Offset: 0x00019038
		public CharacterModel GetCharacter(int Id)
		{
			List<CharacterModel> characters = this.Characters;
			lock (characters)
			{
				foreach (CharacterModel characterModel in this.Characters)
				{
					if (characterModel.Id == Id)
					{
						return characterModel;
					}
				}
			}
			return null;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001AEC0 File Offset: 0x000190C0
		public CharacterModel GetCharacterSlot(int Slot)
		{
			List<CharacterModel> characters = this.Characters;
			lock (characters)
			{
				foreach (CharacterModel characterModel in this.Characters)
				{
					if (characterModel.Slot == Slot)
					{
						return characterModel;
					}
				}
			}
			return null;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001AF48 File Offset: 0x00019148
		public CharacterModel GetCharacter(int Slot, out int Index)
		{
			List<CharacterModel> characters = this.Characters;
			lock (characters)
			{
				for (int i = 0; i < this.Characters.Count; i++)
				{
					CharacterModel characterModel = this.Characters[i];
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

		// Token: 0x060003ED RID: 1005 RVA: 0x0001AFC0 File Offset: 0x000191C0
		public void AddCharacter(CharacterModel Character)
		{
			List<CharacterModel> characters = this.Characters;
			lock (characters)
			{
				this.Characters.Add(Character);
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001B008 File Offset: 0x00019208
		public bool RemoveCharacter(CharacterModel Character)
		{
			List<CharacterModel> characters = this.Characters;
			bool flag2;
			lock (characters)
			{
				flag2 = this.Characters.Remove(Character);
			}
			return flag2;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001B050 File Offset: 0x00019250
		public int GenSlotId(int ItemId)
		{
			int num = -1;
			if (ItemId == 601001)
			{
				num = 0;
			}
			else if (ItemId == 602002)
			{
				num = 1;
			}
			else
			{
				int i = 2;
				while (i < this.Characters.Count + 1)
				{
					bool flag = false;
					using (List<CharacterModel>.Enumerator enumerator = this.Characters.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.Slot == i)
							{
								flag = true;
								break;
							}
						}
						goto IL_74;
					}
					IL_6E:
					i++;
					continue;
					IL_74:
					if (!flag)
					{
						num = i;
						break;
					}
					goto IL_6E;
				}
				if (num == -1)
				{
					CLogger.Print(string.Format("No available slot for character ID: {0}", ItemId), LoggerType.Warning, null);
					return -1;
				}
			}
			if (ItemId == 601001)
			{
				if (this.Characters.Any(new Func<CharacterModel, bool>(PlayerCharacters.Class10.<>9.method_0)))
				{
					goto IL_FE;
				}
			}
			if (ItemId == 602002)
			{
				if (this.Characters.Any(new Func<CharacterModel, bool>(PlayerCharacters.Class10.<>9.method_1)))
				{
					goto IL_FE;
				}
			}
			return num;
			IL_FE:
			CLogger.Print(string.Format("Desired slot for character ID: {0} is already taken.", ItemId), LoggerType.Warning, null);
			return -1;
		}

		// Token: 0x0400017E RID: 382
		[CompilerGenerated]
		private List<CharacterModel> list_0;

		// Token: 0x02000062 RID: 98
		[CompilerGenerated]
		[Serializable]
		private sealed class Class10
		{
			// Token: 0x060003F0 RID: 1008 RVA: 0x000042F3 File Offset: 0x000024F3
			// Note: this type is marked as 'beforefieldinit'.
			static Class10()
			{
			}

			// Token: 0x060003F1 RID: 1009 RVA: 0x00002116 File Offset: 0x00000316
			public Class10()
			{
			}

			// Token: 0x060003F2 RID: 1010 RVA: 0x000042FF File Offset: 0x000024FF
			internal bool method_0(CharacterModel characterModel_0)
			{
				return characterModel_0.Slot == 0;
			}

			// Token: 0x060003F3 RID: 1011 RVA: 0x0000430A File Offset: 0x0000250A
			internal bool method_1(CharacterModel characterModel_0)
			{
				return characterModel_0.Slot == 1;
			}

			// Token: 0x0400017F RID: 383
			public static readonly PlayerCharacters.Class10 <>9 = new PlayerCharacters.Class10();

			// Token: 0x04000180 RID: 384
			public static Func<CharacterModel, bool> <>9__11_0;

			// Token: 0x04000181 RID: 385
			public static Func<CharacterModel, bool> <>9__11_1;
		}
	}
}
