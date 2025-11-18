using System;

namespace Server.Match.Data.Enums
{
	// Token: 0x02000069 RID: 105
	[Flags]
	public enum UdpGameEvent : uint
	{
		// Token: 0x040001D2 RID: 466
		ActionState = 256U,
		// Token: 0x040001D3 RID: 467
		Animation = 2U,
		// Token: 0x040001D4 RID: 468
		PosRotation = 134217728U,
		// Token: 0x040001D5 RID: 469
		SoundPosRotation = 8388608U,
		// Token: 0x040001D6 RID: 470
		UseObject = 4U,
		// Token: 0x040001D7 RID: 471
		ActionForObjectSync = 16U,
		// Token: 0x040001D8 RID: 472
		RadioChat = 32U,
		// Token: 0x040001D9 RID: 473
		WeaponSync = 67108864U,
		// Token: 0x040001DA RID: 474
		WeaponRecoil = 128U,
		// Token: 0x040001DB RID: 475
		HpSync = 8U,
		// Token: 0x040001DC RID: 476
		Suicide = 1048576U,
		// Token: 0x040001DD RID: 477
		MissionData = 2048U,
		// Token: 0x040001DE RID: 478
		RetriveDataForClient = 4096U,
		// Token: 0x040001DF RID: 479
		SeizeDataForClient = 32768U,
		// Token: 0x040001E0 RID: 480
		DropWeapon = 4194304U,
		// Token: 0x040001E1 RID: 481
		GetWeaponForClient = 16777216U,
		// Token: 0x040001E2 RID: 482
		FireData = 33554432U,
		// Token: 0x040001E3 RID: 483
		CharaFireNHitData = 1024U,
		// Token: 0x040001E4 RID: 484
		HitData = 131072U,
		// Token: 0x040001E5 RID: 485
		GrenadeHit = 268435456U,
		// Token: 0x040001E6 RID: 486
		GetWeaponForHost = 512U,
		// Token: 0x040001E7 RID: 487
		FireDataOnObject = 1073741824U,
		// Token: 0x040001E8 RID: 488
		FireNHitDataOnObject = 8192U,
		// Token: 0x040001E9 RID: 489
		BattleRoyalItem = 64U,
		// Token: 0x040001EA RID: 490
		DirectPickUp = 16384U,
		// Token: 0x040001EB RID: 491
		DeathDataForClient = 1024U
	}
}
