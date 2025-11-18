namespace Plugin.Core.Colorful
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public sealed class TextFormatter
    {
        private Color color_0;
        private TextPattern textPattern_0;
        private readonly string string_0;

        public TextFormatter(Color color_1)
        {
            this.string_0 = "{[0-9][^}]*}";
            this.color_0 = color_1;
            this.textPattern_0 = new TextPattern(this.string_0);
        }

        public TextFormatter(Color color_1, string string_1)
        {
            this.string_0 = "{[0-9][^}]*}";
            this.color_0 = color_1;
            this.textPattern_0 = new TextPattern(this.string_0);
        }

        public List<KeyValuePair<string, Color>> GetFormatMap(string input, object[] args, Color[] colors)
        {
            List<KeyValuePair<string, Color>> list = new List<KeyValuePair<string, Color>>();
            List<MatchLocation> source = this.textPattern_0.GetMatchLocations(input).ToList<MatchLocation>();
            List<string> list3 = this.textPattern_0.GetMatches(input).ToList<string>();
            this.method_0(ref args, ref colors);
            int startIndex = 0;
            for (int i = 0; i < source.Count<MatchLocation>(); i++)
            {
                char[] trimChars = new char[] { '{' };
                char[] chArray2 = new char[] { '}' };
                int index = int.Parse(list3[i].TrimStart(trimChars).TrimEnd(chArray2));
                int end = 0;
                if (i > 0)
                {
                    end = source[i - 1].End;
                }
                startIndex = source[i].End;
                list.Add(new KeyValuePair<string, Color>(input.Substring(end, source[i].Beginning - end), this.color_0));
                list.Add(new KeyValuePair<string, Color>(args[index].ToString(), colors[index]));
            }
            if (startIndex < input.Length)
            {
                list.Add(new KeyValuePair<string, Color>(input.Substring(startIndex, input.Length - startIndex), this.color_0));
            }
            return list;
        }

        private void method_0(ref object[] object_0, ref Color[] color_1)
        {
            if (color_1.Length < object_0.Length)
            {
                Color color = color_1[0];
                color_1 = new Color[object_0.Length];
                for (int i = 0; i < object_0.Length; i++)
                {
                    color_1[i] = color;
                }
            }
        }
    }
}

