namespace Server.Match.Data.Managers
{
    using Server.Match.Data.Models;
    using System;
    using System.Collections.Generic;

    public static class AssistManager
    {
        public static List<AssistServerData> Assists = new List<AssistServerData>();

        public static void AddAssist(AssistServerData Assist)
        {
            List<AssistServerData> assists = Assists;
            lock (assists)
            {
                Assists.Add(Assist);
            }
        }

        public static AssistServerData GetAssist(int VictimId, int RoomId)
        {
            AssistServerData data2;
            List<AssistServerData> assists = Assists;
            lock (assists)
            {
                using (List<AssistServerData>.Enumerator enumerator = Assists.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            AssistServerData current = enumerator.Current;
                            if ((current.Victim != VictimId) || (current.RoomId != RoomId))
                            {
                                continue;
                            }
                            data2 = current;
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    }
                }
            }
            return data2;
        }

        public static bool RemoveAssist(AssistServerData Assist)
        {
            List<AssistServerData> assists = Assists;
            lock (assists)
            {
                return Assists.Remove(Assist);
            }
        }
    }
}

