namespace Plugin.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PlayerQuickstart
    {
        public PlayerQuickstart()
        {
            this.Quickjoins = new List<QuickstartModel>();
        }

        public QuickstartModel GetMapList(byte MapId)
        {
            QuickstartModel model2;
            List<QuickstartModel> quickjoins = this.Quickjoins;
            lock (quickjoins)
            {
                using (List<QuickstartModel>.Enumerator enumerator = this.Quickjoins.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            QuickstartModel current = enumerator.Current;
                            if (current.MapId != MapId)
                            {
                                continue;
                            }
                            model2 = current;
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    }
                }
            }
            return model2;
        }

        public long OwnerId { get; set; }

        public List<QuickstartModel> Quickjoins { get; set; }
    }
}

