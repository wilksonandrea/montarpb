using System;

namespace Plugin.Core.Enums
{
	// Token: 0x020000DC RID: 220
	[Flags]
	public enum RoomWeaponsFlag
	{
		// Token: 0x04000659 RID: 1625
		None = 0,
		// Token: 0x0400065A RID: 1626
		Grenade = 1,
		// Token: 0x0400065B RID: 1627
		Melee = 2,
		// Token: 0x0400065C RID: 1628
		Secondary = 4,
		// Token: 0x0400065D RID: 1629
		Accessory = 8,
		// Token: 0x0400065E RID: 1630
		Assault = 16,
		// Token: 0x0400065F RID: 1631
		SMG = 32,
		// Token: 0x04000660 RID: 1632
		Sniper = 64,
		// Token: 0x04000661 RID: 1633
		Shotgun = 128,
		// Token: 0x04000662 RID: 1634
		Machinegun = 256,
		// Token: 0x04000663 RID: 1635
		Barefist = 512,
		// Token: 0x04000664 RID: 1636
		RPG7 = 1024,
		// Token: 0x04000665 RID: 1637
		Shield = 2048
	}
}
