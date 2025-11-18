namespace Plugin.Core.Models
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class PlayerCharacters
    {
        public PlayerCharacters()
        {
            this.Characters = new List<CharacterModel>();
        }

        public void AddCharacter(CharacterModel Character)
        {
            List<CharacterModel> characters = this.Characters;
            lock (characters)
            {
                this.Characters.Add(Character);
            }
        }

        public int GenSlotId(int ItemId)
        {
            int num = -1;
            if (ItemId != 0x92ba9)
            {
                if (ItemId != 0x92f92)
                {
                    for (int i = 2; i < (this.Characters.Count + 1); i++)
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
                        }
                        if (!flag)
                        {
                            num = i;
                            break;
                        }
                    }
                }
                else
                {
                    num = 1;
                    goto TR_000B;
                }
            }
            else
            {
                num = 0;
                goto TR_000B;
            }
            if (num == -1)
            {
                CLogger.Print($"No available slot for character ID: {ItemId}", LoggerType.Warning, null);
                return -1;
            }
            goto TR_000B;
        TR_0000:
            CLogger.Print($"Desired slot for character ID: {ItemId} is already taken.", LoggerType.Warning, null);
            return -1;
        TR_000B:
            if (ItemId == 0x92ba9)
            {
                Func<CharacterModel, bool> predicate = Class10.<>9__11_0;
                if (Class10.<>9__11_0 == null)
                {
                    Func<CharacterModel, bool> local1 = Class10.<>9__11_0;
                    predicate = Class10.<>9__11_0 = new Func<CharacterModel, bool>(Class10.<>9.method_0);
                }
                if (this.Characters.Any<CharacterModel>(predicate))
                {
                    goto TR_0000;
                }
            }
            if (ItemId == 0x92f92)
            {
                Func<CharacterModel, bool> predicate = Class10.<>9__11_1;
                if (Class10.<>9__11_1 == null)
                {
                    Func<CharacterModel, bool> local2 = Class10.<>9__11_1;
                    predicate = Class10.<>9__11_1 = new Func<CharacterModel, bool>(Class10.<>9.method_1);
                }
                if (this.Characters.Any<CharacterModel>(predicate))
                {
                    goto TR_0000;
                }
            }
            return num;
        }

        public CharacterModel GetCharacter(int Id)
        {
            CharacterModel model2;
            List<CharacterModel> characters = this.Characters;
            lock (characters)
            {
                using (List<CharacterModel>.Enumerator enumerator = this.Characters.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            CharacterModel current = enumerator.Current;
                            if (current.Id != Id)
                            {
                                continue;
                            }
                            model2 = current;
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    }
                }
            }
            return model2;
        }

        public CharacterModel GetCharacter(int Slot, out int Index)
        {
            CharacterModel model2;
            List<CharacterModel> characters = this.Characters;
            lock (characters)
            {
                int num = 0;
                while (true)
                {
                    if (num < this.Characters.Count)
                    {
                        CharacterModel model = this.Characters[num];
                        if (model.Slot != Slot)
                        {
                            num++;
                            continue;
                        }
                        Index = num;
                        model2 = model;
                    }
                    else
                    {
                        Index = -1;
                        return null;
                    }
                    break;
                }
            }
            return model2;
        }

        public int GetCharacterIdx(int Slot)
        {
            int num2;
            List<CharacterModel> characters = this.Characters;
            lock (characters)
            {
                int num = 0;
                while (true)
                {
                    if (num < this.Characters.Count)
                    {
                        if (this.Characters[num].Slot != Slot)
                        {
                            num++;
                            continue;
                        }
                        num2 = num;
                    }
                    else
                    {
                        return -1;
                    }
                    break;
                }
            }
            return num2;
        }

        public CharacterModel GetCharacterSlot(int Slot)
        {
            CharacterModel model2;
            List<CharacterModel> characters = this.Characters;
            lock (characters)
            {
                using (List<CharacterModel>.Enumerator enumerator = this.Characters.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            CharacterModel current = enumerator.Current;
                            if (current.Slot != Slot)
                            {
                                continue;
                            }
                            model2 = current;
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    }
                }
            }
            return model2;
        }

        public bool RemoveCharacter(CharacterModel Character)
        {
            List<CharacterModel> characters = this.Characters;
            lock (characters)
            {
                return this.Characters.Remove(Character);
            }
        }

        public List<CharacterModel> Characters { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class Class10
        {
            public static readonly PlayerCharacters.Class10 <>9 = new PlayerCharacters.Class10();
            public static Func<CharacterModel, bool> <>9__11_0;
            public static Func<CharacterModel, bool> <>9__11_1;

            internal bool method_0(CharacterModel characterModel_0) => 
                characterModel_0.Slot == 0;

            internal bool method_1(CharacterModel characterModel_0) => 
                characterModel_0.Slot == 1;
        }
    }
}

