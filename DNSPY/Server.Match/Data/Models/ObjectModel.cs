using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core.Utility;

namespace Server.Match.Data.Models
{
	// Token: 0x0200003C RID: 60
	public class ObjectModel
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000280A File Offset: 0x00000A0A
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00002812 File Offset: 0x00000A12
		public int Id
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000281B File Offset: 0x00000A1B
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00002823 File Offset: 0x00000A23
		public int Life
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

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000282C File Offset: 0x00000A2C
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00002834 File Offset: 0x00000A34
		public int Animation
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

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000283D File Offset: 0x00000A3D
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00002845 File Offset: 0x00000A45
		public int UltraSync
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000284E File Offset: 0x00000A4E
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00002856 File Offset: 0x00000A56
		public int UpdateId
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000285F File Offset: 0x00000A5F
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00002867 File Offset: 0x00000A67
		public bool NeedSync
		{
			[CompilerGenerated]
			get
			{
				return this.bool_0;
			}
			[CompilerGenerated]
			set
			{
				this.bool_0 = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00002870 File Offset: 0x00000A70
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00002878 File Offset: 0x00000A78
		public bool Destroyable
		{
			[CompilerGenerated]
			get
			{
				return this.bool_1;
			}
			[CompilerGenerated]
			set
			{
				this.bool_1 = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00002881 File Offset: 0x00000A81
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00002889 File Offset: 0x00000A89
		public bool NoInstaSync
		{
			[CompilerGenerated]
			get
			{
				return this.bool_2;
			}
			[CompilerGenerated]
			set
			{
				this.bool_2 = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00002892 File Offset: 0x00000A92
		// (set) Token: 0x06000164 RID: 356 RVA: 0x0000289A File Offset: 0x00000A9A
		public List<AnimModel> Animations
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

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000028A3 File Offset: 0x00000AA3
		// (set) Token: 0x06000166 RID: 358 RVA: 0x000028AB File Offset: 0x00000AAB
		public List<DeffectModel> Effects
		{
			[CompilerGenerated]
			get
			{
				return this.list_1;
			}
			[CompilerGenerated]
			set
			{
				this.list_1 = value;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000028B4 File Offset: 0x00000AB4
		public ObjectModel(bool bool_3)
		{
			this.NeedSync = bool_3;
			if (bool_3)
			{
				this.Animations = new List<AnimModel>();
			}
			this.UpdateId = 1;
			this.Effects = new List<DeffectModel>();
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000A924 File Offset: 0x00008B24
		public int CheckDestroyState(int Life)
		{
			for (int i = this.Effects.Count - 1; i > -1; i--)
			{
				DeffectModel deffectModel = this.Effects[i];
				if (deffectModel.Life >= Life)
				{
					return deffectModel.Id;
				}
			}
			return 0;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000A968 File Offset: 0x00008B68
		public int GetRandomAnimation(RoomModel Room, ObjectInfo Obj)
		{
			if (this.Animations != null && this.Animations.Count > 0)
			{
				int num = new Random().Next(this.Animations.Count);
				AnimModel animModel = this.Animations[num];
				Obj.Animation = animModel;
				Obj.UseDate = DateTimeUtil.Now();
				if (animModel.OtherObj > 0)
				{
					ObjectInfo objectInfo = Room.Objects[animModel.OtherObj];
					this.GetAnim(animModel.OtherAnim, 0f, 0f, objectInfo);
				}
				return animModel.Id;
			}
			Obj.Animation = null;
			return 255;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000AA04 File Offset: 0x00008C04
		public void GetAnim(int AnimId, float Time, float Duration, ObjectInfo Obj)
		{
			if (AnimId != 255 && Obj != null && Obj.Model != null && Obj.Model.Animations != null && Obj.Model.Animations.Count != 0)
			{
				foreach (AnimModel animModel in Obj.Model.Animations)
				{
					if (animModel.Id == AnimId)
					{
						Obj.Animation = animModel;
						Time -= Duration;
						Obj.UseDate = DateTimeUtil.Now().AddSeconds((double)(Time * -1f));
						break;
					}
				}
				return;
			}
		}

		// Token: 0x04000053 RID: 83
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000054 RID: 84
		[CompilerGenerated]
		private int int_1;

		// Token: 0x04000055 RID: 85
		[CompilerGenerated]
		private int int_2;

		// Token: 0x04000056 RID: 86
		[CompilerGenerated]
		private int int_3;

		// Token: 0x04000057 RID: 87
		[CompilerGenerated]
		private int int_4;

		// Token: 0x04000058 RID: 88
		[CompilerGenerated]
		private bool bool_0;

		// Token: 0x04000059 RID: 89
		[CompilerGenerated]
		private bool bool_1;

		// Token: 0x0400005A RID: 90
		[CompilerGenerated]
		private bool bool_2;

		// Token: 0x0400005B RID: 91
		[CompilerGenerated]
		private List<AnimModel> list_0;

		// Token: 0x0400005C RID: 92
		[CompilerGenerated]
		private List<DeffectModel> list_1;
	}
}
