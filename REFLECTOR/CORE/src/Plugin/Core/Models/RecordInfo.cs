namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class RecordInfo
    {
        public RecordInfo(string[] string_0)
        {
            this.PlayerId = this.GetPlayerId(string_0);
            this.RecordValue = this.GetPlayerValue(string_0);
        }

        public long GetPlayerId(string[] Split)
        {
            try
            {
                return long.Parse(Split[0]);
            }
            catch
            {
                return 0L;
            }
        }

        public int GetPlayerValue(string[] Split)
        {
            try
            {
                return int.Parse(Split[1]);
            }
            catch
            {
                return 0;
            }
        }

        public string GetSplit() => 
            this.PlayerId.ToString() + "-" + this.RecordValue.ToString();

        public long PlayerId { get; set; }

        public int RecordValue { get; set; }
    }
}

