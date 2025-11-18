using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core.Utility;

namespace Server.Match.Data.Models;

public class ObjectModel
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private int int_2;

	[CompilerGenerated]
	private int int_3;

	[CompilerGenerated]
	private int int_4;

	[CompilerGenerated]
	private bool bool_0;

	[CompilerGenerated]
	private bool bool_1;

	[CompilerGenerated]
	private bool bool_2;

	[CompilerGenerated]
	private List<AnimModel> list_0;

	[CompilerGenerated]
	private List<DeffectModel> list_1;

	public int Id
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		set
		{
			int_0 = value;
		}
	}

	public int Life
	{
		[CompilerGenerated]
		get
		{
			return int_1;
		}
		[CompilerGenerated]
		set
		{
			int_1 = value;
		}
	}

	public int Animation
	{
		[CompilerGenerated]
		get
		{
			return int_2;
		}
		[CompilerGenerated]
		set
		{
			int_2 = value;
		}
	}

	public int UltraSync
	{
		[CompilerGenerated]
		get
		{
			return int_3;
		}
		[CompilerGenerated]
		set
		{
			int_3 = value;
		}
	}

	public int UpdateId
	{
		[CompilerGenerated]
		get
		{
			return int_4;
		}
		[CompilerGenerated]
		set
		{
			int_4 = value;
		}
	}

	public bool NeedSync
	{
		[CompilerGenerated]
		get
		{
			return bool_0;
		}
		[CompilerGenerated]
		set
		{
			bool_0 = value;
		}
	}

	public bool Destroyable
	{
		[CompilerGenerated]
		get
		{
			return bool_1;
		}
		[CompilerGenerated]
		set
		{
			bool_1 = value;
		}
	}

	public bool NoInstaSync
	{
		[CompilerGenerated]
		get
		{
			return bool_2;
		}
		[CompilerGenerated]
		set
		{
			bool_2 = value;
		}
	}

	public List<AnimModel> Animations
	{
		[CompilerGenerated]
		get
		{
			return list_0;
		}
		[CompilerGenerated]
		set
		{
			list_0 = value;
		}
	}

	public List<DeffectModel> Effects
	{
		[CompilerGenerated]
		get
		{
			return list_1;
		}
		[CompilerGenerated]
		set
		{
			list_1 = value;
		}
	}

	public ObjectModel(bool bool_3)
	{
		NeedSync = bool_3;
		if (bool_3)
		{
			Animations = new List<AnimModel>();
		}
		UpdateId = 1;
		Effects = new List<DeffectModel>();
	}

	public int CheckDestroyState(int Life)
	{
		int num = Effects.Count - 1;
		DeffectModel deffectModel;
		while (true)
		{
			if (num > -1)
			{
				deffectModel = Effects[num];
				if (deffectModel.Life >= Life)
				{
					break;
				}
				num--;
				continue;
			}
			return 0;
		}
		return deffectModel.Id;
	}

	public int GetRandomAnimation(RoomModel Room, ObjectInfo Obj)
	{
		if (Animations != null && Animations.Count > 0)
		{
			int index = new Random().Next(Animations.Count);
			AnimModel animModel2 = (Obj.Animation = Animations[index]);
			Obj.UseDate = DateTimeUtil.Now();
			if (animModel2.OtherObj > 0)
			{
				ObjectInfo obj = Room.Objects[animModel2.OtherObj];
				GetAnim(animModel2.OtherAnim, 0f, 0f, obj);
			}
			return animModel2.Id;
		}
		Obj.Animation = null;
		return 255;
	}

	public void GetAnim(int AnimId, float Time, float Duration, ObjectInfo Obj)
	{
		if (AnimId == 255 || Obj == null || Obj.Model == null || Obj.Model.Animations == null || Obj.Model.Animations.Count == 0)
		{
			return;
		}
		foreach (AnimModel animation in Obj.Model.Animations)
		{
			if (animation.Id == AnimId)
			{
				Obj.Animation = animation;
				Time -= Duration;
				Obj.UseDate = DateTimeUtil.Now().AddSeconds(Time * -1f);
				break;
			}
		}
	}
}
