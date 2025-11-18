namespace Plugin.Core.Models
{
    using System;
    using System.Net;
    using System.Runtime.CompilerServices;

    public class Synchronize
    {
        public Synchronize(string string_0, int int_1)
        {
            this.Connection = new IPEndPoint(IPAddress.Parse(string_0), int_1);
        }

        public int RemotePort { get; set; }

        public IPEndPoint Connection { get; set; }
    }
}

