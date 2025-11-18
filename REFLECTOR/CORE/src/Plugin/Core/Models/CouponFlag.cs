namespace Plugin.Core.Models
{
    using Plugin.Core.Enums;
    using System;
    using System.Runtime.CompilerServices;

    public class CouponFlag
    {
        public int ItemId { get; set; }

        public CouponEffects EffectFlag { get; set; }
    }
}

