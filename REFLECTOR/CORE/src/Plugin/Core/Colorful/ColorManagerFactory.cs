namespace Plugin.Core.Colorful
{
    using System;
    using System.Collections.Concurrent;

    public sealed class ColorManagerFactory
    {
        public ColorManager GetManager(ColorStore colorStore, int maxColorChanges, int initialColorChangeCountValue, bool isInCompatibilityMode) => 
            new ColorManager(colorStore, new ColorMapper(), maxColorChanges, initialColorChangeCountValue, isInCompatibilityMode);

        public ColorManager GetManager(ConcurrentDictionary<Color, ConsoleColor> colorMap, ConcurrentDictionary<ConsoleColor, Color> consoleColorMap, int maxColorChanges, int initialColorChangeCountValue, bool isInCompatibilityMode) => 
            new ColorManager(new ColorStore(colorMap, consoleColorMap), new ColorMapper(), maxColorChanges, initialColorChangeCountValue, isInCompatibilityMode);
    }
}

