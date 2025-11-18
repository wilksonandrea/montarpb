namespace Plugin.Core.Colorful
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public sealed class TextAnnotator
    {
        private StyleSheet styleSheet_0;
        private Dictionary<StyleClass<TextPattern>, Styler.MatchFound> dictionary_0 = new Dictionary<StyleClass<TextPattern>, Styler.MatchFound>();

        public TextAnnotator(StyleSheet styleSheet_1)
        {
            this.styleSheet_0 = styleSheet_1;
            foreach (StyleClass<TextPattern> class2 in styleSheet_1.Styles)
            {
                this.dictionary_0.Add(class2, (class2 as Styler).MatchFoundHandler);
            }
        }

        public List<KeyValuePair<string, Color>> GetAnnotationMap(string input)
        {
            IEnumerable<KeyValuePair<StyleClass<TextPattern>, MatchLocation>> enumerable = this.method_0(input);
            return this.method_1(enumerable, input);
        }

        private List<KeyValuePair<StyleClass<TextPattern>, MatchLocation>> method_0(string string_0)
        {
            List<KeyValuePair<StyleClass<TextPattern>, MatchLocation>> source = new List<KeyValuePair<StyleClass<TextPattern>, MatchLocation>>();
            List<MatchLocation> list2 = new List<MatchLocation>();
            foreach (StyleClass<TextPattern> class2 in this.styleSheet_0.Styles)
            {
                foreach (MatchLocation location in class2.Target.GetMatchLocations(string_0))
                {
                    if (list2.Contains(location))
                    {
                        int index = list2.IndexOf(location);
                        source.RemoveAt(index);
                        list2.RemoveAt(index);
                    }
                    source.Add(new KeyValuePair<StyleClass<TextPattern>, MatchLocation>(class2, location));
                    list2.Add(location);
                }
            }
            Func<KeyValuePair<StyleClass<TextPattern>, MatchLocation>, MatchLocation> keySelector = Class32.<>9__4_0;
            if (Class32.<>9__4_0 == null)
            {
                Func<KeyValuePair<StyleClass<TextPattern>, MatchLocation>, MatchLocation> local1 = Class32.<>9__4_0;
                keySelector = Class32.<>9__4_0 = new Func<KeyValuePair<StyleClass<TextPattern>, MatchLocation>, MatchLocation>(Class32.<>9.method_0);
            }
            return source.OrderBy<KeyValuePair<StyleClass<TextPattern>, MatchLocation>, MatchLocation>(keySelector).ToList<KeyValuePair<StyleClass<TextPattern>, MatchLocation>>();
        }

        private List<KeyValuePair<string, Color>> method_1(IEnumerable<KeyValuePair<StyleClass<TextPattern>, MatchLocation>> ienumerable_0, string string_0)
        {
            List<KeyValuePair<string, Color>> list = new List<KeyValuePair<string, Color>>();
            MatchLocation location = new MatchLocation(0, 0);
            int startIndex = 0;
            foreach (KeyValuePair<StyleClass<TextPattern>, MatchLocation> pair in ienumerable_0)
            {
                MatchLocation location2 = pair.Value;
                if (location.End > location2.Beginning)
                {
                    location = new MatchLocation(0, 0);
                }
                int end = location.End;
                int beginning = location2.Beginning;
                int num4 = beginning;
                startIndex = location2.End;
                string key = string_0.Substring(end, beginning - end);
                string str2 = string_0.Substring(num4, startIndex - num4);
                str2 = this.dictionary_0[pair.Key](string_0, pair.Value, string_0.Substring(num4, startIndex - num4));
                if (key != "")
                {
                    list.Add(new KeyValuePair<string, Color>(key, this.styleSheet_0.UnstyledColor));
                }
                if (str2 != "")
                {
                    list.Add(new KeyValuePair<string, Color>(str2, pair.Key.Color));
                }
                location = location2.Prototype();
            }
            if (startIndex < string_0.Length)
            {
                list.Add(new KeyValuePair<string, Color>(string_0.Substring(startIndex, string_0.Length - startIndex), this.styleSheet_0.UnstyledColor));
            }
            return list;
        }

        [Serializable, CompilerGenerated]
        private sealed class Class32
        {
            public static readonly TextAnnotator.Class32 <>9 = new TextAnnotator.Class32();
            public static Func<KeyValuePair<StyleClass<TextPattern>, MatchLocation>, MatchLocation> <>9__4_0;

            internal MatchLocation method_0(KeyValuePair<StyleClass<TextPattern>, MatchLocation> keyValuePair_0) => 
                keyValuePair_0.Value;
        }
    }
}

