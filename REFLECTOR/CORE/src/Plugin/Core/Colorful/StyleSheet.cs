namespace Plugin.Core.Colorful
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public sealed class StyleSheet
    {
        public Color UnstyledColor;

        public StyleSheet(Color color_0)
        {
            this.Styles = new List<StyleClass<TextPattern>>();
            this.UnstyledColor = color_0;
        }

        public void AddStyle(string target, Color color)
        {
            Styler.MatchFound found = Class30.<>9__8_0 ??= new Styler.MatchFound(Class30.<>9.method_0);
            Styler item = new Styler(target, color, found);
            this.Styles.Add(item);
        }

        public void AddStyle(string target, Color color, Styler.MatchFound matchHandler)
        {
            Styler item = new Styler(target, color, matchHandler);
            this.Styles.Add(item);
        }

        public void AddStyle(string target, Color color, Styler.MatchFoundLite matchHandler)
        {
            Class31 class1 = new Class31();
            class1.matchFoundLite_0 = matchHandler;
            Styler.MatchFound found = new Styler.MatchFound(class1.method_0);
            Styler item = new Styler(target, color, found);
            this.Styles.Add(item);
        }

        public List<StyleClass<TextPattern>> Styles { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class Class30
        {
            public static readonly StyleSheet.Class30 <>9 = new StyleSheet.Class30();
            public static Styler.MatchFound <>9__8_0;

            internal string method_0(string string_0, MatchLocation matchLocation_0, string string_1) => 
                string_1;
        }

        [CompilerGenerated]
        private sealed class Class31
        {
            public Styler.MatchFoundLite matchFoundLite_0;

            internal string method_0(string string_0, MatchLocation matchLocation_0, string string_1) => 
                this.matchFoundLite_0(string_1);
        }
    }
}

