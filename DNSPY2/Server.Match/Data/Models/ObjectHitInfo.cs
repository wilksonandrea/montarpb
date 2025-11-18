using System;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using Server.Match.Data.Enums;

namespace Server.Match.Data.Models
{
	// Token: 0x0200003A RID: 58
	public class ObjectHitInfo
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00002667 File Offset: 0x00000867
		// (set) Token: 0x06000126 RID: 294 RVA: 0x0000266F File Offset: 0x0000086F
		public int Type
		{
			[CompilerGenerated]
			get
			{
				return this.int_0;
			}
			[CompilerGenerated]
			set
			{
				this.int_0 = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00002678 File Offset: 0x00000878
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00002680 File Offset: 0x00000880
		public int ObjSyncId
		{
			[CompilerGenerated]
			get
			{
				return this.int_1;
			}
			[CompilerGenerated]
			set
			{
				this.int_1 = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00002689 File Offset: 0x00000889
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00002691 File Offset: 0x00000891
		public int ObjId
		{
			[CompilerGenerated]
			get
			{
				return this.int_2;
			}
			[CompilerGenerated]
			set
			{
				this.int_2 = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000269A File Offset: 0x0000089A
		// (set) Token: 0x0600012C RID: 300 RVA: 0x000026A2 File Offset: 0x000008A2
		public int ObjLife
		{
			[CompilerGenerated]
			get
			{
				return this.int_3;
			}
			[CompilerGenerated]
			set
			{
				this.int_3 = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000026AB File Offset: 0x000008AB
		// (set) Token: 0x0600012E RID: 302 RVA: 0x000026B3 File Offset: 0x000008B3
		public int KillerSlot
		{
			[CompilerGenerated]
			get
			{
				return this.int_4;
			}
			[CompilerGenerated]
			set
			{
				this.int_4 = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000026BC File Offset: 0x000008BC
		// (set) Token: 0x06000130 RID: 304 RVA: 0x000026C4 File Offset: 0x000008C4
		public int AnimId1
		{
			[CompilerGenerated]
			get
			{
				return this.int_5;
			}
			[CompilerGenerated]
			set
			{
				this.int_5 = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000026CD File Offset: 0x000008CD
		// (set) Token: 0x06000132 RID: 306 RVA: 0x000026D5 File Offset: 0x000008D5
		public int AnimId2
		{
			[CompilerGenerated]
			get
			{
				return this.int_6;
			}
			[CompilerGenerated]
			set
			{
				this.int_6 = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000026DE File Offset: 0x000008DE
		// (set) Token: 0x06000134 RID: 308 RVA: 0x000026E6 File Offset: 0x000008E6
		public int DestroyState
		{
			[CompilerGenerated]
			get
			{
				return this.int_7;
			}
			[CompilerGenerated]
			set
			{
				this.int_7 = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000026EF File Offset: 0x000008EF
		// (set) Token: 0x06000136 RID: 310 RVA: 0x000026F7 File Offset: 0x000008F7
		public int WeaponId
		{
			[CompilerGenerated]
			get
			{
				return this.int_8;
			}
			[CompilerGenerated]
			set
			{
				this.int_8 = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00002700 File Offset: 0x00000900
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00002708 File Offset: 0x00000908
		public byte Accessory
		{
			[CompilerGenerated]
			get
			{
				return this.byte_0;
			}
			[CompilerGenerated]
			set
			{
				this.byte_0 = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00002711 File Offset: 0x00000911
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00002719 File Offset: 0x00000919
		public byte Extensions
		{
			[CompilerGenerated]
			get
			{
				return this.byte_1;
			}
			[CompilerGenerated]
			set
			{
				this.byte_1 = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00002722 File Offset: 0x00000922
		// (set) Token: 0x0600013C RID: 316 RVA: 0x0000272A File Offset: 0x0000092A
		public float SpecialUse
		{
			[CompilerGenerated]
			get
			{
				return this.float_0;
			}
			[CompilerGenerated]
			set
			{
				this.float_0 = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00002733 File Offset: 0x00000933
		// (set) Token: 0x0600013E RID: 318 RVA: 0x0000273B File Offset: 0x0000093B
		public Half3 Position
		{
			[CompilerGenerated]
			get
			{
				return this.half3_0;
			}
			[CompilerGenerated]
			set
			{
				this.half3_0 = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00002744 File Offset: 0x00000944
		// (set) Token: 0x06000140 RID: 320 RVA: 0x0000274C File Offset: 0x0000094C
		public ClassType WeaponClass
		{
			[CompilerGenerated]
			get
			{
				return this.classType_0;
			}
			[CompilerGenerated]
			set
			{
				this.classType_0 = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00002755 File Offset: 0x00000955
		// (set) Token: 0x06000142 RID: 322 RVA: 0x0000275D File Offset: 0x0000095D
		public CharaHitPart HitPart
		{
			[CompilerGenerated]
			get
			{
				return this.charaHitPart_0;
			}
			[CompilerGenerated]
			set
			{
				this.charaHitPart_0 = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00002766 File Offset: 0x00000966
		// (set) Token: 0x06000144 RID: 324 RVA: 0x0000276E File Offset: 0x0000096E
		public CharaDeath DeathType
		{
			[CompilerGenerated]
			get
			{
				return this.charaDeath_0;
			}
			[CompilerGenerated]
			set
			{
				this.charaDeath_0 = value;
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00002777 File Offset: 0x00000977
		public ObjectHitInfo(int int_9)
		{
			this.Type = int_9;
			this.DeathType = CharaDeath.DEFAULT;
		}

		// Token: 0x0400003D RID: 61
		[CompilerGenerated]
		private int int_0;

		// Token: 0x0400003E RID: 62
		[CompilerGenerated]
		private int int_1;

		// Token: 0x0400003F RID: 63
		[CompilerGenerated]
		private int int_2;

		// Token: 0x04000040 RID: 64
		[CompilerGenerated]
		private int int_3;

		// Token: 0x04000041 RID: 65
		[CompilerGenerated]
		private int int_4;

		// Token: 0x04000042 RID: 66
		[CompilerGenerated]
		private int int_5;

		// Token: 0x04000043 RID: 67
		[CompilerGenerated]
		private int int_6;

		// Token: 0x04000044 RID: 68
		[CompilerGenerated]
		private int int_7;

		// Token: 0x04000045 RID: 69
		[CompilerGenerated]
		private int int_8;

		// Token: 0x04000046 RID: 70
		[CompilerGenerated]
		private byte byte_0;

		// Token: 0x04000047 RID: 71
		[CompilerGenerated]
		private byte byte_1;

		// Token: 0x04000048 RID: 72
		[CompilerGenerated]
		private float float_0;

		// Token: 0x04000049 RID: 73
		[CompilerGenerated]
		private Half3 half3_0;

		// Token: 0x0400004A RID: 74
		[CompilerGenerated]
		private ClassType classType_0;

		// Token: 0x0400004B RID: 75
		[CompilerGenerated]
		private CharaHitPart charaHitPart_0;

		// Token: 0x0400004C RID: 76
		[CompilerGenerated]
		private CharaDeath charaDeath_0;
	}
}
