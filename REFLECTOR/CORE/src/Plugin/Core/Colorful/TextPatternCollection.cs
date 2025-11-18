namespace Plugin.Core.Colorful
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public sealed class TextPatternCollection : PatternCollection<string>
    {
        public TextPatternCollection(string[] string_0)
        {
            foreach (string str in string_0)
            {
                base.patterns.Add(new TextPattern(str));
            }
        }

        public override bool MatchFound(string input)
        {
            Class36 class2 = new Class36 {
                string_0 = input
            };
            return base.patterns.Any<Pattern<string>>(new Func<Pattern<string>, bool>(class2.method_0));
        }

        public TextPatternCollection Prototype()
        {
            Func<Pattern<string>, string> selector = Class35.<>9__1_0;
            if (Class35.<>9__1_0 == null)
            {
                Func<Pattern<string>, string> local1 = Class35.<>9__1_0;
                selector = Class35.<>9__1_0 = new Func<Pattern<string>, string>(Class35.<>9.method_0);
            }
            return new TextPatternCollection(base.patterns.Select<Pattern<string>, string>(selector).ToArray<string>());
        }

        protected override PatternCollection<string> PrototypeCore() => 
            this.Prototype();

        [Serializable, CompilerGenerated]
        private sealed class Class35
        {
            public static readonly TextPatternCollection.Class35 <>9 = new TextPatternCollection.Class35();
            public static Func<Pattern<string>, string> <>9__1_0;

            internal string method_0(Pattern<string> pattern_0) => 
                pattern_0.Value;
        }

        [CompilerGenerated]
        private sealed class Class36
        {
            public string string_0;

            internal bool method_0(Pattern<string> pattern_0) => 
                pattern_0.GetMatchLocations(this.string_0).Count<MatchLocation>() > 0;
        }
    }
}

