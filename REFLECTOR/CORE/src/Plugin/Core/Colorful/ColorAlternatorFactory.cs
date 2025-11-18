namespace Plugin.Core.Colorful
{
    using System;
    using System.Drawing;

    public sealed class ColorAlternatorFactory
    {
        public ColorAlternator GetAlternator(string[] patterns, params Color[] colors) => 
            new PatternBasedColorAlternator<string>(new TextPatternCollection(patterns), colors);

        public ColorAlternator GetAlternator(int frequency, params Color[] colors) => 
            new FrequencyBasedColorAlternator(frequency, colors);
    }
}

