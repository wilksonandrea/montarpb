namespace Plugin.Core.Utility
{
    using System;
    using System.Collections.Generic;

    public class DBQuery
    {
        private readonly List<string> list_0 = new List<string>();
        private readonly List<object> list_1 = new List<object>();

        public void AddQuery(string table, object value)
        {
            this.list_0.Add(table);
            this.list_1.Add(value);
        }

        public string[] GetTables() => 
            this.list_0.ToArray();

        public object[] GetValues() => 
            this.list_1.ToArray();
    }
}

