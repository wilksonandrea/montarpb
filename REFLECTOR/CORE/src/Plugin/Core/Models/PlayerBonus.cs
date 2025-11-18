namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class PlayerBonus
    {
        public PlayerBonus()
        {
            this.CrosshairColor = 4;
            this.FakeRank = 0x37;
            this.FakeNick = "";
        }

        public bool AddBonuses(int ItemId)
        {
            int bonuses = this.Bonuses;
            int freePass = this.FreePass;
            if (ItemId <= 0x186a25)
            {
                switch (ItemId)
                {
                    case 0x186a01:
                        this.method_1(1);
                        break;

                    case 0x186a02:
                        this.method_1(2);
                        break;

                    case 0x186a03:
                        this.method_1(4);
                        break;

                    case 0x186a04:
                        this.method_1(0x10);
                        break;

                    default:
                        if (ItemId == 0x186a0b)
                        {
                            this.method_3(0x80);
                        }
                        else if (ItemId == 0x186a25)
                        {
                            this.method_1(8);
                        }
                        break;
                }
            }
            else if (ItemId == 0x186a26)
            {
                this.method_1(0x40);
            }
            else if (ItemId == 0x186a77)
            {
                this.method_1(0x20);
            }
            else
            {
                switch (ItemId)
                {
                    case 0x186ac9:
                        this.method_1(0x200);
                        break;

                    case 0x186aca:
                        this.method_1(0x400);
                        break;

                    case 0x186acb:
                        this.method_1(0x800);
                        break;

                    case 0x186acc:
                        this.method_1(0x1000);
                        break;

                    default:
                        break;
                }
            }
            return ((this.Bonuses != bonuses) || (this.FreePass != freePass));
        }

        private void method_0(int int_6)
        {
            this.Bonuses &= ~int_6;
        }

        private void method_1(int int_6)
        {
            this.Bonuses |= int_6;
        }

        private void method_2(int int_6)
        {
            this.FreePass &= ~int_6;
        }

        private void method_3(int int_6)
        {
            this.FreePass |= int_6;
        }

        public bool RemoveBonuses(int ItemId)
        {
            int bonuses = this.Bonuses;
            int freePass = this.FreePass;
            if (ItemId <= 0x186a25)
            {
                switch (ItemId)
                {
                    case 0x186a01:
                        this.method_0(1);
                        break;

                    case 0x186a02:
                        this.method_0(2);
                        break;

                    case 0x186a03:
                        this.method_0(4);
                        break;

                    case 0x186a04:
                        this.method_0(0x10);
                        break;

                    default:
                        if (ItemId == 0x186a0b)
                        {
                            this.method_2(0x80);
                        }
                        else if (ItemId == 0x186a25)
                        {
                            this.method_0(8);
                        }
                        break;
                }
            }
            else if (ItemId == 0x186a26)
            {
                this.method_0(0x40);
            }
            else if (ItemId == 0x186a77)
            {
                this.method_0(0x20);
            }
            else
            {
                switch (ItemId)
                {
                    case 0x186ac9:
                        this.method_0(0x200);
                        break;

                    case 0x186aca:
                        this.method_0(0x400);
                        break;

                    case 0x186acb:
                        this.method_0(0x800);
                        break;

                    case 0x186acc:
                        this.method_0(0x1000);
                        break;

                    default:
                        break;
                }
            }
            return ((this.Bonuses != bonuses) || (this.FreePass != freePass));
        }

        public long OwnerId { get; set; }

        public int Bonuses { get; set; }

        public int CrosshairColor { get; set; }

        public int MuzzleColor { get; set; }

        public int FreePass { get; set; }

        public int FakeRank { get; set; }

        public int NickBorderColor { get; set; }

        public string FakeNick { get; set; }
    }
}

