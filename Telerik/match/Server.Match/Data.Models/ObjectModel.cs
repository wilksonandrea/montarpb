using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Server.Match.Data.Models
{
	public class ObjectModel
	{
		public int Animation
		{
			get;
			set;
		}

		public List<AnimModel> Animations
		{
			get;
			set;
		}

		public bool Destroyable
		{
			get;
			set;
		}

		public List<DeffectModel> Effects
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public int Life
		{
			get;
			set;
		}

		public bool NeedSync
		{
			get;
			set;
		}

		public bool NoInstaSync
		{
			get;
			set;
		}

		public int UltraSync
		{
			get;
			set;
		}

		public int UpdateId
		{
			get;
			set;
		}

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

		public int CheckDestroyState(int Life)
		{
			for (int i = this.Effects.Count - 1; i > -1; i--)
			{
				DeffectModel ıtem = this.Effects[i];
				if (ıtem.Life >= Life)
				{
					return ıtem.Id;
				}
			}
			return 0;
		}

		public void GetAnim(int AnimId, float Time, float Duration, ObjectInfo Obj)
		{
			if (AnimId == 255 || Obj == null || Obj.Model == null || Obj.Model.Animations == null || Obj.Model.Animations.Count == 0)
			{
				return;
			}
			foreach (AnimModel animation in Obj.Model.Animations)
			{
				if (animation.Id != AnimId)
				{
					continue;
				}
				Obj.Animation = animation;
				Time -= Duration;
				DateTime dateTime = DateTimeUtil.Now();
				Obj.UseDate = dateTime.AddSeconds((double)(Time * -1f));
				return;
			}
		}

		public int GetRandomAnimation(RoomModel Room, ObjectInfo Obj)
		{
			if (this.Animations == null || this.Animations.Count <= 0)
			{
				Obj.Animation = null;
				return 255;
			}
			int ınt32 = (new Random()).Next(this.Animations.Count);
			AnimModel ıtem = this.Animations[ınt32];
			Obj.Animation = ıtem;
			Obj.UseDate = DateTimeUtil.Now();
			if (ıtem.OtherObj > 0)
			{
				ObjectInfo objects = Room.Objects[ıtem.OtherObj];
				this.GetAnim(ıtem.OtherAnim, 0f, 0f, objects);
			}
			return ıtem.Id;
		}
	}
}