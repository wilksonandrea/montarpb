namespace Server.Match.Data.Models
{
    using Plugin.Core.Utility;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ObjectModel
    {
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
                DeffectModel model = this.Effects[i];
                if (model.Life >= Life)
                {
                    return model.Id;
                }
            }
            return 0;
        }

        public void GetAnim(int AnimId, float Time, float Duration, ObjectInfo Obj)
        {
            if ((AnimId != 0xff) && ((Obj != null) && ((Obj.Model != null) && ((Obj.Model.Animations != null) && (Obj.Model.Animations.Count != 0)))))
            {
                foreach (AnimModel model in Obj.Model.Animations)
                {
                    if (model.Id == AnimId)
                    {
                        Obj.Animation = model;
                        Time -= Duration;
                        Obj.UseDate = DateTimeUtil.Now().AddSeconds((double) (Time * -1f));
                        break;
                    }
                }
            }
        }

        public int GetRandomAnimation(RoomModel Room, ObjectInfo Obj)
        {
            if ((this.Animations == null) || (this.Animations.Count <= 0))
            {
                Obj.Animation = null;
                return 0xff;
            }
            int num = new Random().Next(this.Animations.Count);
            AnimModel model = this.Animations[num];
            Obj.Animation = model;
            Obj.UseDate = DateTimeUtil.Now();
            if (model.OtherObj > 0)
            {
                ObjectInfo info = Room.Objects[model.OtherObj];
                this.GetAnim(model.OtherAnim, 0f, 0f, info);
            }
            return model.Id;
        }

        public int Id { get; set; }

        public int Life { get; set; }

        public int Animation { get; set; }

        public int UltraSync { get; set; }

        public int UpdateId { get; set; }

        public bool NeedSync { get; set; }

        public bool Destroyable { get; set; }

        public bool NoInstaSync { get; set; }

        public List<AnimModel> Animations { get; set; }

        public List<DeffectModel> Effects { get; set; }
    }
}

