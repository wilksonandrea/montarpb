namespace Plugin.Core.Models
{
    using Plugin.Core.Enums;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FragInfos
    {
        public FragInfos()
        {
            this.Frags = new List<FragModel>();
        }

        public KillingMessage GetAllKillFlags()
        {
            KillingMessage none = KillingMessage.None;
            foreach (FragModel model in this.Frags)
            {
                if (!none.HasFlag(model.KillFlag))
                {
                    none |= model.KillFlag;
                }
            }
            return none;
        }

        public byte KillerSlot { get; set; }

        public byte KillsCount { get; set; }

        public byte Flag { get; set; }

        public byte Unk { get; set; }

        public CharaKillType KillingType { get; set; }

        public int WeaponId { get; set; }

        public int Score { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public List<FragModel> Frags { get; set; }
    }
}

