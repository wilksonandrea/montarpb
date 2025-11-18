namespace Plugin.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class BattlePassModel
    {
        public BattlePassModel()
        {
            this.Name = "";
        }

        public int GetCardCount()
        {
            int num = 0;
            using (List<PassBoxModel>.Enumerator enumerator = this.Cards.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current.RewardCount <= 0)
                    {
                        continue;
                    }
                    num++;
                }
            }
            return num;
        }

        public (int, int, int, int) GetLevelProgression(int PassPoints)
        {
            Class12 class2 = new Class12();
            int card = 0;
            class2.int_0 = 0;
            int requiredPoints = 0;
            int num3 = 0;
            int requiredPoints = 0;
            Func<PassBoxModel, int> keySelector = Class11.<>9__33_0;
            if (Class11.<>9__33_0 == null)
            {
                Func<PassBoxModel, int> local1 = Class11.<>9__33_0;
                keySelector = Class11.<>9__33_0 = new Func<PassBoxModel, int>(Class11.<>9.method_0);
            }
            List<PassBoxModel> source = this.Cards.OrderBy<PassBoxModel, int>(keySelector).ToList<PassBoxModel>();
            foreach (PassBoxModel model in source)
            {
                if (PassPoints < model.RequiredPoints)
                {
                    class2.int_0 = model.Card;
                    num3 = model.RequiredPoints - (PassPoints - requiredPoints);
                    requiredPoints = requiredPoints + model.RequiredPoints;
                    break;
                }
                card = model.Card;
                requiredPoints = model.RequiredPoints;
            }
            if ((class2.int_0 == 0) && source.Any<PassBoxModel>())
            {
                class2.int_0 = source.Last<PassBoxModel>().Card + 1;
                num3 = 0;
                requiredPoints = requiredPoints;
            }
            else if ((card > 0) && (class2.int_0 == 0))
            {
                class2.int_0 = card + 1;
            }
            else if ((card == 0) && ((class2.int_0 == 0) && source.Any<PassBoxModel>()))
            {
                class2.int_0 = source.First<PassBoxModel>().Card;
                num3 = source.First<PassBoxModel>().RequiredPoints - PassPoints;
                requiredPoints = source.First<PassBoxModel>().RequiredPoints;
            }
            else if (class2.int_0 <= 0)
            {
                if ((card > 0) && ((class2.int_0 == 0) && !source.Any<PassBoxModel>()))
                {
                    num3 = 0;
                    requiredPoints = requiredPoints;
                }
                else if ((card == 0) && (class2.int_0 > 0))
                {
                    int num8 = source.FindIndex(new Predicate<PassBoxModel>(class2.method_2));
                    if (num8 >= 0)
                    {
                        num3 = source[num8].RequiredPoints - PassPoints;
                        requiredPoints = source[num8].RequiredPoints;
                    }
                }
            }
            else
            {
                int count = source.FindIndex(new Predicate<PassBoxModel>(class2.method_0));
                if (count < 0)
                {
                    if ((card > 0) && !source.Any<PassBoxModel>(new Func<PassBoxModel, bool>(class2.method_1)))
                    {
                        num3 = 0;
                        requiredPoints = requiredPoints;
                    }
                }
                else
                {
                    int requiredPoints = source[count].RequiredPoints;
                    int num7 = 0;
                    if (count > 0)
                    {
                        Func<PassBoxModel, int> selector = Class11.<>9__33_3;
                        if (Class11.<>9__33_3 == null)
                        {
                            Func<PassBoxModel, int> local2 = Class11.<>9__33_3;
                            selector = Class11.<>9__33_3 = new Func<PassBoxModel, int>(Class11.<>9.method_1);
                        }
                        num7 = source.Take<PassBoxModel>(count).Sum<PassBoxModel>(selector);
                    }
                    num3 = requiredPoints - (PassPoints - num7);
                    requiredPoints = num7 + requiredPoints;
                }
            }
            return (card, class2.int_0, num3, requiredPoints);
        }

        public List<PassItemModel> GetReward(int Level, bool IsPremium)
        {
            List<PassItemModel> list = new List<PassItemModel>();
            if (!IsPremium)
            {
                list.Add(this.Cards[Level].Normal);
            }
            else
            {
                list.Add(this.Cards[Level].Normal);
                list.Add(this.Cards[Level].PremiumA);
                list.Add(this.Cards[Level].PremiumB);
            }
            return list;
        }

        public bool IsCompleted(int PassPoints)
        {
            if ((this.Cards == null) || !this.Cards.Any<PassBoxModel>())
            {
                return true;
            }
            Func<PassBoxModel, int> selector = Class11.<>9__34_0;
            if (Class11.<>9__34_0 == null)
            {
                Func<PassBoxModel, int> local1 = Class11.<>9__34_0;
                selector = Class11.<>9__34_0 = new Func<PassBoxModel, int>(Class11.<>9.method_2);
            }
            int num = this.Cards.Sum<PassBoxModel>(selector);
            return (PassPoints >= num);
        }

        public bool SeasonIsEnabled()
        {
            uint num = uint.Parse(DateTime.Now.ToString("yyMMddHHmm"));
            return ((this.BeginDate <= num) && (num < this.EndedDate));
        }

        public void SetBoxCounts()
        {
            for (int i = 0; i < 0x63; i++)
            {
                this.Cards[i].SetCount();
            }
        }

        public int Id { get; set; }

        public int MaxDailyPoints { get; set; }

        public string Name { get; set; }

        public uint BeginDate { get; set; }

        public uint EndedDate { get; set; }

        public bool Enable { get; set; }

        public List<PassBoxModel> Cards { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class Class11
        {
            public static readonly BattlePassModel.Class11 <>9 = new BattlePassModel.Class11();
            public static Func<PassBoxModel, int> <>9__33_0;
            public static Func<PassBoxModel, int> <>9__33_3;
            public static Func<PassBoxModel, int> <>9__34_0;

            internal int method_0(PassBoxModel passBoxModel_0) => 
                passBoxModel_0.Card;

            internal int method_1(PassBoxModel passBoxModel_0) => 
                passBoxModel_0.RequiredPoints;

            internal int method_2(PassBoxModel passBoxModel_0) => 
                passBoxModel_0.RequiredPoints;
        }

        [CompilerGenerated]
        private sealed class Class12
        {
            public int int_0;

            internal bool method_0(PassBoxModel passBoxModel_0) => 
                passBoxModel_0.Card == this.int_0;

            internal bool method_1(PassBoxModel passBoxModel_0) => 
                passBoxModel_0.Card == this.int_0;

            internal bool method_2(PassBoxModel passBoxModel_0) => 
                passBoxModel_0.Card == this.int_0;
        }
    }
}

