namespace Plugin.Core.Enums
{
    using System;

    public enum AccountFeatures : uint
    {
        PLAYTIME_ONLY = 0x100,
        CLAN_ONLY = 0x1000,
        TICKET_ONLY = 0x4000,
        CLAN_COUPON = 0x777e,
        TAGS_ONLY = 0x4000000,
        TOKEN_ONLY = 0x7e770000,
        TOKEN_CLAN = 0x7e779934,
        RATING_BOTH = 0x7ffffef8,
        TEST_MODE = 0x7ffffef9,
        FROM_SNIFF = 0x8e66777a,
        ALL = 0x8e66777e
    }
}

